using System;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Plugins.Core.Extensions
{
    public class ModelAttribute<T>
    {
        protected T value ;
        public virtual EntityWrapper Entity { get; set; }
        public virtual string AttributeName { get; set; }
        public virtual T Value
        {
            get
            {
                if (Entity == null)
                    return value;
                if (!HasValue)
                {
                    if (Entity.PreImage == null)
                        return default(T);
                    return Entity.PreImage.GetAttributeValue<T>(AttributeName);
                }
                return (T)Entity[AttributeName];
            }
            set
            {
                if (Entity == null)
                {
                    this.value = value;
                }
                else
                {
                    Entity[AttributeName] = value;
                }

            }
        }

        public ModelAttribute(T attributeValue)
        {
            Value = attributeValue;
        }

        public ModelAttribute(EntityWrapper attributeEntity, string attributeName)
        {
            Entity = attributeEntity;
            AttributeName = attributeName;
        }

        public virtual bool HasValue => Entity.Contains(AttributeName);

        public virtual ModelAttribute<T> PreImage
        {
            get
            {
                if (Entity.PreImage != null)
                    return new ModelAttribute<T>(Entity.PreImage, AttributeName);
                throw new NullReferenceException("Pre image value is null");
            }
        }

        public virtual bool IsNull => Value == null;

        public static implicit operator T(ModelAttribute<T> attributeValue)
        {
            return attributeValue!=null?attributeValue.Value:default(T);
        }

        public static implicit operator ModelAttribute<T>(T rightSideValue)
        {
            return new ModelAttribute<T>(rightSideValue);
        }

        public override string ToString()
        {
            if (Value == null)
            {
                return string.Empty;
            }
            if (typeof(T) == typeof(Money))
            {
                return ((Money)(object)Value).Value.ToString();
            }

            if (typeof(T)== typeof(OptionSetValue))
            {
                return ((OptionSetValue) (object) Value).Value.ToString();
            }

            if (typeof(T) == typeof(EntityReference))
            {
                return  ((EntityReference) (object) Value).Name;
            }
            return Value.ToString();
        }
    }
}
