﻿using System.IO;
using System.Threading;

namespace BeatKeeper.App.Core.Steam
{
    internal class FileStreamData
    {
        public FileStream Stream { get; set; }
        public SemaphoreSlim Lock { get; set; }
        public int chunksToDownload;
    }
}