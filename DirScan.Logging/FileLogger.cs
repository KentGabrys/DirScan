using System;
using System.IO;

namespace DirScan.Logging
{
    public class FileLogger : ILogger
    {
        protected static string _fileName;
        private static ILogger _logger;
        private static string _logHeader;

        internal FileLogger(string fileName) : this(fileName, null)
        {
        }

        internal FileLogger(string fileName, string logHeader)
        {
            _fileName = fileName;
            _logHeader = logHeader;
        }

        public static ILogger Create(string fileName)
        {
            if(fileName == null) 
                throw new ArgumentNullException(nameof(fileName));

            if(_logger != null && _fileName == fileName)
                return _logger;

            _logHeader = null;
            _logger = new FileLogger(fileName);
            return _logger;
        }

        public static ILogger Create(string fileName, string logHeader)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (_logger != null && _fileName == fileName)
                return _logger;

            _logger = new FileLogger(fileName, logHeader);
            return _logger;
        }

        public void Log<T>(T data)
        {
            InsureDirectoryExists();
            if (!File.Exists(_fileName) && _logHeader != null)
                WriteHeader();
            using (var fs = File.AppendText(_fileName))
                fs.Write(data.ToString() + Environment.NewLine);
        }

        public void SaveLogs()
        {
            // nothing to do
        }

        private void WriteHeader()
        {
            using (var fs = File.AppendText(_fileName))
                fs.Write(_logHeader + Environment.NewLine);
        }

        private static void InsureDirectoryExists()
        {
            var fi = new FileInfo(_fileName);
            Directory.CreateDirectory(fi.DirectoryName);
        }


        /*
        how to get MyDocuments folder
        fileName =
            Path.Combine(
                Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ),
                $@"FileRootName_{DateTime.Now.Year,4:0000}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}_{DateTime.Now.Hour,2:00}-{DateTime.Now.Minute,2:00}.xlsx" );


                {DateTime.Now.Year,4:0000}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}_{DateTime.Now.Hour,2:00}-{DateTime.Now.Minute,2:00}
         
         */
    }
}