using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEIntProperty : UEProperty
    {
        public UEIntProperty() { }

        public UEIntProperty(BinaryReader reader, long valueLength, bool term)
        {
            if (term)
            {
                var terminator = reader.ReadByte();
            }
            
            Address = $"0x{ reader.BaseStream.Position - 1:x8}";
            Value = reader.ReadInt32();
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public int Value;
    }
}