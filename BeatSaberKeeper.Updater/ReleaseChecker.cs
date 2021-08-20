using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace BeatSaberKeeper.Updater
{
    public class BskReleaseChecker : IReleaseChecker
    {
        private const string REPO_AUTHOR = "rGunti";
        private const string REPO_NAME = "BeatSaberKeeper";

        private readonly GitHubClient _gitHubClient;

        public BskReleaseChecker()
        {
            _gitHubClient = new GitHubClient(
                new ProductHeaderValue(REPO_NAME));
        }

        private async Task<IEnumerable<Release>> GetReleases(bool includePrerelease)
        {
            IEnumerable<Release> releases = (await _gitHubClient.Repository.Release.GetAll(REPO_AUTHOR, REPO_NAME))
                .Where(r => !r.Draft);
            if (!includePrerelease)
            {
                releases = releases.Where(r => !r.Prerelease);
            }
            return releases;
        }

        public BskVersion GetLatestVersion(bool includePrerelease = false)
        {
            return GetLatestVersionAsync(includePrerelease).GetAwaiter().GetResult();
        }

        public async Task<BskVersion> GetLatestVersionAsync(bool includePrerelease = false)
        {
            Release latestRelease = (await GetReleases(includePrerelease))
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefault();
            if (latestRelease == null)
            {
                return null;
            }

            return BskVersion.TryParse(latestRelease.TagName, out BskVersion bskVersion) ?
                    bskVersion :
                    null;
        }

        public Task<string> GetDownloadUrlForVersionAsync(BskVersion version)
            => GetDownloadUrlForVersionAsync(version.ToString());

        public async Task<string> GetDownloadUrlForVersionAsync(string version)
        {
            Release release = await _gitHubClient.Repository.Release.Get(
                REPO_AUTHOR, REPO_NAME, version);
            return release?.AssetsUrl;
        }
    }
}