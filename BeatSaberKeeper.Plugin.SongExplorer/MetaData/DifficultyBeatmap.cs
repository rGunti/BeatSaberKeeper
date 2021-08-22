using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BeatSaberKeeper.Plugin.SongExplorer.MetaData
{
    public class DifficultyBeatmap
    {
        [JsonPropertyName("_difficulty")]
        public string Difficulty { get; set; }

        [JsonPropertyName("_difficultyRank")]
        public int DifficultyRank { get; set; }

        [JsonPropertyName("_beatmapFilename")]
        public string BeatmapFilename { get; set; }

        [JsonPropertyName("_noteJumpMovementSpeed")]
        public float NoteJumpMovementSpeed { get; set; }

        [JsonPropertyName("_noteJumpStartBeatOffset")]
        public float NoteJumpStartBeatOffset { get; set; }

        [JsonPropertyName("_customData")]
        public Dictionary<string, JsonElement> CustomData { get; set; }
    }
}