﻿using BeatKeeper.App.Utils;

namespace BeatKeeper.App.Config
{
    public class BSKAppConfig
    {
        public string AppVersion { get; set; } = AppInfo.AppVersion.ToString();
        public string GamePath { get; set; }
        public bool PrereleaseOptIn { get; set; }
        public bool CheckForUpdatesOnStartup { get; set; } = true;
    }
}
