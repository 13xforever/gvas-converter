using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEInt32Property : UEProperty
    {
        public UEInt32Property() { }
        public UEInt32Property(BinaryReader reader, long valueLength)
        {
            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            if (valueLength != sizeof(Int32))
                throw new FormatException($"Expected int value of length {sizeof(Int32)}, but was {valueLength}");

            Address = $"0x{ reader.BaseStream.Position - 1:x8}";
            Value = reader.ReadInt32();
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public Int32 Value;
    }
}