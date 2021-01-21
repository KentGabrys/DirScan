using System;
using System.Linq;
using DirScan.Service;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class DirectoryServiceTests
    {
        private DirectoryService _svc;
        private DirectoryData _dirInfo;

        [SetUp]
        public void SetUp()
        { 
            _svc = new DirectoryService();
            _dirInfo = _svc.Scan("c:\\Temp\\DirScanTestArea");
        }

        [Test]
        public void ServiceIsCreatedTest()
        {
            Assert.IsTrue(_svc is IDirectoryService);
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
            Assert.AreEqual(4, _dirInfo.DirectoryFiles.Count());

            foreach (var df in _dirInfo.DirectoryFiles)
                Console.WriteLine($"{df.Name,40} :  {df.IsFile,6} {df.DateCreated,12:MM/dd/yyyy} {df.Size} ");
        }


    }

}