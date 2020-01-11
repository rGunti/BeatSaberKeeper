using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BeatKeeper.BeatSaber
{
    public static class BeatSaberVersions
    {
        private static IReadOnlyDictionary<string, AppVersion> _versionIds;

        static BeatSaberVersions()
        {
            ReadVersionFile();
        }

        private static void ReadVersionFile()
        {
            var lines = File.ReadAllLines("versions.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            var versions = new List<AppVersion>();
            AppVersion v = null;
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
                    v = new AppVersion();
                    v.ReleaseDate = DateTime.ParseExact(
                        line.Trim(),
                        "MMMM d, yyyy – HH:mm:ss 'UTC'",
                        new CultureInfo("en-US"));
                    versions.Add(v);
                }
                else if (i % 3 == 1)
                {
                    // Version
                    v.Version = line.Trim();
                }
                else if (i % 3 == 2)
                {
                    // Manifest ID
                    v.ManifestId = line.Trim();
                }
            }

            _versionIds = versions.ToImmutableDictionary(a => a.Version, a => a);
        }

        public static IEnumerable<string> AppVersionList => _versionIds.Keys
            .OrderBy(s => s)
            .ToArray();
        public static IEnumerable<AppVersion> AppVersions => _versionIds.Values
            .OrderBy(a => a.Version)
            .ToArray();

        public static AppVersion GetVersion(string version) => _versionIds[version];
    }

    public class AppVersion
    {
        public AppVersion() { }
        public AppVersion(
            string releaseDate,
            string version,
            string manifestId)
            : this(
                  DateTime.MinValue,
                  version,
                  manifestId)
        { }
        public AppVersion(
            DateTime releaseDate,
            string version,
            string manifestId)
        {
            ReleaseDate = releaseDate;
            Version = version;
            ManifestId = manifestId;
        }

        public DateTime ReleaseDate { get; set; }
        public string Version { get; set; }
        public string ManifestId { get; set; }
    }
}
