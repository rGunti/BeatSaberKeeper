using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using BeatKeeper.Kernel.Utils;
using Serilog;

namespace BeatKeeper.Kernel.Services
{
    public class SteamCmdService
    {
        private const string DOWNLOAD_URL = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";
        private const string EXECUTABLE = "steamcmd.exe";
        private const string DOWNLOAD_ARTIFACT = "steamcmd.zip";

        private readonly string _steamCmdBasePath;

        public SteamCmdService(string steamCmdBasePath)
        {
            Log.Verbose($"Constructing {GetType().Name} for {steamCmdBasePath}");
            _steamCmdBasePath = steamCmdBasePath;
        }

        private string SteamCmdExecutablePath
            => Path.Combine(_steamCmdBasePath, EXECUTABLE);

        public bool IsInitialized
            => File.Exists(SteamCmdExecutablePath);

        public void Initialize(bool forceInit = false)
        {
            if (!IsInitialized || forceInit)
            {
                // If forceInit is enabled, clear the directory first
                if (forceInit && Directory.Exists(_steamCmdBasePath))
                {
                    Log.Information("Force-initializing SteamCMD ...");
                    Directory.Delete(_steamCmdBasePath, true);
                }

                if (!Directory.Exists(_steamCmdBasePath))
                {
                    Log.Information($"Creating SteamCMD path {_steamCmdBasePath} ...");
                    Directory.CreateDirectory(_steamCmdBasePath);
                }
                DownloadSteamCmd();
                RunSteamCmd(false, "+quit").WaitForExit();
            }
        }

        private void DownloadSteamCmd()
        {
            var downloadArtifact = Path.Combine(_steamCmdBasePath, DOWNLOAD_ARTIFACT);
            using (var client = new WebClient())
            {
                Log.Debug($"Downloading SteamCMD from {DOWNLOAD_URL} ...");
                client.DownloadFile(DOWNLOAD_URL, downloadArtifact);

                Log.Debug("Extracting SteamCMD ...");
                ZipFile.ExtractToDirectory(downloadArtifact, _steamCmdBasePath);
                File.Delete(downloadArtifact);
            }
        }

        private Process ConstructProcess(bool redirect, params string[] commands)
        {
            var p = new Process()
            {
                StartInfo = new ProcessStartInfo(
                    SteamCmdExecutablePath,
                    string.Join(" ", commands))
                {
                    UseShellExecute = !redirect,
                    RedirectStandardInput = redirect,
                    RedirectStandardOutput = redirect,
                    WindowStyle = redirect ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal
                }
            };
            return p;
        }

        private Process RunSteamCmd(bool redirect, params string[] commands)
        {
            Log.Debug($"Running SteamCMD with parameters \"{string.Join(" ", commands)}\"");

            var p = ConstructProcess(redirect, commands);
            p.Start();
            return p;
        }

        public Process Login(string username)
        {
            return RunSteamCmd(
                false,
                $"+login \"{username}\"",
                "+quit");
        }

        [Obsolete("Doesn't work right now")]
        public void Login(string username, string password, string authCode)
        {
            var p = RunSteamCmd(
                true,
                $"+login \"{username}\"",
                "+quit");

            //p.StandardOutput.ReadUntil("password: ");

            var succeded = p.StandardOutput.ReadUntil(
                (reader, line) => line.StartsWith("password: "));
            if (!succeded)
            {
                return;
            }

            p.StandardInput.WriteLine($"{password}\n");
            //p.StandardOutput.ReadUntil("Two-factor code:");

            succeded = p.StandardOutput.ReadUntil(
                (reader, line) => line.StartsWith("Two-factor code:"));
            if (!succeded)
            {
                return;
            }

            p.StandardInput.WriteLine($"{authCode}\n");
            //p.StandardOutput.ReadUntil("Logged in OK");
            p.WaitForExit();
        }

        public Process DownloadArtifact(
            string username,
            string artifactId,
            string depotId = "620981",
            string appId = "620980")
        {
            return RunSteamCmd(
                false,
                $"+login \"{username}\"",
                $"+download_depot {appId} {depotId} {artifactId}",
                "+quit");
        }
    }
}