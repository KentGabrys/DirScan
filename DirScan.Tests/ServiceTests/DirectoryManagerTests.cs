﻿using System;
using System.IO;
using DirScan.Logging;
using DirScan.Service;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class DirectoryManagerTests
    {
        private DirectoryManager _dm;
        private DirectoryDataSummary _dds;
        private int _fileCount = 1;


        [SetUp]
        public void SetUp()
        {
            var logTestPath = "C:\\Temp\\DirScanTestArea";
            var logFile =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $@"DirectoryLog_{DateTime.Now.Year,4:0000}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}_{DateTime.Now.Hour,2:00}-{DateTime.Now.Minute,2:00}-{_fileCount++, 2:00}.log");
            var logger = FileLogger.Create(logFile);

            _dm = new DirectoryManager();
            _dm.Scan(logTestPath, logger);
            _dds = _dm.DirectoryDataSummary;
        }

        [Test]
        public void CreateDirectoryManagerTest()
        {
            Assert.IsTrue(_dm is DirectoryManager);
        }

        [Test]
        public void DirectoryMangerHasDirectoryFileSummary()
        { 
            Assert.IsNotNull(_dds);
        }

        [Test]
        public void DirectoryMangerHasDirectoryCount()
        {
            Assert.IsTrue(_dds.DirectoryCount != 0);
        }

        [Test]
        public void DirectoryMangerHasFileCount()
        {
            Assert.IsTrue(_dds.FileCount != 0);
        }

        [Test]
        public void DirectoryManagerHasTotalSizeOfFilesInPath()
        {
            Assert.AreEqual(7, _dds.FileCount);
            Assert.AreEqual(1, _dds.DirectoryCount);
            Assert.AreEqual( 8455466, _dds.Size);
        }
    }


}
