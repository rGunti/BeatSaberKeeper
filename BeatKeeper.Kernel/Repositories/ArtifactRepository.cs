using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Services;
using Serilog;

namespace BeatKeeper.Kernel.Repositories
{
    public class ArtifactRepository : IRepository<Artifact>
    {
        private readonly string _artifactPath;

        public ArtifactRepository(string artifactPath)
        {
            Log.Verbose($"Constructing new Artifact Repository for {artifactPath}");
            _artifactPath = artifactPath;
        }

        public IEnumerable<Artifact> GetAll()
        {
            return Directory.GetFiles(_artifactPath, "*.bskeep")
                .Select(i =>
                {
                    var fi = new FileInfo(i);
                    var artifact = BeatKeeperPackageProcessor.ReadArchiveMetaData(i);
                    return new Artifact()
                    {
                        Name = Path.GetFileNameWithoutExtension(fi.Name),
                        FullPath = i,
                        Created = fi.CreationTime,
                        LastUpdated = fi.LastWriteTime,
                        Size = fi.Length,
                        GameVersion = artifact?.GameVersion,
                        Type = artifact?.Type ?? ArtifactType.Unknown
                    };
                });
        }

        public Artifact Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Artifact artifact)
        {
            Log.Debug($"Deleting artifact {artifact.Name} ({artifact.FullPath})");
            File.Delete(artifact.FullPath);
        }
    }
}