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
    }
}