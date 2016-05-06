using System;
using System.Collections.Generic;
using Nancy;

namespace WR.AlertsService
{
    public class AlertsModule : NancyModule
    {

        public AlertsModule()
        {
            Get["/v1/alerts"] = parameters =>
            {
                var rates = new List<Alert>
                {
                    new Alert()
                    {
                        FromCurrency = "GBP",
                        ToCurrency = "EUR",
                        RateThreshold = 1.2m,
                        ValidFrom = DateTime.Today,
                        ValidTo = DateTime.Today.AddDays(1),
                        Triggered = false
                    }
                };
                return Response.AsJson(rates);
            };

            Put["/v1/alerts"] = parameters =>
            {
                var rates = new List<Alert>
                {
                    new Alert()
                    {
                        FromCurrency = "GBP",
                        ToCurrency = "EUR",
                        RateThreshold = 1.2m,
                        ValidFrom = DateTime.Today,
                        ValidTo = DateTime.Today.AddDays(1),
                        Triggered = false
                    }
                };
                return Response.AsJson(rates);
            };

        }
    }
}