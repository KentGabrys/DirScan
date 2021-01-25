namespace DirScan.Logging
{
    public interface ILogger
    {
        void Log<T>(T data);
    }
}
