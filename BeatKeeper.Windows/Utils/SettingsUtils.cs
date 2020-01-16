using BeatKeeper.Windows.Properties;

namespace BeatKeeper.Windows.Utils
{
    public static class SettingsUtils
    {
        public static string LastSteamUsername
        {
            get => Settings.Default.LastSteamUsername;
            set
            {
                Settings.Default.LastSteamUsername = value;
                Settings.Default.Save();
            }
        }

        public static string BeatSaberInstallDirectory
        {
            get => Settings.Default.BeatSaberInstallDirectory;
            set
            {
                Settings.Default.BeatSaberInstallDirectory = value;
                Settings.Default.Save();
            }
        }

        public static bool EnableDebugLogging
        {
            get => Settings.Default.EnableDebugLogging;
            set
            {
                Settings.Default.EnableDebugLogging = value;
                Settings.Default.Save();
            }
        }
    }
}