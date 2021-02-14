using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using DirScan.Common;
using DirScan.Data;
using DirScan.ErrorLogging;
using DirScan.Logging;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirScan.Client
{
    public partial class MainForm : Form
    {
        public readonly MainModel _model;
        public MainForm()
        {
            InitializeComponent();

            _model = new MainModel();
            InitializeModel();
            BindControlsToModel();
        }

        private void InitializeModel()
        {
            _model.Message = "Bork bork... ";
            _model.Version = Release.Version;
            _model.CanScanStatistics = false;
            _model.LogDirectories = Properties.Settings.Default.LogDirectories;
            _model.LoggingType = Properties.Settings.Default.LoggerPreference;
            _model.ConnectionString = Properties.Settings.Default.ConnectionString;
            progress.Visible = false;
            status_Resize(this, EventArgs.Empty);
        }


        private void BindControlsToModel()
        {
            // labels
            lblSelectedFolder.DataBindings.Add(
                "Text", _model, "SelectedFolder", false, DataSourceUpdateMode.OnPropertyChanged );
            lblFileTypes.DataBindings.Add(
                "Text", _model, "FileTypesMessage", false, DataSourceUpdateMode.OnPropertyChanged );
            // status bar
            statusMessage.DataBindings.Add(
                "Text", _model, "Message", false, DataSourceUpdateMode.OnPropertyChanged );
            statusLogType.DataBindings.Add(
                "Text", _model, "LoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            statusVersion.DataBindings.Add(
                "Text", _model, "Version", false, DataSourceUpdateMode.OnPropertyChanged );
            //buttons
            btnOpenLogFile.DataBindings.Add(
                "Enabled", _model, "CanOpenLogFile", false, DataSourceUpdateMode.OnPropertyChanged );
            btnScanStats.DataBindings.Add(
                "Enabled", _model, "CanScanStatistics", false, DataSourceUpdateMode.OnPropertyChanged );
        }

        private void btnSelectFolder_Click(object sender, System.EventArgs e)
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    _model.SelectedFolder = dlg.FileName;
                    PrepareScan();
                }
            }
        }
        #region Prepare Scan (Prepares form for a new scan)
        // public for testing
        public void PrepareScan()
        {
            _model.PrepareScan();
            lvStats.Items.Clear();
            lvFileTypes.Items.Clear();
            SetModelLogger();
        }
        #endregion

        private void btnScanStats_Click(object sender, System.EventArgs e)
        {
            InitializeScan();

            var bgScan = new BackgroundWorker();
            bgScan.DoWork += BgScanOnDoWork;
            bgScan.RunWorkerCompleted += BgScanOnRunWorkerCompleted;
            bgScan.RunWorkerAsync();
        }

        #region Initialize Scan (Initialize form for the scan process when scan starts)
        // Public for testing purposes
        public void InitializeScan()
        {
            progress.Visible = true;
            progress.Style = ProgressBarStyle.Marquee;
            progress.MarqueeAnimationSpeed = 50;

            status_Resize(this, EventArgs.Empty);

            _model.Message = "Scan started, please wait...";
        }
        #endregion      

        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            _model.OpenLogFile();
        }

        public virtual void status_Resize(object sender, EventArgs e)
        {
            var formWidth = this.Width;
            var statusControlsWidthMax = formWidth - 40;
            if (progress.Visible)
                statusMessage.Width = statusControlsWidthMax - progress.Width - statusLogType.Width - statusVersion.Width;
            else
                statusMessage.Width = statusControlsWidthMax - statusLogType.Width - statusVersion.Width;
        }

        private void miFilePreferences_Click( object sender, EventArgs e )
        {
            using ( var preferenceForm = new PreferenceForm() )
            {
                SetSavedPreferencesToPreferenceForm(preferenceForm);

                if ( preferenceForm.ShowDialog(this) == DialogResult.OK )
                {
                    UpdateModelFromPreferenceForm(preferenceForm);
                    SaveUserPreferences(preferenceForm);
                }
                SetModelLogger();
            }
        }
        #region  Save User Preferences to user settings
        private static void SetSavedPreferencesToPreferenceForm(PreferenceForm form)
        {
            form.LoggingType = Properties.Settings.Default.LoggerPreference;
            form.ConnectionString =
                !string.IsNullOrEmpty(Properties.Settings.Default.ConnectionString)
                    ? Properties.Settings.Default.ConnectionString
                    : ConfigurationManager.ConnectionStrings["logConnection"].ConnectionString;
            form.LogDirectories = Properties.Settings.Default.LogDirectories;
        }


        private void UpdateModelFromPreferenceForm(PreferenceForm form)
        {
            _model.LoggingType = form.LoggingType;
            _model.LogDirectories = form.LogDirectories;
            _model.ConnectionString = form.ConnectionString;
        }
        private void SaveUserPreferences(PreferenceForm form)
        {
            Properties.Settings.Default.LoggerPreference = form.LoggingType;
            Properties.Settings.Default.LogDirectories = form.LogDirectories;
            if (form.LoggingType == LoggingType.SqlLogger)
                Properties.Settings.Default.ConnectionString = form.ConnectionString;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region SetModelLogger (File or Sql)
        // public and virtual for unit testing
        public virtual void SetModelLogger()
        {
            switch (_model.LoggingType)
            {
                case LoggingType.SqlLogger:
                    _model.CreateSessionSqlLogging();
                    break;
                case LoggingType.FileLogger:
                default:
                    try
                    {
                        _model.CreateSessionLogging();
                    }
                    catch (ScanNotPreparedException exception)
                    {
                        var _errorLogger = ErrorLoggerFactory.Create();
                        _errorLogger.Log(exception, $"Error setting logger type: {_model.LoggingType}");
                    }
                    break;
            }
        }
        #endregion

        #region BackGroundWorker EventHandlers
        private void BgScanOnDoWork(object sender, DoWorkEventArgs e)
        {
            if (_model.FolderSelected)
                e.Result  = _model.ScanStatistics();
        }

        private void BgScanOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progress.Style = ProgressBarStyle.Blocks;
            progress.MarqueeAnimationSpeed = 0;
            progress.Visible = false;
            status_Resize(sender, EventArgs.Empty);

            _model.LoadStatsListView(lvStats, lvFileTypes, e.Result as DirectoryDataSummary);
            _model.ScanComplete();
        }
        #endregion

        private void miFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
