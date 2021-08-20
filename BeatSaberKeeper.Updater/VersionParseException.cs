using System;

namespace BeatSaberKeeper.Updater
{
    public class VersionParseException : Exception
    {
        public VersionParseException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}