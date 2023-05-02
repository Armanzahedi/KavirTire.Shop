using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class LocationCrmRepository : CrmRepositoryBase<Location>
    {
        public LocationCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }

        public List<Location> GetLocations()
        {
            var query = new QueryExpression(CrmResource.Location)
            {
                ColumnSet = new ColumnSet(
                    CrmResource.Location_Name,
                    CrmResource.Location_ParentLocation,
                    CrmResource.Location_PostCostCategory
                    ),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal, (int)StatusReason.Active)
                    }
                }
            };

            return serviceProxy.RetrieveMultiple(query).Entities?.Select(e=>e.ToEntity<Location>()).ToList();
        }
    }
}