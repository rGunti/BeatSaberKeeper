using BeatKeeper.App.Core.Steam;
using System;
using System.Windows.Forms;

namespace BeatKeeper.App
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            SteamSession.Instance?.Dispose();
        }
    }
}
