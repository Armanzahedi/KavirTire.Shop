using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavirTire.Shop.Presentation.Common
{
    public static class RequestExtensionMethods
    {
        public static string? ReadAsString(this Stream body)
        {

            var reader = new StreamReader(body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var rawMessage = reader.ReadToEnd();
            return rawMessage;
        }
    }
}