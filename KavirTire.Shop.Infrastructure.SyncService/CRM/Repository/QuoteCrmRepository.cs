using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Quote;
using KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService.Model;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class QuoteCrmRepository : CrmRepositoryBase<Quote>
    {
        public QuoteCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }

        public void SyncQuote(CrmQuote quoteModel)
        {
            serviceProxy.Execute(new OrganizationRequest("bmsd_KavireTireSyncQuoteOrderPaymentWithShop")
            {
                ["JsonObject"] = JsonConvert.SerializeObject(quoteModel)
            });
        }
    }
}