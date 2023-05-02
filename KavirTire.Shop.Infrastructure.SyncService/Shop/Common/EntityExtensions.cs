using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Common
{
    public static class EntityExtensions
    {
        public static string GetTableName<T>() {
            return ((TableAttribute)typeof(T).GetCustomAttribute(typeof(TableAttribute))).Name;
        }
    }
}