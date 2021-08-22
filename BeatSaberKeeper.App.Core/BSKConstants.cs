using System;
using System.IO;
using System.Reflection;
using BeatSaberKeeper.App.Core.Utils;

namespace BeatSaberKeeper.App.Core
{
    public static class BSKConstants
    {
        public static class Steam
        {
            public const string DEFAULT_BRANCH = "Public";

            public const uint BEAT_SABER_APP_ID = 620980;
            public const uint BEAT_SABER_DEPOT_ID = 620981;

            [Obsolete]
            public const ulong TEST_MANIFEST_ID = 543439039654962432;
        }

        public static class Paths
        {
            public static void EnsureDirectoryTreeExists()
            {
                DefaultWorkingPath.EnsureDirectory();
                Archives.EnsureDirectory();
            }

            private static string _baseDirectory = AppContext.BaseDirectory;
            public static string BaseDirectory => _baseDirectory;

            public static void SetBaseDirectory(string baseDirectory)
            {
                _baseDirectory = baseDirectory;
            }

            public const string DEFAULT_WORKING_DIRECTORY = ".bsk";
            public const string DEFAULT_TEMP_DIRECTORY = "temp";
            public const string DEFAULT_STAGING_DIRECTORY = "staging";
            public const string DEFAULT_ARCHIVES_DIRECTORY = "archives";
            public const string DEFAULT_LOG_DIRECTORY = "logs";

            public static string DefaultWorkingPath => Path.Combine(BaseDirectory, DEFAULT_WORKING_DIRECTORY);
            public static string Temp => Path.Combine(DefaultWorkingPath, DEFAULT_TEMP_DIRECTORY);
            public static string Staging => Path.Combine(DefaultWorkingPath, DEFAULT_STAGING_DIRECTORY);
            public static string Logs => Path.Combine(DefaultWorkingPath, DEFAULT_LOG_DIRECTORY);

            public static string Archives => Path.Combine(DefaultWorkingPath, DEFAULT_ARCHIVES_DIRECTORY);
            //public static readonly string VanillaArchives = Path.Combine(Archives, "vanilla");
            //public static readonly string BackupArchives = Path.Combine(Archives, "backup");
        }

        public static class FileExtensions
        {
            public const string ARCHIVE = ".bskeep";
            public const string CHECKSUM = ".sha";
        }
    }
}
