namespace BeatSaberKeeper.Kernel.Services.DepotDownloader
{
    public interface IDepotDownloaderServiceFactory
    {
        DepotDownloaderService Build();
    }
    public class PrebuiltDepotDownloaderServiceFactory : IDepotDownloaderServiceFactory
    {
        private readonly string _path;

        public PrebuiltDepotDownloaderServiceFactory(string path)
        {
            _path = path;
        }

        public DepotDownloaderService Build()
        {
            return new DepotDownloaderService(_path);
        }
    }
}
