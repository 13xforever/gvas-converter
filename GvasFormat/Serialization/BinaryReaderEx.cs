using System;
using System.IO;
using System.Text;

namespace GvasFormat.Serialization
{
    public static class BinaryReaderEx
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public static byte Reverse(byte value)
        {
            byte reverse = 0;
            for (int bit = 0; bit < 8; bit++)
            {
                reverse <<= 1;
                reverse |= (byte)(value & 1);
                value >>= 1;
            }

            return reverse;
        }

        public static string ReadUEString(this BinaryReader reader)
        {
            if (reader.PeekChar() < 0)
                return null;


            var bytes = reader.ReadBytes(4);
            var length = BitConverter.ToInt32(bytes, 0);

            if (length == 0)
                return null;

            if (length == 1)
                return "";

            if (length < 0)
            {
                // negative seems to indicate double length encoding, utf-8 rather than ascii
                length = Math.Abs(length)*2;
            }

            var valueBytes = reader.ReadBytes(length);
            return Utf8.GetString(valueBytes, 0, valueBytes.Length - 1);
        }

        public static void WriteUEString(this BinaryWriter writer, string value)
        {
            if (value == null)
            {
                writer.Write(0);
                return;
            }

            var valueBytes = Utf8.GetBytes(value);
            writer.Write(valueBytes.Length + 1);
            if (valueBytes.Length > 0)
                writer.Write(valueBytes);
            writer.Write((byte)0);
        }
    }
}
