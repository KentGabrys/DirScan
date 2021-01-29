using System.Data.Entity.ModelConfiguration;

namespace DirScan.Data
{
    public class DirScanLogConfiguration : EntityTypeConfiguration<DirScanLog>
    {
        public DirScanLogConfiguration()
        {
            this.ToTable( "DirScanLog" ).HasKey( e => e.Id );
            this.Property( p => p.File ).HasColumnName( "File" );
            this.Property( p => p.Size ).HasColumnName( "Size" );
            this.Property( p => p.DateCreated ).HasColumnName( "DateCreated" );
            this.Property( p => p.Attributes ).HasColumnName( "FileAttributes" );
            // this.Property( p => p.UserId ).HasColumnName( "User_Id" );
            // this.Property( p => p.ProcessDate ).HasColumnName( "Process_Date" );
        }

   
    }
}