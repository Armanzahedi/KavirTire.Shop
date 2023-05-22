using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.Plugins.Core.Extensions;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("bmsd_kv_barcode")]
    public class Barcode : EntityWrapper
    {
        public Barcode() : base(PluginResource.Barcode)
        {

        }
        public string SerialNumber
        {
            get { return this.GetAttributeValue<string>(PluginResource.Barcode_SerialNumber); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Barcode_SerialNumber, value);
            }
        }
        public string TrackingCode
        {
            get { return this.GetAttributeValue<string>(PluginResource.Barcode_TrackingCode); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Barcode_TrackingCode, value);
            }
        }
        public EntityReference Product
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.Barcode_Product); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Barcode_Product, value);
            }
        }
        public EntityReference Order
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.Barcode_Order); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Barcode_Order, value);
            }
        }
    }
}
