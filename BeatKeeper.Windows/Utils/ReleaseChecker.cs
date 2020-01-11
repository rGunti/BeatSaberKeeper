using System;
using System.Diagnostics;
using System.Net.Http;
using GitHub.ReleaseDownloader;

namespace BeatKeeper.Windows.Utils
{
    public class ReleaseChecker : IDisposable
    {
        private const string REPO_AUTHOR = "rGunti";
        private const string REPO_NAME = "BeatSaberKeeper";

        private readonly HttpClient _client;
        private readonly ReleaseDownloader _downloader;

        public ReleaseChecker(
            bool includePreRelease,
            string downloadPath = null)
            : this(new HttpClient(), includePreRelease, downloadPath)
        {
        }

        public ReleaseChecker(
            HttpClient client,
            bool includePreRelease,
            string downloadPath)
        {
            _client = client;
            _downloader = new ReleaseDownloader(
                new ReleaseDownloaderSettings(
                    _client,
                    REPO_AUTHOR,
                    REPO_NAME,
                    includePreRelease,
                    ""));
        }

        public string InstalledVersion =>
            $"{AppInfo.AppVersion.Major}.{AppInfo.AppVersion.Minor}.{AppInfo.AppVersion.Build}";

        public bool HasNewVersion()
        {
            try
            {
                return _downloader.IsLatestRelease(InstalledVersion);
            }
            catch (InvalidOperationException ex)
            {
                return false;
            }
        }

        public void OpenReleasePage()
        {
            try
            {
                Process.Start($"https://github.com/{REPO_AUTHOR}/{REPO_NAME}/releases/latest");
            } catch (Exception) { }
        }

        /*
        public void DownloadNewVersion()
        {
            _downloader.DownloadLatestRelease();
        }
        */

        public void Dispose()
        {
            _downloader.DeInit();
            _client?.Dispose();
        }
    }
}