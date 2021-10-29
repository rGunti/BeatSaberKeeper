using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using BeatSaberKeeper.App.Config;
using BeatSaberKeeper.App.Properties;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Plugin.SongExplorer;
using BeatSaberKeeper.Plugin.SongExplorer.MetaData;
using NAudio.Utils;
using NAudio.Vorbis;
using NAudio.Wave;
using Serilog;

namespace BeatSaberKeeper.App.Tools
{
    public partial class SongExplorer : Form
    {
        private static readonly ILogger Logger = Log.ForContext<SongExplorer>();
        
        private readonly ConfigManager _configManager = ConfigManager.Instance;
        private readonly SongReader _songReader;
        
        private readonly WaveOut _waveOut = new();
        private IWaveProvider _currentlyPlayingFile = null;
        private Level _currentlyPlayingSong = null;

        public SongExplorer()
        {
            InitializeComponent();
            _songReader = new SongReader(_configManager.Config.GamePath);
            UpdateCurrentlyPlayingStatus();
        }
        
        public bool IsReadOnly { get; set; }

        private void SetStatus(string statusString)
        {
            this.RunInUiThread(() =>
            {
                SEStatusLabel.Text = statusString;
            });
        }

        private void UpdateCurrentlyPlayingStatus()
        {
            SEPlayerPauseButton.Image = _waveOut.PlaybackState == PlaybackState.Playing ? Resources.pause : Resources.play;
            if (_currentlyPlayingSong == null)
            {
                SEPlayerTimeLabel.Text = @$"{TimeSpan.FromSeconds(0):h':'mm':'ss}";
                SECurrentSongLabel.Text = "< Ready! Select a song to play. >";
            } else
            {
                LevelInfo levelInfo = _currentlyPlayingSong.LevelInfo;
                SetStatus($"Playing \"{levelInfo.SongName}\" by {levelInfo.SongAuthorName}");
                SEPlayerTimeLabel.Text = @$"{_waveOut.GetPositionTimeSpan():h':'mm':'ss}";
                SECurrentSongLabel.Text = $"\"{levelInfo.SongName}\" by {levelInfo.SongAuthorName}";
            }
            SEPlayerVolumeDisplay.Text = @$"Volume: {(_waveOut.Volume * 100):0} %";
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SongExplorer_Load(object sender, EventArgs e)
        {
            UpdateContextMenuControlState(GetSelectedLevel());
            LoadSongList();
        }

        private void LoadSongList()
        {
            if (!_configManager.IsGamePathValid())
            {
                SetStatus("No or invalid game path set");
                MessageBoxUtils.Error("You have not specified where Beat Saber is installed on your machine. " +
                                      "As a result, BeatSaberKeeper cannot list your installed songs. " +
                                      "Please close the Song Explorer and set the game path in the Settings menu.");
                Close();
                return;
            }

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
                lvi.ImageKey = "audio";
                lvi.SubItems.AddRange(new []
                {
                    level.LevelInfo.SongAuthorName,
                    level.LevelInfo.LevelAuthorName,
                    $"{Math.Round(level.LevelInfo.BeatsPerMinute)}",
                    BuildBeatMapTypesString(level.LevelInfo),
                    BuildDifficultyString(level.LevelInfo),
                });
                lvi.ToolTipText = $"Level \"{level.Name}\"";
            }

            SEList.Enabled = true;
            SEList.EndUpdate();
        }

        private string BuildDifficultyString(LevelInfo levelInfo)
        {
            List<DifficultyBeatmap> beatMaps = levelInfo.DifficultyBeatmapSets
                .Where(i => (i.DifficultyBeatmaps?.Count ?? 0) > 0)
                .SelectMany(i => i.DifficultyBeatmaps)
                .ToList();

            return string.Join(", ", beatMaps.Select(b => b.Difficulty).Distinct());
        }

        private string BuildBeatMapTypesString(LevelInfo levelInfo)
        {
            return string.Join(", ", levelInfo.DifficultyBeatmapSets.Select(i => i.BeatmapCharacteristicName));
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            LoadSongList();
        }

        private Level GetSelectedLevel(ListView listView)
        {
            if (listView == null)
            {
                return null;
            }

            ListView.SelectedListViewItemCollection selectedItems = listView.SelectedItems;
            if (selectedItems.Count != 1)
            {
                return null;
            }

            ListViewItem selectedListViewItem = selectedItems[0];
            return selectedListViewItem.Tag as Level;
        }

        private Level GetSelectedLevel()
            => GetSelectedLevel(SEList);
        
        private void DoLevelContextAction(Func<Level> extractor, Action<Level> action)
        {
            Level artifact = extractor();
            if (artifact != null)
            {
                action(artifact);
            }
        }
        private void DoLevelContextAction(Action<Level> action)
            => DoLevelContextAction(GetSelectedLevel, action);
        private void DoLevelContextAction(ToolStripMenuItem sourceItem, Action<Level> action)
            => DoLevelContextAction(() => sourceItem?.Tag as Level, action);

        private void UpdateContextMenuControlState(Level level)
        {
            foreach (ToolStripItem item in SEContextMenu.Items)
            {
                item.Enabled = level != null;
                item.Tag = level;
            }

            DeleteMapContextMenuItem.Enabled = level != null && !IsReadOnly;

            PlayMenuItem.Enabled = level != null;
            DeleteMenuItem.Enabled = level != null && !IsReadOnly;
        }

        private void SEContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var contextMenuStrip = (ContextMenuStrip) sender;
            Level level = GetSelectedLevel(contextMenuStrip.SourceControl as ListView);
            UpdateContextMenuControlState(level);
        }

        private void DisposePlayingSong()
        {
            if (_currentlyPlayingFile is IDisposable disposable)
            {
                Logger.Debug("Disposing currently playing file {File}", _currentlyPlayingFile);
                disposable.Dispose();
            }
        }

        private void PlaySong(Level level)
        {
            if (level == null)
            {
                return;
            }

            if (_waveOut.PlaybackState != PlaybackState.Stopped)
            {
                _waveOut.Stop();
            }

            DisposePlayingSong();

            _currentlyPlayingSong = level;
            _currentlyPlayingFile = new VorbisWaveReader(level.AudioFilePath);
            _waveOut.Init(_currentlyPlayingFile);
            _waveOut.Play();
        }

        private void DeleteLevel(Level level)
        {
            if (level == null) return;
            if (!MessageBoxUtils.Ask(
                $"Do you really want to delete \"{level.Name}\"?"))
            {
                return;
            }

            if (_currentlyPlayingSong == level)
            {
                StopSong();
            }
            
            if (!_songReader.DeleteLevel(level))
            {
                MessageBoxUtils.Error("Failed to delete your level. " +
                                      "More information is available in the log files.");
            }
            LoadSongList();
        }

        private void StopSong()
        {
            _waveOut.Stop();
            DisposePlayingSong();
            _currentlyPlayingFile = null;
            _currentlyPlayingSong = null;
            UpdateCurrentlyPlayingStatus();
        }

        private async void PlaySongContextMenuItem_Click(object sender, EventArgs e)
            => DoLevelContextAction(sender as ToolStripMenuItem, PlaySong);

        private void SEStatusUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_waveOut.PlaybackState != PlaybackState.Stopped)
            {
                try
                {
                    UpdateCurrentlyPlayingStatus();
                } catch (NullReferenceException) { /* ignore */ }
            }
        }

        private void SEPlayerStopButton_Click(object sender, EventArgs e)
        {
            StopSong();
        }

        private void SEPlayerPauseButton_Click(object sender, EventArgs e)
        {
            switch (_waveOut.PlaybackState)
            {
                case PlaybackState.Playing:
                    _waveOut.Pause();
                    break;
                case PlaybackState.Paused:
                    _waveOut.Play();
                    break;
            }
        }

        private void SEPlayerVolumeDownButton_Click(object sender, EventArgs e)
        {
            _waveOut.Volume = Math.Max(0, _waveOut.Volume - 0.05f);
            UpdateCurrentlyPlayingStatus();
        }

        private void SEPlayerVolumeUpButton_Click(object sender, EventArgs e)
        {
            _waveOut.Volume = Math.Min(1, _waveOut.Volume + 0.05f);
            UpdateCurrentlyPlayingStatus();
        }

        private void SEList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateContextMenuControlState(GetSelectedLevel(sender as ListView));
        }

        private void SEList_MouseDoubleClick(object sender, MouseEventArgs e)
            => DoLevelContextAction(PlaySong);

        private void DeleteMapContextMenuItem_Click(object sender, EventArgs e)
            => DoLevelContextAction(sender as ToolStripMenuItem, DeleteLevel);

        private void PlayMenuItem_Click(object sender, EventArgs e)
            => DoLevelContextAction(PlaySong);

        private void DeleteMenuItem_Click(object sender, EventArgs e)
            => DoLevelContextAction(DeleteLevel);
    }
}