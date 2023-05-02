using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("bmsd_postcostcategory")]
    public class PostCostCategory : EntityWrapper
    {
        public PostCostCategory() : base(CrmResource.PostCostCategory)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.PostCostCategory_Id);
            set
            {
                this.SetAttributeValue(CrmResource.PostCostCategory_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PostCostCategory_Id;
            }
        }
        public ModelAttribute<string> Name
        {
            get => new ModelAttribute<string>(this, CrmResource.PostCostCategory_Name);
            set
            {
                this.SetAttributeValue(CrmResource.PostCostCategory_Name, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PostCostCategory_Name;
            }
        }
        public ModelAttribute<Money> TirePostCost
        {
            get => new ModelAttribute<Money>(this, CrmResource.PostCostCategory_TirePostCost);
            set
            {
                this.SetAttributeValue(CrmResource.PostCostCategory_TirePostCost, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.PostCostCategory_Name;
            }
        }
    }
}