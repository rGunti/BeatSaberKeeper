using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeatSaberKeeper.Kernel.V2
{
    public static class V2InternalExtensions
    {
        internal static void CopyPartTo(this Stream input, Stream output, int bytes, int bufferSize = 32768)
        {
            byte[] buffer = new byte[bufferSize];
            int read;
            while (bytes > 0 && 
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        public static string ToHexString(this IEnumerable<byte> bytes)
            => string.Join("", bytes.Select(b => $"{b:x2}"));
    }
}