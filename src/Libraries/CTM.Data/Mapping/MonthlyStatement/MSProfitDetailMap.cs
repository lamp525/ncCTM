using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class MSProfitDetailMap : EntityTypeConfiguration<MSProfitDetail>
    {
        public MSProfitDetailMap()
        {
            this.ToTable(nameof(MSProfitDetail));
            this.HasKey(p => p.Id);

            this.Property(p => p.PositionVolume).HasPrecision(24, 0);
            this.Property(p => p.AccumulatedProfit ).HasPrecision(24, 4);
            this.Property(p => p.YearProfit ).HasPrecision(24, 4);
            this.Property(p => p.MonthProfit ).HasPrecision(24, 4);

        }
    }
}