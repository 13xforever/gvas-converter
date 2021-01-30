using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GvasFormat.Utils;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("Count = {Map.Count}", Name = "{Name}")]
    public sealed class UEMapProperty : UEProperty
    {
        public UEMapProperty() { }
        public UEMapProperty(BinaryReader reader, long valueLength)
        {
            var keyType = reader.ReadUEString();
            var valueType = reader.ReadUEString();

            if (valueType == "IntProperty")
                valueType += "Array";

            var unknown = reader.ReadBytes(5);
            if (unknown.Any(b => b != 0))
                throw new InvalidOperationException($"Offset: 0x{reader.BaseStream.Position-5:x8}. Expected ??? to be 0, but was 0x{unknown.AsHex()}");

            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                UEProperty key, value;

                if (keyType == "StructProperty")
                    key = Read(reader);
                else
                    key = UESerializer.Deserialize(null, keyType, -1, reader);
                var values = new List<UEProperty>();

                switch(valueType) {
                    case "StructProperty":
                        value = Read(reader);
                        break;
                    case "BoolProperty":
                        value = new UEBoolProperty()
                        {
                            Value = reader.ReadBoolean()
                        };
                        Debug.WriteLine(String.Format("  {0}: {1}", ((UEStringProperty)key).Value, ((UEBoolProperty)value).Value));
                        break;
                    case "FloatProperty":
                        var bytes = reader.ReadBytes(4);
                        value = new UEFloatProperty()
                        {
                            Value = System.BitConverter.ToSingle(bytes, 0)
                        };
                        Debug.WriteLine(String.Format("  {0}: {1}", ((UEStringProperty)key).Value, ((UEFloatProperty)value).Value));
                        break;
                    default:
                        value = UESerializer.Deserialize(null, valueType, -1, reader);
                        break;
                }

                values.Add(value);

                Map.Add(new UEKeyValuePair{Key = key, Values = values});
            }
        }
        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public List<UEKeyValuePair> Map = new List<UEKeyValuePair>();

        public class UEKeyValuePair
        {
            public UEProperty Key;
            public List<UEProperty> Values;
        }
    }
}