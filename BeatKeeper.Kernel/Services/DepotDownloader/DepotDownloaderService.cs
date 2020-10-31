using Octokit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace BeatKeeper.Kernel.Services.DepotDownloader
{
    public class DepotDownloaderService
    {
        private const string DEPOTDOWNLOADER_ARTIFACT = "depotdownloader.zip";

        private readonly string _basePath;

        public DepotDownloaderService(string basePath)
        {
            Log.Verbose($"Constructing {GetType().Name} for {basePath}");
            _basePath = basePath;
        }

        private string GetPath(string file)
            => Path.Combine(_basePath, file);

        private string Executable
            => Path.Combine(_basePath, "depotdownloader.bat");
        private string DotNetExecutable
            => Path.Combine(_basePath, "depotdownloader.dll");

        private void ClearBaseFolder()
        {
            try
            {
                Log.Debug("Clearing DepotDownloader folder ...");
                Directory.Delete(_basePath, true);

                Log.Debug("Recreating folder ...");
                Directory.CreateDirectory(_basePath);
            } catch (Exception ex)
            {
                Log.Error(ex, "Failed to clear folder");
                throw ex;
            }
        }

        public void DownloadLatestRelease()
        {
            var client = new GitHubClient(new ProductHeaderValue("BeatSaberKeeper"));
            var releases = client.Repository.Release.GetAll("SteamRE", "DepotDownloader")
                .GetAwaiter().GetResult();
            var latestRelease = releases.First();

            var downloadArtifact = GetPath(DEPOTDOWNLOADER_ARTIFACT);
            using (var webClient = new WebClient())
            {
                ClearBaseFolder();

                Log.Debug($"Downloading latest release of DepotDownloader ...");
                webClient.DownloadFile(latestRelease.Assets.First().BrowserDownloadUrl, downloadArtifact);

                Log.Debug("Extracting DepotDownloader ...");
                ZipFile.ExtractToDirectory(downloadArtifact, _basePath);
                File.Delete(downloadArtifact);
            }
        }

        private Process ConstructProcess(
            bool redirect,
            bool useDotNet,
            params string[] args)
        {
            var executable = useDotNet ? "dotnet" : Executable;
            var processArgs = (useDotNet ? $"\"{DotNetExecutable}\" " : "") + string.Join(" ", args);

            var p = new Process()
            {
                StartInfo = new ProcessStartInfo(executable, processArgs)
                {
                    UseShellExecute = !redirect,
                    RedirectStandardInput = redirect,
                    RedirectStandardOutput = redirect,
                    WindowStyle = redirect ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal,
                    WorkingDirectory = _basePath
                }
            };
            return p;
        }

        private Process RunDDProcess(bool redirect, bool useDotNet, params string[] args)
        {
            Log.Debug($"Running DepotDownloader with parameters \"{string.Join(" ", args)}\"");

            var p = ConstructProcess(redirect, useDotNet, args);
            p.Start();
            return p;
        }

        public Process DownloadArtifact(string appId, string depotId, string manifestId,
            string username)
        {
            return RunDDProcess(false, true,
                $"-app {appId}",
                $"-depot {depotId}",
                $"-manifest {manifestId}",
                $"-username \"{username}\"",
                $"-remember-password");
        }
    }
}
