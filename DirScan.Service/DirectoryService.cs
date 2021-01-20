using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirScan.Service
{
    public class DirectoryService : IDirectoryService
    {
        public DirectoryData Scan(string path)
        {
            var dir = new DirectoryInfo(path);
            var files = dir.EnumerateFiles("*.*").ToList();
            IEnumerable<FileType> fileTypes = GetFileTypes(files);

            var dirData = new DirectoryData()
            {
                DirectoryCount = dir.EnumerateDirectories("*.*").Count(),
                FileCount = files.Count(),
                DirectoryFileSize = files.Sum(fi => fi.Length),
                FileTypes = fileTypes
            };
            return dirData;
        }

        private IEnumerable<FileType> GetFileTypes(List<FileInfo> files)
        {
            var fileTypes = new List<FileType>();
            foreach (var fi in files)
            {
                var fileType = new FileType()
                {
                    Extension = fi.Extension,
                    Length = fi.Length
                };
                var ft = fileTypes.FirstOrDefault(f => f.Extension == fileType.Extension);
                if (ft != null)
                {
                    ft.Length += fileType.Length;
                }
                else
                {
                    fileTypes.Add( fileType );
                }
            }
            return fileTypes;
        }
    }
}
