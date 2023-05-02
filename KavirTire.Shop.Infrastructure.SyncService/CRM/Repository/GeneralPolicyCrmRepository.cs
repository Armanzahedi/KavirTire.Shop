using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class GeneralPolicyCrmRepository : CrmRepositoryBase<GeneralPolicy>
    {
        public GeneralPolicyCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }

        public GeneralPolicy GetGeneralPolicy()
        {
            var query = new QueryExpression(CrmResource.GeneralPolicy)
            {
                ColumnSet = new ColumnSet(
                    CrmResource.GeneralPolicy_PriceList,
                    CrmResource.GeneralPolicy_MaximumNumberOfPurchases,
                    CrmResource.GeneralPolicy_NumberOfPurchaseItems,
                    CrmResource.GeneralPolicy_ApplyNumberOfPurchaseItems,
                    CrmResource.GeneralPolicy_PurchaseIntervalInDays,
                    CrmResource.GeneralPolicy_ApplyMaximumNumberOfPurchases,
                    CrmResource.GeneralPolicy_ApplyPurchaseIntervalInDays,
                    CrmResource.GeneralPolicy_ShowProductsOnlyRelatedToCustomerCar,
                    CrmResource.GeneralPolicy_ExpireQuoteForActionMin,
                    CrmResource.GeneralPolicy_ExpireQuoteForCookieMin
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
            return serviceProxy.RetrieveMultiple(query).Entities.FirstOrDefault()?.ToEntity<GeneralPolicy>();
        }
    }
}