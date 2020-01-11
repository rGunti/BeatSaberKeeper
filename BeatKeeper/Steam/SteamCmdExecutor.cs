using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace BeatKeeper.Steam
{
    public static class SteamCmdExecutor
    {
        private const string DOWNLOAD_URL = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";
        private const string BASE_DIRECTORY = "steamcmd";
        private const string DOWNLOAD_ARTIFACT = @"steamcmd.zip";
        private const string EXECUTABLE = @"steamcmd.exe";

        public static bool IsInitialized => File.Exists(
            Path.Combine(Directory.GetCurrentDirectory(), BASE_DIRECTORY, EXECUTABLE));

        public static void Initialize()
        {
            if (!IsInitialized)
            {
                if (!Directory.Exists(BASE_DIRECTORY))
                {
                    Directory.CreateDirectory(BASE_DIRECTORY);
                }

                using (var client = new WebClient())
                {
                    // Download
                    client.DownloadFile(
                        DOWNLOAD_URL,
                        Path.Combine(Directory.GetCurrentDirectory(), BASE_DIRECTORY, DOWNLOAD_ARTIFACT));
                    // Unpack
                    ZipFile.ExtractToDirectory(
                        Path.Combine(Directory.GetCurrentDirectory(), BASE_DIRECTORY, DOWNLOAD_ARTIFACT),
                        Path.Combine(Directory.GetCurrentDirectory(), BASE_DIRECTORY));
                    // Delete ZIP
                    File.Delete(
                        Path.Combine(Directory.GetCurrentDirectory(), BASE_DIRECTORY, DOWNLOAD_ARTIFACT));
                }
            }
        }

        public static Process RunDownload(
            string username,
            string artifactId)
        {
            return Process.Start(
                Path.Combine(Directory.GetCurrentDirectory(), BASE_DIRECTORY, EXECUTABLE),
                $"+login {username} +download_depot 620980 620981 {artifactId} +quit");
        }
    }
}
