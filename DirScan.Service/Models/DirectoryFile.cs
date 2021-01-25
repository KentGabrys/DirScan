using System;
using System.IO;

namespace DirScan.Service
{
    public class DirectoryFile
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        private string DateCreatedToString => 
            $"{DateCreated.DateTime.Month,2:00}-{DateCreated.DateTime.Day,2:00}-{DateCreated.DateTime.Year, 4:0000}";

//        public bool IsFile { get; set; }
        public FileAttributes FileAttribute { get; set; }

        public override string ToString()
        {
            var attribute = FileAttribute.ToString().Replace(", ", " + ");
            return $"\"{Name}\", {Size}, {DateCreatedToString}, {attribute}";
        }
    }
}