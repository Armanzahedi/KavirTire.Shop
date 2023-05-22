using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.Plugins.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("bmsd_kv_onlinepayment")]
    public class OnlinePayment : EntityWrapper
    {
        public OnlinePayment() : base(PluginResource.OnlinePayment_LogicalName)
        {

        }
        public Money Amount
        {
            get { return this.GetAttributeValue<Money>(PluginResource.OnlinePayment_Amount); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_Amount, value);
            }
        }
        public EntityReference Customer
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePayment_Customer); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_Customer, value);
            }
        }
        public string RRN
        {
            get { return this.GetAttributeValue<string>("bmsd_rrn"); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue("bmsd_rrn", value);
            }
        }
        public string Description
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_Description); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_Description, value);
            }
        }
        public EntityReference Order
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePayment_Order); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_Order, value);
            }
        }
        public EntityReference Quote
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePayment_Quote); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_Quote, value);
            }
        }
        public string PaymentIdentity
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_PaymentIdentity); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_PaymentIdentity, value);
            }
        }
        public string RefNo
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_RefNo); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_RefNo, value);
            }
        }
        public string ResNo
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_ResNo); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_ResNo, value);
            }
        }
        public string ShopResNo
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_ShopResNo); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_ShopResNo, value);
            }
        }
        public string SecurePan
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_SecurePan); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_SecurePan, value);
            }
        }
        public string SystemTraceNo
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePayment_SystemTraceNo); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_SystemTraceNo, value);
            }
        }
        public EntityReference Ipg
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePayment_Ipg); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_Ipg, value);
            }
        }
        public EntityReference BankAccount
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePayment_BankAccount); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_BankAccount, value);
            }
        }
        public EntityReference PostBankAccount
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePayment_PostBankAccount); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePayment_PostBankAccount, value);
            }
        }
    }
}
