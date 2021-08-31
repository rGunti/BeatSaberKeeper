using System;
using System.Diagnostics;
using System.IO;

namespace BeatSaberKeeper.App.Utils
{
    public static class WindowsUtils
    {
        public static void ShowFileInExplorer(string filePath)
        {
            Process.Start(
                "explorer.exe",
                $"/select,\"{Path.GetFullPath(filePath)}\"");
        }

        public static void ShowFolderInExplorer(string filePath)
        {
            Process.Start(
                "explorer.exe",
                $"\"{Path.GetFullPath(filePath)}\"");
        }

        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            } catch (Exception) { /* ignore */ }
        }
    }
}