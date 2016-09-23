using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Department;

namespace CTM.Data.Mapping.Department
{
    public partial class DepartmentInfoMap : EntityTypeConfiguration<DepartmentInfo>
    {
        public DepartmentInfoMap()
        {
            this.ToTable("DepartmentInfo");
            this.HasKey(p => p.Id);

            this.Property(p => p.Code).HasMaxLength(20);
            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.PrincipalCode).HasMaxLength(20);
            this.Property(p => p.Description).HasMaxLength(200);
            this.Property(p => p.Remarks).HasMaxLength(200);
        }
    }
}