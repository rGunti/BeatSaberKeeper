using System;
using System.Reflection;

namespace BeatKeeper.App.Utils
{
    public static class AppInfo
    {
        public static readonly string AppName = "Beat Saber Keeper - Reborn";
        public static readonly Version AppVersion = Assembly.GetExecutingAssembly().GetName().Version;
    }
}