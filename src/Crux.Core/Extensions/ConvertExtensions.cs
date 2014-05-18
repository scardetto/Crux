using System;

namespace Crux.Core.Extensions
{
    public static class ConvertExtensions
    {
        public static Int32 ToInt32(this string val, int defaultValue)
        {
            return val.IsPresent() ? Convert.ToInt32(val) : defaultValue;
        }

        public static bool ToBoolean(this string value)
        {
            if (value.IsBlank()) return false;
            return bool.Parse(value);
        }

        /// <summary>
        /// Returns a DateTime value parsed from the <paramref name="dateTimeValue"/> parameter.
        /// </summary>
        /// <param name="dateTimeValue">A valid, parseable DateTime value</param>
        /// <returns>The parsed DateTime value</returns>
        public static DateTime ToDateTime(this string dateTimeValue)
        {
            return DateTime.Parse(dateTimeValue);
        }
    }
}
