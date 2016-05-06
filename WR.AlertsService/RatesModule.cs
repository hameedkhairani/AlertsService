using System;
using System.Collections.Generic;
using Nancy;

namespace WR.AlertsService
{
    public class RatesModule : NancyModule
    {

        public RatesModule()
        {

            Get["/v1/rates"] = parameters =>
            {
                var rates = new List<Rate>
                {
                    new Rate()
                    {
                        FromCurrency = "GBP",
                        ToCurrency = "EUR",
                        ExchangeRate = 1.5m,
                        ValidFrom = DateTime.Today,
                        ValidTo = DateTime.Today.AddDays(1)
                    }
                };
                return Response.AsJson(rates);
            };
        }
    }
}