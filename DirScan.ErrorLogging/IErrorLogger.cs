using System;

namespace DirScan.ErrorLogging
{
    public interface IErrorLogger
    {
        void Log(Exception exception);
        void Log(Exception exception, string additionalComment);
    }
}