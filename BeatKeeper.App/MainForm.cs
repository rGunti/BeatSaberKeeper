using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatKeeper.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void DownloadVanillaArchiveMenuItem_Click(object sender, EventArgs e)
        {
            new DownloadGameArchiveForm().ShowDialog();
        }
    }
}
