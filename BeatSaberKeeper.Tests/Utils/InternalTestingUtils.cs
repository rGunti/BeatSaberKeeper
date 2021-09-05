using System.IO;
using System.IO.Abstractions;

namespace BeatSaberKeeper.Tests.Utils
{
    internal static class InternalTestingUtils
    {
        public static void WriteVirtualFileToDisk(
            this IFileSystem sourceFileSystem,
            string sourceFilePath,
            string destinationFilePath)
        {
            var destinationFileSystem = new FileSystem();
            using Stream sourceFs = sourceFileSystem.FileStream.Create(sourceFilePath, FileMode.Open);
            using Stream destinationFs =
                destinationFileSystem.FileStream.Create(destinationFilePath, FileMode.Create);
            sourceFs.CopyTo(destinationFs);
        }
    }
}