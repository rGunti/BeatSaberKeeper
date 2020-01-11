using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeatKeeper.Windows.Utils;

namespace BeatKeeper.Windows
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public string Username => UsernameTextBox.Text;
        public string Password => PasswordTextBox.Text;
        public string TwoFaCode => TwoFaCodeTextBox.Text;

        private void LoginForm_Load(object sender, EventArgs e)
        {
            UsernameTextBox.Text = SettingsUtils.LastSteamUsername;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            LoginButton.Enabled = !string.IsNullOrEmpty(UsernameTextBox.Text)
                                  && (!string.IsNullOrEmpty(PasswordTextBox.Text) || !PasswordTextBox.Enabled);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            SettingsUtils.LastSteamUsername = Username;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
