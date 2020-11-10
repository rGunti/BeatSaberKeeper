
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
            this.StatusLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DownloadArchiveButton = new System.Windows.Forms.Button();
            this.GameVersionDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DownloaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginButton.Location = new System.Drawing.Point(325, 12);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginStatusLabel
            // 
            this.LoginStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginStatusLabel.Location = new System.Drawing.Point(32, 12);
            this.LoginStatusLabel.Name = "LoginStatusLabel";
            this.LoginStatusLabel.Size = new System.Drawing.Size(287, 23);
            this.LoginStatusLabel.TabIndex = 1;
            this.LoginStatusLabel.Text = "Logged out";
            this.LoginStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownloaderPanel
            // 
            this.DownloaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloaderPanel.Controls.Add(this.StatusLabel);
            this.DownloaderPanel.Controls.Add(this.progressBar1);
            this.DownloaderPanel.Controls.Add(this.DownloadArchiveButton);
            this.DownloaderPanel.Controls.Add(this.GameVersionDropdown);
            this.DownloaderPanel.Controls.Add(this.label1);
            this.DownloaderPanel.Enabled = false;
            this.DownloaderPanel.Location = new System.Drawing.Point(0, 41);
            this.DownloaderPanel.Name = "DownloaderPanel";
            this.DownloaderPanel.Size = new System.Drawing.Size(412, 238);
            this.DownloaderPanel.TabIndex = 2;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 182);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(39, 15);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "Ready";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 200);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(388, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // DownloadArchiveButton
            // 
            this.DownloadArchiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadArchiveButton.Location = new System.Drawing.Point(260, 136);
            this.DownloadArchiveButton.Name = "DownloadArchiveButton";
            this.DownloadArchiveButton.Size = new System.Drawing.Size(140, 23);
            this.DownloadArchiveButton.TabIndex = 2;
            this.DownloadArchiveButton.Text = "Download Archive";
            this.DownloadArchiveButton.UseVisualStyleBackColor = true;
            this.DownloadArchiveButton.Click += new System.EventHandler(this.DownloadArchiveButton_Click);
            // 
            // GameVersionDropdown
            // 
            this.GameVersionDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GameVersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameVersionDropdown.FormattingEnabled = true;
            this.GameVersionDropdown.Location = new System.Drawing.Point(12, 107);
            this.GameVersionDropdown.Name = "GameVersionDropdown";
            this.GameVersionDropdown.Size = new System.Drawing.Size(388, 23);
            this.GameVersionDropdown.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 95);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // DownloadGameArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 279);
            this.Controls.Add(this.DownloaderPanel);
            this.Controls.Add(this.LoginStatusLabel);
            this.Controls.Add(this.LoginButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DownloadGameArchiveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Beat Saber Vanilla Archive Downloader";
            this.Load += new System.EventHandler(this.DownloadGameArchiveForm_Load);
            this.DownloaderPanel.ResumeLayout(false);
            this.DownloaderPanel.PerformLayout();
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
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}