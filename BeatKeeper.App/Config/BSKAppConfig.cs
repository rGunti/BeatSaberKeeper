using BeatKeeper.App.Utils;

namespace BeatKeeper.App.Config
{
    public class BSKAppConfig
    {
        public string AppVersion { get; set; } = AppInfo.AppVersion;
        public string GamePath { get; set; }
    }
}
