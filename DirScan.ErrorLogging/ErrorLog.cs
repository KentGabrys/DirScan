using System;
using System.IO;
using System.Text;

namespace DirScan.ErrorLogging
{
    public static class ErrorLog
    {

        public static void Log( Exception exception )
        {
            var logFile = DefaultFileName();
            Log( logFile, exception );
        }
        public static void Log( Exception exception, string additionText )
        {
            var logFile = DefaultFileName();
            Log( logFile, exception, additionText );
        }

        public static void Log( string fileName, Exception exception )
        {
            using (var fs = File.AppendText( fileName ))
                fs.Write( GetExceptionString( exception ) );

        }

        public static void Log( string fileName, Exception exception, string additionalText )
        {
            using (var fs = File.AppendText( fileName ))
            {
                fs.Write( Environment.NewLine );
                fs.Write( Environment.NewLine );
                fs.Write( additionalText );
                fs.Write( Environment.NewLine );
                fs.Write( GetExceptionString( exception ) );
            }

        }

        public static string DefaultFileName(
            string baseFileName = "DirScanErrorLog", 
            string subDirectory = "")
        {
            // for now, put error logs in MyDocuments
            return
                Path.Combine(
                    Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ), subDirectory,
                    $@"{baseFileName}_{DateTime.Now.Year,2:00}{DateTime.Now.Month,2:00}{DateTime.Now.Day,2:00}.log" );
        }

        private static string GetExceptionString( Exception exception )
        {

            StringBuilder str = new StringBuilder();
            try
            {
                var ex = exception;
                str.Append( "Message: " );
                str.Append( ex.Message );
                str.Append( " (" );
                str.Append( ex.GetType().FullName );
                str.Append( ")" );
                str.Append( Environment.NewLine );

                while ((ex = ex.InnerException) != null)
                {
                    str.Append( "Inner message: " );
                    str.Append( ex.Message );
                    str.Append( " (" );
                    str.Append( ex.GetType().FullName );
                    str.Append( ")" );
                    str.Append( Environment.NewLine );
                }

                str.Append( "Type: " );
                str.Append( exception.GetType().FullName );
                str.Append( Environment.NewLine );

                str.Append( "Source: " );
                str.Append( exception.Source );
                str.Append( Environment.NewLine );

                str.Append( "Method: " );
                str.Append( exception.TargetSite );
                str.Append( Environment.NewLine );

                str.Append( "Stack Trace:" );
                str.Append( Environment.NewLine );
                str.Append( exception.StackTrace );
                str.Append( Environment.NewLine );
            }
            catch
            {
                //at least try to pass the message
                str.Append( "An exception occurred trying to log the text: " + exception.Message );
            }
            return str.ToString();
        }
    }
}
