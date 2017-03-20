using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class DSDeliveryAccountMap : EntityTypeConfiguration<DSDeliveryAccount>
    {
        public DSDeliveryAccountMap()
        {
            this.ToTable(nameof(DSDeliveryAccount));
            this.HasKey(p => p.Id);

            this.Property(p => p.AccountCode).HasMaxLength(20);

            this.Property(p => p.TotalAsset).HasPrecision(24, 4);
            this.Property(p => p.AvailableFund).HasPrecision(24, 4);
            this.Property(p => p.PositionValue).HasPrecision(24, 4);
            this.Property(p => p.FinancedAmount).HasPrecision(24, 4);
            this.Property(p => p.FinancingLimit).HasPrecision(24, 4);
            this.Property(p => p.AccumulatedProfit).HasPrecision(24, 4);
            this.Property(p => p.YearProfit).HasPrecision(24, 4);
            this.Property(p => p.DayProfit).HasPrecision(24, 4);
        }
    }
}