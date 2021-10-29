using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Security.Cryptography;
using BeatSaberKeeper.Kernel.Abstraction;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using BeatSaberKeeper.Kernel.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeatSaberKeeper.Tests.Kernel.V2
{
    [TestClass]
    public class V2CompressionInterfaceTests
    {
        private const string ARCHIVE = @"C:\archive.zip";
        
        private readonly IFileSystem _fileSystem;
        private readonly V2CompressionInterface _interface;

        public V2CompressionInterfaceTests()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\src\a-file.txt", new MockFileData("This is a file!") },
                { @"C:\src\b-file.txt", new MockFileData("This is B file!") },
                { @"C:\src\BeatSaberVersion.txt", new MockFileData("1.2.3") }
            });
            _interface = new V2CompressionInterface(_fileSystem);
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
                ARCHIVE,
                Report);

            Assert.IsTrue(_fileSystem.File.Exists(ARCHIVE));
        }

        [TestMethod]
        public void CanUnpackArchive()
        {
            // Call the previous test to create the archive
            CanPackArchiveWithoutError();
            
            _interface.UnpackArchiveToFolder(
                ARCHIVE,
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

            ArchiveMetaData metadata = _interface.ReadMetaDataFromArchive(ARCHIVE);
            Assert.IsNotNull(metadata);
            
            Assert.AreEqual(V2ArchiveMetaData.VERSION, metadata.ArchiveVersion);
            Assert.AreEqual("1.2.3", metadata.GameVersion);
            Assert.AreEqual(ArtifactType.ModBackup, metadata.Type);
        }

        [TestMethod]
        public void CanUpdateArchive()
        {
            // Call the previous test to create the archive
            CanPackArchiveWithoutError();
            
            _fileSystem.File.WriteAllText(@"C:\src\b-file.txt", "This is an updated B file!");
            
            _interface.UpdateArchiveFromFolder(@"C:\src", ARCHIVE, Report);

            var metadata = (V2ArchiveMetaData)_interface.ReadMetaDataFromArchive(ARCHIVE);

            CommitFile file = metadata.Files.FirstOrDefault(f => f.Path == "b-file.txt");
            Assert.IsNotNull(file, "File record is missing");
            Assert.AreEqual(2, file.Commits.Count, "Number of commits doesn't match");
        }

        [TestMethod]
        public void DeletedFileIsRecordedCorrectly()
        {
            // Call the previous test to create the archive
            CanPackArchiveWithoutError();
            
            _fileSystem.File.Delete(@"C:\src\b-file.txt");
            
            _interface.UpdateArchiveFromFolder(@"C:\src", ARCHIVE, Report);

            var metadata = (V2ArchiveMetaData)_interface.ReadMetaDataFromArchive(ARCHIVE);

            CommitFile file = metadata.Files.FirstOrDefault(f => f.Path == "b-file.txt");
            Assert.IsNotNull(file, "File record is missing");
            
            Assert.IsTrue(file.GetNewestCommit().FileDeleted, "Latest commit doesn't mark file as deleted");
            
            // Unpack archive and check if the file is not produced
            _interface.UnpackArchiveToFolder(ARCHIVE, @"C:\dst", Report);
            
            Assert.IsFalse(_fileSystem.File.Exists(@"C:\dst\b-file.txt"),
                "Deleted file was produced from archive");
        }

        [TestMethod]
        public void CanRestoreOlderVersion()
        {
            // Call the previous test to create the archive
            DeletedFileIsRecordedCorrectly();
            
            // Clear extraction directory
            _fileSystem.Directory.Delete(@"C:\dst", true);

            // Get Date and Time when second commit was made
            var metadata = (V2ArchiveMetaData)_interface.ReadMetaDataFromArchive(ARCHIVE);

            CommitFile file = metadata.Files.First(f => f.Path == "b-file.txt");
            DateTime commitDate = file.Commits.First(c => !c.FileDeleted).CommitDate;

            // Extract as of time before b-file.txt was deleted
            _interface.UnpackArchiveToFolder(ARCHIVE, @"C:\dst", commitDate, Report);

            Assert.IsTrue(_fileSystem.File.Exists(@"C:\dst\b-file.txt"),
                "Deleted file was restored");
        }

        private void GenerateFile(string fileName, long fileSize)
        {
            var rng = new Random();
            var bytes = new byte[fileSize];
            rng.NextBytes(bytes);

            _fileSystem.File.WriteAllBytes(fileName, bytes);
        }

        private MD5 md5 = MD5.Create();
        private void CompareFiles(string expectedFile, string actualFile, string message = null)
        {
            using var expected = _fileSystem.FileStream.Create(expectedFile, FileMode.Open);
            using var actual = _fileSystem.FileStream.Create(actualFile, FileMode.Open);

            Assert.AreEqual(
                md5.ComputeHash(expected).ToHexString(),
                md5.ComputeHash(actual).ToHexString(), message);
        }

        [TestMethod]
        public void SplitFilesWhenTooLarge()
        {
            // To make things easier to test, the multipart file size is reduced
            V2CompressionInterface.Flags.MinMultiPartFileSize = 1024;
            
            // Generate some random files
            GenerateFile(@"C:\src\rng.1k", 1024); // <- this file should not be split
            GenerateFile(@"C:\src\rng.5k", 1024 * 5); // <-- this file should be split
            
            // Generate archive and unpack it again
            CanUnpackArchive();

            // Getting metadata
            var metadata = (V2ArchiveMetaData)_interface.ReadMetaDataFromArchive(ARCHIVE);

            CommitFile file1k = metadata.Files.First(f => f.Path == "rng.1k");
            CommitFile file5k = metadata.Files.First(f => f.Path == "rng.5k");

            // Validate if multipart records are present in metadata
            Assert.IsFalse(file1k.Commits.First().HasMultiPart, "1k file should not have been split");
            Assert.IsTrue(file5k.Commits.First().HasMultiPart, "5k file should have been split");

            // Check unpacked file
            Assert.IsTrue(_fileSystem.File.Exists(@"C:\dst\rng.5k"), "5k file was not unpacked");
            CompareFiles(@"C:\src\rng.5k", @"C:\dst\rng.5k", "Unpacked file is different from source file");
        }

        [TestMethod]
        public void SplitFilesOnUpdateWhenTooLarge()
        {
            // Run the regular split test
            SplitFilesWhenTooLarge();
            
            // Update the 5k file
            GenerateFile(@"C:\src\rng.5k", 1024 * 5);

            // Update the archive
            _interface.UpdateArchiveFromFolder(@"C:\src", ARCHIVE, Report);

            
            // Getting metadata
            var metadata = (V2ArchiveMetaData)_interface.ReadMetaDataFromArchive(ARCHIVE);
            
            // Validating if newest commit is multipart
            CommitFile file5k = metadata.Files.First(f => f.Path == "rng.5k");
            Assert.IsTrue(file5k.Commits.Count > 1, "No new commit has been made");
            Assert.IsTrue(file5k.Commits.OrderByDescending(c => c.CommitDate).First().HasMultiPart,
                "Newest commit is not multipart");
        }
    }
}