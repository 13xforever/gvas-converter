using System.Collections.Generic;
using System.Text;
using GvasFormat.Serialization.UETypes;

namespace GvasFormat
{
    /*
     * General format notes:
     * Strings are 4-byte length + value + \0, length includes \0
     *
     */
    public class Gvas
    {
        public static readonly byte[] Header = Encoding.ASCII.GetBytes("GVAS");
        public int SaveGameVersion;
        public int PackageVersion;
        public EngineVersion EngineVersion = new EngineVersion();
        public int CustomFormatVersion;
        public CustomFormatData CustomFormatData = new CustomFormatData();
        public string SaveGameType;
        public List<UEProperty> Properties = new List<UEProperty>();
    }
}
