using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.User;

namespace CTM.Data.Mapping.User
{
    public partial class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            this.ToTable("UserInfo");
            this.HasKey(p => p.Id);

            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.Code).HasMaxLength(20);
            this.Property(p => p.Password).HasMaxLength(20);
            this.Property(p => p.Superior).HasMaxLength(20);
            this.Property(p => p.CooperatorCode).HasMaxLength(20);
            this.Property(p => p.RandomKey).HasMaxLength(50);
            this.Property(p => p.Remarks).HasMaxLength(200);

            this.Property(p => p.AllotFund).HasPrecision(24, 4);
            this.Property(p => p.RiskControlLine).HasPrecision(18, 4);

            this.Ignore(p => p.PositionName);
            this.Ignore(p => p.DepartmentName);
            this.Ignore(p => p.CooperatorName);
            this.Ignore(p => p.SuperiorName);
        }
    }
}