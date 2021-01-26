using System.Configuration;

namespace DirScan.Data
{
    public class DirScanRepository
    {
        private readonly DirScanDbContext _context;

        public DirScanRepository(string connectionString)
        {
            if(string.IsNullOrEmpty( connectionString ))
                connectionString = ConfigurationManager.ConnectionStrings["logConnection"].ConnectionString;
            _context = new DirScanDbContext( connectionString );
        }

        public DirScanLog AddDirScanLog( DirScanLog entity )
        {
            return _context.DirScanLogs.Add( entity );
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
