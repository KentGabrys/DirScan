using System.Windows.Forms;
using DirScan.Logging;

namespace DirScan.Client
{
    public partial class PreferenceForm : Form
    {
        // private LoggingType _loggingType;
        // private string _connectionString;
        public readonly PreferenceModel _preferenceModel;

        public PreferenceForm()
        {
            InitializeComponent();
            _preferenceModel = new PreferenceModel();
            BindControls();
        }

        private void BindControls()
        {
            rbSqlLogger.DataBindings.Add(
                "Checked", _preferenceModel, "IsSqlLoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            rbFileLogger.DataBindings.Add(
                "Checked", _preferenceModel, "IsFileLoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            gbSqlLoggerConfig.DataBindings.Add(
                "Enabled", _preferenceModel, "IsSqlLoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            txtConnectionString.DataBindings.Add(
                "Text", _preferenceModel, "ConnectionString", false, DataSourceUpdateMode.OnPropertyChanged );
        }

        public LoggingType LoggingType
        {
            get => _preferenceModel.LoggingType;
            set => _preferenceModel.LoggingType = value;
        }

        public string ConnectionString
        {
            get => _preferenceModel.ConnectionString;
            set => _preferenceModel.ConnectionString = value;
        }

        private void btnTestConnection_Click( object sender, System.EventArgs e )
        {
            _preferenceModel.TestConnectionString();
        }

        private void btnTableSqlClipboard_Click( object sender, System.EventArgs e )
        {
            _preferenceModel.PutTableCreationSqlOnClipboard();
        }

        private bool _suspendAction = false;

        public void rb_CheckedChanged( object sender, System.EventArgs e )
        {
            if ( _suspendAction ) return;
            _suspendAction = true;
            var rb = sender as RadioButton;
            _preferenceModel.LoggingType = rb?.Name is "rbSqlLogger"
                ? LoggingType.SqlLogger
                : LoggingType.FileLogger;
            _suspendAction = false;
        }
        
        private void txtConnectionString_TextChanged( object sender, System.EventArgs e )
        {
            _preferenceModel.ConnectionString = txtConnectionString.Text;
        }


    }

}
