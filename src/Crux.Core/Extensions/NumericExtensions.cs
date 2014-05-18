using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Crux.Core.Extensions
{
    public static class NumericExtensions
    {
        public static bool InRange(this int value, int lowerBound, int upperBound)
        {
            return (value >= lowerBound && value <= upperBound);
        }

        public static bool Between(this int value, int lowerBound, int upperBound)
        {
            return (value > lowerBound && value < upperBound);
        }

        [DebuggerStepThrough]
        public static IEnumerable<int> Times(this int i)
        {
            return Enumerable.Range(0, i);
        }

        [DebuggerStepThrough]
        public static void Times(this int i, Action action)
        {
            Enumerable.Range(0, i).Each(c => action());
        }

        [DebuggerStepThrough]
        public static void Times(this int i, Action<int> action)
        {
            Enumerable.Range(0, i).Each(action);
        }
    }
}
