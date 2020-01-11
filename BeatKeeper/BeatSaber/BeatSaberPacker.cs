using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace BeatKeeper.BeatSaber
{
    public class BeatSaberPacker
    {
        private const string ARCHIVE_FOLDER = "archive";

        private AppVersion _appVersion;

        public BeatSaberPacker(AppVersion appVersion)
        {
            _appVersion = appVersion;
        }

        private static string BaseArchivePath
            => Path.Combine(
                Directory.GetCurrentDirectory(),
                ARCHIVE_FOLDER);

        public static string GetArchiveFileName(AppVersion appVersion)
            => GetArchiveFileName(appVersion.Version);

        public static string GetArchiveFileName(string appVersion)
            => Path.Combine(
                BaseArchivePath,
                $"BeatSaber_{appVersion}.zip");

        public static string SourcePath
            => Path.Combine(
                Directory.GetCurrentDirectory(),
                "steamcmd", "steamapps", "content", "app_620980", "depot_620981");

        public static void Cleanup()
        {
            if (Directory.Exists(SourcePath))
            {
                Directory.Delete(SourcePath, true);
            }
        }

        public static string[] GetPackagedVersions()
        {
            if (!Directory.Exists(BaseArchivePath)) return new string[0];
            return Directory.GetFiles(BaseArchivePath, "BeatSaber_*.zip")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Select(f => f.Split('_')[1])
                .ToArray();
        }

        public static void RunVersion(
            string version,
            string installPath)
        {
            string archivePath = GetArchiveFileName(version);

            // Clean up install directory
            if (Directory.Exists(installPath))
            {
                Directory.Delete(installPath, true);
            }
            Directory.CreateDirectory(installPath);

            // Unpack Version
            ZipFile.ExtractToDirectory(archivePath, installPath);

            // Run EXE
            Process.Start(Path.Combine(installPath, "Beat Saber.exe"));
        }

        public void Run()
        {
            if (!Directory.Exists(BaseArchivePath))
            {
                Directory.CreateDirectory(BaseArchivePath);
            }

            var files = CreateFileIndex(SourcePath);
            using (var zip = ZipFile.Open(GetArchiveFileName(_appVersion), ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    var zipEntry = file.Replace(SourcePath, "")
                        .Replace('\\', '/')
                        .Substring(1);
                    zip.CreateEntryFromFile(file, zipEntry);
                }
            }
        }

        private IEnumerable<string> CreateFileIndex(string path)
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
}
