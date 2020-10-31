using BeatKeeper.Kernel.Services.DepotDownloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatKeeper.Windows.Utils
{
    public class DepotDownloaderServiceFactory : IDepotDownloaderServiceFactory
    {
        public static DepotDownloaderServiceFactory Instance { get; } = new DepotDownloaderServiceFactory();

        public DepotDownloaderService Build()
        {
            return new DepotDownloaderService(ClientPathUtils.DepotDownloaderFolder);
        }
    }
}
