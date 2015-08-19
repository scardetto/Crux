using System.Collections.Generic;

namespace Crux.Core.Extensions
{
    public static class ReadOnlyDictionaryExtensions 
    {
        public static TV GetValue<TK, TV>(this IReadOnlyDictionary<TK,TV> dictionary, TK key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : default(TV);
        }
    }
}
