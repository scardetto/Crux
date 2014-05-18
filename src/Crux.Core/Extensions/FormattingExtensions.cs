using System;
using System.Text.RegularExpressions;

namespace Crux.Core.Extensions
{
    public static class FormattingExtensions
    {
        private const string ELLIPSES = "...";

        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return String.Format(stringFormat, args);
        }

        public static string TrimToSpace(this string val, int length)
        {
            return TrimToSpace(val, length, ELLIPSES);
        }

        public static string TrimToSpace(this string val, int length, string suffix)
        {
            if (val.Length <= length + suffix.Length) {
                return val;
            }

            var pos = val.LastIndexOf(" ", length, StringComparison.Ordinal);

            if (pos != -1) {
                val = val.Substring(0, pos) + suffix;
            }

            return val;
        }

        public static string ToGmtFormattedDate(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd hh':'mm':'ss tt 'GMT'");
        }

        /// <summary>
        /// Formats a multi-line string for display on the web
        /// </summary>
        /// <param name="plainText"></param>
        public static string ConvertLineBreaksToBreakTags(this string plainText)
        {
            return new Regex("(\r\n|\n)").Replace(plainText, "<br/>");
        }
    }
}
