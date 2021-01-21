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
            var dirs = dir.EnumerateDirectories("*.*").ToList();
            var files = dir.EnumerateFiles("*.*").ToList();

            var dirFiles = GetDirectoryFiles(dirs, files);
            var fileTypes = GetFileTypes(files);

            var dirData = new DirectoryData()
            {
                DirectoryCount = dirs.Count(),
                FileCount = files.Count(),
                DirectoryFileSize = files.Sum(fi => fi.Length),
                FileTypes = fileTypes,
                DirectoryFiles = dirFiles
            };
            return dirData;
        }

        private IEnumerable<DirectoryFile> GetDirectoryFiles(List<DirectoryInfo> dirs, List<FileInfo> files)
        {
            var dirFiles = new List<DirectoryFile>();
            foreach (var di in dirs)
                dirFiles.Add(new DirectoryFile { Name = di.FullName, DateCreated = di.CreationTime });
            foreach (var f in files)
                dirFiles.Add(new DirectoryFile { Name = f.FullName, DateCreated = f.CreationTime, Size = f.Length, IsFile = true});
            return dirFiles;
        }

        private IEnumerable<FileType> GetFileTypes(List<FileInfo> files)
        {
            var fileTypes = new List<FileType>();
            foreach (var fi in files)
            {
                var fileType = new FileType { Extension = fi.Extension, Length = fi.Length };
                var ft = fileTypes.FirstOrDefault(f => f.Extension == fileType.Extension);
                if (ft != null)
                    ft.Length += fileType.Length;
                else
                    fileTypes.Add(fileType);

            }
            return fileTypes;
        }
    }
}
