using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Configuration;

namespace KavirTire.Shop.Infrastructure.SyncService
{
    class WindowsService
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private IDisposable _webApp;
        public bool Start()
        {
            logger.Info($"{ConfigurationManager.AppSettings["APPLICATION_NAME"]} is Starting...");
 
            try
            {

                string serverPort = ConfigurationManager.AppSettings["APPLICATION_PORT"] ?? "8081";
                string serverProtocol = ConfigurationManager.AppSettings["APPLICATION_PROTOCOL"] ?? "http";

                string baseAddress = $"{serverProtocol}://*:{serverPort}/";
                _webApp = WebApp.Start<Startup>(url: baseAddress);

                logger.Info($"{ConfigurationManager.AppSettings["APPLICATION_NAME"]} is running on port {serverPort}");
                return true;

            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }

        }
        public void Stop()
        {
            logger.Info($"{ConfigurationManager.AppSettings["APPLICATION_NAME"]} Stopped");
            _webApp.Dispose();
        }
        public void Pause()
        {
            logger.Info($"{ConfigurationManager.AppSettings["APPLICATION_NAME"]} Paused");
        }

        public void Continue()
        {
            logger.Info($"{ConfigurationManager.AppSettings["APPLICATION_NAME"]} Continued");
        }
    }
}
