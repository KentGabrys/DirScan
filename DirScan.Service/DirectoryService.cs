using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using DirScan.Common.Models;
using DirScan.ErrorLogging;
using DirScan.Logging;

namespace DirScan.Service
{
    public class DirectoryService : DirectoryServiceBase
    {
        public DirectoryService(ILogger logger, IMapper mapper)
            : base(logger, mapper)
        {
        }

        public override DirectoryData Scan(string path, bool logDirectories = false)
        {
            try
            {
                var dir = new DirectoryInfo(path);
                var dirs = dir.EnumerateDirectories("*.*").ToList();
                var files = dir.EnumerateFiles("*.*").ToList();
                var fileTypes = GetFileTypes(files);

                var logDirs = logDirectories ? dirs : null;
                LogDirectoryFiles(logDirs, files);
                _logger.SaveLogs();

                var dirData = new DirectoryData()
                {
                    Directories = dirs,
                    Files = files,
                    DirectoryCount = dirs.Count(),
                    FileCount = files.Count(),
                    DirectoryFileSize = files.Sum(fi => fi.Length),
                    FileTypes = fileTypes,
                };
                return dirData;
            }
            catch (Exception exception)
            {
                ErrorLog.Log(exception, $"An exception occurred attempting to scan {path}.");
                return new DirectoryData();
            }
        }

        private void LogDirectoryFiles(List<DirectoryInfo> dirs, List<FileInfo> files)
        {
            if (dirs != null)
                foreach (var di in dirs)
                    _logger.Log(_mapper.Map<DirectoryFile>(di));

            foreach (var f in files)
                _logger.Log(_mapper.Map<DirectoryFile>(f));
        }


    }
}
