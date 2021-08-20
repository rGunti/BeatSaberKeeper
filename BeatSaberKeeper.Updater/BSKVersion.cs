using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PandaDotNet.Utils;

namespace BeatSaberKeeper.Updater
{
    public class BskVersion
    {
        private static readonly Regex ParseRegex = new Regex(@"(\d+)\.(\d+)\.(\d+)(-([a-z0-9]+))?(\+([a-f0-9]+))?");
        private const int RX_GROUP_MAJOR = 1;
        private const int RX_GROUP_MINOR = 2;
        private const int RX_GROUP_REVISION = 3;
        private const int RX_GROUP_SUFFIX = 5;
        private const int RX_GROUP_COMMIT = 7;

        private static readonly string[] ReleaseStages = new[]
        {
            "alpha",
            "beta",
            "rc"
        };

        public int Major { get; set; }
        public int Minor { get; set; }
        public int Revision { get; set; }
        public string Suffix { get; set; }
        public string Commit { get; set; }

        public bool IsKnownSuffix => ReleaseStages.Contains(Suffix);
        public int SuffixStage => string.IsNullOrWhiteSpace(Suffix) ?
            int.MaxValue :
            Array.IndexOf(ReleaseStages, Suffix) + 1;

        public int CompareTo(object version)
        {
            if (version == null) return 1;
            if (version is BskVersion v) return CompareTo(v);
            throw new ArgumentException($"Argument must be of type {nameof(BskVersion)}.");
        }

        public int CompareTo(BskVersion value)
        {
            // Validation has been copied and adjusted from System.Version
            return
                object.ReferenceEquals(value, this) ? 0 :
                value is null ? 1 :
                Major != value.Major ? (Major > value.Major ? 1 : -1) :
                Minor != value.Minor ? (Minor > value.Minor ? 1 : -1) :
                Revision != value.Revision ? (Revision > value.Revision ? 1 : -1) :
                SuffixStage != value.SuffixStage ? (SuffixStage > value.SuffixStage ? 1 : -1) :
                0;
        }

        public static bool operator <(BskVersion a, BskVersion b)
            => a.CompareTo(b) < 0;

        public static bool operator >(BskVersion a, BskVersion b)
            => a.CompareTo(b) > 0;
        
        public static bool operator ==(BskVersion a, BskVersion b)
        {
            if(((object)a) == ((object)b)) return true;
            if(((object)a) == null || ((object)b) == null) return false;
            return a.Major == b.Major
                && a.Minor == b.Minor
                && a.Revision == b.Revision
                && a.Suffix == b.Suffix
                && a.Commit == b.Commit;
        }

        public static bool operator !=(BskVersion a, BskVersion b)
        {
            return !(a == b);
        }

        public static BskVersion Parse(string versionString)
        {
            versionString = versionString.OrThrowNullArg(nameof(versionString));

            Match deconstructed = ParseRegex.Match(versionString);
            if (!deconstructed.Success)
            {
                throw new VersionParseException($"Could not parse version string \"{versionString}\"");
            }

            GroupCollection groups = deconstructed.Groups;
            return new BskVersion
            {
                Major = int.Parse(groups[RX_GROUP_MAJOR].Value),
                Minor = int.Parse(groups[RX_GROUP_MINOR].Value),
                Revision = int.Parse(groups[RX_GROUP_REVISION].Value),
                Suffix = groups[RX_GROUP_SUFFIX].Value,
                Commit = groups[RX_GROUP_COMMIT].Value
            };
        }

        public static bool TryParse(string versionString, out BskVersion version)
        {
            version = null;
            if (!ParseRegex.IsMatch(versionString))
            {
                return false;
            }

            try
            {
                version = Parse(versionString);
                return true;
            }
            catch (VersionParseException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Major}.{Minor}.{Revision}");
            if (!string.IsNullOrWhiteSpace(Suffix))
            {
                sb.Append($"-{Suffix}");
            }

            if (!string.IsNullOrWhiteSpace(Commit))
            {
                sb.Append($"+{Commit}");
            }

            return sb.ToString();
        }

        public bool IsSameAs(BskVersion version, BskVersionEqualityLevel level = BskVersionEqualityLevel.Exact)
        {
            switch (level)
            {
                case BskVersionEqualityLevel.Exact:
                    return this == version;
                case BskVersionEqualityLevel.Commit:
                    return IsSameAs(version, BskVersionEqualityLevel.Revision)
                           && Commit == version.Commit;
                case BskVersionEqualityLevel.Stage:
                    return IsSameAs(version, BskVersionEqualityLevel.Revision)
                           && SuffixStage == version.SuffixStage;
                case BskVersionEqualityLevel.Revision:
                    return IsSameAs(version, BskVersionEqualityLevel.Minor)
                           && Revision == version.Revision;
                case BskVersionEqualityLevel.Minor:
                    return IsSameAs(version, BskVersionEqualityLevel.Major)
                           && Minor == version.Minor;
                case BskVersionEqualityLevel.Major:
                    return Major == version.Major;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }

    public enum BskVersionEqualityLevel
    {
        Exact,
        Commit,
        Stage,
        Revision,
        Minor,
        Major
    }
}