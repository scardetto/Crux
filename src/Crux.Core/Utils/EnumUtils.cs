using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux.Core.Utils
{
    public static class EnumUtils
    {
        public static IDictionary<int, string> ToDictionary<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be an enumeration");

            var enumType = typeof (T);

            return ((T[]) Enum.GetValues(enumType))
                .Cast<int>()
                .ToDictionary(e => e, e => Enum.GetName(enumType, e));
        }
    }
}
