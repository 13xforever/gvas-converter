using GvasFormat.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UESetProperty : UEProperty
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public UESetProperty() { }
        public UESetProperty(BinaryReader reader, long valueLength)
        {
            var ItemType = reader.ReadUEString();

            if (ItemType == "IntProperty")
                ItemType += "Array";

            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            reader.ReadInt32();
            var count = reader.ReadInt32();
            Address = $"0x{ reader.BaseStream.Position - 1:x8}";

            Items = new UEProperty[count];
            switch (ItemType)
            {
                case "StructProperty":
                    Items = Read(reader, count);
                    break;
                case "ByteProperty":
                    Items = UEByteProperty.Read(reader, valueLength, count);
                    break;
                case "IntPropertyArray":
                    for (var i = 0; i < count; i++)
                    {
                        var value = reader.ReadInt32();
                        Items[i] = new UEIntProperty()
                        {
                            Value = value
                        };
                        Debug.WriteLine(String.Format("  {0} {1}", ((UEIntProperty)Items[i]).Value, $"0x{reader.BaseStream.Position - 1:x8}"));
                    }
                    break;
                case "EnumProperty":
                    for (var i = 0; i < count; i++)
                    {
                        Items[i] = new UEEnumProperty()
                        {
                            Value = reader.ReadUEString(),
                        };
                        Debug.WriteLine(String.Format("  {0} ({1}) {2}", ((UEEnumProperty)Items[i]).EnumType, ((UEEnumProperty)Items[i]).Value, $"0x{reader.BaseStream.Position - 1:x8}"));
                    }
                    break;
                case "SoftObjectProperty":
                    for (var i = 0; i < count; i++)
                    {
                        Items[i] = new UEStringProperty()
                        {
                            Value = reader.ReadUEString(),
                        };
                        Debug.WriteLine(String.Format("  {0} {1}", ((UEStringProperty)Items[i]).Value, $"0x{reader.BaseStream.Position - 1:x8}"));
                    }
                    break;
                default:
                    {
                        for (var i = 0; i < count; i++)
                            Items[i] = UESerializer.Deserialize(null, ItemType, -1, reader);
                        break;
                    }
            }
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public string ItemType;
        public UEProperty[] Items;
    }
}