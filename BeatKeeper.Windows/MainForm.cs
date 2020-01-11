﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Repositories;
using BeatKeeper.Kernel.Services;
using BeatKeeper.Windows.Utils;
using BrightIdeasSoftware;

namespace BeatKeeper.Windows
{
    public partial class MainForm : Form
    {
        private SteamCmdService _steamCmdService;
        private IRepository<Artifact> _artifactRepository;

        public MainForm()
        {
            InitializeComponent();
            _steamCmdService = SteamCmdServiceFactory.Instance.Build();
            _artifactRepository = new CombinedArtifactRepository(
                new ArtifactRepository(ClientPathUtils.VanillaArchiveFolder),
                new ArtifactRepository(ClientPathUtils.BackupArchiveFolder)
                );
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeSteamCmd(true);
            UpdateSteamCmdInitGuard();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            ArtifactListView.Objects = _artifactRepository.GetAll();
            ArtifactListView.Sort("Type");
            Focus();
        }

        private void SetStatus(string status)
        {
            if (InvokeRequired)
            {
                this.RunInUiThread(() => { SetStatus(status); });
            }

            StatusLabel.Text = status;
        }

        private void UpdateSteamCmdInitGuard()
        {
            var isInitialized = _steamCmdService.IsInitialized;
            this.RunInUiThread(() =>
            {
                loginToSteamToolStripMenuItem.Enabled = isInitialized;
                downloadVanillaGameToolStripMenuItem.Enabled = isInitialized;
            });
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InitializeSteamCmd(
            bool runLogin = false,
            bool reinitialize = false)
        {
            if (!_steamCmdService.IsInitialized || reinitialize)
            {
                SetStatus("Initializing SteamCMD ...");
                this.RunInBackgroundThread(() =>
                {
                    _steamCmdService.Initialize(reinitialize);
                }, () =>
                {
                    SetStatus("SteamCMD initialized");
                    if (runLogin)
                    {
                        loginToSteamToolStripMenuItem_Click(null, EventArgs.Empty);
                    }
                    UpdateSteamCmdInitGuard();
                    Focus();
                });
            }
            else
            {
                SetStatus("SteamCMD already initialized");
            }
        }

        private void initializeSteamCMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forceReinit = ModifierKeys == Keys.Shift;
            if (forceReinit
                && MessageBox.Show("Do you want to re-initialize SteamCMD? This will clear all data stored with SteamCMD including your login.",
                    "Reinitialize SteamCMD",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            InitializeSteamCmd(reinitialize: forceReinit);
        }

        private void loginToSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loginDialog = new LoginForm();
            if (loginDialog.ShowDialog() == DialogResult.OK)
            {
                var username = loginDialog.Username;
                var password = loginDialog.Password;
                var twoFaCode = loginDialog.TwoFaCode;

                SetStatus("Logging in ...");
                this.RunInBackgroundThread(() =>
                    {
                        _steamCmdService.Login(username).WaitForExit();
                        //steamCmd.Login(username, password, twoFaCode);
                    },
                    () =>
                    {
                        SetStatus("Login completed");
                        UpdateSteamCmdInitGuard();
                    });
            }
        }

        private void downloadVanillaGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DownloadForm().ShowDialog();
            UpdateGrid();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void ArtifactListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var source = (ObjectListView) sender;

            startToolStripMenuItem.Enabled = source.SelectedItems.Count == 1;
            deleteToolStripMenuItem.Enabled = source.SelectedItems.Count > 0;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArtifactListView.SelectedItems.Count == 1)
            {
                var selectedArtifact = (Artifact) ArtifactListView.SelectedItem.RowObject;
                if (MessageBoxUtils.Ask(
                    $"Would you like to delete {selectedArtifact.Name}?",
                    "Delete Artifact"))
                {
                    _artifactRepository.Delete(selectedArtifact);
                    UpdateGrid();
                    SetStatus("1 artifact deleted");
                }
            } else if (ArtifactListView.SelectedItems.Count > 1)
            {
                if (MessageBoxUtils.Ask(
                    $"Would you like to delete {ArtifactListView.SelectedItems.Count} artifacts?",
                    "Delete Artifacts"))
                {
                    var artifactCount = ArtifactListView.SelectedItems.Count;
                    SetStatus($"Deleting {artifactCount} artifacts ...");
                    foreach (var artifact in ArtifactListView.SelectedObjects
                        .OfType<Artifact>())
                    {
                        _artifactRepository.Delete(artifact);
                    }
                    UpdateGrid();
                    SetStatus($"{artifactCount} artifacts deleted");
                }
            }
        }

        private void ArtifactListView_DoubleClick(object sender, EventArgs e)
        {
            var source = (ObjectListView)sender;
            var selectedArtifact = source.SelectedObjects.OfType<Artifact>().FirstOrDefault();
            if (selectedArtifact != null)
            {
                RunArtifact(selectedArtifact);
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArtifactListView_DoubleClick(ArtifactListView, e);
        }

        private void RunArtifact(Artifact artifact)
        {
            new StartUpForm(artifact).ShowDialog();
        }

        private void createArchiveFromCurrentStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new BackupNameForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var versionFile = Path.Combine(SettingsUtils.BeatSaberInstallDirectory, "BeatSaberVersion.txt");
                string gameVersion;
                if (File.Exists(versionFile))
                {
                    gameVersion = File.ReadAllText(versionFile).Trim();
                }
                else
                {
                    gameVersion = "<unknown>";
                }

                Enabled = false;
                SetStatus($"Packing {dialog.ArchiveName} ...");
                this.RunInBackgroundThread(() =>
                {
                    try
                    {
                        BeatKeeperPackageProcessor.PackBackupArtifactV1(
                            SettingsUtils.BeatSaberInstallDirectory,
                            Path.Combine(ClientPathUtils.BackupArchiveFolder, $"{dialog.ArchiveName}.bskeep"),
                            gameVersion);
                        SetStatus($"Artifact packed successfully");
                    }
                    catch (IOException ex)
                    {
                        SetStatus($"Artifact packing failed: {ex.Message}");
                        MessageBoxUtils.Error($"Could not create archive.\n{ex.Message}");
                    }
                }, () =>
                {
                    Enabled = true;
                    UpdateGrid();
                });
            }
        }
    }
}
