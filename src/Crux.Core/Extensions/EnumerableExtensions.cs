using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Crux.Core.Extensions
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static bool IsBlank<T>(this IEnumerable<T> values)
        {
            return values == null || !values.Any();
        }

        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> values, Action<T, int> eachAction)
        {
            int index = 0;
            foreach (T item in values) {
                eachAction(item, index++);
            }
        }

        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> values, Action<T> eachAction)
        {
            foreach (T item in values) {
                eachAction(item);
            }
        }
    }
}
