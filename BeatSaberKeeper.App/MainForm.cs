using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BeatSaberKeeper.App.Core;
using BeatSaberKeeper.Kernel.Entities;
using BeatSaberKeeper.Kernel.Repositories;
using BeatSaberKeeper.Kernel.Services;
using BeatSaberKeeper.App.Config;
using BeatSaberKeeper.App.Controls;
using BeatSaberKeeper.App.Tools;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Updater;

namespace BeatSaberKeeper.App
{
    public partial class MainForm : Form
    {
        private readonly ArtifactRepository _artifactRepository;
        private readonly ConfigManager _configManager;
        private readonly IReleaseChecker _releaseChecker = new BskReleaseChecker();

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
            UpdateConfigDisplay();

            // ReSharper disable once VirtualMemberCallInConstructor
#if DEBUG
            Text = $@"{AppInfo.AppName} - v{AppInfo.AppVersion}";
#else
            Text = $@"{AppInfo.AppName} - v{AppInfo.AppVersion.ToShortString()}";
#endif
        }

        private void UpdateConfigDisplay()
        {
            string gameDirectory = _configManager.Config.GamePath;
            
            _selectGameExeDialog.FileName = gameDirectory;
            GameDirectoryTextBox.Text = gameDirectory;

            CheckForUpdatesOnStartupMenuItem.Checked = _configManager.Config.CheckForUpdatesOnStartup;
            PreReleaseMenuItem.Checked = _configManager.Config.PrereleaseOptIn;
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
            if (_configManager.Config.CheckForUpdatesOnStartup)
            {
                CheckForUpdates();
            }
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
        private static readonly Func<Artifact, bool> IsDefect = a => a != null && a.IsDefect;

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
                        ImageKey = a.IsDefect ? "Defect" : "Saber"
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
                        ImageKey = a.IsDefect ? "Defect" : "SaberPack"
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

        private void CheckForUpdates()
        {
            SetStatus("Checking for updates ...");
            BskVersion latestVersion = null;
            this.RunInBackgroundThread(() =>
            {
                latestVersion = _releaseChecker.GetLatestVersion(_configManager.Config.PrereleaseOptIn);
            }, () =>
            {
                if (latestVersion != null && latestVersion > AppInfo.AppVersion)
                {
                    SetStatus($"New version available: {latestVersion}", 0);
                    if (MessageBoxUtils.Ask(
                        $"A new version of BeatSaberKeeper is available.\n" +
                        $"\n" +
                        $"You are currently running {AppInfo.AppVersion}, the latest available " +
                        $"version is {latestVersion}.\n" +
                        $"Do you want to download it?",
                        "Update available"))
                    {
                        DownloadUpdate(latestVersion);
                    }
                }
                else if (latestVersion != null)
                {
                    SetStatus("Checked for updates, you have the latest version available", 0);
                }
                else
                {
                    SetStatus("Couldn't check for updates (Is your internet connection working?)", 0);
                }
            });
        }

        private void DownloadUpdate(BskVersion version)
        {
            SetStatus("Getting update ready to download ...");
            this.RunInBackgroundThread(async () =>
            {
                string downloadUrl = await _releaseChecker.GetDownloadUrlForVersionAsync(version);
                if (!string.IsNullOrWhiteSpace(downloadUrl))
                {
                    WindowsUtils.OpenUrl(downloadUrl);
                }
            }, () =>
            {
                SetStatus("Update requested", 0);
            });
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
                bool isDefect = IsDefect(selectedArtifact);
                bool isModBackup = IsBackupArchive(selectedArtifact) && !isDefect;

                unpackToolStripMenuItem.Enabled = !isDefect;
                unpackRunToolStripMenuItem.Enabled = !isDefect;
                updateToolStripMenuItem.Enabled = !isDefect;
                cloneToolStripMenuItem.Enabled = isModBackup;
                updateToolStripMenuItem.Enabled = isModBackup;
                renameToolStripMenuItem.Enabled = isModBackup;
            }
        }

        private void UpdateMenuItems(
            Artifact selectedArtifact)
        {
            bool isSelected = selectedArtifact != null;
            bool isDefect = IsDefect(selectedArtifact);
            bool isModBackup = IsBackupArchive(selectedArtifact) && !isDefect;

            UnpackRunMenuItem.Enabled = isSelected && !isDefect;
            UnpackMenuItem.Enabled = isSelected && !isDefect;
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
            if (MessageBoxUtils.Ask($"Do you want to update \"{artifact.Name}\"?"))
            {
                PackArchive(artifact.Name);
            }
        }

        private void ShowArtifactProperties(Artifact artifact)
        {
            MessageBoxUtils.Show(
                $"Size: {artifact.HumanReadableSize} ({artifact.Size} bytes)\n" +
                $"Archive Type: {artifact.Type} [{artifact.ArchiveVersion}]{(artifact.IsDefect ? " [BROKEN]" : "")}\n" +
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
            SetStatus("Unpacking archive ...");
            new BackgroundProcessControl(
                    $"Unpacking {artifact.Name} ...",
                    d =>
                    {
                        BeatKeeperPackageProcessor.UnpackArchive(
                            artifact.FullPath,
                            _configManager.Config.GamePath,
                            d.SetStatus);
                    }, () =>
                    {
                        SetStatus("Archive unpacked", 0);
                    })
                .ShowDialog();
        }

        private void UnpackAndRunArtifact(Artifact artifact)
        {
            UnpackArtifact(artifact);
            SetStatus("Launching game ...");
            this.RunInBackgroundThread(() =>
            {
                Thread.Sleep(1500);
                BeatSaberLauncher.Launch(_configManager.Config.GamePath);
            }, () =>
            {
                SetStatus("Game launched", 0);
            });
        }

        private void ShowArtifactInSystemExplorer(Artifact artifact)
        {
            WindowsUtils.ShowFileInExplorer(artifact.FullPath);
        }

        private bool SetGameDirectory()
        {
            if (_selectGameExeDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            if (!File.Exists(_selectGameExeDialog.FileName))
            {
                return false;
            }

            _configManager.Config.GamePath = Path.GetDirectoryName(Path.GetFullPath(_selectGameExeDialog.FileName));
            _configManager.WriteConfig();
            
            UpdateConfigDisplay();
            return true;
        }

        private void PackArchive()
        {
            var dialog = new RenameArchiveForm
            {
                Text = "Enter an archive name",
                ConfirmationButtonText = "Pack"
            };
            while (true)
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var newName = dialog.NewArchiveName;
                if (_artifactRepository.Exists(newName))
                {
                    MessageBoxUtils.Warn($"The name \"{newName}\" is already taken. Please select a different name.");
                    continue;
                }

                PackArchive(newName);
                break;
            }
        }

        private void PackArchive(string archiveName)
        {
            string versionFile = Path.Combine(_configManager.Config.GamePath, "BeatSaberVersion.txt");
            string gameVersion = File.Exists(versionFile) ? File.ReadAllText(versionFile).Trim() : "<unknown>";
            SetStatus("Creating new archive ...");
            new BackgroundProcessControl(
                    $"Packing {archiveName} ...",
                    bgDialog =>
                    {
                        bgDialog.SetStatus("Packing archive ...");
                        try
                        {
                            BeatKeeperPackageProcessor.PackBackupArtifactV1(
                                _configManager.Config.GamePath,
                                Path.Combine(BSKConstants.Paths.Archives, $"{archiveName}.bskeep"),
                                gameVersion,
                                (s, v, m) =>
                                {
                                    bgDialog.SetStatus(s, v, m);
                                    SetStatus(s.Split('\n').FirstOrDefault(), (int)Math.Floor((double)v / m * 100));
                                });
                        }
                        catch (IOException ex)
                        {
                            SetStatus($"Failed to pack game state", 0);
                            MessageBoxUtils.Error($"Could not create archive.\n{ex.Message}");
                        }
                    }, UpdateGrids,
                    TimeSpan.FromMilliseconds(100))
                .ShowDialog();
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
            => SetGameDirectory();

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

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_configManager.Config.GamePath))
            {
                MessageBoxUtils.Warn("You haven't set the game directory yet. " +
                                     "Please select the folder where Beat Saber is installed.");
                if (!SetGameDirectory())
                {
                    return;
                }
            }

            PackArchive();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void PreReleaseMenuItem_Click(object sender, EventArgs e)
        {
            _configManager.Config.PrereleaseOptIn = !_configManager.Config.PrereleaseOptIn;
            _configManager.WriteConfig();
            UpdateConfigDisplay();
        }

        private void CheckForUpdatesOnStartupMenuItem_Click(object sender, EventArgs e)
        {
            _configManager.Config.CheckForUpdatesOnStartup = !_configManager.Config.CheckForUpdatesOnStartup;
            _configManager.WriteConfig();
            UpdateConfigDisplay();
        }
        
        private void SongExplorerMenuItem_Click(object sender, EventArgs e)
        {
            new SongExplorer().ShowDialog();
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            UpdateGrids();
            UpdateMenuItems(null);
        }
    }
}
