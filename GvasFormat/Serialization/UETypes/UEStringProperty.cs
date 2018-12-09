using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEStringProperty : UEProperty
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public UEStringProperty() { }
        public UEStringProperty(BinaryReader reader, long valueLength)
        {
            if (valueLength > -1)
            {
                var terminator = reader.ReadByte();
                if (terminator != 0)
                    throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");
            }

            Value = reader.ReadUEString();
        }

        public override void Serialize(BinaryWriter writer)
        {
            if (Value == null)
            {
                writer.Write(0L);
                writer.Write((byte)0);
            }
            else
            {
                var bytes = Utf8.GetBytes(Value);
                writer.Write(bytes.Length + 6L);
                writer.Write((byte)0);
                writer.Write(bytes.Length+1);
                if (bytes.Length > 0)
                    writer.Write(bytes);
                writer.Write((byte)0);
            }
        }

        public string Value;
    }
}