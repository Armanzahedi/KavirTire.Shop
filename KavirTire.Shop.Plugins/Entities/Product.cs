using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.Plugins.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("product")]
    public class Product : EntityWrapper
    {
        public Product() : base(PluginResource.Product)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, PluginResource.Product_Id);
            set
            {
                this.SetAttributeValue(PluginResource.Product_Id, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Product_Id;
            }
        }
        public ModelAttribute<EntityReference> DefaultUnit
        {
            get => new ModelAttribute<EntityReference>(this, PluginResource.Product_DefaultUnit);
            set
            {
                this.SetAttributeValue(PluginResource.Product_DefaultUnit, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Product_DefaultUnit;

            }
        }
    }
}
