using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        [XmlElement("File")]
        public List<CommitFile> Files { get; set; } = new();

        public IImmutableDictionary<DateTime, List<CommitFile>> GetChangeSets()
        {
            var dict = new Dictionary<DateTime, List<CommitFile>>();
            foreach (CommitFile file in Files)
            {
                foreach (Commit commit in file.Commits)
                {
                    if (!dict.ContainsKey(commit.CommitDate))
                    {
                        dict.Add(commit.CommitDate, new List<CommitFile>());
                    }
                    dict[commit.CommitDate].Add(file);
                }
            }
            return dict.ToImmutableDictionary();
        }
    }

    public class CommitFile
    {
        [XmlIgnore]
        public string SourcePath { get; set; }
        [XmlAttribute("p")]
        public string Path { get; set; }
        [XmlElement("Commit")]
        public List<Commit> Commits { get; set; }

        public Commit GetNewestCommit() => Commits?.OrderByDescending(c => c.CommitDate).FirstOrDefault();

        public Commit GetNewestCommitAsOf(DateTime latestCommitDateTime) => Commits?
            .OrderByDescending(c => c.CommitDate)
            .FirstOrDefault(c => c.CommitDate <= latestCommitDateTime);
    }

    public class Commit
    {
        [XmlAttribute("date")]
        public DateTime CommitDate { get; set; }
        [XmlAttribute("hash")]
        public string Hash { get; set; }

        [XmlAttribute("size")]
        public long Size { get; set; } = -1;
        [XmlAttribute("deleted")]
        public bool FileDeleted { get; set; }
        
        [XmlElement("MultiPart")]
        public List<CommitFilePart> FilePart { get; set; } = new();
        
        [XmlIgnore]
        public bool HasMultiPart => (FilePart?.Count ?? 0) > 0;

        public bool ShouldSerializeSize()
        {
            return Size >= 0;
        }
        public bool ShouldSerializeFileDeleted()
        {
            return FileDeleted;
        }
    }

    public class CommitFilePart
    {
        [XmlAttribute("index")]
        public int Index { get; set; }
        [XmlAttribute("size")]
        public long Size { get; set; }
    }
}