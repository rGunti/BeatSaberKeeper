using BeatSaberKeeper.Kernel.Abstraction;

namespace BeatSaberKeeper.Kernel.V1
{
    public class V1ArchiveMetaData : ArchiveMetaData
    {
        public const string V1 = "v1";

        public override string ArchiveVersion { get; set; } = V1;
    }
}