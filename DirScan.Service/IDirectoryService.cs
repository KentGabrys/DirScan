using DirScan.Logging;

namespace DirScan.Service
{
    public interface IDirectoryService
    {
        DirectoryData Scan(string path, ILogger logger);

    }
}