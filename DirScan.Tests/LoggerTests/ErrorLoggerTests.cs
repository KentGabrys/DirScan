using System;
using System.IO;
using DirScan.ErrorLogging;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class ErrorLoggerTests
    {

        private IErrorLogger _logger;

        private readonly string _logFilePath =
            $@"C:\\Temp\\DirScanTestLoggingArea\\DirScanErrorLogs\\DirScanErrorLog.log";

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(_logFilePath))
                File.Delete(_logFilePath);
            _logger = ErrorLoggerFactory.Create(_logFilePath);
        }
        [Test]
        public void ErrorLoggerHasInterface()
        {
            Assert.IsTrue( _logger is IErrorLogger);
        }

        [Test]
        public void ErrorLoggerLogsExceptions()
        {

            var exception = new ArgumentNullException ("TestParameter");
            _logger.Log(exception);

            Assert.IsTrue(File.Exists(_logFilePath));

            var fileContent = File.ReadAllText(_logFilePath);
            Assert.IsTrue(fileContent.StartsWith(
                "Message: Value cannot be null.\r\nParameter name: TestParameter"));
        }

        [Test]
        public void ErrorLoggerLogsExceptionsWithComment()
        {
            var exception = new ArgumentNullException("TestParameter");
            _logger.Log(exception, @"TestComment");

            Assert.IsTrue(File.Exists(_logFilePath));

            var fileContent = File.ReadAllText(_logFilePath);
            
            Assert.IsTrue(fileContent.StartsWith(
                "\r\n" + ("").PadLeft(20, '-') + "\r\n" + "TestComment\r\nMessage: Value cannot be null.\r\nParameter name: TestParameter"));
        }
    }
}