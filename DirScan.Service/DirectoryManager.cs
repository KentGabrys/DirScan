namespace DirScan.Service
{
    public class DirectoryManager
    {
        public void Scan(string path)
        {
            ScanPath(path);
        }

        private void ScanPath(string path)
        {
            var dirSvc = new DirectoryService();
            var dd = dirSvc.Scan(path);

            if (dd.DirectoryCount > 0)
                foreach (var dir in dd.DirectoryFiles)
                    if (!dir.IsFile)
                        ScanPath(dir.Name);

            DirectoryDataSummary.DirectoryCount += dd.DirectoryCount;
            DirectoryDataSummary.FileCount += dd.FileCount;
            foreach (var file in dd.DirectoryFiles)
                DirectoryDataSummary.Size += file.Size;
        }

        public DirectoryDataSummary DirectoryDataSummary { get; set; } = new DirectoryDataSummary();
    }
}