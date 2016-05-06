using System;

namespace WR.AlertsService
{
    public class Alert
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal RateThreshold { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Email { get; set; }

        public bool Triggered { get; set; }
    }
}