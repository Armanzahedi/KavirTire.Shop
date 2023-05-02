using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Plugins.Core.Extensions
{
    public static class EnumExtensions
    {
        public static OptionSetValue ToOptionSet(this Enum value)
        {
            return new OptionSetValue((int)(IConvertible)value);
        }
    }
}
