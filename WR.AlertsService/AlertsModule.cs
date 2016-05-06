using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

namespace WR.AlertsService
{
    public class AlertsModule : NancyModule
    {
        private readonly AlertRepository _alertRepository = new AlertRepository();

        public AlertsModule()
        {

            Get["/v1/alerts"] = parameters =>
            {
                var alerts = _alertRepository.GetAll();
                return Response.AsJson(alerts);
            };

            Put["/v1/alert"] = parameters =>
            {
                var alert = this.Bind<Alert>();
                _alertRepository.Add(alert);

                return Response.AsJson(alert);
            };

        }
    }
}