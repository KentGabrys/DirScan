using System;
using System.IO;
using DirScan.Logging;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class FileLoggerTests
    {
        private ILogger _logger;

        private readonly string _testFileName = 
            $@"C:\\Temp\\DirScanTestLoggingArea\\TestLog.log";

        [SetUp]
        public void SetUp()
        {
            if( File.Exists( _testFileName ))
                File.Delete(_testFileName);
            _logger = FileLogger.Create(_testFileName);
        }
        [Test]
        public void FileLoggerCreatedWithInterfaceTest()
        {
            Assert.IsTrue(_logger is ILogger);
        }

        [Test]
        public void FileLoggerLogsString()
        {
            const string testString = "This is a test";
            _logger.Log(testString);

            Assert.IsTrue(File.Exists(_testFileName));

            var fileContent = File.ReadAllText(_testFileName);
            Assert.AreEqual(testString + Environment.NewLine, fileContent );
        }

        [Test]
        public void FileLoggerHeaderTest()
        {
            var headerFileName = $@"C:\\Temp\\DirScanTestLoggingArea\\TestLogWithHeader.log";
            if(File.Exists(_testFileName))
                File.Delete(_testFileName);
            if(File.Exists(headerFileName))
                File.Delete(headerFileName);

            const string headerString = "Header Test";
            const string testString = "This is a test";

            var log = FileLogger.Create(headerFileName , headerString);
            log.Log(testString);

            var fileContent = File.ReadAllText(headerFileName);
            Assert.AreEqual(headerString + Environment.NewLine + testString + Environment.NewLine, fileContent);

        }
    }
}
