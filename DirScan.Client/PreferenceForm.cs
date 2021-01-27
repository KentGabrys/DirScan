using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DirScan.Client.Annotations;
using DirScan.Logging;

namespace DirScan.Client
{
    internal class PreferenceModel : INotifyPropertyChanged
    {
        private LoggingType _loggingType;

        public LoggingType LoggingType
        {
            get => _loggingType;
            set
            {
                _loggingType = value; 
                OnPropertyChanged();
            }
        }


        #region PropertyChanged Event & Handler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        #endregion
    }

    public partial class PreferenceForm : Form
    {
        private LoggingType _loggingType;
        private string _connectionString;
        private readonly PreferenceModel _preferenceModel;

        public PreferenceForm()
        {
            InitializeComponent();
            _preferenceModel = new PreferenceModel();
            BindControls();
        }

        private void BindControls()
        {
            rbSqlLogger.DataBindings.Add(
                "Checked", _preferenceModel, "LoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
        }

        public LoggingType LoggingType
        {
            get => _loggingType;
            set
            {
                _loggingType = value;
                _preferenceModel.LoggingType = value;
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
					[DateLastModified] varchar(20) not null,
					[Owner] varchar(255) not null,
                    [FileAttributes] varchar(512) not null,
                    
                constraint [PK__DirScanLog] primary key clustered 
                ( [Id] asc )with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 80) on [primary]
                ) on [primary];
                GO" );

        }
    }

}
