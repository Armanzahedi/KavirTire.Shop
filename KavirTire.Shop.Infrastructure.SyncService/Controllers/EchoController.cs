using System.Web.Http;
using NLog;

namespace KavirTire.Shop.Infrastructure.SyncService.Controllers
{
    public class EchoController : ApiController
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public string Get()
        {
            logger.Info("KavirTire Sync Service is running");
            return "KavirTire Sync Service is running";
        }
    }
}