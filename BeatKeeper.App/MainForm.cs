using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeatKeeper.App.Core;
using BeatKeeper.App.Utils;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Repositories;

namespace BeatKeeper.App
{
    public partial class MainForm : Form
    {
        private readonly ArtifactRepository _artifactRepository;

        private List<Artifact> _artifacts = new();

        public MainForm()
        {
            InitializeComponent();
            _artifactRepository = new ArtifactRepository(BSKConstants.Paths.Archives);
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
                SetStatus($"Done, found {_artifacts.Count} archives", 0);
            });
        }

        private void RenderGrids()
        {
            VanillaArchivesListView.Items.Clear();
            BackupArchivesListView.Items.Clear();

            VanillaArchivesListView.BeginUpdate();
            BackupArchivesListView.BeginUpdate();

            VanillaArchivesListView.Items.AddRange(_artifacts
                .Where(a => a.Type == ArtifactType.Vanilla)
                .Select(a =>
                {
                    var item = new ListViewItem
                    {
                        Text = a.GameVersion,
                        Tag = a,
                        ImageKey = "Saber"
                    };
                    item.SubItems.Add(a.HumanReadableSize);
                    return item;
                })
                .ToArray());
            BackupArchivesListView.Items.AddRange(_artifacts
                .Where(a => a.Type == ArtifactType.ModBackup)
                .Select(a =>
                {
                    var item = new ListViewItem
                    {
                        Text = a.Name,
                        Tag = a,
                        ImageKey = "SaberPack"
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
