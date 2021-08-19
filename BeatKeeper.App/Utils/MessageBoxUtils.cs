using System;
using System.Windows.Forms;

namespace BeatKeeper.App.Utils
{
    public static class MessageBoxUtils
    {
        private const string APP_NAME = "Beat Saber Keeper";

        public static void Info(
            string message,
            string title = null)
        {
            MessageBox.Show(
                message,
                title ?? APP_NAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public static void Show(
            string message,
            string title = null)
        {
            MessageBox.Show(
                message,
                title ?? APP_NAME,
                MessageBoxButtons.OK);
        }

        public static void Warn(
            string message,
            string title = null)
        {
            MessageBox.Show(
                message,
                title ?? APP_NAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static bool Ask(
            string message,
            string title = null,
            MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            return MessageBox.Show(
                message,
                title ?? APP_NAME,
                MessageBoxButtons.YesNo,
                icon) == DialogResult.Yes;
        }

        public static void Error(
            string errorMessage,
            string title = null)
        {
            MessageBox.Show(
                errorMessage,
                title ?? APP_NAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void Ex(Exception ex, string processDescription)
        {
            var exText = $"An exception occurred while {processDescription}.";
            exText += $"\n\n{ex.GetType().FullName}: {ex.Message}";
#if DEBUG
            exText += $"\n\n{ex.StackTrace}";
#endif
            Error(exText,
                "Exception during runtime");
        }

        public static void AboutApp()
        {
            MessageBox.Show(
                $"{AppInfo.AppName}\n" +
                $"Ver. {AppInfo.AppVersion}\n" +
                $"Copyright (C) 2020-{DateTime.Now.Year} Raphael \"rGunti\" Guntersweiler" +
                "\n\n" +
                "This tool allows you to create archives of your Beat Saber installation, " +
                "making it easy to backup and restore your current game state once Steam " +
                "desides to automatically update the game. You can also download older " +
                "versions of Beat Saber if you have a Steam account and a valid Beat Saber " +
                "license in case you want to downgrade and don't have earlier versions of " +
                "the game backed up." +
                "\n\n" +
                "This tool has been built using .NET 5!" +
                "\n\n" +
                "LICENSE INFORMATION\n" + 
                "This program is free software: you can redistribute it and/or modify " +
                "it under the terms of the GNU General Public License as published by " +
                "the Free Software Foundation, either version 3 of the License, or " +
                "(at your option) any later version.\n\n" +
                "This program is distributed in the hope that it will be useful, " +
                "but WITHOUT ANY WARRANTY; without even the implied warranty of " +
                "MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the " +
                "GNU General Public License for more details.\n\n" +
                "You should have received a copy of the GNU General Public License " +
                "along with this program. If not, see <http://www.gnu.org/licenses/>.",
                APP_NAME,
                MessageBoxButtons.OK
            );
        }

        public static void NotImplemented(string featureName = null)
        {
            MessageBox.Show(
                $"{featureName ?? "This feature"} is not yet implemented!",
                APP_NAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}