using System.Data.Entity;

namespace DirScan.Data
{
    public class DirScanDbContext: DbContext
    {
        public DirScanDbContext(string connectionString) : base( connectionString )
        {
            Database.SetInitializer<DirScanDbContext>(null);
        }

        public DbSet<DirScanLog> DirScanLogs { get; set; }

        protected override void OnModelCreating( DbModelBuilder builder )
        {
            builder.Configurations.Add( new DirScanLogConfiguration() );

            base.OnModelCreating( builder );
        }
    }
}
