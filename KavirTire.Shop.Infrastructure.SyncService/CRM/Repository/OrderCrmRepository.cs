using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Order;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class OrderCrmRepository : CrmRepositoryBase<Order>
    {
        public OrderCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }
        
        
        
        public List<Order> GetAllOrders(long? lastRowVersion = null)
        {
            var query = new QueryExpression(CrmResource.Order)
            {
                ColumnSet = new ColumnSet(CrmResource.Order_RegistrationDate,CrmResource.Order_TotalQuantity,CrmResource.Order_Customer,
                    CrmResource.Order_VersionNumber),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.NotEqual, (int)OrderStatusReason.RefundRequested)
                    }
                }
            };

            if (lastRowVersion != null)
                query.Criteria.Conditions.Add(new ConditionExpression(CrmResource.Order_VersionNumber, ConditionOperator.GreaterThan, lastRowVersion));
            
            
            return GetMoreThan5000WithQueryExpression(query)?.Select(e=>e.ToEntity<Order>()).ToList();
        }
    }
}