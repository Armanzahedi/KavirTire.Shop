using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.Plugins.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("quotedetail")]
    public class QuoteProduct : EntityWrapper
    {
        public QuoteProduct() : base(PluginResource.QuoteProduct_LogicalName)
        {

        }
        public EntityReference Quote
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.QuoteProduct_Quote); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.QuoteProduct_Quote, value);
            }
        }
        public EntityReference Product
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.QuoteProduct_Product); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.QuoteProduct_Product, value);
            }
        }
        public decimal Quantity
        {
            get { return this.GetAttributeValue<decimal>(PluginResource.QuoteProduct_Quantity); }

            set
            {
                this.SetAttributeValue(PluginResource.QuoteProduct_Quantity, value);
            }
        }
        public EntityReference Unit
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.QuoteProduct_Unit); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.QuoteProduct_Unit, value);
            }
        }

        public decimal PricePerUnit
        {
            get { return this.GetAttributeValue<Money>(PluginResource.QuoteProduct_PricePerUnit).Value; }

            set
            {
                this.SetAttributeValue(PluginResource.QuoteProduct_PricePerUnit, value);
            }
        }
    }
}
