using System.Threading.Tasks;
using System.Web.Http;
using KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService;
using NLog;

namespace KavirTire.Shop.Infrastructure.SyncService.Controllers
{
    public class EchoController : ApiController
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly SyncCrmService _syncCrmService;

        public EchoController(SyncCrmService syncCrmService)
        {
            _syncCrmService = syncCrmService;
        }

        public async Task<string> Get()
        {
            await _syncCrmService.Run();
            logger.Info("KavirTire Sync Service is running");
            return "KavirTire Sync Service is running";
        }
    }
}