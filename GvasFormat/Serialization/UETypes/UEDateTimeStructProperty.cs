using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEDateTimeStructProperty : UEStructProperty
    {
        public UEDateTimeStructProperty() { }

        public UEDateTimeStructProperty(BinaryReader reader) => Value = DateTime.FromBinary(reader.ReadInt64());

        public DateTime Value;
    }
}