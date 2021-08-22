namespace BeatSaberKeeper.App.Core.Steam
{
    public class DepotDownloadCounter
    {
        public ulong CompleteDownloadSize { get; set; }
        public ulong SizeDownloaded { get; set; }
        public ulong DepotBytesCompressed { get; set; }
        public ulong DepotBytesUncompressed { get; set; }
    }
}
