using System;

namespace Crux.Core.Extensions
{
    public static class GuidExtensions
    {
        private static readonly int GUID_STRING_LENGTH = Guid.Empty.ToString().Length;

        public static Guid ToGuid(this string val)
        {
            if (val == null || val.Length != GUID_STRING_LENGTH) {
                return Guid.Empty;
            }

            return new Guid(val);
        }

        public static string AsString(this Guid val)
        {
            return val.Equals(Guid.Empty) ? String.Empty : val.ToString();
        }

        public static bool IsGuid(this string strGuid)
        {
            try {
                new Guid(strGuid);
                return true;
            } catch {
                return false;
            }
        }
    }
}
