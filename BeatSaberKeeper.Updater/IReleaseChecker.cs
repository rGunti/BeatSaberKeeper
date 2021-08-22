using System.Threading.Tasks;

namespace BeatSaberKeeper.Updater
{
    public interface IReleaseChecker
    {
        BskVersion GetLatestVersion(bool includePrerelease = false);
        Task<BskVersion> GetLatestVersionAsync(bool includePrerelease = false);

        Task<string> GetDownloadUrlForVersionAsync(BskVersion version);
        Task<string> GetDownloadUrlForVersionAsync(string version);
    }
}