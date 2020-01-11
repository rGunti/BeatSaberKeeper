using System;
using BeatKeeper.Kernel.Utils;

namespace BeatKeeper.Kernel.Entities
{
    public class Artifact
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string GameVersion { get; set; }
        public long Size { get; set; }
        public ArtifactType Type { get; set; }
        /// <summary>
        /// only required when <see cref="Type"/> is set to <see cref="ArtifactType.DownloadableVanilla"/>
        /// </summary>
        public string ManifestId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public string HumanReadableSize => Size.ToHumanReadableFileSize();

        public override string ToString()
        {
            return Type == ArtifactType.DownloadableVanilla ?
                $"{GameVersion} ({Created})" : 
                $"{Name} ({Type} @{GameVersion})";
        }
    }

    public enum ArtifactType
    {
        Vanilla,
        DownloadableVanilla,
        ModBackup,
        Unknown
    }
}