using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BeatKeeper
{
    public partial class MainWindow : Form
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Forms Designer generated code
        private void InitializeComponent()
        {
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 400);
            this.Name = "MainWindow";
            this.Text = "BeatKeeper - Run Multiple Versions of BeatSaber";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
