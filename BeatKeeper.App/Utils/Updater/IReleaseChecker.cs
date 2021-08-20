using System.Threading.Tasks;

namespace BeatKeeper.App.Utils.Updater
{
    public interface IReleaseChecker
    {
        string InstalledVersion { get; }
        string CheckForNewVersion(bool includePrerelease);
        Task<string> CheckForNewVersionAsync(bool includePrerelease);

        bool HasNewVersion(bool includePrerelease);
        bool HasNewVersion(string latestVersion);
        Task<bool> HasNewVersionAsync(bool includePrerelease);

        void DownloadLatestVersion(bool includePrerelease);
    }
}