using System;
using System.IO;
using System.Linq;
using DirScan.Logging;

namespace DirScan.Service
{
    public class DirectoryManager
    {
        public void Scan(string path, ILogger logger)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            ScanPath(path, logger);
        }

        private void ScanPath(string path, ILogger logger)
        {
            var dirSvc = new DirectoryService();
            var dd = dirSvc.Scan(path, logger);

            if ( dd.CanBeProcessed )
            {
                if ( dd.DirectoryCount > 0 )
                    foreach ( var dir in dd.Directories )
                        ScanPath( dir.FullName, logger );

                DirectoryDataSummary.DirectoryCount += dd.DirectoryCount;
                DirectoryDataSummary.FileCount += dd.FileCount;
                DirectoryDataSummary.MergeFileTypes( dd.FileTypes.ToList() );
                foreach ( var file in dd.Files )
                    DirectoryDataSummary.Size += new FileInfo( file.FullName ).Length;
            }
        }

        public DirectoryDataSummary DirectoryDataSummary { get; set; } = new DirectoryDataSummary();
    }
}