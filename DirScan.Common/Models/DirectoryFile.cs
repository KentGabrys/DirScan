using DirScan.Data;
using System;
using System.IO;

namespace DirScan.Common.Models
{
    public class DirectoryFile : IDirScanLogDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        private string DateCreatedToString => 
            $"{DateCreated.DateTime.Month,2:00}-{DateCreated.DateTime.Day,2:00}-{DateCreated.DateTime.Year, 4:0000} {DateCreated.LocalDateTime.ToShortTimeString(), 2:00}";
        public DateTimeOffset DateLastModified { get; set; }
        private string DateLastModifiedToString =>
            $"{DateLastModified.DateTime.Month,2:00}-{DateLastModified.DateTime.Day,2:00}-{DateLastModified.DateTime.Year,4:0000} {DateLastModified.LocalDateTime.ToShortTimeString(),2:00}";

        public string Owner { get; set; }

        public FileAttributes FileAttributes { get; set; }
        private string FileAttributesString => FileAttributes.ToString().Replace( ", ", " + " );

        public override string ToString()
        {
            return $"\"{Name}\", {Size}, {DateCreatedToString}, {DateLastModifiedToString}, {Owner}, {FileAttributesString}";
        }

        public DirScanLogDto ToData()
        {
            return new DirScanLogDto()
                { 
                    File = Name, 
                    Size = Size, 
                    DateCreated = DateCreatedToString, 
                    DateLastModified = DateLastModifiedToString, 
                    Owner = Owner, 
                    Attributes = FileAttributesString
                };
        }
    }
}