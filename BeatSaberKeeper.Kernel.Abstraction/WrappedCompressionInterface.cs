using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using Serilog;

namespace BeatSaberKeeper.Kernel.Abstraction
{
    public class WrappedCompressionInterface : ICompressionInterface
    {
        private static readonly ILogger Logger = Log.ForContext<WrappedCompressionInterface>();
        private readonly IDictionary<string, ICompressionInterface> _interfaces;
        private readonly string _defaultVersion;

        public WrappedCompressionInterface(
            IDictionary<string, ICompressionInterface> interfaces,
            string defaultVersion)
        {
            _interfaces = interfaces.ToImmutableDictionary();
            _defaultVersion = defaultVersion;
        }

        private IEnumerable<ICompressionInterface> GetInterfacesWithCapability(
            CompressionInterfaceCapabilities capability)
        {
            return _interfaces.Values.Where(i => i.Capabilities.HasFlag(capability));
        }

        private ICompressionInterface GetInterfaceByProbing(string archiveFile)
        {
            return _interfaces.Values.FirstOrDefault(@interface => @interface.ProbeVersion(archiveFile));
        }

        private ICompressionInterface DefaultInterface => _interfaces[_defaultVersion];

        public CompressionInterfaceCapabilities Capabilities => CompressionInterfaceCapabilities.ReadMetaData;

        public void CreateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null,
            ArtifactType artifactType = ArtifactType.ModBackup)
        {
            if (DefaultInterface.Capabilities.HasFlag(CompressionInterfaceCapabilities.PackArchive))
            {
                DefaultInterface.CreateArchiveFromFolder(sourcePath, archivePath, report, artifactType);
                return;
            }

            foreach (ICompressionInterface @interface in GetInterfacesWithCapability(CompressionInterfaceCapabilities.PackArchive))
            {
                try
                {
                    @interface.CreateArchiveFromFolder(sourcePath, archivePath, report, artifactType);
                    return;
                }
                catch (Exception ex)
                {
                    Logger.Warning(ex, "Failed to create archive {ArchivePath} from {SourcePath} using interface {Interface}",
                        archivePath, sourcePath, @interface);
                }
            }

            throw new NotImplementedException($"No interface could be found to read this metadata");
        }

        public void UnpackArchiveToFolder(string archivePath, string destinationPath, ReportProgressDelegate report = null)
        {
            foreach (ICompressionInterface @interface in GetInterfacesWithCapability(CompressionInterfaceCapabilities.UnpackArchive))
            {
                try
                {
                    @interface.UnpackArchiveToFolder(archivePath, destinationPath, report);
                    return;
                }
                catch (Exception ex)
                {
                    Logger.Warning(ex, "Failed to unpack archive {ArchivePath} to {SourcePath} using interface {Interface}",
                        archivePath, destinationPath, @interface);
                }
            }

            throw new NotImplementedException($"No interface could be found to read this metadata");
        }

        public void UpdateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null)
        {
            foreach (ICompressionInterface @interface in GetInterfacesWithCapability(CompressionInterfaceCapabilities.UpdateArchive))
            {
                try
                {
                    @interface.UpdateArchiveFromFolder(sourcePath, archivePath, report);
                    return;
                }
                catch (Exception ex)
                {
                    Logger.Warning(ex, "Failed to update archive {ArchivePath} from {SourcePath} using interface {Interface}",
                        archivePath, sourcePath, @interface);
                }
            }

            throw new NotImplementedException($"No interface could be found to read this metadata");
        }

        public ArchiveMetaData ReadMetaDataFromArchive(string archivePath)
        {
            ICompressionInterface probedInterface = GetInterfaceByProbing(archivePath);
            if (probedInterface != null)
            {
                return probedInterface.ReadMetaDataFromArchive(archivePath);
            }

            // If probing didn't work, just try every interface possible
            foreach (ICompressionInterface @interface in GetInterfacesWithCapability(CompressionInterfaceCapabilities.ReadMetaData))
            {
                try
                {
                    return @interface.ReadMetaDataFromArchive(archivePath);
                }
                catch (Exception ex)
                {
                    Logger.Warning(ex, "Failed to read metadata from archive {ArchivePath} using interface {Interface}",
                        archivePath, @interface);
                }
            }

            throw new NotImplementedException($"No interface could be found to read this metadata");
        }

        public bool ProbeVersion(string archivePath)
        {
            foreach (ICompressionInterface @interface in _interfaces.Values)
            {
                if (@interface.ProbeVersion(archivePath))
                {
                    return true;
                }
            }

            return false;
        }
    }
}