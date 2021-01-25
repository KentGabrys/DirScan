namespace DirScan.Logging
{
    public class LogDatum : ILogDatum
    {
        private string _attributes;

        public string File { get; set; }
        public long Size { get; set; }
        public string DateCreated { get; set; }

        public string Attributes
        {
            get => _attributes.Replace( ", ", " + " );
            set => _attributes = value;
        }

        public LogDatum ToData()
        {
            return this;
        }

        public override string ToString()
        {
            return $"\"{File}\", {Size}, {DateCreated}, {Attributes}";
        }
    }

}