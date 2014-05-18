using System;
using System.Globalization;
using System.Text;

namespace Crux.Core.Extensions
{
    public static class HexStringExtensions
    {
        public static string ToHexString(this string input)
        {
            return input.ToHexString(Encoding.ASCII);
        }

        public static string ToHexString(this string input, Encoding encoding)
        {
            return input.IsBlank() ? null : input.Encode(encoding).ToHexString();
        }

        public static string ToHexString(this byte[] buffer)
        {
            var builder = new StringBuilder(buffer.Length * 2);

            foreach (var b in buffer) {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }

        public static string FromHexString(this string input)
        {
            return FromHexString(input, Encoding.ASCII);
        }

        public static string FromHexString(this string input, Encoding encoding)
        {
            return input.HexStringToBytes().Decode(encoding);
        }

        public static byte[] HexStringToBytes(this string input)
        {
            if (input == null) {
                return null;
            }

            if (input.Length % 2 != 0) {
                throw new ArgumentException("Not a valid hex string", "input");
            }

            // Allocate new buffer
            var buffer = new byte[input.Length / 2];

            // Parse bytes from hex string one at a time.
            for (var i = 0; i < input.Length; i = i + 2) {
                buffer[i / 2] = Byte.Parse(input.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            }

            return buffer;
        }
    }
}
