using System.Windows.Forms;

namespace BeatKeeper.Windows.Utils
{
    public static class MessageBoxUtils
    {
        public static bool Ask(
            string message,
            string title,
            MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                icon) == DialogResult.Yes;
        }

        public static void Error(string errorMessage)
        {
            MessageBox.Show(
                errorMessage,
                "BeatKeeper",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}