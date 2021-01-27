using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using AutoMapper;
using DirScan.Client.Properties;
using DirScan.Common;
using DirScan.Common.Models;
using DirScan.Data;
using DirScan.Logging;
using DirScan.Service;

namespace DirScan.Client
{
    public class DirScanModel : INotifyPropertyChanged
    {
        private string _selectedFolder;
        private string _message = string.Empty;
        private string _version = Release.Version;
        private bool _canOpenLogFile;
        private int _fileTypeCount;
        private bool _canScanStatistics;
        private string _logFileName;
        private ILogger _logger;
        private LoggingType _loggingType;
        
        private readonly Mapper _mapper;

        public DirScanModel()
        {
            _mapper = new Mapper( new MapperConfiguration( a => a.AddProfile( new MappingProfile() ) ) );
        }

        #region Properties

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                OnPropertyChanged();
            }
        }

        public bool FolderSelected => !string.IsNullOrEmpty( SelectedFolder );

        public int FileTypeCount
        {
            set
            {
                _fileTypeCount = value;
                OnPropertyChanged( nameof( FileTypesMessage ) );
            }
        }

        public string FileTypesMessage => $"File Types: {_fileTypeCount,5}";

        public bool CanScanStatistics
        {
            get => _canScanStatistics;
            set
            {
                _canScanStatistics = value;
                OnPropertyChanged();
            }
        }

        public bool CanOpenLogFile
        {
            get => _canOpenLogFile;
            set
            {
                _canOpenLogFile = value;
                OnPropertyChanged();
            }
        }

        public LoggingType LoggingType
        {
            get => _loggingType;
            set
            {
                _loggingType = value;
                OnPropertyChanged();
            }
        }

        public string ConnectionString { get; set; }

        #endregion

        #region PropertyChanged EventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        #endregion

        public void CreateSessionLogging()
        {
            if (!FolderSelected) throw new ScanNotPreparedException();

            const string logHeader = "File, Size, Date Created, Date Last Modified, Owner, File Attributes";

            var fi = new FileInfo( SelectedFolder );
            var fileName = $"DirScanLogging\\{fi.Name}_DirScan_";

            _logFileName =
                Path.Combine(
                    Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ),
                    $@"{fileName}{DateTime.Now.Year,4:0000}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}_{DateTime.Now.Hour,2:00}_{DateTime.Now.Minute,2:00}_{DateTime.Now.Second,2:00}.csv" );

            _logger = FileLogger.Create( _logFileName, logHeader );
        }

        public void CreateSessionSqlLogging()
        {
            _logger = new SqlLogger(
                new DirScanRepository( ConnectionString ), _mapper );
        }
        public void PrepareScan()
        {
            CanOpenLogFile = false;
            CanScanStatistics = true;
            FileTypeCount = 0;
            Message = "New scan prepared, awaiting Scan Statistics keypress...";
        }
        public DirectoryDataSummary ScanStatistics()
        {
            var dm = new DirectoryManager( _logger, _mapper );
            dm.Scan( SelectedFolder );
            return dm.DirectoryDataSummary;
        }

        public void LoadStatsListView( ListView lvStats, ListView lvFileTypes, DirectoryDataSummary data )
        {
            lvStats.Items.Clear();
            lvFileTypes.Items.Clear();
            var props = new List<PropertyInfo>( data.GetType().GetProperties() );
            foreach (var pi in props)
            {
                if (pi.Name != "FileTypes")
                {
                    lvStats.Items.Add( new ListViewItem( new string[]
                    {
                        pi.Name,
                        pi.GetValue(data, null).ToString()
                    } ) );
                }
                else
                {
                    if (pi.GetValue( data, null ) is IEnumerable<FileType> fileTypes)
                        foreach (var ft in fileTypes.OrderBy( f => f.Extension ))
                        {
                            lvFileTypes.Items.Add( new ListViewItem( new string[]
                            {
                                ft.Extension,
                                ft.TotalSize
                            } ) );
                        }

                    FileTypeCount = lvFileTypes.Items.Count;
                }
            }
        }

        public void ScanComplete()
        {
            if (_logger is FileLogger)
                CanOpenLogFile = true;
            Message = "Scan Complete";
        }


        public void OpenLogFile()
        {
            var fi = new FileInfo( _logFileName );
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start( _logFileName );
            }
            else
            {
                MessageBox.Show( $@"{_logFileName} not found.", @"File not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }



    }
}
