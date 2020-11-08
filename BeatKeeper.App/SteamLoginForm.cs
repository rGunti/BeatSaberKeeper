﻿using BeatKeeper.App.Core.Steam;
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
                    RememberLoginCheckbox.Enabled = !_inSteamGuardMode;

                    SteamGuardLabel.Visible = _inSteamGuardMode;
                    SteamGuardTextBox.Visible = _inSteamGuardMode;
                });
            }
        }

        public SteamLoginForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            RunLogin(
                UsernameTextBox.Text,
                PasswordTextBox.Text,
                SteamGuardTextBox.Text,
                RememberLoginCheckbox.Checked);
        }

        private async void RunLogin(string username, string password, string steamGuardCode, bool rememberLogin,
            bool tryLoadingFromSaved = false)
        {
            UsernameTextBox.Text = username;
            RememberLoginCheckbox.Checked = rememberLogin;

            try
            {
                var login = await SteamSession.Instance.Login(
                    username, password,
                    null, InSteamGuardMode ? steamGuardCode : null,
                    rememberLogin, tryLoadingFromSaved);

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
                    MessageBoxUtils.Warn($"Login result was: {login}");
                }
            }
            catch (TimeoutException ex)
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

        private void SteamLoginForm_Load(object sender, EventArgs e)
        {
            var session = SteamSession.Instance;
            if (session.HasSavedLogin)
            {
                string username = session.GetSavedLoginName();
                if (username != null)
                {
                    RunLogin(username, null, null, true, true);
                }
            }
        }
    }
}
