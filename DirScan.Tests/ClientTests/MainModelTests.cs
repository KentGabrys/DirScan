using System.Collections.Generic;
using DirScan.Client;
using DirScan.Common.Models;
using DirScan.Logging;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class MainModelTests
    {
        private MainModel _model;
        private string _folder;

        [SetUp]
        public void SetUp()
        {
            _model = new MainModel();
            _folder = "C:\\Temp";
            _model.SelectedFolder = _folder;
        }

        [Test]
        public void ModelIsCreatedTest()
        {

            Assert.IsTrue( _model is MainModel );
        }

        [Test]
        public void ModelHasSelectedFolderTest()
        {
            Assert.AreEqual( _folder, _model.SelectedFolder );
        }

        [Test]
        public void ModelHasFolderSelectedTest()
        {
            Assert.IsTrue( _model.FolderSelected );
        }

        [Test]
        public void ModelHasNoFolderSelectedTest()
        {
            _model.SelectedFolder = null;
            Assert.IsFalse( _model.FolderSelected );
        }

        [Test]
        public void FileTypeMessageUpdatesTest()
        {
            var fileTypes = new List<FileType>
            {
                new FileType {Extension = "exe", Length = 4829999},
                new FileType {Extension = "txt", Length = 48299},
                new FileType {Extension = "json", Length = 2954173289}

            };
            var dds = new DirectoryDataSummary
            {
                DirectoryCount = 3,
                FileCount = 100,
                Size = 34829918287,
                FileTypes = fileTypes
            };

            var form = new MainForm();
            form.Show();
            _model.LoadStatsListView( form.lvStats, form.lvFileTypes, dds );
            Assert.AreEqual( "File Types:     3", _model.FileTypesMessage );
        }

        [Test]
        public void PrepareScanInitializesCanOpenFileIsFalse()
        {
            _model.CanOpenLogFile = true;
            _model.PrepareScan();
            Assert.IsFalse( _model.CanOpenLogFile );
        }

        [Test]
        public void PrepareScanInitializesCanScanStatisticsIsTrue()
        {
            _model.CanScanStatistics = false;
            _model.PrepareScan();
            Assert.IsTrue( _model.CanScanStatistics );
        }

        [Test]
        public void PrepareScanInitializesFileTypeCountToZero()
        {
            _model.FileTypeCount = 423;
            Assert.AreEqual( "File Types:   423", _model.FileTypesMessage );
            _model.PrepareScan();
            Assert.AreEqual( "File Types:     0", _model.FileTypesMessage );
        }

        [Test]
        public void PrepareScanInitializesMessageThatANewScanIsPrepared()
        {
            Assert.AreEqual( "", _model.Message );
            _model.PrepareScan();
            Assert.AreEqual( "New scan prepared, awaiting Scan Statistics keypress...", _model.Message );
        }

        [Test]
        public void CreateSessionLoggerWithoutSelectedFolderThrowsScanNotPreparedExceptionTest()
        {
            Assert.Throws<ScanNotPreparedException>( CreateSessionLoggerWithoutSelectedFolder );
        }

        private void CreateSessionLoggerWithoutSelectedFolder()
        {
            _model.SelectedFolder = null;
            _model.CreateSessionLogging();
        }

        [Test]
        public void CreateSessionLoggerCreatesFileLoggerTest()
        {
            _model.CreateSessionLogging();
            Assert.IsTrue( _model._logger is FileLogger);
        }

        [Test]
        public void CreateSessionSqlLoggerThrowsConnectionStringNotFoundException()
        {
            Assert.Throws<ConnectionStringNotFoundException>( CreateSessionSqlLoggerWithoutConnectionString );
        }

        private void CreateSessionSqlLoggerWithoutConnectionString()
        {
            _model.ConnectionString = null;
            _model.CreateSessionSqlLogging();
        }

        [Test]
        public void CreateSessionSqlLoggerCreatesSqlLoggerTest()
        {
            _model.ConnectionString = "my connection string";
            _model.CreateSessionSqlLogging();
            Assert.IsTrue( _model._logger is SqlLogger );
        }

        [Test]
        public void ScanCompleteSetsCanOpenFileFalseTest()
        {
            _model.ScanComplete();
            Assert.IsFalse( _model.CanOpenLogFile );
        }
        [Test]
        public void ScanCompleteSetsCanOpenFileTest()
        {
            _model.CreateSessionLogging();
            _model.ScanComplete();
            Assert.IsTrue( _model.CanOpenLogFile );
        }
    }
}
