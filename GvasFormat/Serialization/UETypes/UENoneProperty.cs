using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("", Name = "{Name}")]
    public sealed class UENoneProperty : UEProperty
    {
        public override void Serialize(BinaryWriter writer) { throw new System.NotImplementedException(); }
    }
}