using System.IO;
using BeatSaberKeeper.Plugin.SongExplorer.MetaData;

namespace BeatSaberKeeper.Plugin.SongExplorer
{
    public class Level
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public LevelInfo LevelInfo { get; set; }
        public LevelLoadError Error { get; set; }

        public string InfoDatPath => Path.Combine(FullPath, "info.dat");
        public string AudioFilePath => Path.Combine(FullPath, LevelInfo.SongFilename);

        public override string ToString()
        {
            return $"Level \"{Name}\" {(Error != LevelLoadError.None ? $"Error={Error}" : "")}".Trim();
        }
    }
}