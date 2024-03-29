﻿using Serilog;
using System.IO;
using BeatSaberKeeper.App.Core.Exceptions;

namespace BeatSaberKeeper.App.Core.Utils
{
    public static class PathUtils
    {
        private static ILogger Logger => Log.ForContext(typeof(PathUtils));

        /// <summary>
        /// Ensures that a directory with the given name already exists and returns its full path.
        /// If it does not, it will be created.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="throwOnConflict"></param>
        /// <returns></returns>
        public static string EnsureDirectory(this string path, bool throwOnConflict = true)
        {
            if (!Directory.Exists(path))
            {
                if (File.Exists(path))
                {
                    Logger.Error("Could not create directory {path} because a file with the given name already exists.", path);
                    if (throwOnConflict)
                    {
                        throw new PathCreationException(path, "A file with the given name already exists.");
                    }
                    return path;
                }
                Logger.Information("Creating directory {path}", path);
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string EnsureDirectoryForFile(
            this string path,
            bool throwOnConflict = true)
        {
            if (!File.Exists(path))
            {
                if (Directory.Exists(path))
                {
                    if (throwOnConflict)
                    {
                        throw new PathCreationException(path, "Directory with file name already exists.");
                    }
                } else
                {
                    Path.GetDirectoryName(path).EnsureDirectory(throwOnConflict);
                }
            }
            return path;
        }

        public static string AppendExtension(this string path, string extension)
        {
            return $"{path}{extension}";
        }

        public static string ConstructStagingFilePath(uint appId, uint depotId, ulong manifest)
        {
            return Path.Combine(BSKConstants.Paths.Staging, $"{appId}", $"{depotId}_{manifest}");
        }
    }
}
