using System.Collections.Generic;
using DirScan.Client;
using DirScan.Common.Models;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class ClientFormTests
    {
        private DirScanModel _model;
        private string _folder;

        [SetUp]
        public void SetUp()
        {
            _model = new DirScanModel();
            _folder = "C:\\Temp";
            _model.SelectedFolder = _folder;
        }
        [Test]
        public void ModelLoadsListView()
        {
            var fileTypes = new List<FileType>
            {
                new FileType() {Extension = "exe", Length = 4829999},
                new FileType() {Extension = "txt", Length = 48299}
            };
            var dds = new DirectoryDataSummary()
            {
                DirectoryCount = 3,
                FileCount = 100,
                Size = 34829918287,
                FileTypes = fileTypes
            };
            var form = new MainForm();
            form.Show();
            _model.LoadStatsListView(form.lvStats, form.lvFileTypes, dds);
            Assert.AreEqual(4, form.lvStats.Items.Count);
            Assert.AreEqual(2, form.lvFileTypes.Items.Count);
        }

        [Test]
        public void OpenFileEnabledTest()
        {
            var form = new MainForm();
            form.Show();
            form._model.CanOpenLogFile = true;
            Assert.IsTrue(form.btnOpenLogFile.Enabled);

        }
    }
}