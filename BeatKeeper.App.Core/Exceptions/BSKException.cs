using System;

namespace BeatKeeper.App.Core.Exceptions
{
    public class BSKException : Exception
    {
        public BSKException(string message, Exception innerException = null): base(message, innerException)
        {
        }
    }

    public class PathCreationException : BSKException
    {
        public PathCreationException(string path, string reason = null)
            : base($"Could not create path {path}. {reason ?? "No detailed reason provided."}")
        { }
    }
}
