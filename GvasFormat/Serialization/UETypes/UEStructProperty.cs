using System;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    public abstract class UEStructProperty : UEProperty
    {
        public UEStructProperty() { }

        public static UEStructProperty Read(BinaryReader reader, long valueLength)
        {
            var type = reader.ReadUEString();
            var id = new Guid(reader.ReadBytes(16));
            if (id != Guid.Empty)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 16:x8}. Expected struct ID {Guid.Empty}, but was {id}");

            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            return ReadStructValue(type, reader);
        }

        public static UEStructProperty[] Read(BinaryReader reader, long valueLength, int count)
        {
            var type = reader.ReadUEString();
            var id = new Guid(reader.ReadBytes(16));
            if (id != Guid.Empty)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 16:x8}. Expected struct ID {Guid.Empty}, but was {id}");

            var terminator = reader.ReadByte();
            if (terminator != 0)
                throw new FormatException($"Offset: 0x{reader.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

            var result = new UEStructProperty[count];
            for (var i = 0; i < count; i++)
                result[i] = ReadStructValue(type, reader);
            return result;
        }

        protected static UEStructProperty ReadStructValue(string type, BinaryReader reader)
        {
            UEStructProperty result;
            switch (type)
            {
                case "DateTime":
                    result = new UEDateTimeStructProperty(reader);
                    break;
                case "Guid":
                    result = new UEGuidStructProperty(reader);
                    break;
                case "Vector":
                case "Rotator":
                    result = new UEVectorStructProperty(reader);
                    break;
                case "LinearColor":
                    result = new UELinearColorStructProperty(reader);
                    break;
                default:
                    var tmp = new UEGenericStructProperty();
                        while (Read(reader) is UEProperty prop)
                        {
                            tmp.Properties.Add(prop);
                            if (prop is UENoneProperty)
                                break;
                        }
                    result = tmp;
                    break;
            }
            result.Type = type;
            return result;
        }

        public override void Serialize(BinaryWriter writer) { throw new NotImplementedException(); }

        public string StructType;
        //public Guid Unknown = Guid.Empty;
    }
}