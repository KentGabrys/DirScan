using DirScan.Client;
using DirScan.Logging;
using NUnit.Framework;

namespace DirScan.Tests
{
    [TestFixture]
    public class PreferenceFormTests
    {
        private PreferenceForm _form;

        [SetUp]
        public void SetUp()
        {
            _form = new PreferenceForm();
            _form.Show();
        }

        [Test]
        public void PreferenceFormSqlLoggerControlIsDisabledTest()
        {
            _form.LoggingType = LoggingType.FileLogger;
            Assert.IsFalse( _form.gbSqlLoggerConfig.Enabled );
        }
        [Test]
        public void PreferenceFormSqlLoggerControlIsEnabledTest()
        {
            _form.LoggingType = LoggingType.SqlLogger;
            Assert.IsTrue( _form.gbSqlLoggerConfig.Enabled );
        }

        [Test]
        public void PreferenceFormInitializesModelFileLoggerTest()
        {
            _form.LoggingType = LoggingType.FileLogger;
            Assert.IsTrue( _form._preferenceModel.IsFileLoggingType );
            Assert.IsFalse( _form._preferenceModel.IsSqlLoggingType );
        }
        [Test]
        public void PreferenceFormInitializesModelSqlLoggerTest()
        {
            _form.LoggingType = LoggingType.SqlLogger;
            Assert.IsFalse( _form._preferenceModel.IsFileLoggingType );
            Assert.IsTrue( _form._preferenceModel.IsSqlLoggingType );
        }

        [Test]
        public void PrefFormRadioButtonChangesLoggingTypeToFileLogger()
        {
            _form.LoggingType = LoggingType.SqlLogger;
            _form.rbFileLogger.Checked = true;
            Assert.IsTrue( _form.rbFileLogger.Checked );
            Assert.IsFalse( _form.rbSqlLogger.Checked );
            Assert.AreEqual( LoggingType.FileLogger, _form.LoggingType );
        }

        [Test]
        public void PrefFormRadioButtonChangesLoggingTypeToSqlLogger()
        {
            _form.LoggingType = LoggingType.FileLogger;
            _form.rbSqlLogger.Checked = true;
            Assert.IsFalse( _form.rbFileLogger.Checked );
            Assert.IsTrue( _form.rbSqlLogger.Checked );
            Assert.AreEqual( LoggingType.SqlLogger, _form.LoggingType );
        }

        [Test]
        public void FormConnectionStringUpdatesModelTest()
        {
            _form.ConnectionString = "Test String";
            Assert.AreEqual( "Test String", _form._preferenceModel.ConnectionString );
        }

        [Test]
        public void FormConnectionStringUpdatesTextBoxTest()
        {
            _form.ConnectionString = "Test String";
            Assert.AreEqual( "Test String", _form.txtConnectionString.Text );

        }
    }
}