using System;
using System.IO;
using DirScan.Logging;

namespace DirScan.Service
{
    public class DirectoryFile : ILogDatum
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

        public LogDatum ToData()
        {
            var attribute = FileAttribute.ToString().Replace( ", ", " + " );
            return new LogDatum()
                { File = Name, Size = Size, DateCreated = DateCreatedToString, Attributes = attribute };
        }
    }
}