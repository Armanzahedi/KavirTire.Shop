using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers
{
    public class ModelAttribute<T>
    {
        protected T value;
        public virtual EntityWrapper Entity { get; set; }
        public virtual string AttributeName { get; set; }
        public virtual T Value
        {
            get
            {
                if (Entity == null)
                    return value;

                if (Entity.Contains(AttributeName))
                {
                    return (T)Entity[AttributeName];
                }

                return default(T);
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

        public virtual bool HasValue
        {
            get { return Entity.Contains(AttributeName); }
        }


        public virtual bool IsNull
        {
            get { return Value == null; }
        }

        public static implicit operator T(ModelAttribute<T> attributeValue)
        {
            return attributeValue != null ? attributeValue.Value : default(T);
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

            if (typeof(T) == typeof(OptionSetValue))
            {
                return ((OptionSetValue)(object)Value).Value.ToString();
            }

            if (typeof(T) == typeof(EntityReference))
            {
                return ((EntityReference)(object)Value).Name;
            }
            return Value.ToString();
        }
    }
}
