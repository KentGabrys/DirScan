using DirScan.Client;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class ClientDirScanModelTests
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
        public void ModelIsCreatedTest()
        {

            Assert.IsTrue(_model is DirScanModel);
        }

        [Test]
        public void ModelHasSelectedFolderTest()
        {
            Assert.AreEqual(_folder, _model.SelectedFolder);
        }

        [Test]
        public void ModelHasFolderSelectedTest()
        {
            Assert.IsTrue(_model.FolderSelected);
        }


    }
}
