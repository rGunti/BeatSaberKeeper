using BeatSaberKeeper.App.Core;
using BeatSaberKeeper.App.Core.Logging;
using BeatSaberKeeper.App.Core.Steam;
using Serilog;
using System;
using System.IO;
using System.Windows.Forms;
using BeatSaberKeeper.App.Config;

namespace BeatSaberKeeper.App
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogInitializer.Init(enableFile: true);
            BSKConstants.Paths.EnsureDirectoryTreeExists();
            ConfigManager.Initialize(Path.Combine(BSKConstants.Paths.DefaultWorkingPath, "config.json"));

            AppDomain.CurrentDomain.UnhandledException += HandleException;

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm
            {
                StartPosition = FormStartPosition.CenterScreen
            });

            SteamSession.Instance?.Dispose();
            ConfigManager.Instance.WriteConfig();
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Log.ForContext(typeof(Program)).Fatal(ex, "An unhandled exception has occurred.");
        }
    }
}
