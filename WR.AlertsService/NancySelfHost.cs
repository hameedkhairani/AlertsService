using System;
using Nancy.Hosting.Self;

namespace WR.AlertsService
{
    public class NancySelfHost
    {
        private NancyHost _mNancyHost;

        public void Start()
        {
            _mNancyHost = new NancyHost(new Uri("http://localhost:5000"));
            _mNancyHost.Start();
            Console.WriteLine("Service started...");
        }

        public void Stop()
        {
            _mNancyHost.Stop();
            Console.WriteLine("Service Stopped");
        }
    }
}