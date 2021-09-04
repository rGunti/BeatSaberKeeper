﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Serialization;
using BeatSaberKeeper.Kernel.Abstraction;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using BeatSaberKeeper.Kernel.Abstraction.Utils;
using PandaDotNet.Utils;

namespace BeatSaberKeeper.Kernel.V2
{
    public class V2CompressionInterface : ICompressionInterface
    {
        private const string METADATA_FILE = "v2.meta";
        private static readonly string[] FileBlackList =
        {
            METADATA_FILE
        };

        private static readonly SHA256 Sha256 = SHA256.Create();

        private readonly IFileSystem _fileSystem;

        public V2CompressionInterface(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public CompressionInterfaceCapabilities Capabilities =>
            CompressionInterfaceCapabilities.ReadMetaData |
            CompressionInterfaceCapabilities.PackArchive |
            CompressionInterfaceCapabilities.UnpackArchive |
            CompressionInterfaceCapabilities.UpdateArchive;

        public void CreateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null,
            ArtifactType artifactType = ArtifactType.ModBackup)
        {
            string versionFilePath = _fileSystem.Path.Combine(sourcePath, "BeatSaberVersion.txt");
            var archiveMetaData = new V2ArchiveMetaData
            {
                GameVersion = _fileSystem.File.Exists(versionFilePath)
                    ? _fileSystem.File.ReadAllText(versionFilePath).Trim()
                    : "<unknown>",
                Type = artifactType
            };
            report.Submit("Scanning for files ...");
            IEnumerable<ArchiveFileInfo> fileList = ScanForFiles(sourcePath);
            CreateArchive(archivePath, fileList, archiveMetaData, report);
        }

        public void UnpackArchiveToFolder(string archivePath, string destinationPath, ReportProgressDelegate report = null)
        {
            if (_fileSystem.Directory.Exists(destinationPath))
            {
                report.Submit("Cleaning destination ...");
                _fileSystem.Directory.Delete(destinationPath, true);
            }

            if (!_fileSystem.Directory.Exists(destinationPath))
            {
                report.Submit("Creating destination directory ...");
                _fileSystem.Directory.CreateDirectory(destinationPath);
            }

            using var zipStream = _fileSystem.FileStream.Create(archivePath, FileMode.Open);
            using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);

            V2ArchiveMetaData metaData = ReadMetaDataFromArchive(zip);
            int fileIndex = 0;
            int totalFileCount = metaData.Files.Count;
            report.Submit("Start unpacking files ...");
            var missingFiles = new List<CommitFile>();
            foreach (CommitFile file in metaData.Files)
            {
                Commit newestCommit = file.GetNewestCommit();
                if (newestCommit.FileDeleted)
                {
                    // File has been deleted so we don't have to unpack anything
                    totalFileCount--;
                    continue;
                }

                fileIndex++;
                string missingFileStatusSuffix = missingFiles.Any() ? $" ({missingFiles.Count} file(s) missing!)" : "";
                report.Submit($"Unpacking file {fileIndex} / ~{totalFileCount}{missingFileStatusSuffix}\n" +
                              $"{file.Path}",
                    fileIndex, totalFileCount);

                string zipFilePath = ConstructZipFilePathFromCommit(file, newestCommit);
                string targetPath = Path.Combine(destinationPath, zipFilePath.Substring(zipFilePath.IndexOf('/') + 1));

                targetPath.EnsureDirectoryExistsForFile(_fileSystem);
                ZipArchiveEntry zipEntry = zip.GetEntry(zipFilePath);
                if (zipEntry == null)
                {
                    // TODO: Log a warning or something
                    missingFiles.Add(file);
                    continue;
                }
                ExtractFile(zipEntry, targetPath);
            }
            
            List<Tuple<CommitFile, string>> reports = ValidateFileHash(destinationPath, metaData.Files, report)
                .ToList();
        }

        public void UpdateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null)
        {
            throw new System.NotImplementedException();
        }

        public ArchiveMetaData ReadMetaDataFromArchive(string archivePath)
        {
            using var zipStream = _fileSystem.FileStream.Create(archivePath, FileMode.Open);
            using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);
            return ReadMetaDataFromArchive(zip);
        }

        private IEnumerable<string> CreateFileIndex(string path)
        {
            var list = new List<string>();
            foreach (string dir in _fileSystem.Directory.GetDirectories(path))
            {
                list.AddRange(CreateFileIndex(dir));
            }
            list.AddRange(_fileSystem.Directory.GetFiles(path));
            return list;
        }

        private IEnumerable<ArchiveFileInfo> ScanForFiles(string sourcePath)
        {
            return CreateFileIndex(sourcePath)
                .Select(f => ArchiveFileInfo.ConstructFromPath(f, sourcePath))
                .Where(f => !FileBlackList.Contains(f.Destination))
                .Select(f =>
                {
                    f.FileSize = _fileSystem.FileInfo.FromFileName(f.SourceFile).Length;
                    return f;
                });
        }

        private V2ArchiveMetaData ReadMetaDataFromArchive(ZipArchive zipArchive)
        {
            var metaEntry = zipArchive.GetEntry(METADATA_FILE);
            if (metaEntry == null)
            {
                return null;
            }

            var xml = new XmlSerializer(typeof(V2ArchiveMetaData));
            return xml.Deserialize(metaEntry.Open()) as V2ArchiveMetaData;
        }

        private void CreateArchive(
            string archivePath,
            IEnumerable<ArchiveFileInfo> files,
            V2ArchiveMetaData metaData,
            ReportProgressDelegate report)
        {
            DateTime commitDate = DateTime.UtcNow;
            metaData.Files = files
                .Select(f =>
                {
                    report.Submit($"Calculating file hash ...\n{f.Destination}");
                    string hash = CalculateFileHash(f.SourceFile);
                    return new CommitFile
                    {
                        SourcePath = f.SourceFile,
                        Path = f.Destination,
                        Commits = new List<Commit>
                        {
                            new()
                            {
                                CommitDate = commitDate,
                                Hash = hash,
                                Size = f.FileSize ?? -1
                            }
                        }
                    };
                })
                .ToList();
            int fileCount = metaData.Files.Count;

            using Stream zipStream = _fileSystem.FileStream.Create(archivePath, FileMode.Create);
            using var zip = new ZipArchive(zipStream, ZipArchiveMode.Create);

            var currentFile = 0;
            foreach (CommitFile fileCommit in metaData.Files)
            {
                currentFile++;
                report.Submit(
                    $"Packing file {currentFile} / {fileCount}\n" +
                    $"{fileCommit.Path}",
                    currentFile, fileCount);
                AddFileToZipArchive(zip, fileCommit);
            }
            
            report.Submit("Writing Metadata ...");
            GenerateMetaData(zip, metaData);
        }

        private void AddFileToZipArchive(
            ZipArchive zipArchive,
            CommitFile file,
            Commit commit = null)
        {
            commit = commit.Or(file.Commits.LastOrDefault);
            ZipArchiveEntry entry = zipArchive.CreateEntry(ConstructZipFilePathFromCommit(file, commit));
            using Stream zipFs = entry.Open();
            using Stream sourceFs = _fileSystem.FileStream.Create(file.SourcePath, FileMode.Open);
            sourceFs.CopyTo(zipFs);
        }

        private void ExtractFile(ZipArchiveEntry entry, string path)
        {
            using Stream zipFileStream = entry.Open();
            using Stream destinationFileStream = _fileSystem.FileStream.Create(path, FileMode.Create);
            zipFileStream.CopyTo(destinationFileStream);
        }

        private string ConstructZipFilePathFromCommit(CommitFile file, Commit commit) =>
            $"{commit!.CommitDate:yyyyMMdd'-'HHmmss}/{file.Path}";

        private string CalculateFileHash(string filePath)
        {
            using Stream fs = _fileSystem.File.OpenRead(filePath);
            return string.Join("", Sha256.ComputeHash(fs).Select(b => b.ToString("x2")));
        }
        
        private void GenerateMetaData(
            ZipArchive zipArchive,
            V2ArchiveMetaData metaData)
        {
            var xml = new XmlSerializer(metaData.GetType());
            ZipArchiveEntry entry = zipArchive.CreateEntry(METADATA_FILE);
            using Stream stream = entry.Open();
            xml.Serialize(stream, metaData);
        }

        private IEnumerable<Tuple<CommitFile, string>> ValidateFileHash(
            string unpackPath,
            IEnumerable<CommitFile> files,
            ReportProgressDelegate report,
            int expectedFileCount = -1)
        {
            report.Submit("Start validating files ...");
            int fileIndex = 0;
            int encounteredErrors = 0;
            if (expectedFileCount < 0)
            {
                files = files.ToList();
                expectedFileCount = files.Count();
            }
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (CommitFile file in files)
            {
                string targetPath = _fileSystem.Path.Combine(unpackPath, file.Path);
                Commit newestCommit = file.GetNewestCommit();
                if (newestCommit.FileDeleted)
                {
                    if (_fileSystem.File.Exists(targetPath))
                    {
                        yield return Tuple.Create(file, "File should not be there anymore");
                    }
                    continue;
                }

                fileIndex++;
                report.Submit($"Validating file {fileIndex} / {expectedFileCount} ({encounteredErrors} issues found) ...\n" +
                              $"{file.Path}");

                if (!_fileSystem.File.Exists(targetPath))
                {
                    yield return Tuple.Create(file, "Expected file is missing");
                    encounteredErrors++;
                    continue;
                }
                
                if (newestCommit.Size >= 0)
                {
                    long fileSize = _fileSystem.FileInfo.FromFileName(targetPath).Length;
                    if (fileSize != newestCommit.Size)
                    {
                        yield return Tuple.Create(file, $"File size did not match (expected {newestCommit.Size}b, got {fileSize}b)");
                        encounteredErrors++;
                        continue;
                    }
                }
                
                string hash = CalculateFileHash(targetPath);
                if (newestCommit.Hash != hash)
                {
                    encounteredErrors++;
                    yield return Tuple.Create(file, $"File hash did not match (expected {newestCommit.Hash}, got {hash}");
                    // ReSharper disable once RedundantJumpStatement
                    continue;
                }
            }
        }
    }
}