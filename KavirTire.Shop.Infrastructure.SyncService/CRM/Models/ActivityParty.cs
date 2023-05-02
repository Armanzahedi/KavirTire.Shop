using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("activityparty")]
    public class ActivityParty : Entity
    {
        public EntityReference PartyId
        {
            get { return GetAttributeValue<EntityReference>(CrmResource.ActivityParty_PartyId); }
            set { SetAttributeValue(CrmResource.ActivityParty_PartyId, value); }
        }
    }
}
