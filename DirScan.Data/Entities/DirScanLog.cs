using System;

namespace DirScan.Data
{
    public class DirScanLog
    {
        public int Id { get; set; }
        public string File { get; set; }
        public long Size { get; set; }
        public string DateCreated { get; set; }
        public string DateLastModified { get; set; }
        public string Owner { get; set; }
        public string Attributes { get; set; }
        public int UserId { get; set; } = 2;
        public DateTime ProcessDate { get; set; } = DateTime.Now;
    }
}
