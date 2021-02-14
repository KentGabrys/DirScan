using System.Collections.Generic;
using System.IO;

namespace DirScan.Data
{
    public class DirectoryData
    {
        public int FileCount { get; set; }
        public int DirectoryCount { get; set; }
        public long DirectoryFileSize { get; set; }

        public List<DirectoryInfo> Directories { get; set; }
        public IEnumerable<FileType> FileTypes { get; set; }

        public bool CanBeProcessed => ( DirectoryCount > 0 || FileCount > 0 ) && FileTypes != null;

    }
}