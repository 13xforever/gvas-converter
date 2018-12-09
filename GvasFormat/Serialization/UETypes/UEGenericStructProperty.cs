using System.Collections.Generic;
using System.Diagnostics;

namespace GvasFormat.Serialization.UETypes
{
    [DebuggerDisplay("Count = {Properties.Count}", Name = "{Name}")]
    public sealed class UEGenericStructProperty : UEStructProperty
    {
        public List<UEProperty> Properties = new List<UEProperty>();
    }
}