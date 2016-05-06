using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using WR.AlertsService;

namespace NotificationService
{
    public interface IFxRateRepository
    {
        List<FxRate> GetRates();
        void UpdateRate(FlattenedRate rate);
        FlattenedRate GetCurrentRate(string fromCcy, string toCcy);
        IEnumerable<FxRate> CreateHistoricalRates();
    }

    public class FxRateRepository : IFxRateRepository
    {
        private static Dictionary<string,FxRate> rateList;

        public FxRateRepository()
        {
            rateList = new Dictionary<string, FxRate>();
            ReadRatesFromFile();
        }

        public List<FxRate> GetRates()
        {
            return rateList.Values.ToList();
        }

        public void UpdateRate(FlattenedRate requestedRate)
        {
            var currentRate = rateList[requestedRate.FromCurrency]
                                .ExchangeRates
                                .Single(e => e.Currency == requestedRate.ToCurrency);

            currentRate.ConversionRates
                                .Single()
                                .Rate = requestedRate.ConversionRate;
        }

        public FlattenedRate GetCurrentRate(string fromCcy, string toCcy)
        {
            var rate = rateList[fromCcy].ExchangeRates.Single(e => e.Currency == toCcy);
            var flatternedRate = new FlattenedRate()
            {
                ToCurrency = toCcy,
                FromCurrency = fromCcy,
                ConversionRate  = rate.ConversionRates.Single().Rate
            };
            return flatternedRate;
        }


        public IEnumerable<FxRate> CreateHistoricalRates()
        {
            Dictionary<string, FxRate> historicalRates = new Dictionary<string, FxRate>(rateList);
            foreach (var fxRate in historicalRates)
            {
                var toCcyRates = fxRate.Value.ExchangeRates;
                foreach (var toCcy in toCcyRates)
                {
                    var existingRate = toCcy.ConversionRates.First();
                    var dummyHistoricalPrices = GetHistoricalPrices(existingRate.ValidFrom.AddDays(-1), existingRate.Rate);
                    toCcy.ConversionRates.AddRange(dummyHistoricalPrices);
                }
            }
            return historicalRates.Values.ToList();
        }

        private List<ConversionRate> GetHistoricalPrices(DateTime validFrom, decimal rate)
        {
            var result = new List<ConversionRate>();

            var random = new Random(3);
            for (int i = 0; i < 15; i++)
            {
                var difference = (decimal)(random.Next(1, 10) / 10.0);

                var date = validFrom.AddDays(-i);
                var newRate = rate - difference;
                result.Add(new ConversionRate
                {
                    Rate = newRate,
                    ValidFrom = date,
                    ValidTo = date.AddDays(1)
                });

            }

            return result;
        }

      

        private void ReadRatesFromFile()
        {
            var lines = File.ReadAllLines("TestFxRates.txt");
            foreach (var line in lines)
            {
                var splittedLines = line.Split(',');
                var rate = CreateRate(splittedLines);

                var fromCurrency = splittedLines[0];

                if (rateList.ContainsKey(fromCurrency))
                {
                    rateList[fromCurrency].ExchangeRates.Add(rate);
                }
                else
                {
                    var fxR = new FxRate()
                    {
                        Currency = fromCurrency,
                        ExchangeRates = new List<Rate> { rate }
                    };
                    rateList.Add(fromCurrency,fxR);
                }
            }
        }

        private static Rate CreateRate(string[] splittedLines)
        {
            return new Rate
            {
                Currency = splittedLines[1],
                
                ConversionRates = new List<ConversionRate>
                {
                    new ConversionRate
                    {
                        Rate = decimal.Parse(splittedLines[3],System.Globalization.NumberStyles.Float),
                        ValidFrom =  DateTime.Parse(splittedLines[4]),
                        ValidTo = DateTime.Parse(splittedLines[4]).AddDays(1)
                    }
                }
            };
        }
    }
}