using System;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    public abstract class UEProperty
    {
        public string Name;
        public string Type;

        public abstract void Serialize(BinaryWriter writer);

        public static UEProperty Read(BinaryReader reader)
        {
            if (reader.PeekChar() < 0)
                return null;

            var name = reader.ReadUEString();
            if (name == null)
                return null;

            if (name == "None")
                return new UENoneProperty { Name = name };

            var type = reader.ReadUEString();
            var valueLength = reader.ReadInt64();
            return UESerializer.Deserialize(name, type, valueLength, reader);
        }

        public static UEProperty[] Read(BinaryReader reader, int count)
        {
            if (reader.PeekChar() < 0)
                return null;

            var name = reader.ReadUEString();
            var type = reader.ReadUEString();
            var valueLength = reader.ReadInt64();
            return UESerializer.Deserialize(name, type, valueLength, count, reader);
        }
    }
}