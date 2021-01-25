using System;
using System.IO;

namespace DirScan.Logging
{
    public class FileLogger : ILogger
    {
        protected static string _fileName;
        private static ILogger _logger;

        internal FileLogger(string fileName)
        {
            _fileName = fileName;
        }
        public static ILogger Create(string fileName)
        {
            if(fileName == null) 
                throw new ArgumentNullException(nameof(fileName));

            if(_logger != null && _fileName == fileName)
                return _logger;

            _logger = new FileLogger(fileName);
            return _logger;
        }

        public void Log<T>(T data)
        {
            InsureDirectoryExists();
            using (var fs = File.AppendText(_fileName))
                fs.Write(data.ToString() + Environment.NewLine);
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