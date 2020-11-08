using BeatKeeper.App.Core.Steam;
using BeatKeeper.App.Utils;
using System;
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (SteamSession.Instance.IsLoggedIn)
            {
                MessageBoxUtils.NotImplemented();
            } else
            {
                new SteamLoginForm().ShowDialog();
                UpdateFormState();
            }
        }

        private void DownloadGameArchiveForm_Load(object sender, EventArgs e)
        {
            UpdateFormState();
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
    }
}
