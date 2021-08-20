using System.Threading.Tasks;
using BeatSaberKeeper.Updater;

namespace BeatKeeper.App.Utils.Updater
{
    public interface IReleaseChecker
    {
        BskVersion GetLatestVersion(bool includePrerelease = false);
        Task<BskVersion> GetLatestVersionAsync(bool includePrerelease = false);

        string GetDownloadUrlForVersion(BskVersion version);
        Task<string> GetDownloadUrlForVersion(string version);
    }
}