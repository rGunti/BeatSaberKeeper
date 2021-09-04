using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BeatSaberKeeper.Kernel.Abstraction;

namespace BeatSaberKeeper.Kernel.V2
{
    [XmlRoot("BeatSaberKeeperV2")]
    public class V2ArchiveMetaData : ArchiveMetaData
    {
        public const string VERSION = "v2";
        public override string ArchiveVersion { get; set; } = VERSION;
        public List<CommitFile> Files { get; set; } = new();
    }

    public class CommitFile
    {
        [XmlIgnore]
        public string SourcePath { get; set; }
        public string Path { get; set; }
        public List<Commit> Commits { get; set; }

        public Commit GetNewestCommit() => Commits?.OrderByDescending(c => c.CommitDate).FirstOrDefault();
    }

    public class Commit
    {
        public DateTime CommitDate { get; set; }
        public string Hash { get; set; }
        public long Size { get; set; }
        public bool FileDeleted { get; set; }
    }
}