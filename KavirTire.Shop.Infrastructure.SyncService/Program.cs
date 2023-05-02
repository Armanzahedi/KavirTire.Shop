using System.Configuration;
using Topshelf;

namespace KavirTire.Shop.Infrastructure.SyncService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configure =>
            {
                configure.Service<WindowsService>(service =>
                {
                    service.ConstructUsing(s => new WindowsService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                configure.EnableServiceRecovery(recovery => recovery.RestartService(delayInMinutes: 1));
                configure.EnableShutdown();
                configure.StartAutomatically();
                configure.RunAsLocalSystem();
                // configure.UseNLog();
                configure.SetServiceName(ConfigurationManager.AppSettings["APPLICATION_NAME"]);
                configure.SetDisplayName(ConfigurationManager.AppSettings["APPLICATION_NAME"]);
                configure.SetDescription(ConfigurationManager.AppSettings["APPLICATION_DESCRIPTION"]);
            });
        }
    }
}
