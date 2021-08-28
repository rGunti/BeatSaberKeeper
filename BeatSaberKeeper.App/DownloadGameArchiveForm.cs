using BeatSaberKeeper.App.Core;
using BeatSaberKeeper.App.Core.Steam;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BeatSaberKeeper.App.Core.Utils;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Kernel.Abstraction;
using BeatSaberKeeper.Kernel.Abstraction.Entities;

namespace BeatSaberKeeper.App
{
    public partial class DownloadGameArchiveForm : Form
    {
        private DateTime _lastUpdated = DateTime.MinValue;
        private BeatSaberVersionDownloader _downloader = null;
        private CancellationTokenSource _cancellationTokenSource = new();

        private readonly ICompressionInterface _compressionInterface;

        public DownloadGameArchiveForm(ICompressionInterface compressionInterface)
        {
            _compressionInterface = compressionInterface;
            InitializeComponent();
        }

        private void UpdateStatus(string status, int value = 0, int maxValue = 100, bool force = true)
        {
            if (!force)
            {
                TimeSpan dif = DateTime.UtcNow - _lastUpdated;
                if (dif < TimeSpan.FromMilliseconds(250))
                {
                    return;
                }
            }
            this.RunInUiThread(() =>
            {
                StatusLabel.Text = status;
                if (value >= 0)
                {
                    DownloadStatusProgressBarr.Value = value;
                    DownloadStatusProgressBarr.Maximum = maxValue;
                    DownloadStatusProgressBarr.Style = ProgressBarStyle.Continuous;
                }
                else
                {
                    DownloadStatusProgressBarr.Style = ProgressBarStyle.Marquee;
                }

                _lastUpdated = DateTime.UtcNow;
            });
        }

        private void Login()
        {
            _ = new SteamLoginForm
            {
                StartPosition = FormStartPosition.CenterScreen
            }.ShowDialog();
            UpdateFormState();
            if (SteamSession.Instance.IsLoggedIn)
            {
                this.RunInBackgroundThread(UpdateLicenseInfo, () => { });
            }
        }

        private async void Logout()
        {
            this.RunInUiThread(() =>
            {
                LoginButton.Enabled = false;
            });
            await SteamSession.Instance.Logout();
            Close();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (SteamSession.Instance.IsLoggedIn)
            {
                Logout();
            }
            else
            {
                Login();
            }
        }

        private void DownloadGameArchiveForm_Load(object sender, EventArgs e)
        {
            UpdateFormState();
            if (!SteamSession.Instance.IsLoggedIn)
            {
                Login();
            }

            SteamAppIdTextBox.Text = @$"{BSKConstants.Steam.BEAT_SABER_APP_ID}";
            SteamDepotIdTextBox.Text = @$"{BSKConstants.Steam.BEAT_SABER_DEPOT_ID}";
            SteamBranchTextBox.Text = BSKConstants.Steam.DEFAULT_BRANCH;
            
            this.RunInBackgroundThread(InitializeDownloader, () =>
            {
                this.RunInUiThread(FillGameVersionDropdown);
            });
        }

        private void InitializeDownloader()
        {
            //string filepath = BeatSaberVersionDownloader.DownloadRecentVersionFile();
            _downloader = new BeatSaberVersionDownloader();
        }

        private void FillGameVersionDropdown()
        {
            GameVersionDropdown.Items.Clear();
            GameVersionDropdown.Items.AddRange(
                // ReSharper disable once CoVariantArrayConversion
                _downloader.Artifacts
                    .OrderByDescending(a => a.Created)
                    .ToArray());
        }

        private void UpdateFormState()
        {
            this.RunInUiThread(() =>
            {
                var session = SteamSession.Instance;
                DownloaderPanel.Enabled = session.IsLoggedIn;
                if (session.IsLoggedIn)
                {
                    UpdateStatus("Ready");
                    LoginStatusLabel.Text = @$"Logged in as {session.LoggedInUser}";
                }
                else
                {
                    UpdateStatus("User is not logged in, Downloader disabled");
                }
                LoginButton.Text = session.IsLoggedIn ? "Logout" : "Login";
            });
        }

        private async void UpdateLicenseInfo()
        {
            var session = SteamSession.Instance;
            if (await session.CheckAccountAccessForApp(BSKConstants.Steam.BEAT_SABER_APP_ID))
            {
                UpdateStatus("License checked. Your account is in possession of a valid Beat Saber license.");
            }
            else
            {
                MessageBoxUtils.Error("Your account does not have the required license to download Beat Saber.\n\n" +
                    "Please login with a Steam account owning a valid license or purchase the game.");
                Logout();
            }
        }

        private void DownloadArchiveButton_Click(object sender, EventArgs e)
        {
            var currentVersion = GameVersionDropdown.SelectedItem as Artifact;
            if (currentVersion == null)
            {
                return;
            }

            var session = SteamSession.Instance;
            if (!session.IsConnected || !session.IsLoggedIn)
            {
                return;
            }

            if (!ulong.TryParse(currentVersion.ManifestId, out ulong manifestId))
            {
                return;
            }

            uint appId = BSKConstants.Steam.BEAT_SABER_APP_ID;
            uint depotId = BSKConstants.Steam.BEAT_SABER_DEPOT_ID;
            
            UpdateStatus("Getting depot information ...");
            this.RunInBackgroundThread(async () =>
            {
                DepotDownloadInfo dlInfo = await session.GetDepotDownloadInfo(
                    appId, depotId, manifestId, BSKConstants.Steam.DEFAULT_BRANCH);
                if (dlInfo == null)
                {
                    UpdateStatus("Requested Depot or Manifest could not be found!");
                    MessageBoxUtils.Error("Could not find requested depot or manifest info!");
                    return;
                }
                
                try
                {
                    DepotFileData fileList = await session.GetFileListForDepotAndManifest(
                        appId, dlInfo, _cancellationTokenSource);
                    UpdateStatus("Starting download ...");

                    await session.DownloadDepot(appId, fileList, (message, percentage) =>
                    {
                        UpdateStatus(message, (int)((percentage ?? -1f) * 100), force: false);
                    }, _cancellationTokenSource);

                    UpdateStatus("Download completed, backing archive ...", -1);

                    string path = PathUtils.ConstructStagingFilePath(
                        appId, depotId, manifestId);
                    File.WriteAllText(Path.Combine(path, "BeatSaberVersion.txt"), currentVersion.GameVersion);
                    BSKConstants.Paths.Archives.EnsureDirectory();
                    _compressionInterface.CreateArchiveFromFolder(
                        path,
                        Path.Combine(BSKConstants.Paths.Archives, $"BeatSaber_{currentVersion.GameVersion}.bskeep"),
                        (status, value, max) =>
                        {
                            UpdateStatus(status.Replace('\n', ' '), value, max, true);
                        },
                        ArtifactType.Vanilla);
                    UpdateStatus("Archive created!", 100);
                }
                catch (OperationCanceledException)
                {
                    UpdateStatus("Cancelled");
                    MessageBoxUtils.Error("Could not get information for download. The requested resource probably doesn't exist or you don't have access to it.");
                }
                catch (Exception ex)
                {
                    UpdateStatus("Failed with exception!");
                    MessageBoxUtils.Ex(ex, "initializing manifest download");
                }
            });
        }

        private void EditAdvancedValuesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var newValue = !(sender as CheckBox).Checked;
            SteamAppIdTextBox.ReadOnly = newValue;
            SteamDepotIdTextBox.ReadOnly = newValue;
            SteamBranchTextBox.ReadOnly = newValue;
        }

        private async void StartAdvancedDownloadButton_Click(object sender, EventArgs e)
        {
            string branch = SteamBranchTextBox.Text;

            if (!uint.TryParse(SteamAppIdTextBox.Text, out uint appId)
                || !uint.TryParse(SteamDepotIdTextBox.Text, out uint depotId)
                || !ulong.TryParse(SteamManifestIdTextBox.Text, out ulong manifestId))
            {
                MessageBoxUtils.Error("One of the provided parameters is invalid! All IDs must be numerical.");
                return;
            }

            var session = SteamSession.Instance;
            if (session.IsConnected && session.IsLoggedIn)
            {
                UpdateStatus("Getting depot information ...", -1);

                var downloadInfo = await session.GetDepotDownloadInfo(appId, depotId, manifestId, branch);
                if (downloadInfo != null)
                {
                    UpdateStatus("Initiating download ...");
                    try
                    {
                        var fileList = await session.GetFileListForDepotAndManifest(
                            appId, downloadInfo, _cancellationTokenSource);
                        UpdateStatus($"Downloading; got {fileList.AllFileNames.Count} file(s) and {fileList.DepotCounter.CompleteDownloadSize / 1024 / 1024:0.00} MiB to download!", -1);

                        await session.DownloadDepot(appId, fileList, (message, percentage) =>
                        {
                            UpdateStatus(message, (int)((percentage ?? -1f) * 100), force: false);
                        }, _cancellationTokenSource);
                        UpdateStatus($"Download completed!", 100);
                    }
                    catch (OperationCanceledException)
                    {
                        UpdateStatus("Cancelled");
                        MessageBoxUtils.Error("Could not get information for download. The requested resource probably doesn't exist or you don't have access to it.");
                    }
                    catch (Exception ex)
                    {
                        UpdateStatus("Failed with exception!");
                        MessageBoxUtils.Ex(ex, "initializing manifest download");
                    }
                }
                else
                {
                    UpdateStatus("Requested Depot or Manifest could not be found!");
                    MessageBoxUtils.Error("Could not find requested depot or manifest info!");
                }
            }
        }
    }
}
