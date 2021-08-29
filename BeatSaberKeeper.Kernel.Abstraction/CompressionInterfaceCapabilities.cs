using System;

namespace BeatSaberKeeper.Kernel.Abstraction
{
    [Flags]
    public enum CompressionInterfaceCapabilities
    {
        /// <summary>
        /// This compression interface supports reading metadata
        /// </summary>
        ReadMetaData = 0x01,
        /// <summary>
        /// This compression interface supports creating new archives
        /// </summary>
        PackArchive = 0x02,
        /// <summary>
        /// This compression interface supports unpacking archives
        /// </summary>
        UnpackArchive = 0x04,
        /// <summary>
        /// This compression interfaces supports updating existing archives
        /// </summary>
        UpdateArchive = 0x08,
    }
}