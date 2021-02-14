using System.Collections.Generic;
using System.Windows.Forms;
using DirScan.Client;
using DirScan.Common;
using DirScan.Data;
using DirScan.Logging;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class MainFormTests
    {
        private MainForm _form;

        [SetUp]
        public void SetUp()
        {
            _form = new MainForm();
            _form.Show();
        }

        [Test]
        public void MainFormInitializesMessageTest()
        {
            Assert.AreEqual( "Bork bork... ", _form._model.Message );
        }

        [Test]
        public void MainFormInitializesVersionTest()
        {
            Assert.AreEqual(Release.Version, _form._model.Version);
        }
        [Test]
        public void MainFormInitializesCanScanStatisticsTest()
        {
            Assert.IsFalse( _form._model.CanScanStatistics );
        }
        [Test]
        public void MainFormInitializesLoggingTypeTest()
        {
            Assert.AreEqual(Client.Properties.Settings.Default.LoggerPreference, _form._model.LoggingType);
        }
        [Test]
        public void MainFormInitializesConnectionStringTest()
        {
            Assert.AreEqual( Client.Properties.Settings.Default.ConnectionString, _form._model.ConnectionString );
        }

        [Test]
        public void MainFormInitializesProgressEnabledFalseTEst()
        {
            Assert.IsFalse(_form.progress.Visible );
        }

        [Test]
        public void MainFormInitializesCallToResizeStatusBarElementsTest()
        {
            var mock = new MockMainForm();
            mock.Show();
            Assert.IsTrue( mock.StatusResizeHasBeenCalled );
        }

        [Test]
        public void MainFormInitializeScanMakesProgressBarVisibleTest()
        {
            _form.InitializeScan();
            Assert.IsTrue( _form.progress.Visible );
        }

        [Test]
        public void MainFormInitializeScanMakesProgressBarStyleIsMarqueeTest()
        {
            Assert.AreNotEqual( ProgressBarStyle.Marquee, _form.progress.Style );
            _form.InitializeScan();
            Assert.AreEqual( ProgressBarStyle.Marquee, _form.progress.Style );
        }

        [Test]
        public void MainFormInitializeScanMarqueeAnimationSpeedTest()
        {
            Assert.AreNotEqual( 50, _form.progress.MarqueeAnimationSpeed );
            _form.InitializeScan();
            Assert.AreEqual( 50, _form.progress.MarqueeAnimationSpeed );
        }
        [Test]
        public void MainFormInitializeScanWillCallResizeStatusBarTest()
        {
            var mock = new MockMainForm();
            mock.Show();
            mock.StatusResizeHasBeenCalled = false;
            mock.InitializeScan();
            Assert.IsTrue( mock.StatusResizeHasBeenCalled );
        }
        [Test]
        public void MainFormInitializeScanSetsMessageTest()
        {
            _form.InitializeScan();
            Assert.AreEqual( "Scan started, please wait...", _form._model.Message );
        }


        [Test]
        public void ModelLoadsListView()
        {
            LoadStatsListView(_form);
            Assert.AreEqual(4, _form.lvStats.Items.Count);
            Assert.AreEqual(2, _form.lvFileTypes.Items.Count);
        }

        [Test]
        public void OpenFileDisabledEnabledTest()
        {
            _form._model.CanOpenLogFile = false;
            Assert.IsFalse( _form.btnOpenLogFile.Enabled );

        }

        [Test]
        public void OpenFileEnabledTest()
        {
            _form._model.CanOpenLogFile = true;
            Assert.IsTrue(_form.btnOpenLogFile.Enabled);

        }

        [Test]
        public void SelectFolderUpdatesTextBox()
        {
            var path = "C:\\Test\\Directory\\Path";
            _form._model.SelectedFolder = path;
            Assert.AreEqual( path, _form.lblSelectedFolder.Text );
        }

        [Test]
        public void MainFormSetModelLoggerCreatesFileLoggerForFileLoggerTypeTest()
        {
            _form._model.LoggingType = LoggingType.FileLogger;
            _form._model.SelectedFolder = "c:\\";
            
            _form.SetModelLogger();
            Assert.IsTrue(_form._model._logger is FileLogger);
        }


        [Test]
        public void MainFormSetModelLoggerCreatesFileLoggerForSqlLoggerTypeTest()
        {
            _form._model.LoggingType = LoggingType.SqlLogger;
            _form._model.ConnectionString = "my test connection string";

            _form.SetModelLogger();
            Assert.IsTrue( _form._model._logger is SqlLogger );
        }

        [Test]
        public void PrepareScanClearsFormListViewsTest()
        {
            LoadStatsListView(_form);

            _form._model.SelectedFolder = "C:\\";
            _form.PrepareScan();

            Assert.AreEqual(0, _form.lvStats.Items.Count);
            Assert.AreEqual(0, _form.lvFileTypes.Items.Count);
        }

        [Test]
        public void PrepareScanCallsSetModelLoggerTest()
        {
            var mock = new MockMainForm();
            LoadStatsListView(mock);

            mock._model.SelectedFolder = "C:\\";
            mock.PrepareScan();

            Assert.IsTrue( mock.SetModelLoggerHasBeenCalled );
        }

        private void LoadStatsListView(MainForm form)
        {
            var fileTypes = new List<FileType>
            {
                new FileType() { Extension = "exe", Length = 4829999 },
                new FileType() { Extension = "txt", Length = 48299 }
            };
            var dds = new DirectoryDataSummary()
            {
                DirectoryCount = 3,
                FileCount = 100,
                Size = 34829918287,
                FileTypes = fileTypes
            };
            form._model.LoadStatsListView( form.lvStats, form.lvFileTypes, dds );
        }
    }
}