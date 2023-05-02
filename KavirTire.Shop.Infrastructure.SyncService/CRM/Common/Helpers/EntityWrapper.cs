using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers
{
    public class EntityWrapper : Entity
    {
        public EntityWrapper(string logicalName) : base(logicalName)
        { }

        protected T GetEntityAttributeValue<T>(string attributeName)
        {
            T attributeValue = default(T);
            attributeValue = GetAttributeValue<T>(attributeName);
            return attributeValue;
        }

    }
}
