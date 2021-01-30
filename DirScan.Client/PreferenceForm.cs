using System.Windows.Forms;
using DirScan.Logging;

namespace DirScan.Client
{
    public partial class PreferenceForm : Form
    {
        public readonly PreferenceModel _model;

        public PreferenceForm()
        {
            InitializeComponent();
            _model = new PreferenceModel();
            BindControls();
        }

        private void BindControls()
        {
            rbSqlLogger.DataBindings.Add(
                "Checked", _model, "IsSqlLoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            rbFileLogger.DataBindings.Add(
                "Checked", _model, "IsFileLoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            gbSqlLoggerConfig.DataBindings.Add(
                "Enabled", _model, "IsSqlLoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            txtConnectionString.DataBindings.Add(
                "Text", _model, "ConnectionString", false, DataSourceUpdateMode.OnPropertyChanged );
            chkLogDirectories.DataBindings.Add(
                "Checked", _model, "LogDirectories", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public LoggingType LoggingType
        {
            get => _model.LoggingType;
            set => _model.LoggingType = value;
        }

        public string ConnectionString
        {
            get => _model.ConnectionString;
            set => _model.ConnectionString = value;
        }

        public bool LogDirectories
        {
            get=> _model.LogDirectories; 
            set=> _model.LogDirectories = value;
        }

        private void btnTestConnection_Click( object sender, System.EventArgs e )
        {
            _model.TestConnectionString();
        }

        private void btnTableSqlClipboard_Click( object sender, System.EventArgs e )
        {
            _model.PutTableCreationSqlOnClipboard();
        }

        private bool _suspendAction = false;
        public void rb_CheckedChanged( object sender, System.EventArgs e )
        {
            if ( _suspendAction ) return;
            _suspendAction = true;
            var rb = sender as RadioButton;
            _model.LoggingType = rb?.Name is "rbSqlLogger"
                ? LoggingType.SqlLogger
                : LoggingType.FileLogger;
            _suspendAction = false;
        }
        
        private void txtConnectionString_TextChanged( object sender, System.EventArgs e )
        {
            _model.ConnectionString = txtConnectionString.Text;
        }
    }
}
