using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavirTire.Shop.Application.Common.Helpers
{
    public static class RandomDigitGenerator
    {
        public static string Create(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}