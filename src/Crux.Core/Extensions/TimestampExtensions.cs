using System;

namespace Crux.Core.Extensions
{
    /// <summary>
    /// A C++ time_t helper class
    /// </summary>
    public static class TimestampExtensions
    {
        private static readonly DateTime BASE_DATE = new DateTime(1970, 1, 1);

        public static DateTime ToDateTime(this int timeT)
        {
            return BASE_DATE.AddSeconds(timeT);
        }

        public static DateTime ToDateTime(this long timeT)
        {
            return BASE_DATE.AddSeconds(timeT);
        }

        public static int ToTimestamp(this DateTime dateTime)
        {
            TimeSpan diff = dateTime - BASE_DATE;
            return Convert.ToInt32(diff.TotalSeconds);
        }

        public static int FromDateTime(this DateTime dateTime)
        {
            TimeSpan diff = dateTime - BASE_DATE;
            return Convert.ToInt32(diff.TotalSeconds);
        }
    }
}
