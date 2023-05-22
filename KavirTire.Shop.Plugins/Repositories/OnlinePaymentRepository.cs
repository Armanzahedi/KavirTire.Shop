using KavirTire.Shop.KavirTire.Shop.Plugins.Entities;
using KavirTire.Shop.Plugins.Core;
using KavirTire.Shop.Plugins.Core.Repository;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Repositories
{
    public class OnlinePaymentRepository : PluginRepository<OnlinePayment>
    {
        public OnlinePaymentRepository(PluginBase.LocalPluginContext currentContext) : base(currentContext)
        {
        }
    }
}
