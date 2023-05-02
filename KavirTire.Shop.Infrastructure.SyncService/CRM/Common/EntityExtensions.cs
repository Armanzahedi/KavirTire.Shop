using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common
{
    public static class EntityExtensions
    {
        public static T GetAttributeValueOrDefault<T>(this Entity entity,string attrName)
        {
            return entity.TryGetAttributeValue(attrName, out T val) ? val : default;
        }
    }
}