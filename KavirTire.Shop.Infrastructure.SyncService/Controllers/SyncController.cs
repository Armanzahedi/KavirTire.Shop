using System.Threading.Tasks;
using System.Web.Http;
using KavirTire.Shop.Infrastructure.SyncService.Services;
using NLog;

namespace KavirTire.Shop.Infrastructure.SyncService.Controllers
{
    public class SyncController : ApiController
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly DataMigrationService _dataMigrationService;

        public SyncController(DataMigrationService dataMigrationService)
        {
            _dataMigrationService = dataMigrationService;
        }

        public async Task<string> Get()
        {
            await _dataMigrationService.Start();
            return "KavirTire Sync Service is starting";
        }
    }
}