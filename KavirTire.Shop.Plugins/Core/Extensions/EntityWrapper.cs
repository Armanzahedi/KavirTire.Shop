using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Plugins.Core.Extensions
{
    public class EntityWrapper : Entity
    {
        public EntityWrapper PreImage { get; set; }

        public EntityWrapper(string logicalName) : base(logicalName)
        { }

        protected T GetEntityAttributeValue<T>(string attributeName)
        {
            T attributeValue = default(T);
            if (Attributes.Contains(attributeName))
            {
                attributeValue = GetAttributeValue<T>(attributeName);
            }
            else
            {
                if (PreImage != null)
                    attributeValue = PreImage.GetAttributeValue<T>(attributeName);
            }
            return attributeValue;
        }
        public object GetAttributeLastValue(string attributeName)
        {
            if (this.Contains(attributeName) && this[attributeName] != null)
            {
                return this[attributeName];
            }
            else if (this.PreImage != null &&
                     this.PreImage.Contains(attributeName) && this.PreImage[attributeName] != null)
            {
                return this.PreImage[attributeName];
            }
            return null;
        }

    }
}