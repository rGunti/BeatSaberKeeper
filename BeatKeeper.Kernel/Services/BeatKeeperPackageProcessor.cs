using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Utils;

namespace BeatKeeper.Kernel.Services
{
    public static class BeatKeeperPackageProcessor
    {
        private const string METADATA_FILE = "meta.xml";

        private static readonly HashSet<string> FileBlackList = new HashSet<string>()
        {
            METADATA_FILE
        };

        public static void PackVanillaArtifactV1(
            string sourcePath,
            string targetPath,
            string gameVersion,
            Action<string, int, int> statusReport = null)
        {
            var fileIndex = CreateFileIndex(sourcePath)
                .Select(f => new BeatKeeperArchiveFile(f, f.Replace(sourcePath, "").Substring(1)))
                .Where(i => !FileBlackList.Contains(i.Destination))
                .ToList();
            CreateArchive(
                Path.Combine(targetPath, $"BeatSaber_{gameVersion}.bskeep"),
                new BeatKeeperArchiveMetaData()
                {
                    GameVersion = gameVersion,
                    Type = ArtifactType.Vanilla
                },
                fileIndex,
                statusReport);
        }

        public static void PackBackupArtifactV1(
            string sourcePath,
            string targetPath,
            string gameVersion,
            Action<string, int, int> statusReport = null)
        {
            var fileIndex = CreateFileIndex(sourcePath)
                .Select(f => new BeatKeeperArchiveFile(f, f.Replace(sourcePath, "").Substring(1)))
                .Where(i => !FileBlackList.Contains(i.Destination))
                .ToList();
            CreateArchive(
                targetPath,
                new BeatKeeperArchiveMetaData()
                {
                    GameVersion = gameVersion,
                    Type = ArtifactType.ModBackup
                },
                fileIndex,
                statusReport);
        }

        public static void CreateArchive(
            string targetPath,
            BeatKeeperArchiveMetaData metaData,
            IEnumerable<BeatKeeperArchiveFile> files,
            Action<string, int, int> statusReport = null)
        {
            statusReport?.Invoke("Preparing Archive ...", -1, -1);
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            files = files.ToList();

            var fileCount = files.Count();
            var currentFile = 0;
            using (var zip = ZipFile.Open(targetPath.Replace('?', '-'),
                ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    currentFile++;
                    statusReport?.Invoke(
                        $"Packing file {currentFile} of {fileCount} ...\n{file.Destination}",
                        currentFile, fileCount);

                    zip.CreateEntryFromFile(
                        file.SourceFile,
                        $"files/{file.Destination}");
                }

                statusReport?.Invoke(
                    "Generating Meta data ...",
                    -1, currentFile);
                GenerateMetaFile(zip, metaData);
            }
        }

        public static void UnpackArchive(
            string artifactPath,
            string gamePath,
            Action<string, int, int> statusReport = null)
        {
            statusReport?.Invoke("Cleaning destination ...", -1, -1);
            if (Directory.Exists(gamePath))
            {
                Directory.Delete(gamePath, true);
            }

            var parentDirectory = Path.GetDirectoryName(gamePath);
            var filesDir = Path.Combine(parentDirectory, "files");
            if (Directory.Exists(filesDir))
            {
                Directory.Delete(filesDir, true);
            }

            var metaDataFile = Path.Combine(parentDirectory, METADATA_FILE);
            if (File.Exists(metaDataFile))
            {
                File.Delete(metaDataFile);
            }

            // Unpack to parent
            statusReport?.Invoke("Unpacking archive ...", -1, -1);
            Directory.CreateDirectory(gamePath);
            using (var zip = ZipFile.Open(artifactPath, ZipArchiveMode.Read))
            {
                statusReport?.Invoke("Unpacking meta file ...", -1, -1);
                var metaFile = zip.GetEntry(METADATA_FILE);
                metaFile.ExtractToFile(Path.Combine(gamePath, METADATA_FILE), true);

                var files = zip.Entries
                    .Where(e => e.FullName.StartsWith("files/"))
                    .ToList();
                var fileCount = files.Count;
                var i = 0;

                foreach (var file in files)
                {
                    i++;
                    statusReport?.Invoke($"Unpacking {i} / {fileCount} ...\n{file.FullName}",
                        i, fileCount);

                    var targetPath = Path.Combine(gamePath, file.FullName.Substring(6));
                    PathUtils.EnsureDirectoryExists(targetPath);
                    file.ExtractToFile(targetPath, true);
                }
            }
        }

        public static BeatKeeperArchiveMetaData ReadArchiveMetaData(
            string archivePath)
        {
            using (var zip = ZipFile.Open(archivePath, ZipArchiveMode.Read))
            {
                var metaEntry = zip.GetEntry(METADATA_FILE);
                if (metaEntry == null)
                {
                    return null;
                }
                return DeserializeMetaData(metaEntry.Open());
            }
        }

        private static string ReadAsString(
            this ZipArchiveEntry zipArchiveEntry)
        {
            using (var stream = zipArchiveEntry.Open())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static void GenerateMetaFile(
            ZipArchive zip,
            BeatKeeperArchiveMetaData metaData)
        {
            if (File.Exists(METADATA_FILE))
            {
                File.Delete(METADATA_FILE);
            }

            var xml = new XmlSerializer(metaData.GetType());
            using (var s = new MemoryStream())
            {
                xml.Serialize(s, metaData);
                File.WriteAllBytes(METADATA_FILE, s.ToArray());
            }
            zip.CreateEntryFromFile(METADATA_FILE, METADATA_FILE);
        }

        private static BeatKeeperArchiveMetaData DeserializeMetaData(
            Stream stream)
        {
            var s = new XmlSerializer(typeof(BeatKeeperArchiveMetaData));
            return (BeatKeeperArchiveMetaData) s.Deserialize(stream);
        }

        private static IEnumerable<string> CreateFileIndex(string path)
        {
            List<string> list = new List<string>();
            foreach (string dir in Directory.GetDirectories(path))
            {
                list.AddRange(CreateFileIndex(dir));
            }
            list.AddRange(Directory.GetFiles(path));
            return list;
        }
    }

    public class BeatKeeperArchiveMetaData
    {
        public const string V1 = "v1";

        public string ArchiveVersion { get; set; } = V1;

        public string GameVersion { get; set; }
        public ArtifactType Type { get; set; }
    }

    public class BeatKeeperArchiveFile
    {
        public BeatKeeperArchiveFile()
        {
        }

        public BeatKeeperArchiveFile(string sourceFile, string destination)
        {
            SourceFile = sourceFile;
            Destination = destination;
        }

        public string SourceFile { get; set; }
        public string Destination { get; set; }
    }

    public static class BeatKeeperArchiveExtensions
    {
        public static BeatKeeperArchiveFile ToArchive(
            this string sourceFile,
            string destination)
        {
            return new BeatKeeperArchiveFile(sourceFile, destination);
        }
    }
}