using System;

namespace Crux.Core.Extensions
{
    public static class PredicateExtensions
    {
        public static Func<T, bool> Not<T>(this Func<T, bool> predicate)
        {
            return v => !predicate(v);
        }

        public static Func<T, bool> And<T>(this Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return v => predicate1(v) && predicate2(v);
        }

        public static Func<T, bool> Or<T>(this Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return v => predicate1(v) || predicate2(v);
        }
    }
}
