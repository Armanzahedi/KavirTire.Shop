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
    public class PostCostCategoryCrmRepository : CrmRepositoryBase<PostCostCategory>
    {
        public PostCostCategoryCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }
        
        public List<PostCostCategory> GetActivePostCostCategories()
        {
            var query = new QueryExpression(CrmResource.PostCostCategory)
            {
                ColumnSet = new ColumnSet(CrmResource.PostCostCategory_Name,CrmResource.PostCostCategory_TirePostCost),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal, (int)StatusReason.Active)
                    }
                }
            };

            return serviceProxy.RetrieveMultiple(query)?.Entities.Select(e=>e.ToEntity<PostCostCategory>()).ToList();
        }
    }
}