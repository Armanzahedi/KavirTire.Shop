using System;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Plugins.Core.Extensions
{
    public class ModelAttributeOptionSet<T> : ModelAttribute<T>
    {
        public ModelAttributeOptionSet(T attributeValue) : base(attributeValue)
        {
        }

        public ModelAttributeOptionSet(EntityWrapper attributeEntity, string attributeName) : base(attributeEntity, attributeName)
        {
        }

        public override T Value
        {
            get
            {
                if (Entity == null)
                {
                    return value;
                }

                if (!HasValue)
                {
                    if (Entity.PreImage == null)
                        return default(T);
                    return (T)(object)Entity.PreImage.GetAttributeValue<OptionSetValue>(AttributeName).Value;
                }
                return (T)(object)Entity.GetAttributeValue<OptionSetValue>(AttributeName).Value;
            }
            set
            {
                if (Entity == null)
                {
                    this.value = value;
                }
                else
                {
                    Entity[AttributeName] = new OptionSetValue((int)(object)value);
                }

            }
        }

        public static implicit operator T(ModelAttributeOptionSet<T> attributeValue)
        {
            return attributeValue != null ? attributeValue.Value : default(T);
        }

        public static implicit operator ModelAttributeOptionSet<T>(T rightSideValue)
        {
            var value = Enum.Parse(typeof(T), rightSideValue.ToString());
            return new ModelAttributeOptionSet<T>((T)value);
        }
    }
}
