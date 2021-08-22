using System.ComponentModel;

namespace BeatSaberKeeper.App
{
    partial class RenameArchiveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.ArchiveNameTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.CancelRenameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ArchiveNameTextBox
            // 
            this.ArchiveNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArchiveNameTextBox.Location = new System.Drawing.Point(12, 12);
            this.ArchiveNameTextBox.Name = "ArchiveNameTextBox";
            this.ArchiveNameTextBox.Size = new System.Drawing.Size(304, 23);
            this.ArchiveNameTextBox.TabIndex = 0;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmButton.Location = new System.Drawing.Point(241, 45);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 1;
            this.ConfirmButton.Text = "Rename";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // CancelButton
            // 
            this.CancelRenameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelRenameButton.Location = new System.Drawing.Point(160, 45);
            this.CancelRenameButton.Name = "CancelButton";
            this.CancelRenameButton.Size = new System.Drawing.Size(75, 23);
            this.CancelRenameButton.TabIndex = 2;
            this.CancelRenameButton.Text = "Cancel";
            this.CancelRenameButton.UseVisualStyleBackColor = true;
            this.CancelRenameButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RenameArchiveForm
            // 
            this.AcceptButton = this.ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelRenameButton;
            this.ClientSize = new System.Drawing.Size(328, 80);
            this.Controls.Add(this.CancelRenameButton);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.ArchiveNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameArchiveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename Archive";
            this.Load += new System.EventHandler(this.RenameArchiveForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ArchiveNameTextBox;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Button CancelRenameButton;
    }
}