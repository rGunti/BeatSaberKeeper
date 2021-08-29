using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using BeatSaberKeeper.Kernel.Abstraction;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using BeatSaberKeeper.Kernel.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeatSaberKeeper.Tests.Kernel.V1
{
    [TestClass]
    public class V1CompressionInterfaceTests
    {
        private readonly IFileSystem _fileSystem;
        private readonly V1CompressionInterface _interface;

        public V1CompressionInterfaceTests()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\src\a-file.txt", new MockFileData("This is a file!") },
                { @"C:\src\b-file.txt", new MockFileData("This is B file!") },
                { @"C:\src\BeatSaberVersion.txt", new MockFileData("1.2.3") }
            });
            _interface = new V1CompressionInterface(_fileSystem);
        }

        private void Report(string status, int value, int max)
        {
            Debug.WriteLine($"Status Report: {status.Replace('\n', ' ')} ({value}, max={max})");
        }

        /// <summary>
        /// Helper function you can call when debugging to write the archive to disk for analysis
        /// </summary>
        /// <param name="realPath"></param>
        private void WriteArchiveToRealDisk(string realPath)
        {
            File.WriteAllBytes(realPath, _fileSystem.File.ReadAllBytes(@"C:\archive.zip"));
        }

        [TestMethod]
        public void CanPackArchiveWithoutError()
        {
            _interface.CreateArchiveFromFolder(
                @"C:\src",
                @"C:\archive.zip",
                Report);

            Assert.IsTrue(_fileSystem.File.Exists(@"C:\archive.zip"));
        }

        [TestMethod]
        public void CanUnpackArchive()
        {
            // Call the previous test to create the archive
            CanPackArchiveWithoutError();
            
            _interface.UnpackArchiveToFolder(
                @"C:\archive.zip",
                @"C:\dst",
                Report);
            
            Assert.IsTrue(_fileSystem.Directory.Exists(@"C:\dst"), "Did not create destination directory");
            Assert.IsTrue(_fileSystem.File.Exists(@"C:\dst\a-file.txt"), "Did not create one of the files from the archive source");

            var sourceFileContent = _fileSystem.File.ReadAllText(@"C:\src\a-file.txt");
            var extractedFileContent = _fileSystem.File.ReadAllText(@"C:\dst\a-file.txt");
            
            Assert.AreEqual(sourceFileContent, extractedFileContent, "File Content doesn't match");
        }

        [TestMethod]
        public void CanReadMetaData()
        {
            // Call the previous test to create the archive
            CanPackArchiveWithoutError();

            ArchiveMetaData metadata = _interface.ReadMetaDataFromArchive(@"C:\archive.zip");
            Assert.IsNotNull(metadata);
            
            Assert.AreEqual(V1ArchiveMetaData.V1, metadata.ArchiveVersion);
            Assert.AreEqual("1.2.3", metadata.GameVersion);
            Assert.AreEqual(ArtifactType.ModBackup, metadata.Type);
        }
    }
}