using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

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
}