namespace DirScan.Service
{
    public class FileType
    {
        public string Extension { get; set; }
        public long Length { get; set; }
        public string TotalSize
        {
            get
            {
                if (Length > 1024000)
                    return $"{(double)Length / 1024000,5:0.#} MB";
                else if (Length > 1024)
                    return $"{(double)Length / 1024,5:0.#} KB";

                return $"{Length,5} Bytes";
            }

        }
    }
}