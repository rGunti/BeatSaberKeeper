using System.Reflection;
using BeatSaberKeeper.Updater;

namespace BeatSaberKeeper.App.Utils
{
    public static class AppInfo
    {
        private static readonly string AppVersionString = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
            .InformationalVersion;

        /// <summary>
        /// Returns the name of the app
        /// </summary>
        public static readonly string AppName = "Beat Saber Keeper - Reborn";
        
        /// <summary>
        /// Returns the currently running version of the app
        /// </summary>
        public static readonly BskVersion AppVersion = BskVersion.Parse(AppVersionString);
    }
}