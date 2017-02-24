using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class MSAccountPositionMap : EntityTypeConfiguration<MSAccountPosition>
    {
        public MSAccountPositionMap()
        {
            this.ToTable(nameof(MSAccountPosition));
            this.HasKey(p => p.Id);

            this.Property(p => p.PositionVolume).HasPrecision(24, 0);
            this.Property(p => p.CostPrice).HasPrecision(18, 4);
        }
    }
}