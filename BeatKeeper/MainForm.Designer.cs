namespace BeatKeeper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VersionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SteamUsernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RunSteamCmdDownloadButton = new System.Windows.Forms.Button();
            this.InitSteamCmdButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ArchiveButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BeatSaberInstallPathTextBox = new System.Windows.Forms.TextBox();
            this.BeatSaberInstallPathButton = new System.Windows.Forms.Button();
            this.PackedVersionComboBox = new System.Windows.Forms.ComboBox();
            this.LaunchGameButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.VersionComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Select a version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version:";
            // 
            // VersionComboBox
            // 
            this.VersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionComboBox.FormattingEnabled = true;
            this.VersionComboBox.Location = new System.Drawing.Point(143, 22);
            this.VersionComboBox.Name = "VersionComboBox";
            this.VersionComboBox.Size = new System.Drawing.Size(121, 23);
            this.VersionComboBox.TabIndex = 0;
            this.VersionComboBox.SelectedIndexChanged += new System.EventHandler(this.VersionComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SteamUsernameTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.RunSteamCmdDownloadButton);
            this.groupBox2.Controls.Add(this.InitSteamCmdButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. Download Version";
            // 
            // SteamUsernameTextBox
            // 
            this.SteamUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SteamUsernameTextBox.Enabled = false;
            this.SteamUsernameTextBox.Location = new System.Drawing.Point(111, 51);
            this.SteamUsernameTextBox.Name = "SteamUsernameTextBox";
            this.SteamUsernameTextBox.Size = new System.Drawing.Size(200, 23);
            this.SteamUsernameTextBox.TabIndex = 5;
            this.SteamUsernameTextBox.TextChanged += new System.EventHandler(this.SteamUsernameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Steam Username:";
            // 
            // RunSteamCmdDownloadButton
            // 
            this.RunSteamCmdDownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RunSteamCmdDownloadButton.Enabled = false;
            this.RunSteamCmdDownloadButton.Location = new System.Drawing.Point(317, 50);
            this.RunSteamCmdDownloadButton.Name = "RunSteamCmdDownloadButton";
            this.RunSteamCmdDownloadButton.Size = new System.Drawing.Size(137, 23);
            this.RunSteamCmdDownloadButton.TabIndex = 3;
            this.RunSteamCmdDownloadButton.Text = "Run Download";
            this.RunSteamCmdDownloadButton.UseVisualStyleBackColor = true;
            this.RunSteamCmdDownloadButton.Click += new System.EventHandler(this.RunSteamCmdDownloadButton_Click);
            // 
            // InitSteamCmdButton
            // 
            this.InitSteamCmdButton.Location = new System.Drawing.Point(6, 22);
            this.InitSteamCmdButton.Name = "InitSteamCmdButton";
            this.InitSteamCmdButton.Size = new System.Drawing.Size(130, 23);
            this.InitSteamCmdButton.TabIndex = 2;
            this.InitSteamCmdButton.Text = "Initialize SteamCMD";
            this.InitSteamCmdButton.UseVisualStyleBackColor = true;
            this.InitSteamCmdButton.Click += new System.EventHandler(this.InitSteamCmdButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ArchiveButton);
            this.groupBox3.Location = new System.Drawing.Point(12, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 53);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3. Archive BeatSaber Version";
            // 
            // ArchiveButton
            // 
            this.ArchiveButton.Enabled = false;
            this.ArchiveButton.Location = new System.Drawing.Point(6, 22);
            this.ArchiveButton.Name = "ArchiveButton";
            this.ArchiveButton.Size = new System.Drawing.Size(202, 23);
            this.ArchiveButton.TabIndex = 0;
            this.ArchiveButton.Text = "Archive Downloaded Version";
            this.ArchiveButton.UseVisualStyleBackColor = true;
            this.ArchiveButton.Click += new System.EventHandler(this.ArchiveButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.PackedVersionComboBox);
            this.groupBox4.Controls.Add(this.LaunchGameButton);
            this.groupBox4.Controls.Add(this.BeatSaberInstallPathButton);
            this.groupBox4.Controls.Add(this.BeatSaberInstallPathTextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 83);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "4. Run different version";
            // 
            // BeatSaberInstallPathTextBox
            // 
            this.BeatSaberInstallPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BeatSaberInstallPathTextBox.Location = new System.Drawing.Point(6, 22);
            this.BeatSaberInstallPathTextBox.Name = "BeatSaberInstallPathTextBox";
            this.BeatSaberInstallPathTextBox.Size = new System.Drawing.Size(414, 23);
            this.BeatSaberInstallPathTextBox.TabIndex = 0;
            this.BeatSaberInstallPathTextBox.Text = "C:\\Games\\Steam\\steamapps\\common\\Beat Saber";
            // 
            // BeatSaberInstallPathButton
            // 
            this.BeatSaberInstallPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BeatSaberInstallPathButton.Location = new System.Drawing.Point(426, 22);
            this.BeatSaberInstallPathButton.Name = "BeatSaberInstallPathButton";
            this.BeatSaberInstallPathButton.Size = new System.Drawing.Size(28, 23);
            this.BeatSaberInstallPathButton.TabIndex = 1;
            this.BeatSaberInstallPathButton.Text = "...";
            this.BeatSaberInstallPathButton.UseVisualStyleBackColor = true;
            // 
            // PackedVersionComboBox
            // 
            this.PackedVersionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PackedVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PackedVersionComboBox.FormattingEnabled = true;
            this.PackedVersionComboBox.Location = new System.Drawing.Point(193, 51);
            this.PackedVersionComboBox.Name = "PackedVersionComboBox";
            this.PackedVersionComboBox.Size = new System.Drawing.Size(71, 23);
            this.PackedVersionComboBox.TabIndex = 2;
            // 
            // LaunchGameButton
            // 
            this.LaunchGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LaunchGameButton.Location = new System.Drawing.Point(270, 51);
            this.LaunchGameButton.Name = "LaunchGameButton";
            this.LaunchGameButton.Size = new System.Drawing.Size(184, 23);
            this.LaunchGameButton.TabIndex = 3;
            this.LaunchGameButton.Text = "Run Game with given Version";
            this.LaunchGameButton.UseVisualStyleBackColor = true;
            this.LaunchGameButton.Click += new System.EventHandler(this.LaunchGameButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "MainForm";
            this.Text = "BeatKeeper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox VersionComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button InitSteamCmdButton;
        private System.Windows.Forms.Button RunSteamCmdDownloadButton;
        private System.Windows.Forms.TextBox SteamUsernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ArchiveButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BeatSaberInstallPathButton;
        private System.Windows.Forms.TextBox BeatSaberInstallPathTextBox;
        private System.Windows.Forms.Button LaunchGameButton;
        private System.Windows.Forms.ComboBox PackedVersionComboBox;
    }
}

