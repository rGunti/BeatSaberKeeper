using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BeatKeeper.Windows.Utils;
using Serilog;
using Serilog.Events;

namespace BeatKeeper.Windows
{
    static class Program
    {
        private const string LOG_FORMAT =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetupLogging();

            if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
            {
                Application.ThreadException += ApplicationOnThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            Log.CloseAndFlush();
        }

        private static void SetupLogging()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .WriteTo.Debug(
                    outputTemplate: LOG_FORMAT,
                    restrictedToMinimumLevel: LogEventLevel.Verbose
                )
#endif
                .WriteTo.RollingFile(
                    pathFormat: $"{Path.Combine(Directory.GetCurrentDirectory(), "beatsaberkeeper-{Date}.log")}",
                    restrictedToMinimumLevel: SettingsUtils.EnableDebugLogging ? LogEventLevel.Verbose : LogEventLevel.Information,
                    outputTemplate: LOG_FORMAT,
                    retainedFileCountLimit: 10
                )
                .CreateLogger();
            Log.Information("Logger initialized");

            if (SettingsUtils.EnableDebugLogging)
            {
                Log.Warning("Debug Logging is enabled! This might cause a slight performance decrease.");
            }
        }

        private static void HandleException(Exception ex)
        {
            Log.Logger?.Fatal(ex, "Fatal crash occurred!");
            Log.CloseAndFlush();

            Application.Exit();
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }
    }
}
