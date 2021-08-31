using System.IO.Abstractions;
using BeatSaberKeeper.App.Config;
using PandaDotNet.Utils;

namespace BeatSaberKeeper.App.Utils
{
    public static class ConfigExtensions
    {
        public static bool IsGamePathSet(this ConfigManager configManager)
            => configManager.Config.IsGamePathSet();

        public static bool IsGamePathSet(this BSKAppConfig config)
            => config != null
               && !string.IsNullOrWhiteSpace(config.GamePath);

        public static bool IsGamePathValid(this ConfigManager configManager, IFileSystem fs = null)
            => configManager.Config.IsGamePathValid(fs);
        
        public static bool IsGamePathValid(
            this BSKAppConfig config,
            IFileSystem fs = null)
        {
            fs = fs.Or(() => new FileSystem());
            return config.IsGamePathSet() && fs.Directory.Exists(config.GamePath);
        }
    }
}