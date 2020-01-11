using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Services;
using BeatKeeper.Kernel.Utils;
using BeatKeeper.Windows.Utils;

namespace BeatKeeper.Windows
{
    public partial class DownloadForm : Form
    {
        private readonly BeatSaberVersionDownloader _beatSaberVersionDownloader;

        public DownloadForm()
        {
            InitializeComponent();
            _beatSaberVersionDownloader = new BeatSaberVersionDownloader(
                SteamCmdServiceFactory.Instance.Build(),
                SettingsUtils.LastSteamUsername);
        }

        private void SetEnabled(bool enabled)
        {
            this.RunInUiThread(() => { Enabled = enabled; });
        }

        private void SetStatus(string status)
        {
            this.RunInUiThread(() => { DownloadStatusText.Text = status; });
        }

        private void SetProgress(int value, int max)
        {
            this.RunInUiThread(() =>
            {
                DownloadProgressBar.Maximum = max;
                DownloadProgressBar.Value = Math.Min(value, max);
            });
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            DownloadVersionComboBox.Items.Clear();
            DownloadVersionComboBox.Items.AddRange(
                _beatSaberVersionDownloader.Artifacts
                    .OrderByDescending(a => a.GameVersion)
                    .ToArray());
        }

        private void DownloadStartButton_Click(object sender, EventArgs e)
        {
            if (!(DownloadVersionComboBox.SelectedItem is Artifact downloadArtifact))
            {
                return;
            }

            SetEnabled(false);
            this.RunInBackgroundThread(() =>
            {
                this.RunInUiThread(() =>
                {
                    SetStatus($"Downloading BeatSaber v{downloadArtifact.GameVersion} ...");
                    DownloadProgressBar.Style = ProgressBarStyle.Marquee;
                });

                _beatSaberVersionDownloader.DownloadArtifact(downloadArtifact);

                SetStatus($"Packing BeatSaber v{downloadArtifact.GameVersion} ...");
                BeatKeeperPackageProcessor.PackVanillaArtifactV1(
                    ClientPathUtils.SteamCmdDownloadFolder,
                    ClientPathUtils.VanillaArchiveFolder,
                    downloadArtifact.GameVersion
                );
            }, () =>
            {
                SetStatus("Download complete");
                SetEnabled(true);
                DownloadProgressBar.Style = ProgressBarStyle.Continuous;
                Close();
            });
        }

        private void DownloadVersionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DownloadStartButton.Enabled = DownloadVersionComboBox.SelectedItem != null;
        }

        private void DownloadAllVersionsButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Are you sure you want to download all BeatSaver versions?",
                    "Download all BeatSaber versions",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            SetEnabled(false);
            SetStatus("Initializing download ...");

            this.RunInBackgroundThread(() =>
            {
                var total = _beatSaberVersionDownloader.AppVersionList.Count();
                var i = 0;

                foreach (var artifact in _beatSaberVersionDownloader.Artifacts
                    .OrderByDescending(v => v.GameVersion))
                {
                    i++;
                    SetStatus($"({i}/{total}) Downloading BeatSaber {artifact} ...");
                    SetProgress(i - 1, total * 2);
                    if (Directory.Exists(ClientPathUtils.SteamCmdDownloadFolder))
                    {
                        Directory.Delete(ClientPathUtils.SteamCmdDownloadFolder, true);
                    }
                    _beatSaberVersionDownloader.DownloadArtifact(artifact);

                    SetStatus($"({i}/{total}) Packing BeatSaber {artifact} ...");
                    SetProgress(i, total * 2);
                    BeatKeeperPackageProcessor.PackVanillaArtifactV1(
                        ClientPathUtils.SteamCmdDownloadFolder,
                        ClientPathUtils.VanillaArchiveFolder,
                        artifact.GameVersion);
                }
            }, () =>
            {
                SetStatus("Download complete");
                SetEnabled(true);
                Close();
            });
        }
    }
}
