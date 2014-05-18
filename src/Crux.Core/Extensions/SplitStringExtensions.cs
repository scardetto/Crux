using System.Globalization;

namespace Crux.Core.Extensions
{
    public static class SplitStringExtensions
    {
        public static SplitString DivideString(this string input, char token)
        {
            return DivideString(input, token.ToString(CultureInfo.InvariantCulture));
        }

        public static SplitString DivideString(this string input, string token, bool removeToken = true, bool reverse = false)
        {
            SplitString split;

            var pos = reverse
                    ? input.LastIndexOf(token, System.StringComparison.Ordinal)
                    : input.IndexOf(token, System.StringComparison.Ordinal);

            if (pos > 0) {
                var offset = removeToken ? token.Length : 0;

                split.Left = input.Substring(0, pos);
                split.Right = input.Substring(pos + offset);
            } else {
                split.Left = input;
                split.Right = "";
            }

            return split;
        }
    }

    public struct SplitString
    {
        public string Left;
        public string Right;
    }
}
