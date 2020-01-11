using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatKeeper.Windows.Controls
{
    public delegate void FileSelectorUpdatedDelegateEventHandler(object sender, EventArgs e);

    public partial class FileSelector : UserControl
    {
        public event FileSelectorUpdatedDelegateEventHandler FileSelectorChangedPath;

        public FileSelector()
        {
            InitializeComponent();
        }

        public FileDialog FileDialog { get; set; } = new OpenFileDialog();

        public bool TextReadOnly
        {
            get => PathTextField.ReadOnly;
            set => PathTextField.ReadOnly = value;
        }
        public string SelectedPath
        {
            get => PathTextField.Text;
            set => PathTextField.Text = value;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                PathTextField.Text = FileDialog.FileName;
                FileSelectorChangedPath?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
