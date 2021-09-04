using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BeatSaberKeeper.App.Utils;
using BeatSaberKeeper.Kernel.V2;
using PandaDotNet.Utils;

namespace BeatSaberKeeper.App.Tools
{
    public partial class HistoryExplorer : Form
    {
        private readonly string _archiveFilePath;
        private readonly V2CompressionInterface _compressionInterface;
        private V2ArchiveMetaData _metaData;
        private IImmutableDictionary<DateTime, List<CommitFile>> _changeSet;

        public HistoryExplorer(string archiveFilePath, V2CompressionInterface compressionInterface)
        {
            InitializeComponent();
            _archiveFilePath = archiveFilePath;
            _compressionInterface = compressionInterface;
        }

        private void HistoryExplorer_Load(object sender, System.EventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            this.RunInBackgroundThread(() =>
            {
                _metaData = _compressionInterface.ReadMetaDataFromArchive(_archiveFilePath) as V2ArchiveMetaData;
                _changeSet = _metaData?.GetChangeSets() ?? new Dictionary<DateTime, List<CommitFile>>()
                    .ToImmutableDictionary();
            }, () =>
            {
                RenderVersionList();
                RenderFileList(null);
            });
        }

        private void RenderFileList(DateTime? selectedVersion)
        {
            FileListView.BeginUpdate();
            FileListView.Items.Clear();

            if (selectedVersion.HasValue)
            {
                FileListView.Items.AddRange(_changeSet[selectedVersion.Value]
                    .OrderBy(f => f.Path)
                    .Select(f =>
                    {
                        Commit commit = f.Commits.FirstOrDefault(c => c.CommitDate == selectedVersion.Value);
                        var lvi = new ListViewItem
                        {
                            Text = f.Path,
                            ToolTipText = f.Path,
                            Tag = f
                        };
                        if (commit?.FileDeleted ?? false)
                        {
                            lvi.SubItems.AddRange(new [] { "-", "deleted" });
                        }
                        else if (commit != null)
                        {
                            lvi.SubItems.Add(commit.Size.ToHumanReadableFileSize() ?? "-");
                            lvi.SubItems.Add(commit.Hash ?? "-");
                        }
                        else
                        {
                            lvi.SubItems.AddRange(new [] { "-", "-" });
                        }
                        return lvi;
                    })
                    .ToArray());
            }
            
            FileListView.EndUpdate();
        }

        private void RenderVersionList()
        {
            VersionListView.BeginUpdate();
            VersionListView.Items.Clear();

            foreach ((DateTime changeDate, List<CommitFile> fileList) in _metaData.GetChangeSets()
                .OrderByDescending(c => c.Key))
            {
                ListViewItem lvi = VersionListView.Items.Add(new ListViewItem
                {
                    Text = @$"{changeDate:g}",
                    Tag = changeDate
                });
                lvi.SubItems.Add($"{fileList.Count}");
            }

            VersionListView.EndUpdate();
        }

        private void UpdateFileList()
        {
            DateTime? selectedVersion = GetSelectedVersion();
            RenderFileList(selectedVersion);
        }

        private DateTime? GetSelectedVersion()
        {
            var selectedItems = VersionListView.SelectedItems;
            if (selectedItems.Count != 1)
            {
                return null;
            }

            return selectedItems[0].Tag as DateTime? ?? default;
        }

        private void DoVersionContextAction(Func<DateTime?> extractor, Action<DateTime> action)
        {
            DateTime? artifact = extractor();
            if (artifact.HasValue)
            {
                action(artifact.Value);
            }
        }

        private void DoVersionContextAction(Action<DateTime> action)
            => DoVersionContextAction(GetSelectedVersion, action);

        private void VersionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFileList();
        }
    }
}