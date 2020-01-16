using System;
using System.IO;
using System.Windows.Forms;
using BeatKeeper.Windows.Utils;

namespace BeatKeeper.Windows
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            GameInstallDirectorySelector.FileDialog = new OpenFileDialog()
            {
                Title = "Select BeatSaber executable",
                Filter = "Beat Saber Executable|Beat Saber.exe|Executables|*.exe|Any Files|*"
            };
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            GameInstallDirectorySelector.SelectedPath = SettingsUtils.BeatSaberInstallDirectory;
            EnableDebugLoggingCheckbox.Checked = SettingsUtils.EnableDebugLogging;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SettingsUtils.BeatSaberInstallDirectory = !string.IsNullOrWhiteSpace(GameInstallDirectorySelector.SelectedPath)
                && Path.HasExtension(GameInstallDirectorySelector.SelectedPath) ?
                Path.GetDirectoryName(GameInstallDirectorySelector.SelectedPath) :
                GameInstallDirectorySelector.SelectedPath;
            SettingsUtils.EnableDebugLogging = EnableDebugLoggingCheckbox.Checked;
            Close();
        }
    }
}
