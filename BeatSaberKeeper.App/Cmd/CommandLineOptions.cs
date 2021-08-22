using CommandLine;

namespace BeatSaberKeeper.App.Cmd
{
    public class CommandLineOptions
    {
        [Option('w', "startup-window", Required = false,
            HelpText = "Determines which window will be opened on startup")]
        public StartupWindowType StartupWindow { get; set; }

        [Option("nolog", Required = false, HelpText = "Disables the log file")]
        public bool NoLogFile { get; set; }
        
        [Option('d', "data-dir", Required = false,
            HelpText = "If defined, changes the directory where data is stored")]
        public string DataDirectory { get; set; }
    }

    public enum StartupWindowType
    {
        Default,
        Downloader,
        SongExplorer
    }
}