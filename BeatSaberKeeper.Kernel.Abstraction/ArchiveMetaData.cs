namespace BeatSaberKeeper.Kernel.Abstraction
{
    public abstract class ArchiveMetaData
    {
        public abstract string ArchiveVersion { get; set; }
        public string GameVersion { get; set; }
        public ArtifactType Type { get; set; }
    }
}