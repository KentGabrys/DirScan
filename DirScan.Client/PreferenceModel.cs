using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DirScan.Client.Annotations;
using DirScan.Logging;

namespace DirScan.Client
{
    public class PreferenceModel : INotifyPropertyChanged
    {
        private LoggingType _loggingType;
        private string _connectionString;
        private bool _logDirectories;

        public LoggingType LoggingType
        {
            get => _loggingType;
            set
            {
                _loggingType = value;
                OnPropertyChanged(nameof(IsFileLoggingType));
                OnPropertyChanged(nameof(IsSqlLoggingType));
            }
        }

        public bool LogDirectories
        {
            get => _logDirectories;
            set
            {
                _logDirectories = value;
                OnPropertyChanged();
            }
        }


        public bool IsSqlLoggingType => LoggingType == LoggingType.SqlLogger;

        public bool IsFileLoggingType => LoggingType == LoggingType.FileLogger;

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                OnPropertyChanged();
            }
        }


        #region PropertyChanged Event & Handler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void TestConnectionString()
        {
            var cur = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            var message = string.Empty;

            try
            {
                var formatString = "{0} to \"" + ConnectionString + "\"";

                if (TestConnection())
                    message = string.Format(formatString, "Successfully connected ");
                else
                    message = string.Format(formatString, "Failed connection ");
            }
            finally
            {
                Cursor.Current = cur;
            }

            MessageBox.Show(message, @"Test Connection",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private bool TestConnection()
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    sqlConnection.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void PutTableCreationSqlOnClipboard()
        {
            Clipboard.Clear();
            Clipboard.SetText(
                @"create table dbo.DirScanLog(
                    Id int identity(1,1) not null,
                    [File] varchar(max) not null,
                    [Size] bigint not null,
                    [DateCreated] varchar(20) not null,
                    [DateLastModified] varchar(20) not null,
                    [Owner] varchar(255) not null,
                    [FileAttributes] varchar(512) not null,
                    
                constraint [PK__DirScanLog] primary key clustered 
                ( [Id] asc )with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 80) on [primary]
                ) on [primary];
                GO");

        }

    }
}