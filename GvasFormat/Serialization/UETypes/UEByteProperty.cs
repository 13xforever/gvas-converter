using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using GvasFormat.Utils;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEByteProperty : UEProperty
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false);

        public UEByteProperty() { }
        public static UEByteProperty Read(BinaryReader reader, long valueLength)
        {
            var none = reader.ReadUEString();
            var throwAway = reader.ReadBytes(1);
            var bytes = reader.ReadBytes(Convert.ToInt32(valueLength));
            return new UEByteProperty { Value = ((int)bytes[0]).ToString()};
    }

        public static UEProperty[] Read(BinaryReader reader, long valueLength, int count)
        {
            var none = reader.ReadUEString();
            var throwAway = reader.ReadBytes(1);
            var bytes = reader.ReadBytes(Convert.ToInt32(valueLength));
            return new UEProperty[] { new UEByteProperty { Value = ((int)bytes[0]).ToString() } };
        }

        public override void Serialize(BinaryWriter writer) => throw new NotImplementedException();

        public string Value;
    }
}