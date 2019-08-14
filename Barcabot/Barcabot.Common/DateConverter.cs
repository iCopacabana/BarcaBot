using System;
using System.Globalization;

namespace Barcabot.Common
{
    public static class DateConverter
    {
        public static string ConvertToString(string originalJsonDate)
        {
            // 2019-08-16T19:00:00Z UTC
            // to
            // 19:00 16/08/2019
            
            var date = DateTime.Parse(originalJsonDate, null, DateTimeStyles.RoundtripKind);
            var culture = CultureInfo.CreateSpecificCulture("pl-PL");

            return date.ToString(culture);
            //return $"{date.Hour:D2}:{date.Minute:D2} {date.Day}/{date.Month}/{date.Year}";
        }

        public static DateTime ConvertToDateTime(string originalJsonDate)
        {
            return DateTime.Parse(originalJsonDate, null, DateTimeStyles.RoundtripKind);
        }
    }
}