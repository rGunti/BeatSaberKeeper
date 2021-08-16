using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeatKeeper.App.Core;
using BeatKeeper.App.Utils;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Repositories;

namespace BeatKeeper.App
{
    public partial class MainForm : Form
    {
        private readonly ArtifactRepository _artifactRepository;

        private List<Artifact> _artifacts = new();

        public MainForm()
        {
            InitializeComponent();
            _artifactRepository = new ArtifactRepository(BSKConstants.Paths.Archives);
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

        private static readonly Func<Artifact, bool> IsVanillaArchive = a => a.Type == ArtifactType.Vanilla;
        private static readonly Func<Artifact, bool> IsBackupArchive = a => a.Type == ArtifactType.ModBackup;

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

        private void ArchiveContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var contextMenuStrip = (ContextMenuStrip) sender;
            if (!(contextMenuStrip.SourceControl is ListView ctxMenuSourceList))
            {
                UpdateContextMenuControlState(null);
                return;
            }

            ListView.SelectedListViewItemCollection selectedItems = ctxMenuSourceList.SelectedItems;
            if (selectedItems.Count != 1)
            {
                UpdateContextMenuControlState(null);
                return;
            }

            ListViewItem selectedListViewItem = selectedItems[0];
            var selectedArchive = selectedListViewItem.Tag as Artifact;
            UpdateContextMenuControlState(selectedArchive);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;
            if (menuItem.Tag is Artifact selectedArchive)
            {
                MessageBoxUtils.Show(
                    $"Size: {selectedArchive.HumanReadableSize} ({selectedArchive.Size} bytes)\n" +
                    $"Archive Type: {selectedArchive.Type} [{selectedArchive.ArchiveVersion}]\n" +
                    $"Game Version: {selectedArchive.GameVersion}\n" +
                    $"Created: {selectedArchive.Created}\n" +
                    $"Last updated: {selectedArchive.LastUpdated}\n\n" +
                    $"Stored at: {selectedArchive.FullPath}",
                    $"About Archive {selectedArchive.Name}");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;
            if (menuItem.Tag is not Artifact selectedArchive)
            {
                return;
            }

            if (MessageBoxUtils.Ask($"Do you want to delete the archive {selectedArchive.Name}?"))
            {
                SetStatus($"Deleting archive {selectedArchive.Name} ...");
                this.RunInBackgroundThread(() =>
                {
                    _artifactRepository.Delete(selectedArchive);
                }, UpdateGrids);
            }
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;
            if (menuItem.Tag is not Artifact selectedArchive)
            {
                return;
            }

            if (MessageBoxUtils.Ask($"Do you want to clone the archive {selectedArchive.Name}?"))
            {
                SetStatus($"Cloning archive {selectedArchive.Name} ...");
                this.RunInBackgroundThread(() =>
                {
                    var newName = $"{selectedArchive.Name}_CLONE";
                    _artifactRepository.Clone(selectedArchive, newName);
                }, UpdateGrids);
            }
        }

        private void showInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;
            if (menuItem.Tag is not Artifact selectedArchive)
            {
                return;
            }
            
            WindowsUtils.ShowFileInExplorer(selectedArchive.FullPath);
        }
    }
}
