namespace BeatSaberKeeper.Kernel.Abstraction
{
    public interface ICompressionInterface
    {
        CompressionInterfaceCapabilities Capabilities { get; }
        
        void CreateArchiveFromFolder(
            string sourcePath,
            string archivePath,
            ReportProgressDelegate report = null);

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
    }
}