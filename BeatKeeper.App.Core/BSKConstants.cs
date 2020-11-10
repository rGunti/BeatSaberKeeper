using System;
using System.IO;
using System.Reflection;

namespace BeatKeeper.App.Core
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
            public static readonly string BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            public const string DEFAULT_WORKING_DIRECTORY = ".bsk_tmp";
            public static readonly string DefaultWorkingPath = Path.Combine(BaseDirectory, DEFAULT_WORKING_DIRECTORY);
            public static readonly string Temp = Path.Combine(DefaultWorkingPath, "temp");
        }

        public static class FileExtensions
        {
            public const string ARCHIVE = ".bska";
            public const string CHECKSUM = ".sha";
        }
    }
}
