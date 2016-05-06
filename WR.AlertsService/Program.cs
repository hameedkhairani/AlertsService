using Topshelf;

namespace WR.AlertsService
{
    public class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<NancySelfHost>(s =>
                {
                    s.ConstructUsing(name => new NancySelfHost());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("WR.AlertsService");
                x.SetDisplayName("WR Alerts Service");
                x.SetServiceName("WR.AlertsService");
            });
        }
    }
}
