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
    public class VehicleTypeCrmRepository : CrmRepositoryBase<VehicleType>
    {
        public VehicleTypeCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }

        public List<VehicleType> GetActiveVehicleTypes()
        {
            var query = new QueryExpression(CrmResource.VehicleType)
            {
                ColumnSet = new ColumnSet(CrmResource.VehicleType_Name),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal, (int)StatusReason.Active)
                    }
                }
            };

            return GetMoreThan5000WithQueryExpression(query)?.Select(e=>e.ToEntity<VehicleType>()).ToList();
        }
    }
}