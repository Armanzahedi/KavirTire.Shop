using System;
using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models.PriceList;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class PriceListCrmRepository : CrmRepositoryBase<PriceList>
    {
        public PriceListCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }
        public List<PriceList> GetActivePriceLists()
        {
            var query = new QueryExpression(CrmResource.PriceList)
            {
                ColumnSet = new ColumnSet(CrmResource.PriceList_Name),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal, (int)PriceListStatusReason.Active)
                    }
                }
            };

            return serviceProxy.RetrieveMultiple(query).Entities.ToList()?.Select(e=>e.ToEntity<PriceList>()).ToList();
        }
        public List<PriceListItem> GetPriceListItems()
        {

            var query = new QueryExpression(CrmResource.PriceListItem)
            {
                ColumnSet = new ColumnSet(CrmResource.PriceListItem_Amount,
                    CrmResource.PriceListItem_ProductId,
                    CrmResource.PriceListItem_PriceListId)
            };

            return serviceProxy.RetrieveMultiple(query).Entities.ToList()?.Select(e=>e.ToEntity<PriceListItem>()).ToList();
        }
        public List<PriceListItem> GetPriceListItemsByProductId(Guid productId)
        {

            var query = new QueryExpression(CrmResource.PriceListItem)
            {
                ColumnSet = new ColumnSet(CrmResource.PriceListItem_Amount,
                    CrmResource.PriceListItem_ProductId,
                    CrmResource.PriceListItem_PriceListId),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.PriceListItem_ProductId, ConditionOperator.Equal, productId)
                    }
                }
            };
            return serviceProxy.RetrieveMultiple(query).Entities.ToList()?.Select(e=>e.ToEntity<PriceListItem>()).ToList();
        }
        
        public List<PriceListItem> GetPriceListItemsByPriceListId(Guid priceListId)
        {

            var query = new QueryExpression(CrmResource.PriceListItem)
            {
                ColumnSet = new ColumnSet(CrmResource.PriceListItem_Amount,
                    CrmResource.PriceListItem_ProductId,
                    CrmResource.PriceListItem_PriceListId),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.PriceListItem_PriceListId, ConditionOperator.Equal, priceListId)
                    }
                }
            };
            return GetMoreThan5000WithQueryExpression(query)?.Select(e=>e.ToEntity<PriceListItem>()).ToList();
        }
    }
}