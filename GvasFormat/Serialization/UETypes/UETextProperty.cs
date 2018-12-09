using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UETextProperty : UEProperty
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public UETextProperty() { }
        public UETextProperty(BinaryReader reader, long valueLength)
        {
            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            // valueLength starts here
            Flags = reader.ReadInt64();
/*
            if (Flags != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 8:x8}. Expected text ??? {0x00}, but was {Flags:x16}");
*/

            terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            Id = reader.ReadUEString();
            Value = reader.ReadUEString();
        }

        public override void Serialize(BinaryWriter writer) => throw new NotImplementedException();

        public long Flags;
        public string Id;
        public string Value;
    }
}