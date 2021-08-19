using BeatKeeper.App.Core.Steam;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatKeeper.App.Core.Utils
{
    internal static class SteamUtils
    {
        public static List<ProtoManifest.ChunkData> ValidateSteamFileChecksums(FileStream fs, ProtoManifest.ChunkData[] chunkdata)
        {
            var neededChunks = new List<ProtoManifest.ChunkData>();
            int read;

            foreach (var data in chunkdata)
            {
                byte[] chunk = new byte[data.UncompressedLength];
                fs.Seek((long)data.Offset, SeekOrigin.Begin);
                read = fs.Read(chunk, 0, (int)data.UncompressedLength);

                byte[] tempchunk;
                if (read < data.UncompressedLength)
                {
                    tempchunk = new byte[read];
                    Array.Copy(chunk, 0, tempchunk, 0, read);
                }
                else
                {
                    tempchunk = chunk;
                }

                byte[] adler = AdlerHash(tempchunk);
                if (!adler.SequenceEqual(data.Checksum))
                {
                    neededChunks.Add(data);
                }
            }

            return neededChunks;
        }

        public static byte[] AdlerHash(byte[] input)
        {
            uint a = 0, b = 0;
            for (int i = 0; i < input.Length; i++)
            {
                a = (a + input[i]) % 65521;
                b = (b + a) % 65521;
            }
            return BitConverter.GetBytes(a | (b << 16));
        }

        public static async void WriteToFile(this CDNClient.DepotChunk chunk, FileStreamData data)
        {
            try
            {
                await data.Lock.WaitAsync().ConfigureAwait(false);

                var stream = data.Stream;
                stream.Seek((long)chunk.ChunkInfo.Offset, SeekOrigin.Begin);
                await stream.WriteAsync(chunk.Data, 0, chunk.Data.Length);
            }
            finally
            {
                data.Lock.Release();
            }
        }
    }
}
