using System;

namespace WR.AlertsService
{
    public class Alert
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal RateThreshold { get; set; }
        public bool Triggered { get; set; }
    }
}