using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirScan.ErrorLogging;
using DirScan.Logging;

namespace DirScan.Service
{
    public class DirectoryService : IDirectoryService
    {
        private ILogger _logger;

        public DirectoryData Scan(string path, ILogger logger)
        {
            try
            {

                _logger = logger;

                var dir = new DirectoryInfo( path );
                var dirs = dir.EnumerateDirectories( "*.*" ).ToList();
                var files = dir.EnumerateFiles( "*.*" ).ToList();
                var fileTypes = GetFileTypes( files );

                LogDirectoryFiles( dirs, files );
                _logger.SaveLogs();

                var dirData = new DirectoryData()
                {
                    Directories = dirs,
                    Files = files,
                    DirectoryCount = dirs.Count(),
                    FileCount = files.Count(),
                    DirectoryFileSize = files.Sum( fi => fi.Length ),
                    FileTypes = fileTypes,
                };
                return dirData;
            }
            catch ( Exception exception )
            {
                ErrorLog.Log( exception, $"An exception occurred attempting to scan {path}." );
                return new DirectoryData();
            }
        }

        private void LogDirectoryFiles(List<DirectoryInfo> dirs, List<FileInfo> files)
        {
            foreach (var di in dirs)
            {
                
                var df = new DirectoryFile {Name = di.FullName, DateCreated = di.CreationTime, FileAttribute = di.Attributes};
                _logger.Log(df);
            }

            foreach (var f in files)
            { 
                var df = new DirectoryFile {Name = f.FullName, DateCreated = f.CreationTime, Size = f.Length, FileAttribute = f.Attributes};
                _logger.Log(df);
            }

        }

        private IEnumerable<FileType> GetFileTypes(List<FileInfo> files)
        {
            var fileTypes = new List<FileType>();
            foreach (var fi in files)
            {
                if (fi.Attributes != FileAttributes.Directory)
                {
                    var fileType = new FileType{ Extension = "None", Length = fi.Length };
                    if( fi.Extension.Length > 0 )
                        fileType = new FileType
                        {
                            Extension = fi.Extension.ToLower().Trim(), 
                            Length = fi.Length
                        };
                    var ft = fileTypes.FirstOrDefault( f => f.Extension.ToLower() == fileType.Extension.ToLower() );
                        
                    if (ft != null)
                        ft.Length += fileType.Length;
                    else
                        fileTypes.Add(fileType);
                }
            }
            return fileTypes;
        }
    }
}
