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
        FxRate GetRate(string fromCurrency);
        Rate GetRate(string fromCurrency, string toCurrency);

        IEnumerable<FxRate> CreateHistoricalRates();
        Rate UpdateRateForNextMinute(Rate rate, decimal delta);
    }

    public class FxRateRepository : IFxRateRepository
    {
        private static Dictionary<string,FxRate> rateList;

        public FxRateRepository()
        {
            rateList = new Dictionary<string, FxRate>();
            ReadRatesFromFile();
        }

        public FxRate GetRate(string fromCurrency)
        {
            return rateList[fromCurrency];
        }

        public Rate GetRate(string fromCurrency, string toCurrency)
        {
            return null;
        }

        public IEnumerable<FxRate> CreateHistoricalRates()
        {
            //var historical = new List<Rate>();
            //foreach (var rates in rateList)
            //{
            //    historical.Add(  );
            //}
            return null;
        }

        public Rate UpdateRateForNextMinute(Rate rate, decimal delta)
        {
            return null;
            //var key = rate.FromCurrency;

            //var currentRate = rateList[key];
            //currentRate.ValidFrom = DateTime.Now;
            //currentRate.ValidTo = DateTime.Now.AddMinutes(1);
            //currentRate.ExchangeRate += delta;

            //return currentRate;
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