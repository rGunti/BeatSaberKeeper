using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHub.ReleaseDownloader;
using Octokit;
using Serilog;

namespace BeatKeeper.Windows.Utils
{
    public interface IReleaseChecker
    {
        string InstalledVersion { get; }
        string CheckForNewVersion();
        Task<string> CheckForNewVersionAsync();

        bool HasNewVersion();
        bool HasNewVersion(string latestVersion);
        Task<bool> HasNewVersionAsync();

        void DownloadLatestVersion();
    }

    public class BskReleaseChecker : IReleaseChecker
    {
        private const string REPO_AUTHOR = "rGunti";
        private const string REPO_NAME = "BeatSaberKeeper";

        private const string ARTIFACT_FILE = "update.zip";

        private readonly GitHubClient _gitHubClient;
        private Release _latestRelease;

        public BskReleaseChecker()
        {
            _gitHubClient = new GitHubClient(new ProductHeaderValue("BeatSaberKeeper", InstalledVersion));
        }

        public string InstalledVersion =>
            $"{AppInfo.AppVersion.Major}.{AppInfo.AppVersion.Minor}.{AppInfo.AppVersion.Build}";

        private async Task<Release> GetLatestRelease()
        {
            if (_latestRelease == null)
            {
                _latestRelease =
                    (await _gitHubClient.Repository.Release.GetAll(REPO_AUTHOR, REPO_NAME)).FirstOrDefault();
            }
            return _latestRelease;
        }

        public string CheckForNewVersion()
        {
            return CheckForNewVersionAsync().GetAwaiter().GetResult();
        }

        public async Task<string> CheckForNewVersionAsync()
        {
            var latestRelease = await GetLatestRelease();
            return latestRelease.TagName;
        }

        public bool HasNewVersion()
        {
            return HasNewVersionAsync().GetAwaiter().GetResult();
        }

        public bool HasNewVersion(string latestVersion)
        {
            return latestVersion != InstalledVersion;
        }

        public async Task<bool> HasNewVersionAsync()
        {
            return HasNewVersion(await CheckForNewVersionAsync());
        }

        public void DownloadLatestVersion()
        {
            var release = GetLatestRelease().GetAwaiter().GetResult();
            try
            {
                Process.Start(release.Assets.First().BrowserDownloadUrl);
            }
            catch (Exception) { }
        }
    }

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
            Log.Verbose($"Constructing {GetType().Name} ...");
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
                Log.Debug("Checking for new version ...");
                return _downloader.IsLatestRelease(InstalledVersion);
            }
            catch (InvalidOperationException ex)
            {
                Log.Information(ex, "Failed to check for updates due to InvalidOperationException:");
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
            Log.Verbose($"Disposing {GetType().Name} ...");
            _downloader.DeInit();
            _client?.Dispose();
        }
    }
}