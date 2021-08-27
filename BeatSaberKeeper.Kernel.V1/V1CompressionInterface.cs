using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using BeatSaberKeeper.Kernel.Abstraction;
using BeatSaberKeeper.Kernel.Abstraction.Utils;
using PandaDotNet.Utils;

namespace BeatSaberKeeper.Kernel.V1
{
    public class V1CompressionInterface : ICompressionInterface
    {
        private const string METADATA_FILE = "meta.xml";
        private static readonly string[] FileBlackList = {
            METADATA_FILE
        };

        private readonly IFileSystem _fileSystem;

        public V1CompressionInterface(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public CompressionInterfaceCapabilities Capabilities =>
            CompressionInterfaceCapabilities.PackArchive |
            CompressionInterfaceCapabilities.UnpackArchive |
            CompressionInterfaceCapabilities.ReadMetaData;

        public void CreateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null)
        {
            var archiveMeta = new V1ArchiveMetaData
            {
                GameVersion = _fileSystem.File.ReadAllText(Path.Combine(sourcePath, "BeatSaberVersion.txt"))
                    .Trim(),
                Type = ArtifactType.ModBackup
            };
            report.Submit("Scanning for files ...");
            var fileList = ScanForFiles(sourcePath);
            CreateZipFile(archivePath, fileList, archiveMeta, report);
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
            var files = zip.Entries
                .Where(e => e.FullName.StartsWith("files/"))
                .ToList();
            var fileCount = files.Count;
            var i = 0;

            foreach (var file in files)
            {
                i++;
                report.Submit($"Unpacking file {i} / {fileCount} ...\n{file.FullName}", i, fileCount);

                var targetPath = Path.Combine(destinationPath, file.FullName.Substring(6));
                targetPath.EnsureDirectoryExistsForFile(_fileSystem);
                ExtractFile(file, targetPath);
            }
        }

        public void UpdateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null)
        {
            throw new NotImplementedException($"Updating archives is not supported by V1 Archives");
        }

        public ArchiveMetaData ReadMetaDataFromArchive(string archivePath)
        {
            using var zipStream = _fileSystem.FileStream.Create(archivePath, FileMode.Open);
            using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);
            var metaEntry = zip.GetEntry(METADATA_FILE);
            if (metaEntry == null)
            {
                return null;
            }

            var xml = new XmlSerializer(typeof(V1ArchiveMetaData));
            return xml.Deserialize(metaEntry.Open()) as V1ArchiveMetaData;
        }

        private IEnumerable<ArchiveFileInfo> ScanForFiles(string sourcePath)
        {
            return CreateFileIndex(sourcePath)
                .Select(f => ArchiveFileInfo.ConstructFromPath(f, sourcePath))
                .Where(f => !FileBlackList.Contains(f.Destination));
        }
        
        private IEnumerable<string> CreateFileIndex(string path)
        {
            var list = new List<string>();
            foreach (var dir in _fileSystem.Directory.GetDirectories(path))
            {
                list.AddRange(CreateFileIndex(dir));
            }
            list.AddRange(_fileSystem.Directory.GetFiles(path));
            return list;
        }

        private void CreateZipFile(
            string archivePath,
            IEnumerable<ArchiveFileInfo> files,
            V1ArchiveMetaData metaData,
            ReportProgressDelegate report)
        {
            report.Submit("Preparing archive ...");
            var fileList = files.ToList();
            var fileCount = fileList.Count;

            using var zipStream = _fileSystem.FileStream.Create(archivePath, FileMode.Create);
            using var zip = new ZipArchive(zipStream, ZipArchiveMode.Create);

            var currentFile = 0;
            foreach (var file in fileList)
            {
                report.Submit(
                    $"Packing file {currentFile} / {fileCount}\n" + 
                    $"{file.Destination}",
                    currentFile, fileCount);
                AddFileToZipArchive(zip, file);
                currentFile++;
            }
                
            report.Submit("Generating Metadata ...");
            GenerateMetaData(zip, metaData);
        }

        private void AddFileToZipArchive(
            ZipArchive zipArchive,
            ArchiveFileInfo file)
        {
            var entry = zipArchive.CreateEntry($"files/{file.Destination}");
            using var zipFileStream = entry.Open();
            using var sourceFileStream = _fileSystem.FileStream.Create(file.SourceFile, FileMode.Open);
            sourceFileStream.CopyTo(zipFileStream);
        }

        private void ExtractFile(ZipArchiveEntry entry, string path)
        {
            using var zipFileStream = entry.Open();
            using var destinationFileStream = _fileSystem.FileStream.Create(path, FileMode.Create);
            zipFileStream.CopyTo(destinationFileStream);
        }

        private void GenerateMetaData(
            ZipArchive zipArchive,
            V1ArchiveMetaData metaData)
        {
            var xml = new XmlSerializer(metaData.GetType());
            var entry = zipArchive.CreateEntry(METADATA_FILE);
            using (var stream = entry.Open())
            {
                xml.Serialize(stream, metaData);
            }
        }
    }
}