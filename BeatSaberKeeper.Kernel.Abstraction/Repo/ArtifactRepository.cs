using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using Serilog;

namespace BeatSaberKeeper.Kernel.Abstraction.Repo
{
    public class ArtifactRepository : IRepository<Artifact>
    {
        private static readonly ILogger Logger = Log.ForContext<ArtifactRepository>();
        
        private readonly string _artifactPath;
        private readonly IFileSystem _fileSystem;
        private readonly ICompressionInterface _compressionInterface;

        public ArtifactRepository(
            string artifactPath,
            IFileSystem fileSystem,
            ICompressionInterface compressionInterface)
        {
            Logger.Verbose("Constructing new Artifact Repository for {ArtifactPath}", artifactPath);
            _artifactPath = artifactPath;
            _fileSystem = fileSystem;
            _compressionInterface = compressionInterface;
        }

        public IEnumerable<Artifact> GetAll()
        {
            return _fileSystem.Directory
                .GetFiles(_artifactPath, "*.bskeep")
                .Select(ConvertToArtifact);
        }

        private Artifact ConvertToArtifact(string path)
        {
            IFileInfo fi = _fileSystem.FileInfo.FromFileName(path);
            try
            {
                ArchiveMetaData artifact = _compressionInterface.ReadMetaDataFromArchive(path);
                return new Artifact()
                {
                    Name = Path.GetFileNameWithoutExtension(fi.Name),
                    FullPath = path,
                    Created = fi.CreationTime,
                    LastUpdated = fi.LastWriteTime,
                    Size = fi.Length,
                    GameVersion = artifact?.GameVersion,
                    Type = artifact?.Type ?? ArtifactType.Unknown,
                    ArchiveVersion = artifact?.ArchiveVersion ?? ArchiveMetaData.UNKNOWN_VERSION
                };
            }
            catch (InvalidDataException ex)
            {
                Logger.Error(ex, "Failed to read archive data from {ArchivePath}", path);
                return new Artifact
                {
                    Name = Path.GetFileNameWithoutExtension(fi.Name),
                    FullPath = path,
                    Created = fi.CreationTime,
                    LastUpdated = fi.LastWriteTime,
                    Size = fi.Length,
                    GameVersion = "<broken>",
                    Type = ArtifactType.ModBackup, // <- this is a ModBackup so we can display it
                    ArchiveVersion = ArchiveMetaData.UNKNOWN_VERSION,
                    IsDefect = true
                };
            }
        }

        public void Clone(Artifact entity, string newName)
        {
            string newFileName = Path.Combine(Path.GetDirectoryName(entity.FullPath) ??
                                              throw new InvalidOperationException(), $"{newName}.bskeep");
            _fileSystem.File.Copy(entity.FullPath, newFileName);
        }

        public Artifact Get(string id)
        {
            try
            {
                return ConvertToArtifact(Path.Combine(_artifactPath, $"{id}.bskeep"));
            }
            catch (IOException)
            {
                return null;
            }
        }

        public bool Exists(string id)
        {
            return _fileSystem.File.Exists(Path.Combine(_artifactPath, $"{id}.bskeep"));
        }

        public void Rename(Artifact artifact, string newName)
        {
            Logger.Debug("Renaming artifact {Artifact} to {NewArtifactName}", artifact, newName);
            string newFileName = Path.Combine(Path.GetDirectoryName(artifact.FullPath) ??
                                              throw new InvalidOperationException(), $"{newName}.bskeep");
            _fileSystem.File.Move(artifact.FullPath, newFileName);
        }

        public void Delete(Artifact artifact)
        {
            Logger.Debug("Deleting artifact {ArtifactName} ({ArtifactFullPath})",
                artifact.Name, artifact.FullPath);
            _fileSystem.File.Delete(artifact.FullPath);
        }
    }
}