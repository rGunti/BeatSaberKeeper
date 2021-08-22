using System.IO;
using Serilog;

namespace BeatSaberKeeper.App.Core.Logging
{
    public static class LogInitializer
    {
        private const string LOG_FORMAT =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | {SourceContext} | [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        private const string LOG_FILE = "bsk-.log";

        public static void Init(bool enableFile = true, string logPath = null)
        {
            var config = new LoggerConfiguration()
                .Enrich.FromLogContext();
#if DEBUG
            config = config
                .WriteTo.Debug(
                    outputTemplate: LOG_FORMAT,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose
                )
                .MinimumLevel.Verbose();
#endif
            if (enableFile)
            {
                // TODO: Debug?
                config = config
                    .WriteTo.Async(a =>
                    {
                        a.File(
                            string.IsNullOrWhiteSpace(logPath) ? LOG_FILE : Path.Combine(logPath, LOG_FILE),
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 5,
                            outputTemplate: LOG_FORMAT
                        )
                        .MinimumLevel.Information();
                    });
            }

            Log.Logger = config.CreateLogger();
            Log.ForContext(typeof(LogInitializer)).Debug("Logger initialized");
        }
    }
}
