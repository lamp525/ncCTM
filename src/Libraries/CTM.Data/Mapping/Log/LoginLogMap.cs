using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Log;

namespace CTM.Data.Mapping.Log
{
    public partial class LoginLogMap : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogMap()
        {
            this.ToTable(nameof(LoginLog));
            this.HasKey(p => p.Id);

            this.Property(p => p.UserCode).HasMaxLength(20);
            this.Property(p => p.UserName).HasMaxLength(20);
            this.Property(p => p.IP).HasMaxLength(20);
            this.Property(p => p.MAC).HasMaxLength(50);
        }
    }
}