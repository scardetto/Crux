using System;
using System.Security.Cryptography;
using System.Text;

namespace Crux.Core.Extensions
{
    public static class Hasher
    {
        public static string ToBase64Hash(this string inputString)
        {
            return ToBase64Hash(inputString, Encoding.ASCII);
        }

        public static string ToBase64Hash(this string inputString, Encoding encoding)
        {
            return ToBase64Hash(inputString, encoding, HashAlgorithm.Create("SHA1"));
        }

        public static string ToBase64Hash(this string inputString, Encoding encoding, HashAlgorithm algo)
        {
            var inputBuffer = encoding.GetBytes(inputString);
            var outputBuffer = algo.ComputeHash(inputBuffer, 0, inputBuffer.Length);

            return Convert.ToBase64String(outputBuffer);
        }

        public static string ToHexStringHash(this string inputString, Encoding encoding, HashAlgorithm algo)
        {
            var inputBuffer = encoding.GetBytes(inputString);
            return ToHexStringHash(inputBuffer, algo);
        }

        public static string ToHexStringHash(this byte[] inputBuffer, HashAlgorithm algo)
        {
            var outputBuffer = algo.ComputeHash(inputBuffer);
            return outputBuffer.ToHexString();
        }
    }
}
