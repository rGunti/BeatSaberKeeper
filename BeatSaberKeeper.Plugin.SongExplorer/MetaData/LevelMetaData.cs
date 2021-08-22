using System.Text.Json.Serialization;

namespace BeatSaberKeeper.Plugin.SongExplorer.MetaData
{
    public class LevelMetaData
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("scannedTime")]
        public long ScannedTime { get; set; }
    }
}