using System;
using System.IO;
using System.Text;

namespace DirScan.ErrorLogging
{

    public class ErrorLogger : IErrorLogger
    {
        private readonly string _logFile;

        internal ErrorLogger(string logFile)
        {
            _logFile = logFile;
            InsureDirectoryExists();
        }

        public void Log(Exception exception)
        {
            using (var fs = File.AppendText(_logFile))
                fs.Write(GetExceptionString(exception));
        }

        public void Log(Exception exception, string additionalComment)
        {
            using (var fs = File.AppendText(_logFile))
            {
                fs.Write(Environment.NewLine);  
                fs.WriteLine(("").PadLeft(20,'-'));
                fs.WriteLine(additionalComment);
                fs.Write(GetExceptionString(exception));
            }
        }


        private void InsureDirectoryExists()
        {
            var fi = new FileInfo(_logFile);
            Directory.CreateDirectory(fi.DirectoryName);
        }


        private string GetExceptionString(Exception exception)
        {

            StringBuilder str = new StringBuilder();
            try
            {
                var ex = exception;
                str.Append("Message: ");
                str.Append(ex.Message);
                str.Append(" (");
                str.Append(ex.GetType().FullName);
                str.Append(")");
                str.Append(Environment.NewLine);

                while ((ex = ex.InnerException) != null)
                {
                    str.Append("Inner message: ");
                    str.Append(ex.Message);
                    str.Append(" (");
                    str.Append(ex.GetType().FullName);
                    str.Append(")");
                    str.Append(Environment.NewLine);
                }

                str.Append("Type: ");
                str.Append(exception.GetType().FullName);
                str.Append(Environment.NewLine);

                str.Append("Source: ");
                str.Append(exception.Source);
                str.Append(Environment.NewLine);

                str.Append("Method: ");
                str.Append(exception.TargetSite);
                str.Append(Environment.NewLine);

                str.Append("Stack Trace:");
                str.Append(Environment.NewLine);
                str.Append(exception.StackTrace);
                str.Append(Environment.NewLine);
            }
            catch
            {
                //at least try to pass the message
                str.Append("An exception occurred trying to log the text: " + exception.Message);
            }
            return str.ToString();
        }

    }
}
