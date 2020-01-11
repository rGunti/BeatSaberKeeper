namespace BeatKeeper.Windows
{
    partial class DownloadForm
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
            this.DownloadVersionComboBox = new System.Windows.Forms.ComboBox();
            this.DownloadStartButton = new System.Windows.Forms.Button();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DownloadStatusText = new System.Windows.Forms.Label();
            this.DownloadAllVersionsButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a version to download:";
            // 
            // DownloadVersionComboBox
            // 
            this.DownloadVersionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadVersionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DownloadVersionComboBox.FormattingEnabled = true;
            this.DownloadVersionComboBox.Location = new System.Drawing.Point(6, 19);
            this.DownloadVersionComboBox.Name = "DownloadVersionComboBox";
            this.DownloadVersionComboBox.Size = new System.Drawing.Size(362, 21);
            this.DownloadVersionComboBox.TabIndex = 1;
            this.DownloadVersionComboBox.SelectedIndexChanged += new System.EventHandler(this.DownloadVersionComboBox_SelectedIndexChanged);
            // 
            // DownloadStartButton
            // 
            this.DownloadStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadStartButton.Enabled = false;
            this.DownloadStartButton.Location = new System.Drawing.Point(293, 46);
            this.DownloadStartButton.Name = "DownloadStartButton";
            this.DownloadStartButton.Size = new System.Drawing.Size(75, 23);
            this.DownloadStartButton.TabIndex = 2;
            this.DownloadStartButton.Text = "Download";
            this.DownloadStartButton.UseVisualStyleBackColor = true;
            this.DownloadStartButton.Click += new System.EventHandler(this.DownloadStartButton_Click);
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadProgressBar.Location = new System.Drawing.Point(12, 132);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(382, 23);
            this.DownloadProgressBar.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(382, 101);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.DownloadStartButton);
            this.tabPage1.Controls.Add(this.DownloadVersionComboBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(374, 75);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Single version";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DownloadAllVersionsButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(395, 102);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mass Download";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DownloadStatusText
            // 
            this.DownloadStatusText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DownloadStatusText.AutoSize = true;
            this.DownloadStatusText.Location = new System.Drawing.Point(12, 116);
            this.DownloadStatusText.Name = "DownloadStatusText";
            this.DownloadStatusText.Size = new System.Drawing.Size(38, 13);
            this.DownloadStatusText.TabIndex = 5;
            this.DownloadStatusText.Text = "Ready";
            // 
            // DownloadAllVersionsButton
            // 
            this.DownloadAllVersionsButton.Location = new System.Drawing.Point(6, 6);
            this.DownloadAllVersionsButton.Name = "DownloadAllVersionsButton";
            this.DownloadAllVersionsButton.Size = new System.Drawing.Size(169, 53);
            this.DownloadAllVersionsButton.TabIndex = 0;
            this.DownloadAllVersionsButton.Text = "Download all versions";
            this.DownloadAllVersionsButton.UseVisualStyleBackColor = true;
            this.DownloadAllVersionsButton.Click += new System.EventHandler(this.DownloadAllVersionsButton_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 167);
            this.Controls.Add(this.DownloadStatusText);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.DownloadProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download BeatSaber Version";
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DownloadVersionComboBox;
        private System.Windows.Forms.Button DownloadStartButton;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label DownloadStatusText;
        private System.Windows.Forms.Button DownloadAllVersionsButton;
    }
}