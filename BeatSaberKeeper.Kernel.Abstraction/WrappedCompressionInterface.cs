using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BeatSaberKeeper.Kernel.Abstraction
{
    public class WrappedCompressionInterface : ICompressionInterface
    {
        private readonly IDictionary<string, ICompressionInterface> _interfaces;

        public WrappedCompressionInterface(IDictionary<string, ICompressionInterface> interfaces)
        {
            _interfaces = interfaces.ToImmutableDictionary();
        }

        private IEnumerable<ICompressionInterface> GetInterfacesWithCapability(
            CompressionInterfaceCapabilities capability)
        {
            return _interfaces.Values.Where(i => i.Capabilities.HasFlag(capability));
        }

        public CompressionInterfaceCapabilities Capabilities => CompressionInterfaceCapabilities.ReadMetaData;

        public void CreateArchiveFromFolder(string sourcePath, string archivePath, ReportProgressDelegate report = null)
        {
            foreach (ICompressionInterface @interface in GetInterfacesWithCapability(CompressionInterfaceCapabilities.PackArchive))
            {
                try
                {
                    @interface.CreateArchiveFromFolder(sourcePath, archivePath, report);
                    return;
                }
                catch (Exception)
                {
                    // skip everything
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
                catch (Exception)
                {
                    // skip everything
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
                catch (Exception)
                {
                    // skip everything
                }
            }

            throw new NotImplementedException($"No interface could be found to read this metadata");
        }

        public ArchiveMetaData ReadMetaDataFromArchive(string archivePath)
        {
            foreach (ICompressionInterface @interface in GetInterfacesWithCapability(CompressionInterfaceCapabilities.ReadMetaData))
            {
                try
                {
                    return @interface.ReadMetaDataFromArchive(archivePath);
                }
                catch (Exception)
                {
                    // skip everything
                }
            }

            throw new NotImplementedException($"No interface could be found to read this metadata");
        }
    }
}