using System;
using System.IO;
using System.Linq;
using AutoMapper;
using DirScan.Data;
using DirScan.Service;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class DirectoryServiceTests
    {
        private DirectoryService _svc;
        private DirectoryData _dirInfo;
        private int _fileCount = 1;

        [SetUp]
        public void SetUp()
        {
            var mapper = new Mapper( new MapperConfiguration( a => a.AddProfile( new MappingProfile() ) ) );

            var logFile =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $@"DirectoryLog_{DateTime.Now.Year,4:0000}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}_{DateTime.Now.Hour,2:00}-{DateTime.Now.Minute,2:00}-{_fileCount++}.log");

            var logger = Logging.FileLogger.Create(logFile);

            _svc = new DirectoryService(logger, mapper);
            _dirInfo = _svc.Scan("c:\\Temp\\DirScanTestArea");
        }

        [Test]
        public void ServiceIsCreatedTest()
        {
            Assert.IsTrue(_svc is DirectoryServiceBase);
        }

        [Test]
        public void ServiceScansDirectoryHasFileCount()
        {
            Assert.AreEqual(3, _dirInfo.FileCount);
        }

        [Test]
        public void ServiceScansDirectoryHasDirectoryCount()
        {
            Assert.AreEqual(1, _dirInfo.DirectoryCount);
        }

        [Test]
        public void ServiceScansDirectoryHasDirectoryFileSize()
        {
            Assert.AreEqual(4227699, _dirInfo.DirectoryFileSize);
        }

        [Test]
        public void ServiceScansDirectoryHasFileTypesCount()
        {
            Assert.AreEqual(2, _dirInfo.FileTypes.Count());

            foreach (var ft in _dirInfo.FileTypes)
                Console.WriteLine($"{ft.Extension, 5} : {ft.TotalSize}  ({ft.Length} bytes)");
        }

        [Test]
        public void ServiceScansDirectoryHasDirectoryFilesCount()
        {
            Assert.AreEqual(1, _dirInfo.Directories.Count());
            Assert.AreEqual(3, _dirInfo.Files.Count);

            foreach (var d in _dirInfo.Directories)
                Console.WriteLine($"{d.Name,40}");

            foreach (var f in _dirInfo.Files)
                Console.WriteLine($"{f.Name,40} : {f.Length,15}");
        }


    }

}