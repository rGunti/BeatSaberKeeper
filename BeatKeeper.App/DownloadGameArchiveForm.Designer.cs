﻿
namespace BeatKeeper.App
{
    partial class DownloadGameArchiveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadGameArchiveForm));
            this.LoginButton = new System.Windows.Forms.Button();
            this.LoginStatusLabel = new System.Windows.Forms.Label();
            this.DownloaderPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SingleVersionTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.GameVersionDropdown = new System.Windows.Forms.ComboBox();
            this.DownloadArchiveButton = new System.Windows.Forms.Button();
            this.AdvancedVersionTab = new System.Windows.Forms.TabPage();
            this.SteamBranchTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.EditAdvancedValuesCheckBox = new System.Windows.Forms.CheckBox();
            this.StartAdvancedDownloadButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SteamDepotIdTextBox = new System.Windows.Forms.TextBox();
            this.SteamAppIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SteamManifestIdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.DownloadStatusProgressBarr = new System.Windows.Forms.ProgressBar();
            this.DownloaderPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SingleVersionTab.SuspendLayout();
            this.AdvancedVersionTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginButton.Location = new System.Drawing.Point(367, 10);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(64, 20);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginStatusLabel
            // 
            this.LoginStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginStatusLabel.Location = new System.Drawing.Point(115, 10);
            this.LoginStatusLabel.Name = "LoginStatusLabel";
            this.LoginStatusLabel.Size = new System.Drawing.Size(246, 20);
            this.LoginStatusLabel.TabIndex = 1;
            this.LoginStatusLabel.Text = "Logged out";
            this.LoginStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownloaderPanel
            // 
            this.DownloaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloaderPanel.Controls.Add(this.tabControl1);
            this.DownloaderPanel.Controls.Add(this.StatusLabel);
            this.DownloaderPanel.Controls.Add(this.DownloadStatusProgressBarr);
            this.DownloaderPanel.Enabled = false;
            this.DownloaderPanel.Location = new System.Drawing.Point(0, 36);
            this.DownloaderPanel.Name = "DownloaderPanel";
            this.DownloaderPanel.Size = new System.Drawing.Size(441, 227);
            this.DownloaderPanel.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.SingleVersionTab);
            this.tabControl1.Controls.Add(this.AdvancedVersionTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(441, 175);
            this.tabControl1.TabIndex = 5;
            // 
            // SingleVersionTab
            // 
            this.SingleVersionTab.Controls.Add(this.label1);
            this.SingleVersionTab.Controls.Add(this.GameVersionDropdown);
            this.SingleVersionTab.Controls.Add(this.DownloadArchiveButton);
            this.SingleVersionTab.Location = new System.Drawing.Point(4, 22);
            this.SingleVersionTab.Name = "SingleVersionTab";
            this.SingleVersionTab.Padding = new System.Windows.Forms.Padding(3);
            this.SingleVersionTab.Size = new System.Drawing.Size(433, 149);
            this.SingleVersionTab.TabIndex = 0;
            this.SingleVersionTab.Text = "Single Version";
            this.SingleVersionTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 82);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // GameVersionDropdown
            // 
            this.GameVersionDropdown.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.GameVersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameVersionDropdown.FormattingEnabled = true;
            this.GameVersionDropdown.Location = new System.Drawing.Point(8, 93);
            this.GameVersionDropdown.Name = "GameVersionDropdown";
            this.GameVersionDropdown.Size = new System.Drawing.Size(417, 21);
            this.GameVersionDropdown.TabIndex = 1;
            // 
            // DownloadArchiveButton
            // 
            this.DownloadArchiveButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadArchiveButton.Location = new System.Drawing.Point(317, 120);
            this.DownloadArchiveButton.Name = "DownloadArchiveButton";
            this.DownloadArchiveButton.Size = new System.Drawing.Size(108, 20);
            this.DownloadArchiveButton.TabIndex = 2;
            this.DownloadArchiveButton.Text = "Download Archive";
            this.DownloadArchiveButton.UseVisualStyleBackColor = true;
            this.DownloadArchiveButton.Click += new System.EventHandler(this.DownloadArchiveButton_Click);
            // 
            // AdvancedVersionTab
            // 
            this.AdvancedVersionTab.Controls.Add(this.SteamBranchTextBox);
            this.AdvancedVersionTab.Controls.Add(this.label7);
            this.AdvancedVersionTab.Controls.Add(this.EditAdvancedValuesCheckBox);
            this.AdvancedVersionTab.Controls.Add(this.StartAdvancedDownloadButton);
            this.AdvancedVersionTab.Controls.Add(this.label6);
            this.AdvancedVersionTab.Controls.Add(this.label5);
            this.AdvancedVersionTab.Controls.Add(this.label4);
            this.AdvancedVersionTab.Controls.Add(this.SteamDepotIdTextBox);
            this.AdvancedVersionTab.Controls.Add(this.SteamAppIdTextBox);
            this.AdvancedVersionTab.Controls.Add(this.label3);
            this.AdvancedVersionTab.Controls.Add(this.SteamManifestIdTextBox);
            this.AdvancedVersionTab.Controls.Add(this.label2);
            this.AdvancedVersionTab.Location = new System.Drawing.Point(4, 22);
            this.AdvancedVersionTab.Name = "AdvancedVersionTab";
            this.AdvancedVersionTab.Padding = new System.Windows.Forms.Padding(3);
            this.AdvancedVersionTab.Size = new System.Drawing.Size(433, 149);
            this.AdvancedVersionTab.TabIndex = 1;
            this.AdvancedVersionTab.Text = "Advanced Download";
            this.AdvancedVersionTab.UseVisualStyleBackColor = true;
            // 
            // SteamBranchTextBox
            // 
            this.SteamBranchTextBox.Location = new System.Drawing.Point(192, 121);
            this.SteamBranchTextBox.Name = "SteamBranchTextBox";
            this.SteamBranchTextBox.ReadOnly = true;
            this.SteamBranchTextBox.Size = new System.Drawing.Size(86, 20);
            this.SteamBranchTextBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(192, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Steam Branch:";
            // 
            // EditAdvancedValuesCheckBox
            // 
            this.EditAdvancedValuesCheckBox.AutoSize = true;
            this.EditAdvancedValuesCheckBox.Location = new System.Drawing.Point(8, 79);
            this.EditAdvancedValuesCheckBox.Name = "EditAdvancedValuesCheckBox";
            this.EditAdvancedValuesCheckBox.Size = new System.Drawing.Size(174, 17);
            this.EditAdvancedValuesCheckBox.TabIndex = 9;
            this.EditAdvancedValuesCheckBox.Text = "I know what I\'m doing, promise!";
            this.EditAdvancedValuesCheckBox.UseVisualStyleBackColor = true;
            this.EditAdvancedValuesCheckBox.CheckedChanged += new System.EventHandler(this.EditAdvancedValuesCheckBox_CheckedChanged);
            // 
            // StartAdvancedDownloadButton
            // 
            this.StartAdvancedDownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartAdvancedDownloadButton.Location = new System.Drawing.Point(323, 123);
            this.StartAdvancedDownloadButton.Name = "StartAdvancedDownloadButton";
            this.StartAdvancedDownloadButton.Size = new System.Drawing.Size(102, 20);
            this.StartAdvancedDownloadButton.TabIndex = 8;
            this.StartAdvancedDownloadButton.Text = "Start Download";
            this.StartAdvancedDownloadButton.UseVisualStyleBackColor = true;
            this.StartAdvancedDownloadButton.Click += new System.EventHandler(this.StartAdvancedDownloadButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Do not change these values unless you know what you\'re doing.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Steam Depot ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Steam App ID:";
            // 
            // SteamDepotIdTextBox
            // 
            this.SteamDepotIdTextBox.Location = new System.Drawing.Point(100, 121);
            this.SteamDepotIdTextBox.Name = "SteamDepotIdTextBox";
            this.SteamDepotIdTextBox.ReadOnly = true;
            this.SteamDepotIdTextBox.Size = new System.Drawing.Size(86, 20);
            this.SteamDepotIdTextBox.TabIndex = 4;
            // 
            // SteamAppIdTextBox
            // 
            this.SteamAppIdTextBox.Location = new System.Drawing.Point(8, 121);
            this.SteamAppIdTextBox.Name = "SteamAppIdTextBox";
            this.SteamAppIdTextBox.ReadOnly = true;
            this.SteamAppIdTextBox.Size = new System.Drawing.Size(86, 20);
            this.SteamAppIdTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Steam Manifest ID:";
            // 
            // SteamManifestIdTextBox
            // 
            this.SteamManifestIdTextBox.Location = new System.Drawing.Point(111, 35);
            this.SteamManifestIdTextBox.Name = "SteamManifestIdTextBox";
            this.SteamManifestIdTextBox.Size = new System.Drawing.Size(129, 20);
            this.SteamManifestIdTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(417, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Use this function at your own risk!";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 178);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(38, 13);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "Ready";
            // 
            // DownloadStatusProgressBarr
            // 
            this.DownloadStatusProgressBarr.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadStatusProgressBarr.Location = new System.Drawing.Point(12, 194);
            this.DownloadStatusProgressBarr.Name = "DownloadStatusProgressBarr";
            this.DownloadStatusProgressBarr.Size = new System.Drawing.Size(417, 20);
            this.DownloadStatusProgressBarr.TabIndex = 3;
            // 
            // DownloadGameArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 262);
            this.Controls.Add(this.DownloaderPanel);
            this.Controls.Add(this.LoginStatusLabel);
            this.Controls.Add(this.LoginButton);
            this.MaximizeBox = false;
            this.Name = "DownloadGameArchiveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Beat Saber Vanilla Archive Downloader";
            this.Load += new System.EventHandler(this.DownloadGameArchiveForm_Load);
            this.DownloaderPanel.ResumeLayout(false);
            this.DownloaderPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.SingleVersionTab.ResumeLayout(false);
            this.AdvancedVersionTab.ResumeLayout(false);
            this.AdvancedVersionTab.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label LoginStatusLabel;
        private System.Windows.Forms.Panel DownloaderPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox GameVersionDropdown;
        private System.Windows.Forms.Button DownloadArchiveButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.ProgressBar DownloadStatusProgressBarr;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SingleVersionTab;
        private System.Windows.Forms.TabPage AdvancedVersionTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SteamManifestIdTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SteamDepotIdTextBox;
        private System.Windows.Forms.TextBox SteamAppIdTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button StartAdvancedDownloadButton;
        private System.Windows.Forms.CheckBox EditAdvancedValuesCheckBox;
        private System.Windows.Forms.TextBox SteamBranchTextBox;
        private System.Windows.Forms.Label label7;
    }
}