using System.Collections.Generic;

namespace BeatKeeper.App.Core.Steam
{
    public class DepotFileData
    {
        public DepotDownloadInfo DepotDownloadInfo { get; set; }
        public DepotDownloadCounter DepotCounter { get; set; }
        public string StagingDir { get; set; }
        public ProtoManifest Manifest { get; set; }
        public ProtoManifest PreviousManifest { get; set; }
        public List<ProtoManifest.FileData> FilteredFiles { get; set; }
        public HashSet<string> AllFileNames { get; set; }
    }
}
