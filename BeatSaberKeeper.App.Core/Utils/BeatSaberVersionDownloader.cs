using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using Serilog;

namespace BeatSaberKeeper.App.Core.Utils
{
    public class BeatSaberVersionDownloader
    {
        private const string VERSIONS_URL =
            "https://raw.githubusercontent.com/rGunti/BeatSaberKeeper/master/BeatSaberKeeper.Kernel/versions.txt";
        
        private IReadOnlyDictionary<string, Artifact> _versionIds;
        private readonly string _versionFilePath;

        public BeatSaberVersionDownloader(string versionFilePath = "versions.txt")
        {
            _versionFilePath = versionFilePath;
            ReadVersionFile();
        }

        public static string DownloadRecentVersionFile(
            string url = VERSIONS_URL)
        {
            const string VERSION_TXT = "versions.txt.online";
            using (var client = new WebClient())
            {
                Log.Debug("Downloading online archive version ...");
                client.DownloadFile(url, VERSION_TXT);
                return VERSION_TXT;
            }
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
                                "d MMMM yyyy – HH:mm:ss 'UTC'",
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
    }
}