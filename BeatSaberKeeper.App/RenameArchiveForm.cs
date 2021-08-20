using System.Windows.Forms;
using BeatSaberKeeper.Kernel.Entities;

namespace BeatSaberKeeper.App
{
    public partial class RenameArchiveForm : Form
    {
        private Artifact _artifact = null;
        
        public RenameArchiveForm()
        {
            InitializeComponent();
        }

        public RenameArchiveForm(Artifact artifact) : this()
        {
            _artifact = artifact;
            ArchiveNameTextBox.Text = artifact.Name;
        }

        public string NewArchiveName => ArchiveNameTextBox.Text;

        public string ConfirmationButtonText
        {
            get => ConfirmButton.Text;
            set => ConfirmButton.Text = value;
        }

        private void RenameArchiveForm_Load(object sender, System.EventArgs e)
        {
            ArchiveNameTextBox.Focus();
        }

        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}