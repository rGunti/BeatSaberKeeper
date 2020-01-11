using System;
using System.Reflection;

namespace BeatKeeper.Windows.Utils
{
    public static class AppInfo
    {
        public static readonly string AppName = "BeatSaberKeeper";
        public static readonly Version AppVersion = Assembly.GetExecutingAssembly().GetName().Version;

    }
}