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
                .Select(ConvertToArtifact);
        }

        private Artifact ConvertToArtifact(string path)
        {
            var fi = new FileInfo(path);
            var artifact = BeatKeeperPackageProcessor.ReadArchiveMetaData(path);
            return new Artifact()
            {
                Name = Path.GetFileNameWithoutExtension(fi.Name),
                FullPath = path,
                Created = fi.CreationTime,
                LastUpdated = fi.LastWriteTime,
                Size = fi.Length,
                GameVersion = artifact?.GameVersion,
                Type = artifact?.Type ?? ArtifactType.Unknown,
                ArchiveVersion = artifact?.ArchiveVersion ?? BeatKeeperArchiveMetaData.UNKNOWN
            };
        }

        public void Clone(Artifact entity, string newName)
        {
            string newFileName = Path.Combine(Path.GetDirectoryName(entity.FullPath) ??
                                              throw new InvalidOperationException(), $"{newName}.bskeep");
            File.Copy(entity.FullPath, newFileName);
        }

        public Artifact Get(string id)
        {
            try
            {
                return ConvertToArtifact(Path.Combine(_artifactPath, $"{id}.bskeep"));
            } catch (IOException)
            {
                return null;
            }
        }

        public bool Exists(string id)
        {
            return File.Exists(Path.Combine(_artifactPath, $"{id}.bskeep"));
        }

        public void Rename(Artifact artifact, string newName)
        {
            Log.Debug($"Renaming artifact");
            string newFileName = Path.Combine(Path.GetDirectoryName(artifact.FullPath) ??
                                              throw new InvalidOperationException(), $"{newName}.bskeep");
            File.Move(artifact.FullPath, newFileName);
        }

        public void Delete(Artifact artifact)
        {
            Log.Debug($"Deleting artifact {artifact.Name} ({artifact.FullPath})");
            File.Delete(artifact.FullPath);
        }
    }
}