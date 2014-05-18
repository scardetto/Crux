using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Crux.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsBlank(this string value)
        {
            return String.IsNullOrEmpty(value) 
                || value.Trim() == String.Empty 
                || Convert.IsDBNull(value);
        }

        public static bool IsPresent(this string value)
        {
            return !value.IsBlank();
        }

        public static void AssertNotBlank(this string value, string errorMessage)
        {
            if (value.IsBlank()) {
                throw new ArgumentException(errorMessage);
            }
        }

        public static bool ArePresent(this string[] values)
        {
            return values.All(value => value.IsPresent());
        }

        public static bool AreBlank(this string[] values)
        {
            return values.All(value => value.IsBlank());
        }

        public static bool IsNumeric(this string input)
        {
            return IsNumeric(input, CultureInfo.CurrentCulture);
        }

        public static bool IsNumeric(this string input, CultureInfo culture)
        {
            if (input.IsBlank()) { return false; }

            double outValue;
            return double.TryParse(input.Trim(), NumberStyles.Any,
                                   culture, out outValue);
        }

        public static bool IsAlphanumeric(this string input)
        {
            return input.AllCharsMatch(Char.IsLetterOrDigit);
        }

        public static bool IsAlphabetic(this string input)
        {
            return input.AllCharsMatch(Char.IsLetter);
        }

        public static bool AllCharsMatch(this string input, Func<char, bool> predicate)
        {
            if (input.IsBlank()) { return false; }
            return input.All(predicate);
        }

        public static string StripChars(this string value, Func<char, bool> predicate)
        {
            return value.IsPresent() 
                ? new string(value.Where(predicate.Not()).ToArray()) 
                : string.Empty;
        }

        public static byte[] Encode(this string input)
        {
            return input.Encode(Encoding.Default);
        }

        public static byte[] Encode(this string input, Encoding encoding)
        {
            return encoding.GetBytes(input);
        }

        public static string Decode(this byte[] input)
        {
            return input.Decode(Encoding.Default);
        }

        public static string Decode(this byte[] input, Encoding encoding)
        {
            return encoding.GetString(input);
        }

        /// <summary>
        /// Performs a case-insensitive comparison of strings
        /// </summary>
        public static bool EqualsIgnoreCase(this string thisString, string otherString)
        {
            return thisString.Equals(otherString, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Converts the string to Title Case
        /// </summary>
        public static string Capitalize(this string stringValue)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringValue);
        }

        public static string Left(this string input, int length)
        {
            return input.IsPresent() && length > 0 
                ? new string(input.Take(length).ToArray()) 
                : string.Empty;
        }

        public static string Right(this string input, int length)
        {
            return input.IsPresent() && length > 0 
                ? new string(input.Skip(input.Length - length).ToArray()) 
                : string.Empty;
        }

        /// <summary>
        /// Concatenates a string between each item in a sequence of strings
        /// </summary>
        /// <param name="values"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return String.Join(separator, values);
        }

        public static string Join(this IEnumerable<string> values, char separator)
        {
            return values.Join(separator.ToString());
        }
    }
}