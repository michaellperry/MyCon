using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacetedWorlds.MyCon.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertTo(this DateTime time, string timeZone)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }

        public static DateTime ConvertFrom(this DateTime time, string timeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(time,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }
    }
}