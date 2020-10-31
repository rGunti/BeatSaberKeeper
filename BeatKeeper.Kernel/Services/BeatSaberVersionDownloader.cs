using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BeatKeeper.Kernel.Entities;
using BeatKeeper.Kernel.Services.DepotDownloader;

namespace BeatKeeper.Kernel.Services
{
    public class BeatSaberVersionDownloader
    {
        private IReadOnlyDictionary<string, Artifact> _versionIds;
        private readonly string _username;
        private readonly string _versionFilePath;
        private readonly SteamCmdService _steamCmdService;
        private readonly DepotDownloaderService _depotDownloaderService;

        public BeatSaberVersionDownloader(
            ISteamCmdServiceFactory steamCmdServiceFactory,
            IDepotDownloaderServiceFactory depotDownloaderServiceFactory,
            string username,
            string versionFilePath = "versions.txt")
            : this(steamCmdServiceFactory.Build(),
                  depotDownloaderServiceFactory.Build(),
                  username, versionFilePath)
        {
        }

        public BeatSaberVersionDownloader(
            SteamCmdService steamCmdService,
            DepotDownloaderService depotDownloaderService,
            string username,
            string versionFilePath = "versions.txt")
        {
            _username = username;
            _steamCmdService = steamCmdService;
            _depotDownloaderService = depotDownloaderService;
            _versionFilePath = versionFilePath;
            ReadVersionFile();
        }

        private void ReadVersionFile()
        {
            var lines = File.ReadAllLines(_versionFilePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            var versions = new List<Artifact>();
            Artifact a = null;
            var i = -1;
            foreach (var line in lines)
            {
                i++;
                if (i < 3)
                {
                    continue;
                }

                if (i % 3 == 0)
                {
                    // Date
                    a = new Artifact()
                    {
                        Type = ArtifactType.DownloadableVanilla
                    };
                    try
                    {
                        a.Created = DateTime.ParseExact(
                            line.Trim(),
                            "MMMM d, yyyy – HH:mm:ss 'UTC'",
                            new CultureInfo("en-US"));
                    } catch (FormatException)
                    {
                        try
                        {
                            a.Created = DateTime.ParseExact(
                                line.Trim(),
                                "dd MMMM yyyy – HH:mm:ss 'UTC'",
                                new CultureInfo("en-US"));
                        } catch (FormatException)
                        {
                            try
                            {
                                a.Created = DateTime.Parse(line.Trim());
                            } catch (FormatException)
                            {
                                a.Created = DateTime.MinValue;
                            }
                        }
                    }
                    versions.Add(a);
                }
                else if (i % 3 == 1)
                {
                    // Version
                    a.GameVersion = line.Trim();
                }
                else if (i % 3 == 2)
                {
                    // Manifest ID
                    a.ManifestId = line.Trim();
                }
            }

            _versionIds = versions.ToDictionary(
                artifact => artifact.GameVersion,
                artifact => artifact);
        }

        public IEnumerable<string> AppVersionList => _versionIds.Keys
            .OrderBy(s => s)
            .ToArray();

        public Artifact[] Artifacts => _versionIds.Values
            .OrderBy(a => a.GameVersion)
            .ToArray();

        public Artifact GetArtifact(string version) => _versionIds[version];

        public void DownloadArtifact(Artifact version)
        {
            // _steamCmdService.DownloadArtifact(_username, version.ManifestId)
            //     .WaitForExit();
            _depotDownloaderService.DownloadArtifact("620980", "620981", version.ManifestId, _username)
                .WaitForExit();
        }
    }
}