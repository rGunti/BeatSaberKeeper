using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace BeatKeeper.App.Utils.Updater
{/*
    public class BskReleaseChecker : IReleaseChecker
    {
        private const string REPO_AUTHOR = "rGunti";
        private const string REPO_NAME = "BeatSaberKeeper";

        private readonly GitHubClient _gitHubClient;
        private Release _latestRelease;

        public BskReleaseChecker()
        {
            _gitHubClient = new GitHubClient(new ProductHeaderValue("BeatSaberKeeper", InstalledVersion));
        }

        public string InstalledVersion => throw new NotImplementedException();

        private async Task<Release> GetLatestRelease(bool includePrereleases)
        {
            if (_latestRelease == null)
            {
                IEnumerable<Release> releases = await _gitHubClient.Repository.Release.GetAll(REPO_AUTHOR, REPO_NAME);
                releases = releases.Where(r => !r.Draft);
                if (!includePrereleases)
                {
                    releases = releases.Where(r => !r.Prerelease);
                }
                _latestRelease = releases.FirstOrDefault();
            }
            return _latestRelease;
        }

        public string CheckForNewVersion(bool includePrerelease)
        {
            return CheckForNewVersionAsync(includePrerelease).GetAwaiter().GetResult();
        }

        public async Task<string> CheckForNewVersionAsync(bool includePrerelease)
        {
            var latestRelease = await GetLatestRelease(includePrerelease);
            return latestRelease?.TagName;
        }

        public bool HasNewVersion(bool includePrerelease)
        {
            return HasNewVersionAsync(includePrerelease).GetAwaiter().GetResult();
        }

        public bool HasNewVersion(string latestVersion)
        {
            return latestVersion != null && latestVersion != InstalledVersion;
        }

        public async Task<bool> HasNewVersionAsync(bool includePrerelease)
        {
            return HasNewVersion(await CheckForNewVersionAsync(includePrerelease));
        }

        public void DownloadLatestVersion(bool includePrerelease)
        {
            var release = GetLatestRelease(includePrerelease).GetAwaiter().GetResult();
            var downloadUrl = release.Assets.FirstOrDefault()?.BrowserDownloadUrl;
            if (!string.IsNullOrWhiteSpace(downloadUrl))
            {
                WindowsUtils.OpenUrl(downloadUrl);
            }
        }
    }
*/}