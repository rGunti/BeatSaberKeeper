using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BeatKeeper.Controls;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Repositories;
using BeatKeeper.Kernel.Services;
using BeatKeeper.Windows.Utils;
using BeatKeeper.Controls.Utils;
using BrightIdeasSoftware;

namespace BeatKeeper.Windows
{
    public partial class MainForm : Form
    {
        private SteamCmdService _steamCmdService;
        private IRepository<Artifact> _artifactRepository;
        private readonly ReleaseChecker _releaseChecker;

        public MainForm()
        {
            InitializeComponent();
            _steamCmdService = SteamCmdServiceFactory.Instance.Build();
            _artifactRepository = new CombinedArtifactRepository(
                new ArtifactRepository(ClientPathUtils.VanillaArchiveFolder),
                new ArtifactRepository(ClientPathUtils.BackupArchiveFolder)
                );
            _releaseChecker = new ReleaseChecker(true, string.Empty);

            ArtifactNameColumn.ImageGetter += o => "Pack16";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeSteamCmd(true);
            InitializeDepotDownloader();
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

                if (string.IsNullOrWhiteSpace(SettingsUtils.BeatSaberInstallDirectory))
                {
                    MessageBoxUtils.Warn(
                        "BeatSaberKeeper doesn't know where Beat Saber is installed. " +
                        "Please select your Beat Saber path.");
                    new SettingsForm().ShowDialog();
                }
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
                && MessageBox.Show(
                    "Do you want to re-initialize SteamCMD? This will clear all data stored with SteamCMD including your login.",
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
            //MessageBoxUtils.Warn("Okay, so here's the deal:\n" +
            //    "In Feb 2020, Steam (intentionally) broke the feature in the Steam cloud which is used to download older versions of games." +
            //    "There is a workaround for it but I couldn't find the time to integrate it here.\n" +
            //    "I did not disable that feature here but it won't work when downloading older game versions. Sorry for that.\n" +
            //    "You can use this tool to download older game versions: https://github.com/SteamRE/DepotDownloader. " +
            //    "I put a generated command line in the following window so you can copy/paste it into your terminal.");

            MessageBoxUtils.Warn("Beat Saber Keeper uses DepotDownloader to download older game versions. Currently, this requires that you have installed the " +
                                 ".NET Core 2.1 runtime environment. I am currently working with the developers of DepotDownloader to create a library so Beat Saber Keeper " +
                                 "can integrate this feature and provide the download feature internally without the need for SteamCMD or another external executable.\n\n" +
                                 "Stay tuned for updates coming soon! :)");

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
            unpackToolStripMenuItem.Enabled = source.SelectedItems.Count == 1;
            deleteToolStripMenuItem.Enabled = source.SelectedItems.Count > 0;
            updateArchiveWithCurrentStateToolStripMenuItem.Enabled = source.SelectedItems.Count == 1;

            startToolStripMenuItem1.Enabled = startToolStripMenuItem.Enabled;
            unpackToolStripMenuItem1.Enabled = unpackToolStripMenuItem.Enabled;
            deleteToolStripMenuItem1.Enabled = deleteToolStripMenuItem.Enabled;
            updateArchiveToolStripMenuItem.Enabled = updateArchiveWithCurrentStateToolStripMenuItem.Enabled;
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

        private void RunArtifact(Artifact artifact, bool runGame = true)
        {
            new BackgroundProcessControl(
                    $"Unpacking {artifact.Name} ...",
                    bgDialog =>
                    {
                        bgDialog.SetStatus("Preparing ...", -1, -1);

                        BeatKeeperPackageProcessor.UnpackArchive(
                            artifact.FullPath,
                            SettingsUtils.BeatSaberInstallDirectory,
                            bgDialog.SetStatus);

                        if (runGame)
                        {
                            bgDialog.SetStatus("Starting game ...");
                            BeatSaberLauncher.Launch(SettingsUtils.BeatSaberInstallDirectory);
                        }
                        Thread.Sleep(1000);
                    })
                .ShowDialog();
        }

        private void createArchiveFromCurrentStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new BackupNameForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PackArchive(dialog.ArchiveName);
            }
        }

        private void PackArchive(string archiveName)
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
            SetStatus($"Packing {archiveName} ...");
            new BackgroundProcessControl(
                    $"Packing {archiveName} ...",
                    bgDialog =>
                    {
                        bgDialog.SetStatus("Packing archive ...");
                        try
                        {
                            BeatKeeperPackageProcessor.PackBackupArtifactV1(
                                SettingsUtils.BeatSaberInstallDirectory,
                                Path.Combine(ClientPathUtils.BackupArchiveFolder, $"{archiveName}.bskeep"),
                                gameVersion,
                                bgDialog.SetStatus);
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
                    }, TimeSpan.FromMilliseconds(50))
                .ShowDialog();
        }

        private void aboutBeatKeeperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxUtils.AboutApp();
        }

        private void openUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem source
                && source.Tag is string url)
            {
                try
                {
                    Process.Start(url);
                } catch (Exception ex) { }
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus("Checking for updates ...");
            var newReleaseAvailable = false;
            this.RunInBackgroundThread(() =>
            {
                newReleaseAvailable = _releaseChecker.HasNewVersion();
            }, () =>
            {
                if (newReleaseAvailable
                    && MessageBoxUtils.Ask(
                        "A new version is available to download. Do you want to download it?"))
                {
                    _releaseChecker.OpenReleasePage();
                }
                else
                {
                    SetStatus(newReleaseAvailable ?
                        "New release found" :
                        "No updates available");
                }
            });
        }

        private void unpackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedArtifact = ArtifactListView.SelectedObjects.OfType<Artifact>().FirstOrDefault();
            if (selectedArtifact != null)
            {
                RunArtifact(selectedArtifact, false);
            }
        }

        private void updateArchiveWithCurrentStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedArtifact = ArtifactListView.SelectedObjects.OfType<Artifact>().FirstOrDefault();
            if (selectedArtifact != null)
            {
                if (selectedArtifact.Type == ArtifactType.ModBackup)
                {
                    if (MessageBoxUtils.Ask(
                        $"Do you want to update the archive \"{selectedArtifact.Name}\"?",
                        "Update Archive"))
                    {
                        PackArchive(selectedArtifact.Name);
                    }
                }
                else
                {
                    MessageBoxUtils.Error($"Cannot update archive with current state as it is not a \"ModBackup\" archive.");
                }
            }
        }

        private void InitializeDepotDownloader()
        {
            var service = DepotDownloaderServiceFactory.Instance.Build();
            if (!service.IsInitialized)
            {
                new BackgroundProcessControl("Initializing DepotDownloader ...",
                        d =>
                        {
                            d.SetStatus("Downloading latest release ...", -1, -1);
                            service.DownloadLatestRelease();
                        })
                    .ShowDialog();
            }
        }

        private void initializeDepotDownloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeDepotDownloader();
        }
    }
}
