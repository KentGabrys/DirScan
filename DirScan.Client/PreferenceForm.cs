using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DirScan.Logging;

namespace DirScan.Client
{
    public partial class PreferenceForm : Form
    {
        private LoggingType _loggingType;
        private string _connectionString;

        public PreferenceForm()
        {
            InitializeComponent();

        }

        public LoggingType LoggingType
        {
            get => _loggingType;
            set
            {
                _loggingType = value;
                if (_loggingType == LoggingType.SqlLogger)
                {
                    rbSqlLogger.Checked = true;
                    gbSqlLoggerConfig.Enabled = true;
                }
                else if (_loggingType == LoggingType.FileLogger)
                {
                    rbFileLogger.Checked = true;
                    gbSqlLoggerConfig.Enabled = false;
                }
            }
        }

        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                txtConnectionString.Text = _connectionString;
            }
        }

        private void btnTestConnection_Click( object sender, System.EventArgs e )
        {
            var cur = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            var message = string.Empty;

            try
            {
                var formatString = "{0} to \"" + ConnectionString + "\"";

                if (TestConnection())
                    message = string.Format( formatString, "Successfully connected " );
                else
                    message = string.Format( formatString, "Failed connection " );
            }
            finally
            {
                Cursor.Current = cur;
            }
            
            MessageBox.Show( message, @"Test Connection",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation );

        }

        private void btnTableSqlClipboard_Click( object sender, System.EventArgs e )
        {
            PutTableCreationSqlOnClipboard();
        }

        private void rb_CheckedChanged( object sender, System.EventArgs e )
        {
            gbSqlLoggerConfig.Enabled = ((RadioButton)sender).Name is "rbSqlLogger";
            _loggingType = ((RadioButton)sender).Name is "rbSqlLogger" 
                ? LoggingType.SqlLogger 
                : LoggingType.FileLogger;
        }

        private void txtConnectionString_TextChanged( object sender, System.EventArgs e )
        {
            _connectionString = txtConnectionString.Text;
        }

        public bool TestConnection()
        {
            try
            {
                using (var sqlConnection = new SqlConnection( ConnectionString ))
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

        private void PutTableCreationSqlOnClipboard()
        {
            Clipboard.Clear();
            Clipboard.SetText(
              @"create table dbo.DirScanLog(
                    Id int identity(1,1) not null,
                    [File] varchar(max) not null,
                    [Size] bigint not null,
                    [DateCreated] varchar(20) not null,
                    [FileAttributes] varchar(512) not null,
                    [User_Id] [int] not null,
                    [Process_Date] [datetime] not null,
                    [Row_Version] [timestamp] not null,
                    
                constraint [PK__DirScanLog] primary key clustered 
                ( [Id] asc )with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 80) on [primary]
                ) on [primary];
                GO

                alter table [dbo].[DirScanLog]  with check add  constraint [R__FK__DirScanLog__USER_BL__USER_ID] foreign key([User_Id])
                references [dbo].[USER_BL] ([User_Id])
                GO

                alter table [dbo].[DirScanLog] check constraint [R__FK__DirScanLog__USER_BL__USER_ID]
                GO
               " );
        }
    }
}
