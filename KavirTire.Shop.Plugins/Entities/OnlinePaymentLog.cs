using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("bmsd_kv_onlinepaymentlog")]
    public class OnlinePaymentLog : Entity
    {
        public OnlinePaymentLog() : base(PluginResource.OnlinePaymentLog_LogicalName)
        {

        }
        public string Details
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePaymentLog_Details); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePaymentLog_Details, value);
            }
        }
        public string ErrorCode
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePaymentLog_ErrorCode); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePaymentLog_ErrorCode, value);
            }
        }
        public OptionSetValue LogType
        {
            get { return this.GetAttributeValue<OptionSetValue>(PluginResource.OnlinePaymentLog_LogType); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePaymentLog_LogType, value);
            }
        }
        public string Message
        {
            get { return this.GetAttributeValue<string>(PluginResource.OnlinePaymentLog_Message); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePaymentLog_Message, value);
            }
        }
        public EntityReference OnlinePayment
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.OnlinePaymentLog_OnlinePayment); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.OnlinePaymentLog_OnlinePayment, value);
            }
        }
    }

    public enum OnlinePaymentLogLogType
    {
        Info = 1,
        Error = 2
    }
}
