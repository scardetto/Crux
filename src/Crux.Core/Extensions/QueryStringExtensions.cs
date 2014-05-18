using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Crux.Core.Extensions
{
    public static class QueryStringExtensions
    {
        public static string ToQueryString(this IDictionary<string,string> parameters, bool encode = false, char paramDelimiter = '&', char valueDelimiter = '=')
        {
            if (parameters == null) return null;

            return parameters
                .Select(kvp => kvp.Key + valueDelimiter + EncodeValue(kvp.Value, encode))
                .Join(paramDelimiter);
        }

        public static IDictionary<string, string> FromQueryString(this string input, bool decode = false, char paramDelimiter = '&', char valueDelimiter = '=')
        {
            var parameters = new Dictionary<string, string>();

            input.Split(paramDelimiter)
                .Select(p => p.DivideString(valueDelimiter))
                .Each(parts => parameters.Add(parts.Left, DecodeValue(parts.Right, decode)));

            return parameters;
        }

        private static string EncodeValue(string value, bool encoded)
        {
            return encoded
                ? WebUtility.UrlEncode(value)
                : value;
        }

        private static string DecodeValue(string value, bool encoded)
        {
            return encoded
                ? WebUtility.UrlDecode(value)
                : value;
        }
    }
}
