using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("product")]
    public class Product : EntityWrapper
    {
        
        public Product() : base(CrmResource.Product)
        {
        }
        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.Product_Id);
            set
            {
                this.SetAttributeValue(CrmResource.Product_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Product_Id;
            }
        } 
        public ModelAttribute<string> Name
        {
            get => new ModelAttribute<string>(this, CrmResource.Product_Name);
            set
            {
                this.SetAttributeValue(CrmResource.Product_Name, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Product_Name;
            }
        } 
        public ModelAttribute<EntityReference> FirstImage
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Product_FirstImage);
            set
            {
                this.SetAttributeValue(CrmResource.Product_FirstImage, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Product_FirstImage;
            }
        } 
    }
}