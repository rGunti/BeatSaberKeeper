using BeatKeeper.App.Core;
using BeatKeeper.App.Core.Steam;
using BeatKeeper.App.Utils;
using System;
using System.Threading;
using System.Windows.Forms;

namespace BeatKeeper.App
{
    public partial class DownloadGameArchiveForm : Form
    {
        public DownloadGameArchiveForm()
        {
            InitializeComponent();
        }

        private void UpdateStatus(string status, int value = 0, int maxValue = 100)
        {
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
            });
        }

        private void Login()
        {
            _ = new SteamLoginForm().ShowDialog();
            UpdateFormState();
            if (SteamSession.Instance.IsLoggedIn)
            {
                this.RunInBackgroundThread(() =>
                {
                    UpdateLicenseInfo();
                }, () => { });
            }
        }

        private void Logout()
        {
            MessageBoxUtils.NotImplemented("Logging out");
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

            SteamManifestIdTextBox.Text = $"{BSKConstants.Steam.TEST_MANIFEST_ID}";
            SteamAppIdTextBox.Text = $"{BSKConstants.Steam.BEAT_SABER_APP_ID}";
            SteamDepotIdTextBox.Text = $"{BSKConstants.Steam.BEAT_SABER_DEPOT_ID}";
            SteamBranchTextBox.Text = BSKConstants.Steam.DEFAULT_BRANCH;
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
                    LoginStatusLabel.Text = $"Logged in as {session.LoggedInUser}";
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
                MessageBoxUtils.Info("License checked. Your account is in posession of a valid Beat Saber license.");
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
                    var cts = new CancellationTokenSource();
                    try
                    {
                        var fileList = await session.GetFileListForDepotAndManifest(appId, downloadInfo, cts);
                        UpdateStatus($"Downloading; got {fileList.AllFileNames.Count} file(s) and {fileList.DepotCounter.CompleteDownloadSize / 1024 / 1024:0.00} MiB to download!", -1);

                        await session.DownloadDepot(appId, fileList, cts);
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
