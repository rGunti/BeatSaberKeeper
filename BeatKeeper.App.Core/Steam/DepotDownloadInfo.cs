namespace BeatKeeper.App.Core.Steam
{
    public class DepotDownloadInfo
    {
        public DepotDownloadInfo(
            uint id,
            ulong manifestId,
            string contentName,
            byte[] depotKey = null)
        {
            Id = id;
            ManifestId = manifestId;
            ContentName = contentName;
            DepotKey = depotKey;
        }

        public uint Id { get; private set; }
        public ulong ManifestId { get; private set; }
        public string ContentName { get; private set; }

        public byte[] DepotKey { get; set; }
    }
}
