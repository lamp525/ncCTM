using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class MIAccountPositionMap : EntityTypeConfiguration<MIAccountPosition>
    {
        public MIAccountPositionMap()
        {
            this.ToTable(nameof(MIAccountPosition));
            this.HasKey(p => p.Id);

            this.Property(p => p.PositionVolume).HasPrecision(24, 0);
        }
    }
}