using KavirTire.Shop.KavirTire.Shop.Plugins.Entities;
using KavirTire.Shop.Plugins.Core;
using KavirTire.Shop.Plugins.Core.Repository;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Repositories
{
    public class ProductRepository : PluginRepository<Product>
    {
        public ProductRepository(PluginBase.LocalPluginContext currentContext) : base(currentContext)
        {
        }
    }
}
