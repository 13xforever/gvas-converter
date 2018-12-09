using System;
using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("{Value}", Name = "{Name}")]
    public sealed class UEGuidStructProperty : UEStructProperty
    {
        public UEGuidStructProperty() { }

        public UEGuidStructProperty(BinaryReader reader)
        {
            Value = new Guid(reader.ReadBytes(16));
        }

        public Guid Value;
    }
}