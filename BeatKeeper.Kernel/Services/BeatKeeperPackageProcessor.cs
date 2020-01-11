using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using BeatKeeper.Kernel.Entities;

namespace BeatKeeper.Kernel.Services
{
    public static class BeatKeeperPackageProcessor
    {
        private const string METADATA_FILE = "meta.xml";

        public static void PackVanillaArtifactV1(
            string sourcePath,
            string targetPath,
            string gameVersion)
        {
            var fileIndex = CreateFileIndex(sourcePath)
                .Select(f => new BeatKeeperArchiveFile(f, f.Replace(sourcePath, "").Substring(1)))
                .ToList();
            CreateArchive(
                Path.Combine(targetPath, $"BeatSaber_{gameVersion}.bskeep"),
                new BeatKeeperArchiveMetaData()
                {
                    GameVersion = gameVersion,
                    Type = ArtifactType.Vanilla
                },
                fileIndex);
        }

        public static void PackBackupArtifactV1(
            string sourcePath,
            string targetPath,
            string gameVersion)
        {
            var fileIndex = CreateFileIndex(sourcePath)
                .Select(f => new BeatKeeperArchiveFile(f, f.Replace(sourcePath, "").Substring(1)))
                .ToList();
            CreateArchive(
                targetPath,
                new BeatKeeperArchiveMetaData()
                {
                    GameVersion = gameVersion,
                    Type = ArtifactType.ModBackup
                },
                fileIndex);
        }

        public static void CreateArchive(
            string targetPath,
            BeatKeeperArchiveMetaData metaData,
            IEnumerable<BeatKeeperArchiveFile> files)
        {
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            using (var zip = ZipFile.Open(targetPath.Replace('?', '-'),
                ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    zip.CreateEntryFromFile(
                        file.SourceFile,
                        $"files/{file.Destination}");
                }

                GenerateMetaFile(zip, metaData);
            }
        }

        public static void UnpackArchive(
            string artifactPath,
            string gamePath)
        {
            if (Directory.Exists(gamePath))
            {
                Directory.Delete(gamePath, true);
            }

            // Unpack to parent
            var parentDirectory = Path.GetDirectoryName(gamePath);
            ZipFile.ExtractToDirectory(artifactPath, parentDirectory);
            // Move everything up one level
            Directory.Move(Path.Combine(parentDirectory, "files"), gamePath);
            // Delete the metadata file
            File.Delete(Path.Combine(parentDirectory, METADATA_FILE));
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