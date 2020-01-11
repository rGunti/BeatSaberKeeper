using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BeatKeeper.Windows
{
    public partial class BackupNameForm : Form
    {
        public BackupNameForm()
        {
            InitializeComponent();
        }

        public string ArchiveName => ArtifactNameTextBox.Text;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ArtifactNameTextBox_TextChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = !string.IsNullOrWhiteSpace(ArchiveName)
                               && !Path.GetInvalidFileNameChars()
                                   .Any(c => ArchiveName.Contains(c));
        }
    }
}
