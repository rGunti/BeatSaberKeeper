using BeatKeeper.BeatSaber;
using BeatKeeper.Steam;
using BeatKeeper.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BeatKeeper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            VersionComboBox.Items.AddRange(
                BeatSaberVersions.AppVersionList.Reverse().ToArray());
            InitSteamCmdButton.Enabled = !SteamCmdExecutor.IsInitialized;
            SteamUsernameTextBox.Enabled = SteamCmdExecutor.IsInitialized;

            using (new LoadingCursor())
                BeatSaberPacker.Cleanup();

            UpdatePackedVersions();
        }

        private void UpdatePackedVersions()
        {
            PackedVersionComboBox.Items.Clear();
            PackedVersionComboBox.Items.AddRange(
                BeatSaberPacker.GetPackagedVersions());
        }

        private void OpenSteamConsoleButton_Click(object sender, EventArgs e)
        {
            Process.Start("steam://open/console");
        }

        private void InitSteamCmdButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (new LoadingCursor())
                {
                    SteamCmdExecutor.Initialize();
                }
                MessageBox.Show(
                    "SteamCMD is initialized!",
                    "Initialize SteamCMD",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                InitSteamCmdButton.Enabled = !SteamCmdExecutor.IsInitialized;
                SteamUsernameTextBox.Enabled = SteamCmdExecutor.IsInitialized;
            } catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to initialize SteamCMD!\n\n{ex.Message}",
                    "Initialize SteamCMD",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RunSteamCmdDownloadButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "You may need to enter your login credentials in the following console. " +
                "Also keep your Steam Authenticator ready.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            var cursor = new LoadingCursor();
            var p = SteamCmdExecutor.RunDownload(
                SteamUsernameTextBox.Text,
                BeatSaberVersions.GetVersion(VersionComboBox.SelectedItem as string).ManifestId);
            this.Enabled = false;
            Threading.WaitUntil(() =>
            {
                return p.HasExited;
            }, () =>
            {
                this.BeginInvoke(new MethodInvoker(() => {
                    p.Dispose();
                    this.Enabled = true;
                    ArchiveButton.Enabled = Directory.Exists(BeatSaberPacker.SourcePath);
                }));
            });
        }

        private void SteamUsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            RunSteamCmdDownloadButton.Enabled = VersionComboBox.SelectedItem != null
                && SteamUsernameTextBox.Enabled
                && !string.IsNullOrWhiteSpace(SteamUsernameTextBox.Text);
        }

        private void VersionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SteamUsernameTextBox_TextChanged(sender, e);
        }

        private void ArchiveButton_Click(object sender, EventArgs e)
        {
            var appVersion = BeatSaberVersions.GetVersion(VersionComboBox.SelectedItem as string);

            var cursor = new LoadingCursor();
            ArchiveButton.Enabled = false;
            Threading.RunInBackgroundThread(() =>
            {
                var packer = new BeatSaberPacker(appVersion);
                packer.Run();
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    ArchiveButton.Enabled = true;
                    cursor.Dispose();

                    MessageBox.Show(
                        $"Archive created: {Path.GetFileName(BeatSaberPacker.GetArchiveFileName(appVersion))}",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    UpdatePackedVersions();
                }));
            });
        }

        private void LaunchGameButton_Click(object sender, EventArgs e)
        {
            var version = PackedVersionComboBox.SelectedItem as string;
            var installPath = BeatSaberInstallPathTextBox.Text;

            Enabled = false;
            Threading.RunInBackgroundThread(() => {
                BeatSaberPacker.RunVersion(version, installPath);

                BeginInvoke(new MethodInvoker(() => {
                    Enabled = true;
                }));
            });
        }
    }
}
