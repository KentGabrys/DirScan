using DirScan.Service;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class DirectoryManagerTests
    {
        private DirectoryManager _dm;
        private DirectoryDataSummary _dms;

        [SetUp]
        public void SetUp()
        {
            _dm = new DirectoryManager();
            _dm.Scan("C:\\Temp\\DirScanTestArea");
            _dms = _dm.DirectoryDataSummary;
        }

        [Test]
        public void CreateDirectoryManagerTest()
        {
            Assert.IsTrue(_dm is DirectoryManager);
        }

        [Test]
        public void DirectoryMangerHasDirectoryFileSummary()
        { 
            Assert.IsNotNull(_dms);
        }

        [Test]
        public void DirectoryMangerHasDirectoryCount()
        {
            Assert.IsTrue(_dms.DirectoryCount != 0);
        }

        [Test]
        public void DirectoryMangerHasFileCount()
        {
            Assert.IsTrue(_dms.FileCount != 0);
        }

        [Test]
        public void DirectoryManagerHasTotalSizeOfFilesInPath()
        {
            Assert.AreEqual(7, _dms.FileCount);
            Assert.AreEqual(1, _dms.DirectoryCount);
            Assert.AreEqual(8455454, _dms.Size);
        }
    }


}
