using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

namespace WR.AlertsService
{
    public class RatesModule : NancyModule
    {

        public RatesModule()
        {

            Get["/v1/rates"] = parameters =>
            {
                var rate = new Rate()
                {
                    Currency = "EUR",
                    ConversionRate = 1.5m,
                    ValidFrom = DateTime.Today,
                    ValidTo = DateTime.Today.AddDays(1)
                };
                var rates = new List<Rate> {rate};
                var fxRate = new FxRate {Currency = "GBP", ExchangeRates = rates };
                var fxRates = new List<FxRate> {fxRate};
                return Response.AsJson(fxRates);
            };

            Post["/v1/rate"] = parameters =>
            {
                var alert = this.Bind<FlattenedRate>();

                return HttpStatusCode.OK;
            };

        }
    }
}