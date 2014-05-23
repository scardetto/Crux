using System;
using System.Linq;

namespace Crux.Core.Extensions
{
    public static class EnumExtensions
    {
        public static TDestType SafeCast<TDestType>(this Enum s1)
        {
            AssertEnumType<TDestType>();

            var newValue = s1.ParseAs<TDestType>();

            if (newValue.AsInt() != s1.AsInt()) {
                throw new InvalidCastException("This enum's name match... but their values do not.");
            }

            return newValue;
        }

        public static TDestType AsEnum<TDestType>(this int value)
        {
            value.AssertValidValue<TDestType>();
            return (TDestType)Enum.ToObject(typeof(TDestType), value);
        }

        public static TDestType AsEnum<TDestType>(this string value)
        {
            value.AssertValidValue<TDestType>();
            return (TDestType)Enum.Parse(typeof(TDestType), value, true);
        }

        public static void AssertValidValue<TDestType>(this TDestType value)
        {
            AssertValidValue<TDestType>((object)value);
        }

        public static void AssertValidValue<TDestType>(this object value)
        {
            if (!value.IsValidValue<TDestType>()) {
                throw new InvalidCastException("{0} is not a defined value of {1}".ToFormat(value, typeof(TDestType).Name));
            }
        }

        public static void AssertValidValue<TDestType>(this string value)
        {
            if (!value.IsValidValue<TDestType>()) {
                throw new InvalidCastException("{0} is not a defined value of {1}".ToFormat(value, typeof(TDestType).Name));
            }
        }

        public static bool IsValidValue<TDestType>(this TDestType value)
        {
            return IsValidValue<TDestType>((object)value);
        }

        public static bool IsValidValue<TDestType>(this object value)
        {
            AssertEnumType<TDestType>();
            return Enum.IsDefined(typeof(TDestType), value);
        }

        public static bool IsValidValue<TDestType>(this string value)
        {
            AssertEnumType<TDestType>();
            return Enum.GetNames(typeof(TDestType)).Any(x => x.ToLower() == value.ToLower());
        }

        private static int AsInt(this object input)
        {
            return (int)input;
        }

        private static void AssertEnumType<T>()
        {
            if (!typeof(T).IsEnum) {
                throw new InvalidCastException("This type can only be cast to an Enum type.");
            }
        }

        private static TDestType ParseAs<TDestType>(this Enum source)
        {
            try {
                return (TDestType)Enum.Parse(typeof(TDestType), source.ToString());
            } catch (Exception) {
                throw new InvalidCastException("Unable to parse {0} as {1}.".ToFormat(source, typeof(TDestType).Name));
            }
        }
    }
}
