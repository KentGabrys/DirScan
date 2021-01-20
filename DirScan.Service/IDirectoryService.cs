using System.Collections.Generic;
using System.IO;

namespace DirScan.Service
{
    public interface IDirectoryService
    {
        DirectoryData Scan(string path);

    }
}