using BeatSaberKeeper.Kernel.Abstraction.Entities;

namespace BeatSaberKeeper.Kernel.Abstraction
{
    public interface ICompressionInterface
    {
        CompressionInterfaceCapabilities Capabilities { get; }
        
        void CreateArchiveFromFolder(
            string sourcePath,
            string archivePath,
            ReportProgressDelegate report = null,
            ArtifactType artifactType = ArtifactType.ModBackup);

        void UnpackArchiveToFolder(
            string archivePath,
            string destinationPath,
            ReportProgressDelegate report = null);

        void UpdateArchiveFromFolder(
            string sourcePath,
            string archivePath,
            ReportProgressDelegate report = null);

        ArchiveMetaData ReadMetaDataFromArchive(
            string archivePath);

        /// <summary>
        /// Returns true if the provided archive is using this archive version
        /// </summary>
        /// <param name="archivePath"></param>
        /// <returns></returns>
        bool ProbeVersion(string archivePath);
    }
}