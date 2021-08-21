using System.ComponentModel;

namespace BeatSaberKeeper.App.Utils
{
    partial class MonoSpaceTextOutputForm
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
            this.MonoSpaceTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.MonoSpaceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MonoSpaceTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.MonoSpaceTextBox.Location = new System.Drawing.Point(0, 0);
            this.MonoSpaceTextBox.Name = "MonoSpaceTextBox";
            this.MonoSpaceTextBox.ReadOnly = true;
            this.MonoSpaceTextBox.Size = new System.Drawing.Size(624, 441);
            this.MonoSpaceTextBox.TabIndex = 0;
            this.MonoSpaceTextBox.Text = "Here will be text";
            // 
            // MonoSpaceTextOutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.MonoSpaceTextBox);
            this.Name = "MonoSpaceTextOutputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonoSpaceTextOutputForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox MonoSpaceTextBox;

        #endregion
    }
}