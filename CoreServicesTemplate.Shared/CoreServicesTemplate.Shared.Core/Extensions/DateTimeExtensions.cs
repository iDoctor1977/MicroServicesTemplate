using System;

namespace CoreServicesTemplate.Shared.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStandardString(this DateTime dateTime)
        {
            var standardString = dateTime.ToString("dd-MM-yyyy");
            
            return standardString;
        }

        public static DateTime SetTime(this DateTime dateTime, int hours, int minutes = 0, int seconds = 0, int milliseconds = 0)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
    }
}