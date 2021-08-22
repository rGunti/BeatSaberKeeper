using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace BeatSaberKeeper.Kernel.Utils
{
    public static class StreamUtils
    {
        private static string ReadLoggedLine(
            this StreamReader reader)
        {
            var line = reader.ReadLine();
            Debug.WriteLine($"Read from stream: {line?.Trim() ?? "<null>"}");
            return line;
        }

        public static string ReadUntil(
            this StreamReader sr,
            string delim)
        {
            StringBuilder sb = new StringBuilder();
            bool found = false;

            while (!found && !sr.EndOfStream)
            {
                for (int i = 0; i < delim.Length; i++)
                {
                    char c = (char)sr.Read();
                    sb.Append(c);

                    if (c != delim[i])
                        break;

                    if (i == delim.Length - 1)
                    {
                        sb.Remove(sb.Length - delim.Length, delim.Length);
                        found = true;
                    }
                }
            }

            return sb.ToString();
        }

        public static bool ReadUntil(
            this StreamReader reader,
            Func<StreamReader, string, bool> validationFunc,
            int sleepTime = 100,
            long timeout = 180000)
        {
            var t = new Stopwatch();
            t.Start();

            string line;
            while ((line = reader.ReadLoggedLine()) != null
                   && !validationFunc(reader, line)
                   && (timeout < 0 || t.ElapsedMilliseconds > timeout))
            {
                if (timeout >= 0 && t.ElapsedMilliseconds > timeout)
                {
                    return false;
                }
                Thread.Sleep(sleepTime);
            }

            return true;
        }

        public static bool ReadUntil(
            this StreamReader reader,
            string s,
            int sleepTime = 100,
            long timeout = 180000)
        {
            return reader.ReadUntil(
                (streamReader, line) => line == s,
                sleepTime, timeout);
        }
    }
}