using System.Xml.Serialization;
using BeatSaberKeeper.Kernel.Abstraction;

namespace BeatSaberKeeper.Kernel.V1
{
    [XmlRoot("BeatKeeperArchiveMetaData")]
    public class V1ArchiveMetaData : ArchiveMetaData
    {
        public const string V1 = "v1";

        public override string ArchiveVersion { get; set; } = V1;
    }
}