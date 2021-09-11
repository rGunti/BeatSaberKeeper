using System;

namespace BeatSaberKeeper.Kernel.V2
{
    public class V2CompressionInterfaceFlags
    {
        public const long DEFAULT_MULTIPART_FILESIZE = 536870900L;  // 512 MiB

        public long MinMultiPartFileSize { get; set; } = DEFAULT_MULTIPART_FILESIZE;
    }
}