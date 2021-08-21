using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BeatSaberKeeper.Plugin.SongExplorer.MetaData
{
    public class DifficultyBeatmapSet
    {
        [JsonPropertyName("_beatmapCharacteristicName")]
        public string BeatmapCharacteristicName { get; set; }

        [JsonPropertyName("_difficultyBeatmaps")]
        public List<DifficultyBeatmap> DifficultyBeatmaps { get; set; }
    }
}