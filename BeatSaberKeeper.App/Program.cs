using BeatSaberKeeper.App.Core;
using BeatSaberKeeper.App.Core.Logging;
using BeatSaberKeeper.App.Core.Steam;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BeatSaberKeeper.App.Cmd;
using BeatSaberKeeper.App.Config;
using BeatSaberKeeper.App.Tools;
using CommandLine;

namespace BeatSaberKeeper.App
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(o =>
                {
                    LogInitializer.Init(!o.NoLogFile, BSKConstants.Paths.Logs);
                    BSKConstants.Paths.EnsureDirectoryTreeExists();
                    ConfigManager.Initialize(Path.Combine(BSKConstants.Paths.DefaultWorkingPath, "config.json"));

                    AppDomain.CurrentDomain.UnhandledException += HandleException;

                    Application.SetHighDpiMode(HighDpiMode.SystemAware);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Form mainForm;
                    switch (o.StartupWindow)
                    {
                        case StartupWindowType.Downloader:
                            mainForm = new DownloadGameArchiveForm();
                            break;
                        case StartupWindowType.SongExplorer:
                            mainForm = new SongExplorer();
                            break;
                        case StartupWindowType.Default:
                        default:
                            mainForm = new MainForm();
                            break;
                    }

                    mainForm.StartPosition = FormStartPosition.CenterScreen;
                    
                    Application.Run(mainForm);

                    SteamSession.Instance?.Dispose();
                    ConfigManager.Instance.WriteConfig();
                })
                .WithNotParsed(errors =>
                {
                    Console.Error.WriteLine(string.Join(", ", errors.Select(e => e.Tag)));
                });
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Log.ForContext(typeof(Program)).Fatal(ex, "An unhandled exception has occurred.");
        }
    }
}
