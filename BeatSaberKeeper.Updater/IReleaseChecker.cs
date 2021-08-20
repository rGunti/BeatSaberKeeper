using System.Threading.Tasks;
using BeatSaberKeeper.Updater;

namespace BeatKeeper.App.Utils.Updater
{
    public interface IReleaseChecker
    {
        BskVersion GetLatestVersion(bool includePrerelease = false);
        Task<BskVersion> GetLatestVersionAsync(bool includePrerelease = false);

        Task<string> GetDownloadUrlForVersionAsync(BskVersion version);
        Task<string> GetDownloadUrlForVersionAsync(string version);
    }
}