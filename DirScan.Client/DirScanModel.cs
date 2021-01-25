using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DirScan.Client.Annotations;
using DirScan.Common;
using DirScan.Service;

namespace DirScan.Client
{
    public class DirScanModel : INotifyPropertyChanged
    {
        private string _selectedFolder;
        private string _message = string.Empty;
        private string _version = Release.Version;
        private bool _canOpenLogFile;

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

        public bool FolderSelected => !string.IsNullOrEmpty(SelectedFolder);

        public bool CanOpenLogFile
        {
            get => _canOpenLogFile;
            set
            {
                _canOpenLogFile = value; 
                OnPropertyChanged();
            }
        }

        public void LoadStatsListView(ListView lvStats, ListView lvFileTypes, DirectoryDataSummary data)
        {
            lvStats.Items.Clear();
            lvFileTypes.Items.Clear();
            var props = new List<PropertyInfo>(data.GetType().GetProperties());
            foreach (var pi in props)
            {
                if (pi.Name != "FileTypes")
                {
                    lvStats.Items.Add(new ListViewItem(new string[]
                    {
                        pi.Name,
                        pi.GetValue(data, null).ToString()
                    }));
                }
                else
                {
                    if (pi.GetValue(data, null) is IEnumerable<FileType> fileTypes)
                        foreach (var ft in fileTypes)
                        {
                            lvFileTypes.Items.Add(new ListViewItem(new string[]
                            {
                                ft.Extension,
                                ft.TotalSize
                            }));
                        }
                }
            }
        }
        public void OpenLogFile(string fileName)
        {
            var fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(fileName);
            }
            else
            {
                MessageBox.Show($@"{fileName} not found.",@"File not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region PropertyChanged EventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion

    }
}
