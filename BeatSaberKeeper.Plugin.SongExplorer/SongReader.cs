using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BeatSaberKeeper.Plugin.SongExplorer.MetaData;
using Serilog;

namespace BeatSaberKeeper.Plugin.SongExplorer
{
    public class SongReader
    {
        private static readonly string InternalCustomLevelPath = Path.Combine("Beat Saber_Data", "CustomLevels");
        private static readonly ILogger Logger = Log.ForContext<SongReader>();
        
        private readonly string _gameDirectory;

        public SongReader(string gameDirectory)
        {
            _gameDirectory = gameDirectory;
        }

        public string CustomLevelPath => Path.GetFullPath(Path.Combine(_gameDirectory, InternalCustomLevelPath));

        public IEnumerable<string> GetLevelNames()
        {
            return Directory.GetDirectories(CustomLevelPath)
                .Select(Path.GetFileName);
        }
        
        public Level ReadLevel(string name)
        {
            string dir = Path.Combine(CustomLevelPath, name);
            var level = new Level
            {
                Name = name,
                FullPath = dir
            };
            if (!Directory.Exists(dir))
            {
                Logger.Warning("Level {LevelName} doesn't exist", name);
                level.Error = LevelLoadError.FileMissing;
                return level;
            }

            if (!File.Exists(level.InfoDatPath))
            {
                Logger.Warning("Directory {LevelName} is not a level", name);
                level.Error = LevelLoadError.NotALevel;
                return level;
            }

            try
            {
                string metaInfoJson = File.ReadAllText(level.InfoDatPath);
                level.LevelInfo = JsonSerializer.Deserialize<LevelInfo>(metaInfoJson);
            }
            catch (JsonException ex)
            {
                Logger.Error(ex, "Failed to read level meta data for {LevelName}", name);
                level.Error = LevelLoadError.FailedToParse;
            }

            return level;
        }

        public void DeleteLevel(Level level)
        {
            Directory.Delete(level.FullPath, true);
        }
    }
}