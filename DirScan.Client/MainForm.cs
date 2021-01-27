using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using DirScan.Common;
using DirScan.Common.Models;
using DirScan.Logging;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirScan.Client
{
    public partial class MainForm : Form
    {
        public readonly DirScanModel _model;


        public MainForm()
        {
            InitializeComponent();

            _model = new DirScanModel();
            BindControlsToModel();
            InitializeModel();
        }

        private void InitializeModel()
        {
            _model.Message = "Bork bork... ";
            _model.Version = Release.Version;
            _model.CanScanStatistics = false;
            _model.LoggingType = Properties.Settings.Default.LoggerPreference;
            _model.ConnectionString = Properties.Settings.Default.ConnectionString;
            progress.Visible = false;
            status_Resize(this, EventArgs.Empty);
        }


        private void BindControlsToModel()
        {
            // labels
            lblSelectedFolder.DataBindings.Add(
                "Text", _model, "SelectedFolder", false, DataSourceUpdateMode.OnPropertyChanged);

            lblFileTypes.DataBindings.Add(
                "Text", _model, "FileTypesMessage", false, DataSourceUpdateMode.OnPropertyChanged );
            
            // status bar
            statusMessage.DataBindings.Add(
                "Text", _model, "Message", false, DataSourceUpdateMode.OnPropertyChanged);
            statusLogType.DataBindings.Add(
                "Text", _model, "LoggingType", false, DataSourceUpdateMode.OnPropertyChanged );
            statusVersion.DataBindings.Add(
                "Text", _model, "Version", false, DataSourceUpdateMode.OnPropertyChanged);

            //buttons
            btnOpenLogFile.DataBindings.Add(
                "Enabled", _model, "CanOpenLogFile", false, DataSourceUpdateMode.OnPropertyChanged);

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

        private void PrepareScan()
        {
            _model.PrepareScan();
            lvStats.Items.Clear();
            lvFileTypes.Items.Clear();
            switch ( _model.LoggingType )
            {
                case LoggingType.FileLogger:
                    _model.CreateSessionLogging();
                    break;
                case LoggingType.SqlLogger:
                    _model.CreateSessionSqlLogging();
                    break;
            }
        }

        private void btnScanStats_Click(object sender, System.EventArgs e)
        {
            InitializeScan();

            var bgScan = new BackgroundWorker();
            bgScan.DoWork += BgScanOnDoWork;
            bgScan.RunWorkerCompleted += BgScanOnRunWorkerCompleted;
            bgScan.RunWorkerAsync();
        }

        private void InitializeScan()
        {
            progress.Visible = true;
            progress.Style = ProgressBarStyle.Marquee;
            progress.MarqueeAnimationSpeed = 50;

            status_Resize(this, EventArgs.Empty);

            _model.Message = "Scan started, please wait...";
        }

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


        private void status_Resize(object sender, EventArgs e)
        {
            var formWidth = this.Width;
            var statusControlsWidthMax = formWidth - 40;
            if (progress.Visible)
                statusMessage.Width = statusControlsWidthMax - progress.Width - statusLogType.Width - statusVersion.Width;
            else
                statusMessage.Width = statusControlsWidthMax - statusLogType.Width - statusVersion.Width;
        }

        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            _model.OpenLogFile();
        }

        private void miFilePreferences_Click( object sender, EventArgs e )
        {
            using ( var form = new PreferenceForm() )
            {
                form.LoggingType = _model.LoggingType;
                if ( !string.IsNullOrEmpty( Properties.Settings.Default.ConnectionString ) )
                    form.ConnectionString = Properties.Settings.Default.ConnectionString;
                else 
                    form.ConnectionString = ConfigurationManager.ConnectionStrings["logConnection"].ConnectionString;
                
                if ( form.ShowDialog() == DialogResult.OK )
                {
                    _model.LoggingType = form.LoggingType;
                    Properties.Settings.Default.LoggerPreference = form.LoggingType;
                    if ( _model.LoggingType == LoggingType.SqlLogger )
                        Properties.Settings.Default.ConnectionString = form.ConnectionString;

                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}
