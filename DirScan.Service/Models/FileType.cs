namespace DirScan.Service
{
    public class FileType
    {
        public string Extension { get; set; }
        public long Length {  get; set; }
        public string TotalSize
        {
            get
            {
                if (Length > 1024000)
                {
                    double megaBytes = (double)Length / 1024000;
                    return $"{megaBytes,5:0.#} MB";
                }
                else if (Length > 1024)
                {
                    double kiloBytes = (double)Length / 1024;
                    return $"{kiloBytes,5:0.#} KB";
                }

                return $"{Length,5} Bytes";
            }

        }
    }
}