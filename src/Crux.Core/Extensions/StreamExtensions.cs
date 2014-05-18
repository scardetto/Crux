using System;
using System.IO;
using System.Text;

namespace Crux.Core.Extensions
{
    public static class StreamExtensions
    {
        public static Stream ToStream(this string data)
        {
            return ToStream(data, Encoding.Default);
        }

        public static Stream ToStream(this string data, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(data);
            return new MemoryStream(bytes);
        }

        public static string AsString(this Stream stream)
        {
            var reader = new StreamReader(stream);

            string str = reader.ReadToEnd();
            return str;
        }

        public static string AsString(this byte[] input)
        {
            return AsString(input, Encoding.ASCII);
        }

        public static string AsString(byte[] input, Encoding encoding)
        {
            char[] converted = encoding.GetChars(input);
            return new String(converted);
        }
    }
}
