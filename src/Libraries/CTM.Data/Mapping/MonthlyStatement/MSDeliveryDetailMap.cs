using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class MSDeliveryDetailMap : EntityTypeConfiguration<MSDeliveryDetail>
    {
        public MSDeliveryDetailMap()
        {
            this.ToTable(nameof(MSDeliveryDetail));
            this.HasKey(p => p.Id);

            this.Property(p => p.AccountCode).HasMaxLength(20);
            this.Property(p => p.StockCode).HasMaxLength(20);
            this.Property(p => p.StockName).HasMaxLength(20);

            this.Property(p => p.PositionVolume).HasPrecision(24, 0);
            this.Property(p => p.PositionValue).HasPrecision(24, 4);
            this.Property(p => p.CostPrice).HasPrecision(18, 4);
            this.Property(p => p.AccumulatedProfit).HasPrecision(24, 4);
            this.Property(p => p.YearProfit).HasPrecision(24, 4);
            this.Property(p => p.MonthProfit).HasPrecision(24, 4);
        }
    }
}