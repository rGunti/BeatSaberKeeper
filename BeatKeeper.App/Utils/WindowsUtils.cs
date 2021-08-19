using System;
using System.Diagnostics;
using System.IO;

namespace BeatKeeper.App.Utils
{
    public static class WindowsUtils
    {
        public static void ShowFileInExplorer(string filePath)
        {
            System.Diagnostics.Process.Start(
                "explorer.exe",
                $"/select,\"{Path.GetFullPath(filePath)}\"");
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