//using System.Collections.Generic;
//using Microsoft.Xrm.Sdk;
//using KavirTire.Shop.Plugins.Core.Models;

//namespace KavirTire.Shop.Plugins.Core.Extensions
//{
//    public static class EntityExtensions
//    {
//        public static List<ActivityParty> ToActivityParty(this Entity entity)
//        {
//            return new List<ActivityParty>
//            {
//                new ActivityParty
//                {
//                    PartyId = entity.ToEntityReference(),
//                    LogicalName = "activityparty"
//                }
//            };
//        }
//        public static List<ActivityParty> ToActivityParty(this EntityReference entityRef)
//        {
//            return new List<ActivityParty>
//            {
//                new ActivityParty
//                {
//                    PartyId = entityRef,
//                    LogicalName = "activityparty"
//                }
//            };
//        }
//    }
//}
