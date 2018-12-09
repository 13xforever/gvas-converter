using System.Diagnostics;
using System.IO;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("R = {R}, G = {G}, B = {B}, A = {A}", Name = "{Name}")]
    public sealed class UELinearColorStructProperty : UEStructProperty
    {
        public UELinearColorStructProperty() { }
        public UELinearColorStructProperty(BinaryReader reader)
        {
            R = reader.ReadSingle();
            G = reader.ReadSingle();
            B = reader.ReadSingle();
            A = reader.ReadSingle();
        }

        public float R, G, B, A;
    }
}