using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using DirScan.Data;
using DirScan.Logging;

namespace DirScan.Service
{
    public abstract class DirectoryServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected DirectoryServiceBase( ILogger logger, IMapper mapper )
        {
            _logger = logger;
            _mapper = mapper;
        }

        public abstract DirectoryData Scan(string path, bool logDirectories = false);

        protected IEnumerable<FileType> GetFileTypes( List<FileInfo> files )
        {
            var fileTypes = new List<FileType>();
            foreach (var fi in files)
            {
                if (fi.Attributes != FileAttributes.Directory)
                {
                    var fileType = new FileType { Extension = "None", Length = fi.Length };
                    if (fi.Extension.Length > 0)
                        fileType = new FileType
                        {
                            Extension = fi.Extension.ToLower().Trim(),
                            Length = fi.Length
                        };
                    var ft = fileTypes.FirstOrDefault( f => f.Extension.ToLower() == fileType.Extension.ToLower() );

                    if (ft != null)
                        ft.Length += fileType.Length;
                    else
                        fileTypes.Add( fileType );
                }
            }
            return fileTypes;
        }

    }
}