using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BeatKeeper.App.Core.Steam.Manifest
{
    public class ManifestDescription
    {
        public ulong Id { get; set; }
        public DateTime Creation { get; set; }
        public Dictionary<string, ManifestFileObject> Files { get; set; }
    }

    public class ManifestFileObject
    {
        public ulong FileSize { get; set; }
        public string Hash { get; set; }
    }

    public static class ManifestSaver
    {
        public static void WriteManifestToFile(ProtoManifest newProtoManifest, string path)
        {
            var manifestDesc = new ManifestDescription
            {
                Id = newProtoManifest.ID,
                Creation = newProtoManifest.CreationTime,
                Files = newProtoManifest.Files
                    .ToDictionary(f => f.FileName, f => new ManifestFileObject
                    {
                        FileSize = f.TotalSize,
                        Hash = BitConverter.ToString(f.FileHash).Replace("-", "")
                    })
            };

            File.WriteAllText(path, JsonSerializer.Serialize(manifestDesc, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));
        }
    }
}
