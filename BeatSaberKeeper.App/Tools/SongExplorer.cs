using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BeatSaberKeeper.App.Config;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Plugin.SongExplorer;

namespace BeatSaberKeeper.App.Tools
{
    public partial class SongExplorer : Form
    {
        private readonly ConfigManager _configManager = ConfigManager.Instance;
        private readonly SongReader _songReader;

        public SongExplorer()
        {
            InitializeComponent();
            _songReader = new SongReader(_configManager.Config.GamePath);
        }

        private void SetStatus(string statusString, int percentage = -1)
        {
            this.RunInUiThread(() =>
            {
                SEStatusLabel.Text = statusString;
            });
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SongExplorer_Load(object sender, EventArgs e)
        {
            LoadSongList();
        }

        private void LoadSongList()
        {
            SetStatus("Loading Songs ...");
            var levels = new List<Level>();
            this.RunInBackgroundThread(() =>
            {
                levels = _songReader.GetLevelNames()
                    .Select(_songReader.ReadLevel)
                    .ToList();
            }, () =>
            {
                SetStatus($"Found {levels.Count} song(s)");
                RenderSongList(levels);
            });
        }

        private void RenderSongList(IEnumerable<Level> levels)
        {
            SEList.BeginUpdate();
            SEList.Items.Clear();

            foreach (Level level in levels.Where(l => l.Error == LevelLoadError.None).OrderBy(l => l.LevelInfo.SongName))
            {
                ListViewItem lvi = SEList.Items.Add(level.LevelInfo.SongName);
                lvi.Tag = level;
                lvi.SubItems.AddRange(new []
                {
                    level.LevelInfo.SongAuthorName,
                    level.LevelInfo.LevelAuthorName,
                    $"{Math.Round(level.LevelInfo.BeatsPerMinute)}",
                    $"{level.LevelInfo.DifficultyBeatmapSets.Count}"
                });
                lvi.ToolTipText = $"Level \"{level.Name}\"";
            }

            SEList.Enabled = true;
            SEList.EndUpdate();
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            LoadSongList();
        }
    }
}