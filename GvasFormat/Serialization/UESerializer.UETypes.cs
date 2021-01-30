using System;
using System.Diagnostics;
using System.IO;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat.Serialization
{
    public static partial class UESerializer
    {
        internal static UEProperty Deserialize(string name, string type, long valueLength, BinaryReader reader)
        {
            UEProperty result;
            var itemOffset = reader.BaseStream.Position;
            switch (type)
            {
                case "BoolProperty":
                    result = new UEBoolProperty(reader, valueLength);
                    break;
                case "IntPropertyArray":
                    result = new UEIntProperty(reader, valueLength, false);
                    break;
                case "IntProperty":
                    result = new UEIntProperty(reader, valueLength, true);
                    break;
                case "UInt32Property":
                    result = new UEInt32Property(reader, valueLength);
                    break;
                case "FloatProperty":
                    result = new UEFloatProperty(reader, valueLength);
                    break;
                case "NameProperty":
                case "StrProperty":
                    result = new UEStringProperty(reader, valueLength);
                    break;
                case "TextProperty":
                    result = new UETextProperty(reader, valueLength);
                    break;
                case "EnumProperty":
                    result = new UEEnumProperty(reader, valueLength);
                    break;
                case "StructProperty":
                    result = UEStructProperty.Read(reader, valueLength);
                    result.Address = $"0x{ reader.BaseStream.Position - 1:x8}";
                    break;
                case "ArrayProperty":
                    result = new UEArrayProperty(reader, valueLength);
                    break;
                case "MapProperty":
                    result = new UEMapProperty(reader, valueLength);
                    break;
                case "ByteProperty":
                    result = UEByteProperty.Read(reader, valueLength);
                    result.Address = $"0x{ reader.BaseStream.Position - 1:x8}";
                    break;
                case "SetProperty":
                    result = new UESetProperty(reader, valueLength);
                    break;
                case "SoftObjectProperty":
                    result = new UEStringProperty(reader, valueLength);
                    break;
                case "ObjectProperty":
                    result = new UEStringProperty(reader, valueLength);
                    break;
                default:
                    result = new UEStringProperty(reader, valueLength);
                    break;
            }

            result.Name = name;
            result.Type = type;
            
            Debug.WriteLine(String.Format("{0} ({1}) {2}", name, type, $"0x{ reader.BaseStream.Position - 1:x8}"));

            return result;
        }

        internal static UEProperty[] Deserialize(string name, string type, long valueLength, int count, BinaryReader reader)
        {
            UEProperty[] result;
            switch (type)
            {
                case "StructProperty":
                    result = UEStructProperty.Read(reader, valueLength, count);
                    break;
                case "ByteProperty":
                    result = UEByteProperty.Read(reader, valueLength, count);
                    break;
                default:
                    throw new FormatException($"Unknown value type '{type}' of item '{name}'");
            }
            foreach (var item in result)
            {
                item.Name = name;
                item.Type = type;
            }
            return result;
        }
    }
}