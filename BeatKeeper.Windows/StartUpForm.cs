using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Services;
using BeatKeeper.Windows.Utils;

namespace BeatKeeper.Windows
{
    public partial class StartUpForm : Form
    {
        private readonly Artifact _artifact;

        public StartUpForm(Artifact artifact)
        {
            _artifact = artifact;
            InitializeComponent();
        }

        private void SetStatus(string status)
        {
            this.RunInUiThread(() => { StatusLabel.Text = status; });
        }

        private void StartUpForm_Load(object sender, EventArgs e)
        {
            SetStatus($"Preparing {_artifact.Name} ...");
            this.RunInBackgroundThread(() =>
            {
                SetStatus($"Unpacking {_artifact.Name} ...");
                BeatKeeperPackageProcessor.UnpackArchive(
                    _artifact.FullPath,
                    SettingsUtils.BeatSaberInstallDirectory);

                SetStatus("Starting game ...");
                Process.Start(ClientPathUtils.BeatSaberExecutable);
                Thread.Sleep(1000);
            }, () =>
            {
                if (!Disposing)
                {
                    Close();
                }
            });
        }
    }
}
