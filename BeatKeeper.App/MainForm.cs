using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BeatKeeper.App.Config;
using BeatKeeper.App.Core;
using BeatKeeper.App.Utils;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Repositories;

namespace BeatKeeper.App
{
    public partial class MainForm : Form
    {
        private readonly ArtifactRepository _artifactRepository;
        private readonly ConfigManager _configManager;

        private List<Artifact> _artifacts = new();
        
        private readonly OpenFileDialog _selectGameExeDialog = new()
        {
            Title = @"Select BeatSaber executable",
            Filter = @"Beat Saber Executable|Beat Saber.exe|Executables|*.exe|Any Files|*"
        };

        public MainForm()
        {
            InitializeComponent();
            _artifactRepository = new ArtifactRepository(BSKConstants.Paths.Archives);
            _configManager = ConfigManager.Instance;
            UpdateGameDirectory();
        }

        private void UpdateGameDirectory()
        {
            string gameDirectory = _configManager.Config.GamePath;
            
            _selectGameExeDialog.FileName = gameDirectory;
            GameDirectoryTextBox.Text = gameDirectory;
        }

        private void DownloadVanillaArchiveMenuItem_Click(object sender, EventArgs e)
        {
            new DownloadGameArchiveForm
            {
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
            UpdateGrids();
        }

        private void SetStatus(string statusString, int percentage = -1)
        {
            this.RunInUiThread(() =>
            {
                StatusLabel.Text = statusString;
                if (percentage < 0)
                {
                    StatusProgressBar.Style = ProgressBarStyle.Marquee;
                    StatusProgressBar.Value = 0;
                }
                else
                {
                    StatusProgressBar.Style = ProgressBarStyle.Continuous;
                    StatusProgressBar.Value = Math.Min(percentage, StatusProgressBar.Maximum);
                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateGrids();
            UpdateMenuItems(null);
        }

        private void UpdateGrids()
        {
            SetStatus("Listing archives ....");
            this.RunInBackgroundThread(() =>
            {
                _artifacts = _artifactRepository.GetAll().ToList();
            }, () =>
            {
                RenderGrids();
                UpdateTabCaptions();
                SetStatus($"Done, found {_artifacts.Count} archives", 0);
            });
        }

        private static readonly Func<Artifact, bool> IsVanillaArchive = a => a != null && a.Type == ArtifactType.Vanilla;
        private static readonly Func<Artifact, bool> IsBackupArchive = a => a != null && a.Type == ArtifactType.ModBackup;

        private void RenderGrids()
        {
            VanillaArchivesListView.Items.Clear();
            BackupArchivesListView.Items.Clear();

            VanillaArchivesListView.BeginUpdate();
            BackupArchivesListView.BeginUpdate();

            VanillaArchivesListView.Items.AddRange(_artifacts
                .Where(IsVanillaArchive)
                .Select(a =>
                {
                    var item = new ListViewItem
                    {
                        Text = a.GameVersion,
                        Tag = a,
                        ImageKey = "Saber"
                    };
                    item.SubItems.Add(a.HumanReadableSize);
                    return item;
                })
                .ToArray());
            BackupArchivesListView.Items.AddRange(_artifacts
                .Where(IsBackupArchive)
                .Select(a =>
                {
                    var item = new ListViewItem
                    {
                        Text = a.Name,
                        Tag = a,
                        ImageKey = "SaberPack"
                    };
                    item.SubItems.Add(a.GameVersion);
                    item.SubItems.Add($"{a.LastUpdated}");
                    item.SubItems.Add(a.HumanReadableSize);
                    return item;
                })
                .ToArray());

            VanillaArchivesListView.EndUpdate();
            BackupArchivesListView.EndUpdate();
        }

        private void UpdateTabCaptions()
        {
            BackupTabPage.Text = $"Backups ({_artifacts.Where(IsBackupArchive).Count()})";
            VanillaTabPage.Text = $"Vanilla Archives ({_artifacts.Where(IsVanillaArchive).Count()})";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateContextMenuControlState(
            Artifact selectedArtifact)
        {
            // First, run over the whole menu and enable / disable everything based on if something is selected
            // and add the artifact as a tag.
            foreach (ToolStripItem item in ArchiveContextMenuStrip.Items)
            {
                item.Enabled = selectedArtifact != null;
                item.Tag = selectedArtifact;
            }

            // Then, change some controls specifically when an artifact is selected
            if (selectedArtifact != null)
            {
                bool isModBackup = IsBackupArchive(selectedArtifact);

                cloneToolStripMenuItem.Enabled = isModBackup;
                updateToolStripMenuItem.Enabled = isModBackup;
                renameToolStripMenuItem.Enabled = isModBackup;
            }
        }

        private void UpdateMenuItems(
            Artifact selectedArtifact)
        {
            bool isSelected = selectedArtifact != null;
            bool isModBackup = IsBackupArchive(selectedArtifact);

            UnpackRunMenuItem.Enabled = isSelected;
            UnpackMenuItem.Enabled = isSelected;
            UpdateMenuItem.Enabled = isModBackup;
            CloneMenuItem.Enabled = isModBackup;
            RenameMenuItem.Enabled = isModBackup;
            DeleteMenuItem.Enabled = isSelected;
            ShowInSystemExplorerMenuItem.Enabled = isSelected;
            PropertiesMenuItem.Enabled = isSelected;
        }

        private Artifact GetSelectedArtifact(ListView listView)
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
            return selectedListViewItem.Tag as Artifact;
        }

        private Artifact GetSelectedArtifact()
        {
            var activeTab = TabContainer.SelectedIndex;
            switch (activeTab)
            {
                case 0:
                    return GetSelectedArtifact(BackupArchivesListView);
                case 1:
                    return GetSelectedArtifact(VanillaArchivesListView);
            }
            return null;
        }

        private void DeleteArtifact(Artifact artifact)
        {
            if (MessageBoxUtils.Ask($"Do you want to delete the archive {artifact.Name}?"))
            {
                SetStatus($"Deleting archive {artifact.Name} ...");
                this.RunInBackgroundThread(() =>
                {
                    _artifactRepository.Delete(artifact);
                }, UpdateGrids);
            }
        }

        private void RenameArtifact(Artifact artifact)
        {
            var dialog = new RenameArchiveForm(artifact);
            while (true)
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // If name matches old name, do a placebo-reload
                var newName = dialog.NewArchiveName;
                if (newName.Trim().Equals(artifact.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }

                if (_artifactRepository.Exists(newName))
                {
                    MessageBoxUtils.Warn($"The name \"{newName}\" is already taken. Please select a different name.");
                    continue;
                }

                _artifactRepository.Rename(artifact, newName);
                break;
            }
            UpdateGrids();
        }

        private void UpdateArtifact(Artifact artifact)
        {
            MessageBoxUtils.NotImplemented("Updating artifact");
        }

        private void ShowArtifactProperties(Artifact artifact)
        {
            MessageBoxUtils.Show(
                $"Size: {artifact.HumanReadableSize} ({artifact.Size} bytes)\n" +
                $"Archive Type: {artifact.Type} [{artifact.ArchiveVersion}]\n" +
                $"Game Version: {artifact.GameVersion}\n" +
                $"Created: {artifact.Created}\n" +
                $"Last updated: {artifact.LastUpdated}\n\n" +
                $"Stored at: {artifact.FullPath}",
                $"About Archive {artifact.Name}");
        }

        private void CloneArtifact(Artifact artifact)
        {
            if (MessageBoxUtils.Ask($"Do you want to clone the archive {artifact.Name}?"))
            {
                SetStatus($"Cloning archive {artifact.Name} ...");
                this.RunInBackgroundThread(() =>
                {
                    var newName = $"{artifact.Name}_CLONE";
                    _artifactRepository.Clone(artifact, newName);
                }, UpdateGrids);
            }
        }

        private void UnpackArtifact(Artifact artifact)
        {
            MessageBoxUtils.NotImplemented("Unpacking archive");
        }

        private void UnpackAndRunArtifact(Artifact artifact)
        {
            UnpackArtifact(artifact);
            MessageBoxUtils.NotImplemented("Starting game");
        }

        private void ShowArtifactInSystemExplorer(Artifact artifact)
        {
            WindowsUtils.ShowFileInExplorer(artifact.FullPath);
        }

        private void DoArtifactContextAction(Func<Artifact> extractor, Action<Artifact> action)
        {
            var artifact = extractor();
            if (artifact != null)
            {
                action(artifact);
            }
        }

        private void DoArtifactContextAction(Action<Artifact> action)
            => DoArtifactContextAction(GetSelectedArtifact, action);

        private void DoArtifactContextAction(ToolStripMenuItem sourceItem, Action<Artifact> action)
            => DoArtifactContextAction(() => sourceItem?.Tag as Artifact, action);

        private void ArchiveContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var contextMenuStrip = (ContextMenuStrip) sender;
            var artifact = GetSelectedArtifact(contextMenuStrip.SourceControl as ListView);
            UpdateContextMenuControlState(artifact);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, ShowArtifactProperties);

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, DeleteArtifact);

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, CloneArtifact);

        private void showInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, ShowArtifactInSystemExplorer);

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, RenameArtifact);

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMenuItems(GetSelectedArtifact());
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMenuItems(GetSelectedArtifact(sender as ListView));
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(DeleteArtifact);

        private void RenameMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(RenameArtifact);

        private void CloneMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(CloneArtifact);

        private void UpdateMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(UpdateArtifact);

        private void ShowInSystemExplorerMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(ShowArtifactInSystemExplorer);

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(ShowArtifactProperties);

        private void UnpackRunMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(UnpackAndRunArtifact);

        private void UnpackMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction(UnpackArtifact);

        private void unpackRunToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, UnpackAndRunArtifact);

        private void unpackToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, UnpackArtifact);

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
            => DoArtifactContextAction((ToolStripMenuItem)sender, UpdateArtifact);

        private void SetGameDirectoryMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectGameExeDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (!File.Exists(_selectGameExeDialog.FileName))
            {
                return;
            }

            _configManager.Config.GamePath = Path.GetDirectoryName(Path.GetFullPath(_selectGameExeDialog.FileName));
            _configManager.WriteConfig();
            
            UpdateGameDirectory();
        }

        private void aboutBeatSaberKeeperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxUtils.AboutApp();
        }
        
        private void openUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem {Tag: string url})
            {
                WindowsUtils.OpenUrl(url);
            }
        }
    }
}
