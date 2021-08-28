using BeatSaberKeeper.Kernel.Abstraction.Entities;

namespace BeatSaberKeeper.Kernel.Abstraction
{
    public abstract class ArchiveMetaData
    {
        public const string UNKNOWN_VERSION = "unknown";
        
        public abstract string ArchiveVersion { get; set; }
        public string GameVersion { get; set; }
        public ArtifactType Type { get; set; }
    }
}