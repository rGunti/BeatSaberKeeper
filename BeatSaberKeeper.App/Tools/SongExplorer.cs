﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using BeatSaberKeeper.App.Config;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Plugin.SongExplorer;
using BeatSaberKeeper.Plugin.SongExplorer.MetaData;
using NAudio.Utils;
using NAudio.Vorbis;
using NAudio.Wave;

namespace BeatSaberKeeper.App.Tools
{
    public partial class SongExplorer : Form
    {
        private readonly ConfigManager _configManager = ConfigManager.Instance;
        private readonly SongReader _songReader;
        
        private readonly WaveOut _waveOut = new();
        private Level _currentlyPlayingSong = null;

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

        private void UpdateCurrentlyPlayingStatus()
        {
            if (_currentlyPlayingSong == null || _waveOut.PlaybackState == PlaybackState.Stopped)
            {
                return;
            }

            LevelInfo levelInfo = _currentlyPlayingSong.LevelInfo;
            SetStatus($"Playing \"{levelInfo.SongName}\" by {levelInfo.SongAuthorName} [{_waveOut.GetPositionTimeSpan():h':'mm':'ss}]");
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
        }

        private void SEContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var contextMenuStrip = (ContextMenuStrip) sender;
            Level level = GetSelectedLevel(contextMenuStrip.SourceControl as ListView);
            UpdateContextMenuControlState(level);
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

            _currentlyPlayingSong = level;
            var vorbis = new VorbisWaveReader(level.AudioFilePath);
            _waveOut.Init(vorbis);
            _waveOut.Play();
        }

        private async void PlaySongContextMenuItem_Click(object sender, EventArgs e)
            => DoLevelContextAction(sender as ToolStripMenuItem, PlaySong);

        private void SEStatusUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_waveOut.PlaybackState != PlaybackState.Stopped)
            {
                UpdateCurrentlyPlayingStatus();
            }
        }
    }
}