
using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.Plugins.Core.Extensions;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("bmsd_generalpolicy")]
    public class GeneralPolicy : EntityWrapper
    {
        public GeneralPolicy() : base(PluginResource.GeneralPolicy)
        {

        }
        public string SyncServiceAddress
        {
            get { return this.GetAttributeValue<string>(PluginResource.GeneralPolicy_SyncServiceAddress); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.GeneralPolicy_SyncServiceAddress, value);
            }
        }
    }
}
