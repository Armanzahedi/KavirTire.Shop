using System.Collections.Generic;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers
{
    public static class ActivityPartyHelper
    {
        public static List<ActivityParty> CreateActivityParty(EntityReference reference)
        {
            return new List<ActivityParty>
            {
                new ActivityParty
                {
                    PartyId = reference,
                    LogicalName = "activityparty"
                }
            };
        }
    }
}
