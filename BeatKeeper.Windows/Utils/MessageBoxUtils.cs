using System.Windows.Forms;

namespace BeatKeeper.Windows.Utils
{
    public static class MessageBoxUtils
    {
        public static bool Ask(
            string message,
            string title = null,
            MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            return MessageBox.Show(
                message,
                title ?? AppInfo.AppName,
                MessageBoxButtons.YesNo,
                icon) == DialogResult.Yes;
        }

        public static void Error(string errorMessage)
        {
            MessageBox.Show(
                errorMessage,
                AppInfo.AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void AboutApp()
        {
            MessageBox.Show(
                $"BeatSaberKeeper Ver. {AppInfo.AppVersion}\n" +
                "Copyright (C) 2020 Raphael \"rGunti\" Guntersweiler\n\n" +
                "This program is free software: you can redistribute it and/or modify " +
                "it under the terms of the GNU General Public License as published by " +
                "the Free Software Foundation, either version 3 of the License, or " +
                "(at your option) any later version.\n\n" +
                "This program is distributed in the hope that it will be useful, " +
                "but WITHOUT ANY WARRANTY; without even the implied warranty of " +
                "MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the " +
                "GNU General Public License for more details.\n\n" +
                "You should have received a copy of the GNU General Public License " +
                "along with this program.If not, see <http://www.gnu.org/licenses/>.",
                AppInfo.AppName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}