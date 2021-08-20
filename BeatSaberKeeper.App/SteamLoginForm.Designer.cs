
namespace BeatSaberKeeper.App
{
    partial class SteamLoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelLoginButton = new System.Windows.Forms.Button();
            this.SteamGuardLabel = new System.Windows.Forms.Label();
            this.SteamGuardTextBox = new System.Windows.Forms.TextBox();
            this.RememberLoginCheckbox = new System.Windows.Forms.CheckBox();
            this.LoggingInStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameTextBox.Location = new System.Drawing.Point(106, 10);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(234, 20);
            this.UsernameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordTextBox.Location = new System.Drawing.Point(106, 36);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(234, 20);
            this.PasswordTextBox.TabIndex = 3;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(275, 89);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(64, 20);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "Login";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelLoginButton
            // 
            this.CancelLoginButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelLoginButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelLoginButton.Location = new System.Drawing.Point(206, 89);
            this.CancelLoginButton.Name = "CancelLoginButton";
            this.CancelLoginButton.Size = new System.Drawing.Size(64, 20);
            this.CancelLoginButton.TabIndex = 5;
            this.CancelLoginButton.Text = "Cancel";
            this.CancelLoginButton.UseVisualStyleBackColor = true;
            this.CancelLoginButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SteamGuardLabel
            // 
            this.SteamGuardLabel.AutoSize = true;
            this.SteamGuardLabel.Location = new System.Drawing.Point(10, 63);
            this.SteamGuardLabel.Name = "SteamGuardLabel";
            this.SteamGuardLabel.Size = new System.Drawing.Size(97, 13);
            this.SteamGuardLabel.TabIndex = 6;
            this.SteamGuardLabel.Text = "SteamGuard Code:";
            this.SteamGuardLabel.Visible = false;
            // 
            // SteamGuardTextBox
            // 
            this.SteamGuardTextBox.Location = new System.Drawing.Point(106, 61);
            this.SteamGuardTextBox.Name = "SteamGuardTextBox";
            this.SteamGuardTextBox.Size = new System.Drawing.Size(86, 20);
            this.SteamGuardTextBox.TabIndex = 7;
            this.SteamGuardTextBox.Visible = false;
            // 
            // RememberLoginCheckbox
            // 
            this.RememberLoginCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RememberLoginCheckbox.AutoSize = true;
            this.RememberLoginCheckbox.Location = new System.Drawing.Point(10, 91);
            this.RememberLoginCheckbox.Name = "RememberLoginCheckbox";
            this.RememberLoginCheckbox.Size = new System.Drawing.Size(106, 17);
            this.RememberLoginCheckbox.TabIndex = 8;
            this.RememberLoginCheckbox.Text = "Remember Login";
            this.RememberLoginCheckbox.UseVisualStyleBackColor = true;
            // 
            // LoggingInStatusLabel
            // 
            this.LoggingInStatusLabel.Location = new System.Drawing.Point(198, 64);
            this.LoggingInStatusLabel.Name = "LoggingInStatusLabel";
            this.LoggingInStatusLabel.Size = new System.Drawing.Size(140, 23);
            this.LoggingInStatusLabel.TabIndex = 9;
            this.LoggingInStatusLabel.Text = "Logging in...";
            this.LoggingInStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.LoggingInStatusLabel.Visible = false;
            // 
            // SteamLoginForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelLoginButton;
            this.ClientSize = new System.Drawing.Size(350, 120);
            this.Controls.Add(this.LoggingInStatusLabel);
            this.Controls.Add(this.RememberLoginCheckbox);
            this.Controls.Add(this.SteamGuardTextBox);
            this.Controls.Add(this.SteamGuardLabel);
            this.Controls.Add(this.CancelLoginButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SteamLoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login to Steam";
            this.Load += new System.EventHandler(this.SteamLoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label LoggingInStatusLabel;

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelLoginButton;
        private System.Windows.Forms.Label SteamGuardLabel;
        private System.Windows.Forms.TextBox SteamGuardTextBox;
        private System.Windows.Forms.CheckBox RememberLoginCheckbox;
    }
}