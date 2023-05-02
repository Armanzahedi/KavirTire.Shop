using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository
{
    public class CrmRepositoryBase : CrmRepositoryBase<Entity>, ICrmRepositoryBaseEntity
    {
        private readonly ICrmSettingManager _crmSettingManager;
        public CrmRepositoryBase(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
            _crmSettingManager = currentCrmSettingManager;
        }

    }
}
