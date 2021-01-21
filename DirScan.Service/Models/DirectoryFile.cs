using System;

namespace DirScan.Service
{
    public class DirectoryFile
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool IsFile { get; set; }
    }
}