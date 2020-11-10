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

        private void UpdateStatus(string status)
        {
            this.RunInUiThread(() =>
            {
                StatusLabel.Text = status;
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
            } else
            {
                Login();
            }
        }

        private void DownloadGameArchiveForm_Load(object sender, EventArgs e)
        {
            UpdateFormState();
            Login();
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
            } else
            {
                MessageBoxUtils.Error("Your account does not have the required license to download Beat Saber.\n\n" +
                    "Please login with a Steam account owning a valid license or purchase the game.");
                Logout();
            }
        }

        private async void DownloadArchiveButton_Click(object sender, EventArgs e)
        {
            var session = SteamSession.Instance;
            if (session.IsConnected && session.IsLoggedIn)
            {
                var downloadInfo = await session.GetDepotDownloadInfo(
                    BSKConstants.Steam.BEAT_SABER_APP_ID,
                    BSKConstants.Steam.BEAT_SABER_DEPOT_ID,
                    BSKConstants.Steam.TEST_MANIFEST_ID,
                    BSKConstants.Steam.DEFAULT_BRANCH);
                if (downloadInfo != null)
                {
                    var cts = new CancellationTokenSource();
                    session.ProcessDepotManifestAndFiles(BSKConstants.Steam.BEAT_SABER_APP_ID, downloadInfo, cts);
                } else
                {
                    MessageBoxUtils.Error("I screwed up :(");
                }
            }
        }
    }
}
