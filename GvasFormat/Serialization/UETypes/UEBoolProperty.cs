using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEBoolProperty : UEProperty
    {
        public UEBoolProperty() { }
        public UEBoolProperty(BinaryReader reader, long valueLength)
        {
            Address = $"0x{ reader.BaseStream.Position - 1:x8}";
            var val = reader.ReadInt16();
            if (val == 0)
                Value = false;
            else if (val == 1)
                Value = true;
            else
                throw new InvalidOperationException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected bool value, but was {val}");

            if (reader.PeekChar() == 'N')
            {
                var throwAway = reader.ReadBytes(10);
            }
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public bool Value;
    }
}