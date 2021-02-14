using System;
using System.IO;

namespace DirScan.ErrorLogging
{
    public static class ErrorLoggerFactory
    {
        public static IErrorLogger Create(string logFile = "")
        {
            if (string.IsNullOrEmpty(logFile))
                logFile = Path.Combine(
                    DestinationDirectory(), LogFileName());

            return new ErrorLogger(logFile);
        }

        public static string LogFileName()
        {
            return $@"DirScan_{DateTime.Now.Year,2:00}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}.log";
        }

        public static string DestinationDirectory()
        {
            // put error logs in MyDocuments
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ErrorLogs");
        }
    }
}