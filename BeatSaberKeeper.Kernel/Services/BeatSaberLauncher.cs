using System.Diagnostics;
using System.IO;

namespace BeatSaberKeeper.Kernel.Services
{
    public static class BeatSaberLauncher
    {
        private const string GAME_EXECUTABLE = "Beat Saber.exe";
        private const string IPA_EXECUTABLE = "IPA.exe";

        private static Process ConstructProcess(
            string exePath,
            string args = null)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo(exePath, args)
                {
                    WorkingDirectory = Path.GetDirectoryName(exePath)
                }
            };
            return process;
        }

        public static void Launch(string gamePath, bool forceSkipIpa = false)
        {
            var ipaPath = Path.Combine(gamePath, IPA_EXECUTABLE);
            if (File.Exists(ipaPath) && !forceSkipIpa)
            {
                try
                {
                    var p = ConstructProcess(ipaPath, "-n -l");
                    p.Start();
                    p.WaitForExit();
                }
                catch (IOException) { }
            }
            else
            {
                try
                {
                    var process = ConstructProcess(Path.Combine(gamePath, GAME_EXECUTABLE));
                    process.Start();
                }
                catch (IOException) { }
            }
        }
    }
}