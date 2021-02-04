using System;
using System.IO;
using System.Linq;
using AutoMapper;
using DirScan.Data;
using DirScan.Logging;

namespace DirScan.Service
{
    public class DirectoryManager
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DirectoryManager( ILogger logger, IMapper mapper )
        {
            _logger = logger ?? throw new ArgumentNullException( nameof( _logger ) );
            _mapper = mapper ?? throw new ArgumentNullException( nameof( _mapper ) );
        }

        public void Scan(string path, bool logDirectories = false)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            ScanPath( path, logDirectories );
        }

        private void ScanPath(string path, bool logDirectories = false)
        {
            var dirSvc = new DirectoryService( _logger, _mapper );
            var dd = dirSvc.Scan(path,  logDirectories);

            if ( dd.CanBeProcessed )
            {
                if ( dd.DirectoryCount > 0 )
                    foreach ( var dir in dd.Directories )
                        ScanPath( dir.FullName, logDirectories );

                DirectoryDataSummary.DirectoryCount += dd.DirectoryCount;
                DirectoryDataSummary.FileCount += dd.FileCount;
                DirectoryDataSummary.Size = dd.DirectoryFileSize;
                DirectoryDataSummary.MergeFileTypes( dd.FileTypes.ToList() );
            }
        }

        public DirectoryDataSummary DirectoryDataSummary { get; set; } = new DirectoryDataSummary();
    }
}