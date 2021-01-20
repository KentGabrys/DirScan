using System;
using System.IO;
using System.Linq;
using DirScan.Service;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class DirScanServiceTests
    {
        [Test]
        public void ServiceIsCreatedTest()
        {
            var svc = new DirectoryService();
            Assert.IsTrue(svc is IDirectoryService);
        }

        [Test]
        public void ServiceScansDirectory()
        {
            var svc = new DirectoryService();
            var dirInfo = svc.Scan("c:\\Temp");
            foreach (var ft in dirInfo.FileTypes)
                Console.WriteLine($"{ft.Extension} : {ft.TotalSize}  ({ft.Length} bytes)");

            Assert.AreEqual(3, dirInfo.FileCount);
            Assert.AreEqual(1, dirInfo.DirectoryCount);
            Assert.AreEqual(28128639, dirInfo.DirectoryFileSize);
        }

        [Test]
        public void ServiceMapsDirectory()
        {

        }

        [Test]
        public void FileManagerTest()
        {
            var dirFiles = Directory.EnumerateFiles("c:\\temp", "*.*");
            Assert.AreEqual(3, dirFiles.Count());

            var dirs = Directory.EnumerateDirectories("c:\\temp", "*.*");
            Assert.AreEqual(1, dirs.Count());


        }
    }

}