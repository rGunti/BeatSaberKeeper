using System.Windows.Forms;

namespace BeatSaberKeeper.App.Utils
{
    public partial class MonoSpaceTextOutputForm : Form
    {
        public MonoSpaceTextOutputForm(string textToDisplay, string title = null)
        {
            InitializeComponent();
            // ReSharper disable once VirtualMemberCallInConstructor
            Text = title ?? AppInfo.AppName;
            MonoSpaceTextBox.Text = textToDisplay;
        }
    }
}