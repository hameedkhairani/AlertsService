using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;
using NotificationService;

namespace WR.AlertsService
{
    public class RatesModule : NancyModule
    {
        private readonly FxRateRepository _rateRepository;
        private AlertRepository _alertsRepository;

        public RatesModule()
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;
            _rateRepository = new FxRateRepository();
            _alertsRepository = new AlertRepository();

            Get["/v1/rates"] = parameters =>
            {
                var rates = _rateRepository.CreateHistoricalRates();
                return Response.AsJson(rates);
            };

            Post["/v1/rate"] = parameters =>
            {
                var newRate = this.Bind<FlattenedRate>();
                var currentRate = _rateRepository.GetCurrentRate(newRate.FromCurrency, newRate.ToCurrency);

                var candidates = _alertsRepository.GetAll().Where(p=> p.FromCurrency == newRate.FromCurrency && 
                                                                      p.ToCurrency == newRate.ToCurrency);
                foreach (var alert in candidates)
                {
                    if (alert.RateThreshold < newRate.ConversionRate)
                    {
                        var notification = new SMSNotification();
                        notification.SendSMS(alert.PhoneNumber,
                                             $"exchange rate for {newRate.FromCurrency} to " +
                                             $"                  {newRate.ToCurrency} changed from " +
                                             $"                  {currentRate.ConversionRate} to " +
                                             $"                  {newRate.ConversionRate}");
                    }
                }
                return HttpStatusCode.OK;
                
            };

        }
    }
}