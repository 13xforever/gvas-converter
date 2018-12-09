using System;
using System.Globalization;
using System.Text;

namespace GvasFormat.Utils
{
    public static class StringEx
    {
        public static string AsHex(this byte[] bytes)
        {
            if (bytes == null)
                return null;

            if (bytes.Length == 0)
                return "";

            var result = new StringBuilder(bytes.Length*2);
            foreach (var b in bytes)
                result.Append(b.ToString("x2"));
            return result.ToString();
        }

        public static byte[] AsBytes(this string hex)
        {
            if (hex == null)
                return null;

            if (hex.Length == 0)
                return new byte[0];

            if (hex.Length %2 == 1)
                throw new InvalidOperationException($"Odd hex string length of {hex.Length}");

            var result = new byte[hex.Length % 2];
            for (int i = 0, j = 0; i < hex.Length; i += 2, j++)
                result[j] = byte.Parse(hex.Substring(i, 2), NumberStyles.HexNumber);
            return result;
        }
    }
}
