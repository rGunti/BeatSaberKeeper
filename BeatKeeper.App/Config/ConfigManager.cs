using BeatKeeper.App.Core.Utils;
using Serilog;
using System.IO;
using System.Text.Json;

namespace BeatKeeper.App.Config
{
    public class ConfigManager
    {
        private static readonly ILogger log = Log.ForContext<ConfigManager>();
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            AllowTrailingCommas = true,
            WriteIndented = true
        };

        private readonly string _filepath;

        public static ConfigManager Instance { get; private set; }
        public static void Initialize(string filepath)
        {
            Path.GetDirectoryName(filepath).EnsureDirectory();
            Instance = new ConfigManager(filepath);
        }

        protected ConfigManager(string filepath)
        {
            _filepath = filepath;
            LoadConfig();
        }

        public void LoadConfig()
        {
            if (!File.Exists(_filepath))
            {
                // Config doesn't exist, create a new one
                log.Information("Config file doesn't yet exist at {FilePath}, loading defaults",
                    _filepath);
                Config = new BSKAppConfig();
                WriteConfig();
                LoadConfig();
            } else
            {
                // Read config from file
                log.Debug("Reading config from file {FilePath}", _filepath);
                var json = File.ReadAllText(_filepath);
                Config = JsonSerializer.Deserialize<BSKAppConfig>(json, jsonSerializerOptions);
            }
        }
        public void WriteConfig()
        {
            log.Debug("Writing config to file {FilePath} ...", _filepath);
            var json = JsonSerializer.Serialize(Config, jsonSerializerOptions);
            File.WriteAllText(_filepath, json);
        }

        public bool IsConfigLoaded => Config != null;
        public BSKAppConfig Config { get; private set; }
    }
}
