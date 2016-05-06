using System;
using System.Collections.Generic;

namespace WR.AlertsService
{
    public class FxRate
    {
        public string Currency { get; set; }
        public List<Rate> ExchangeRates { get; set; }
    }

    public class Rate
    {
        public string Currency { get; set; }
        public List<ConversionRate> ConversionRates { get; set; }
    }

    public class ConversionRate
    {
        public decimal Rate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}