using System.IO;
using BeatKeeper.Kernel.Utils;

namespace BeatKeeper.Windows.Utils
{
    public static class ClientPathUtils
    {
        public static readonly string ArchiveFolder = PathUtils.GetResourcePath("archives");
        public static readonly string VanillaArchiveFolder = PathUtils.GetResourcePath("archives", "vanilla");
        public static readonly string BackupArchiveFolder = PathUtils.GetResourcePath("archives", "backup");

        public static readonly string SteamCmdFolder = PathUtils.GetResourcePath("steamcmd");
        public static readonly string SteamCmdDownloadFolder = Path.Combine(SteamCmdFolder, "steamapps", "content", "app_620980", "depot_620981");

        private static readonly string[] EnsureDirectoriesEnabled = new[]
        {
            ArchiveFolder, VanillaArchiveFolder, BackupArchiveFolder,
            SteamCmdFolder, SteamCmdDownloadFolder
        };

        static ClientPathUtils()
        {
            foreach (var dir in EnsureDirectoriesEnabled)
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }
        }

        public static string BeatSaberExecutable =>
            Path.Combine(SettingsUtils.BeatSaberInstallDirectory, "Beat Saber.exe");
    }
}