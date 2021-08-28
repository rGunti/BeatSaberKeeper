using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using BeatSaberKeeper.Kernel.Abstraction.Entities;
using BeatSaberKeeper.Kernel.Abstraction.Repo;
using BeatSaberKeeper.Kernel.V1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeatSaberKeeper.Tests.Kernel
{
    [TestClass]
    public class ArtifactRepositoryTests
    {
        private readonly IFileSystem _fileSystem;
        private readonly V1CompressionInterface _interface;
        private readonly IRepository<Artifact> _repository;

        public ArtifactRepositoryTests()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\src\a-file.txt", new MockFileData("This is a file!") },
                { @"C:\src\b-file.txt", new MockFileData("This is B file!") },
                { @"C:\src\BeatSaberVersion.txt", new MockFileData("1.2.3") },
                { @"C:\zips", new MockDirectoryData() }
            });
            _interface = new V1CompressionInterface(_fileSystem);
            _repository = new ArtifactRepository(@"C:\zips", _fileSystem, _interface);
        }
        
        private void Report(string status, int value, int max)
        {
            Debug.WriteLine($"Status Report: {status.Replace('\n', ' ')} ({value}, max={max})");
        }

        private string PackSampleArchive()
        {
            const string PATH = @"C:\zips\Archive.bskeep";
            _interface.CreateArchiveFromFolder(@"C:\src", PATH, Report);
            Assert.IsTrue(_fileSystem.File.Exists(PATH), "Sample Archive was not created");
            return PATH;
        }

        [TestMethod]
        public void CanReadArchiveList()
        {
            string archivePath = PackSampleArchive();

            List<Artifact> archiveList = _repository.GetAll().ToList();
            Artifact testArchive = archiveList
                .FirstOrDefault(a => a.Name == Path.GetFileNameWithoutExtension(archivePath));
            Assert.IsNotNull(testArchive);
            
            Assert.AreEqual(ArtifactType.ModBackup, testArchive.Type);
            Assert.AreEqual("v1", testArchive.ArchiveVersion);
        }
    }
}