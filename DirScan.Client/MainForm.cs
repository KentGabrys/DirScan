using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DirScan.Common;
using DirScan.Logging;
using DirScan.Service;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirScan.Client
{
    public partial class MainForm : Form
    {
        public readonly DirScanModel _model;
        private ILogger _logger;
        private string _logFileName;

        public MainForm()
        {
            InitializeComponent();

            _model = new DirScanModel();
            BindControlsToModel();
            InitializeModel();
            CreateSessionLogging();
        }

        private void InitializeModel()
        {
            _model.Message = "Bork bork... ";
            _model.Version = Release.Version;
            progress.Visible = false;
            status_Resize(this, EventArgs.Empty);
        }

        private void CreateSessionLogging()
        {
            _logFileName =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $@"DirectoryLog_{DateTime.Now.Year,4:0000}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}_{DateTime.Now.Hour,2:00}-{DateTime.Now.Minute,2:00}.csv");

            _logger = FileLogger.Create(_logFileName);
        }

        private void BindControlsToModel()
        {
            lblSelectedFolder.DataBindings.Add(
                "Text", _model, "SelectedFolder", false, DataSourceUpdateMode.OnPropertyChanged);

            statusMessage.DataBindings.Add(
                "Text", _model, "Message", false, DataSourceUpdateMode.OnPropertyChanged);

            statusVersion.DataBindings.Add(
                "Text", _model, "Version", false, DataSourceUpdateMode.OnPropertyChanged);

            btnOpenLogFile.DataBindings.Add(
                "Enabled", _model, "CanOpenLogFile", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnSelectFolder_Click(object sender, System.EventArgs e)
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    _model.SelectedFolder = dlg.FileName;
            }
        }

        private void btnScanStats_Click(object sender, System.EventArgs e)
        {
            progress.Visible = true;
            progress.Style = ProgressBarStyle.Marquee;
            progress.MarqueeAnimationSpeed = 300;
            progress.Step = 25;
            status_Resize(sender, EventArgs.Empty);

            _model.Message = "Scan Started...";

            var bgScan = new BackgroundWorker();
            bgScan.DoWork += BgScanOnDoWork;
            bgScan.ProgressChanged += BgScanOnProgressChanged;
            bgScan.RunWorkerCompleted += BgScanOnRunWorkerCompleted;
            bgScan.RunWorkerAsync();
        }


        private void BgScanOnDoWork(object sender, DoWorkEventArgs e)
        {
            if (_model.FolderSelected)
            {
                var dm = new DirectoryManager();
                dm.Scan(_model.SelectedFolder, _logger);
                e.Result = dm.DirectoryDataSummary;
            }
        }

        private void BgScanOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var fi = new FileInfo(_logFileName);
            _model.Message = $"Kilobytes written: {fi.Length}";
        }

        private void BgScanOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progress.Style = ProgressBarStyle.Blocks;
            progress.MarqueeAnimationSpeed = 0;
            progress.Visible = false;
            status_Resize(sender, EventArgs.Empty);

            _model.LoadStatsListView(lvStats, lvFileTypes, e.Result as DirectoryDataSummary);
            _model.CanOpenLogFile = true;
            _model.Message = "Scan Complete";
        }

        private void status_Resize(object sender, EventArgs e)
        {
            var formWidth = this.Width;
            var statusControlsWidthMax = formWidth - 40;
            if (progress.Visible)
                statusMessage.Width = statusControlsWidthMax - progress.Width - statusVersion.Width;
            else
                statusMessage.Width = statusControlsWidthMax - statusVersion.Width;
        }

        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            _model.OpenLogFile(_logFileName);
        }
    }
}
