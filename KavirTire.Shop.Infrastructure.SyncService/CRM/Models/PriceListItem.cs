using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("productpricelevel")]
    public class PriceListItem : EntityWrapper
    {
        public PriceListItem() : base(CrmResource.PriceListItem)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.PriceListItem_Id);
            set
            {
                this.SetAttributeValue(CrmResource.PriceListItem_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PriceListItem_Id;
            }
        }
        public ModelAttribute<Money> Amount
        {
            get => new ModelAttribute<Money>(this, CrmResource.PriceListItem_Amount);
            set
            {
                this.SetAttributeValue(CrmResource.PriceListItem_Amount, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PriceListItem_Amount;
            }
        }
        public ModelAttribute<EntityReference> ProductId
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.PriceListItem_ProductId);
            set
            {
                this.SetAttributeValue(CrmResource.PriceListItem_ProductId, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PriceListItem_ProductId;
            }
        }
        public ModelAttribute<EntityReference> PriceListId
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.PriceListItem_PriceListId);
            set
            {
                this.SetAttributeValue(CrmResource.PriceListItem_PriceListId, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PriceListItem_PriceListId;
            }
        }
    }
}