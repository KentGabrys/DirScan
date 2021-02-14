using DirScan.ErrorLogging;
using System;
using System.Windows.Forms;

namespace DirScan.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }catch  (Exception exception)
            {
                var _errorLogger = ErrorLoggerFactory.Create();
                _errorLogger.Log(exception);
                MessageBox.Show( @"An unhandled error has occurred. Please check the error log file for the issue encountered.",
                    @"Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
