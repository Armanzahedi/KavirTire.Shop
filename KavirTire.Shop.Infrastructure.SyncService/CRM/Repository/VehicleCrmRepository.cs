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
    public class VehicleCrmRepository : CrmRepositoryBase<Vehicle>
    {
        public VehicleCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }
        public List<Vehicle> GetActiveVehicles()
        {
            var query = new QueryExpression(CrmResource.Vehicle)
            {
                ColumnSet = new ColumnSet(CrmResource.Vehicle_Customer,
                    CrmResource.Vehicle_VehicleType,
                    CrmResource.Vehicle_VehicleRegistrationCharacter,
                    CrmResource.Vehicle_VehicleRegistrationNumberLeft,
                    CrmResource.Vehicle_VehicleRegistrationNumberMiddle,
                    CrmResource.Vehicle_VehicleRegistrationNumberRight
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

            return GetMoreThan5000WithQueryExpression(query)?.Select(e=>e.ToEntity<Vehicle>()).ToList();
        }
    }
}