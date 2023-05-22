using KavirTire.Shop.KavirTire.Shop.Plugins.Entities;
using KavirTire.Shop.Plugins.Core.Repository;
using KavirTire.Shop.Plugins.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Repositories
{
    public class BarcodeRepository : PluginRepository<Barcode>
    {
        public BarcodeRepository(PluginBase.LocalPluginContext currentContext) : base(currentContext)
        {
        }
    }
}
