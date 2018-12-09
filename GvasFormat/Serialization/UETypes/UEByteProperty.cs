using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using GvasFormat.Utils;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEByteProperty : UEProperty
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public UEByteProperty() { }
        public static UEByteProperty Read(BinaryReader reader, long valueLength)
        {
            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            // valueLength starts here
            var arrayLength = reader.ReadInt32();
            var bytes = reader.ReadBytes(arrayLength);
            return new UEByteProperty {Value = bytes.AsHex()};
        }

        public static UEProperty[] Read(BinaryReader reader, long valueLength, int count)
        {
            // valueLength starts here
            var bytes = reader.ReadBytes(count);
            return new UEProperty[]{ new UEByteProperty {Value = bytes.AsHex()}};
        }

        public override void Serialize(BinaryWriter writer) => throw new NotImplementedException();

        public string Value;
    }
}