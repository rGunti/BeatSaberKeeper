using System.IO;
using PandaDotNet.Utils;

namespace BeatSaberKeeper.Kernel.Abstraction.Utils
{
    public class ArchiveFileInfo
    {
        public ArchiveFileInfo(string sourceFile, string destination)
        {
            SourceFile = sourceFile;
            Destination = destination;
        }

        public string SourceFile { get; set; }
        public string Destination { get; set; }
        public long? FileSize { get; set; }

        public static ArchiveFileInfo ConstructFromPath(
            string filepath,
            string basePath)
        {
            filepath.OrThrowNullArg(nameof(filepath));
            basePath.OrThrowNullArg(nameof(basePath));

            filepath = Path.GetFullPath(filepath);
            basePath = Path.GetFullPath(basePath);

            var destinationPath = filepath.Replace(basePath, "");
            if (destinationPath.StartsWith('/') || destinationPath.StartsWith('\\'))
            {
                destinationPath = destinationPath.Substring(1);
            }

            return new ArchiveFileInfo(filepath, destinationPath);
        }
    }
}