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

            Assert.AreEqual(3, dirInfo.FileCount);
            Assert.AreEqual(1, dirInfo.DirectoryCount);
            Assert.AreEqual(28128639, dirInfo.DirectoryFileSize);
            Assert.AreEqual(3, dirInfo.FileTypes.Count());
            Assert.AreEqual(4, dirInfo.DirectoryFiles.Count());

            foreach (var ft in dirInfo.FileTypes)
                Console.WriteLine($"{ft.Extension} : {ft.TotalSize}  ({ft.Length} bytes)");
            foreach (var df in dirInfo.DirectoryFiles)
                Console.WriteLine($"{df.Name, 40} :  {df.IsFile, 6} {df.DateCreated, 12:MM/dd/yyyy} {df.Size} ");
        }


    }

}