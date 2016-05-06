using System;
using System.Collections.Generic;
using System.IO;
using WR.AlertsService;

namespace NotificationService
{
    public interface IFxRateRepository
    {
        IEnumerable<Rate> GetRates(string fromCurrency, string toCurrency, DateTime timestamp);
    }

    public class FxRateRepository : IFxRateRepository
    {
        public IEnumerable<Rate> GetRates(string fromCurrency, string toCurrency, DateTime timestamp)
        {
            var fxRates = new List<Rate>();
            var lines = File.ReadAllLines("TestFxRates.txt");
            foreach (var line in lines)
            {
                var splittedLines = line.Split(',');
                var rate = CreateRate(splittedLines);
                fxRates.Add(rate);
            }
            return fxRates;
        }

        private static Rate CreateRate(string[] splittedLines)
        {
            return new Rate()
            {
                FromCurrency = splittedLines[0],
                ToCurrency = splittedLines[1],
                ExchangeRate = decimal.Parse(splittedLines[3]),
                ValidFrom = DateTime.Parse(splittedLines[4]),
                ValidTo = DateTime.Parse(splittedLines[4]).AddMinutes(1)
            };
        }
    }
}