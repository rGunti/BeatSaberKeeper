using BeatKeeper.App.Services;
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
    public partial class DownloadGameArchiveForm : Form
    {
        public DownloadGameArchiveForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var result = new SteamLoginForm().ShowDialog();
            if (result == DialogResult.OK)
            {
                var session = SteamSession.Instance;
                if (session.IsLoggedIn)
                {
                    LoginStatusLabel.Text = $"Logged in as {SteamSession.Instance.LoggedInUser}";
                }
            }
        }
    }
}
