using System;

namespace Crux.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void AssertNotNull(this object value, string errorMessage)
        {
            if (value == null) {
                throw new ArgumentException(errorMessage);
            }
        }

        public static void Continue<T>(this T value, Action<T> action) where T : class
        {
            if (value == null) {
                return;
            }

            action(value);
        }
    }
}
