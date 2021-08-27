using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using BeatSaberKeeper.Kernel.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit;

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
    }
}