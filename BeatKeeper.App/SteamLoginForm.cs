using BeatKeeper.App.Services;
using BeatKeeper.App.Utils;
using System;
using System.Windows.Forms;

namespace BeatKeeper.App
{
    public partial class SteamLoginForm : Form
    {
        private bool _inSteamGuardMode;
        public bool InSteamGuardMode
        {
            get => _inSteamGuardMode;
            set
            {
                _inSteamGuardMode = value;
                this.RunInUiThread(() =>
                {
                    UsernameTextBox.Enabled = !_inSteamGuardMode;
                    PasswordTextBox.Enabled = !_inSteamGuardMode;

                    SteamGuardLabel.Visible = _inSteamGuardMode;
                    SteamGuardTextBox.Visible = _inSteamGuardMode;
                });
            }
        }

        public SteamLoginForm()
        {
            InitializeComponent();
        }

        private async void OkButton_Click(object sender, EventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;
            var steamGuardCode = SteamGuardTextBox.Text;

            try
            {
                var login = await SteamSession.Instance.Login(username, password,
                null, InSteamGuardMode ? steamGuardCode : null);

                if (login == SteamLoginResult.Requires2FA)
                {
                    InSteamGuardMode = true;
                    SteamGuardTextBox.Focus();

                    await SteamSession.Instance.Disconnect();
                }
                else if (login == SteamLoginResult.Success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    InSteamGuardMode = false;
                    MessageBox.Show($"Login result was: {login}");
                }
            } catch (TimeoutException ex)
            {
                MessageBoxUtils.Error($"Login process timed out!\n\n{ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (InSteamGuardMode)
            {
                PasswordTextBox.Text = string.Empty;
                SteamGuardTextBox.Text = string.Empty;
                InSteamGuardMode = false;
            } else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
