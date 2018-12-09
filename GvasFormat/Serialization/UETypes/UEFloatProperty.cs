using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEFloatProperty : UEProperty
    {
        public UEFloatProperty() { }
        public UEFloatProperty(BinaryReader reader, long valueLength)
        {
            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            if (valueLength != sizeof(float))
                throw new FormatException($"Expected float value of length {sizeof(float)}, but was {valueLength}");

            Value = reader.ReadSingle();
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public float Value;
    }
}