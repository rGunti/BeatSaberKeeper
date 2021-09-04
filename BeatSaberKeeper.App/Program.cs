using BeatSaberKeeper.App.Core;
using BeatSaberKeeper.App.Core.Logging;
using BeatSaberKeeper.App.Core.Steam;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows.Forms;
using BeatSaberKeeper.App.Cmd;
using BeatSaberKeeper.App.Config;
using BeatSaberKeeper.App.Tools;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Kernel.Abstraction;
using BeatSaberKeeper.Kernel.V1;
using CommandLine;
using CommandLine.Text;

namespace BeatSaberKeeper.App
{
    static class Program
    {
        private static ParserResult<CommandLineOptions> commandLineResult;

        public static string CommandLineHelpText => HelpText.AutoBuild(commandLineResult).ToString();
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            commandLineResult = Parser.Default.ParseArguments<CommandLineOptions>(args);
            return commandLineResult
                .MapResult(
                    DefaultMain,
                    HandleErrors);
        }

        private static int HandleErrors(IEnumerable<Error> errs)
        {
            int result = -2;
            List<Error> errors = errs.ToList();
            var helpText = HelpText.AutoBuild(commandLineResult);
            if (errors.Any(x => x is HelpRequestedError or VersionRequestedError))
            {
                if (errors.Any(x => x is HelpRequestedError))
                {
                    Application.Run(new MonoSpaceTextOutputForm(helpText.ToString()));
                } else if (errors.Any(x => x is VersionRequestedError))
                {
                    MessageBoxUtils.Show($"This is {AppInfo.AppName} {AppInfo.AppVersion}");
                }
                return 0;
            }
            else
            {
                Application.Run(new MonoSpaceTextOutputForm(helpText.ToString()));
            }
            Debug.WriteLine("Exit code {0}", result);
            return result;
        }

        private static int DefaultMain(CommandLineOptions options)
        {
            LogInitializer.Init(!options.NoLogFile, BSKConstants.Paths.Logs, options.EnableDebugLogging);

            if (!string.IsNullOrWhiteSpace(options.DataDirectory))
            {
                BSKConstants.Paths.SetBaseDirectory(options.DataDirectory);
            }
            
            BSKConstants.Paths.EnsureDirectoryTreeExists();
            ConfigManager.Initialize(Path.Combine(BSKConstants.Paths.DefaultWorkingPath, "config.json"));

            AppDomain.CurrentDomain.UnhandledException += HandleException;
            
            // Construct some dependencies
            ICompressionInterface compressionInterface = new WrappedCompressionInterface(
                new Dictionary<string, ICompressionInterface>
                {
                    { "v1", new V1CompressionInterface(new FileSystem()) }
                });

            Form mainForm;
            switch (options.StartupWindow)
            {
                case StartupWindowType.Downloader:
                    mainForm = new DownloadGameArchiveForm(compressionInterface);
                    break;
                case StartupWindowType.SongExplorer:
                    mainForm = new SongExplorer();
                    break;
                case StartupWindowType.Default:
                default:
                    mainForm = new MainForm(compressionInterface);
                    break;
            }

            mainForm.StartPosition = FormStartPosition.CenterScreen;

            Application.Run(mainForm);

            SteamSession.Instance?.Dispose();
            ConfigManager.Instance.WriteConfig();

            return 0;
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Log.ForContext(typeof(Program)).Fatal(ex, "An unhandled exception has occurred.");
        }
    }
}
