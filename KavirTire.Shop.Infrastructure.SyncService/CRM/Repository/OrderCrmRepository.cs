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
        
        public List<Order> GetAllOrders()
        {
            var query = new QueryExpression(CrmResource.Order)
            {
                ColumnSet = new ColumnSet(CrmResource.Order_RegistrationDate,CrmResource.Order_TotalQuantity,CrmResource.Order_Customer),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.NotEqual, (int)OrderStatusReason.RefundRequested)
                    }
                }
            };

            return GetMoreThan5000WithQueryExpression(query)?.Select(e=>e.ToEntity<Order>()).ToList();
        }
    }
}