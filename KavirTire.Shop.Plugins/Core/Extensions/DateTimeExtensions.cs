using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetDayOfMonth(date).ToString("00")}/{pc.GetMonth(date).ToString("00")}/{pc.GetYear(date)} {pc.GetHour(date).ToString("00")}:{pc.GetMinute(date).ToString("00")}:{pc.GetSecond(date).ToString("00")}"; 
        }
    }
}
