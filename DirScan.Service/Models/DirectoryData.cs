using System.Collections.Generic;

namespace DirScan.Service
{
    public class DirectoryData
    {
        public int FileCount { get; set; }
        public int DirectoryCount { get; set; }
        public long DirectoryFileSize { get; set; }
        public IEnumerable<FileType> FileTypes { get; set; }
        public IEnumerable<DirectoryFile> DirectoryFiles { get; set; }
    }
}