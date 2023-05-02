using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models.PriceList
{
    [EntityLogicalName("pricelevel")]
    public class PriceList : EntityWrapper
    {
        public PriceList() : base(CrmResource.PriceList)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.PriceList_Id);
            set
            {
                this.SetAttributeValue(CrmResource.PriceList_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PriceList_Id;
            }
        }
        public ModelAttribute<string> Name
        {
            get => new ModelAttribute<string>(this, CrmResource.PriceList_Name);
            set
            {
                this.SetAttributeValue(CrmResource.PriceList_Name, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PriceList_Name;
            }
        }
    }
}