using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class DSDailyInvestorMap : EntityTypeConfiguration<DSDailyInvestor>
    {
        public DSDailyInvestorMap()
        {
            this.ToTable(nameof(DSDailyInvestor));
            this.HasKey(p => p.Id);

            this.Property(p => p.InvestorCode).HasMaxLength(20);

            this.Property(p => p.PositionValue).HasPrecision(24, 4);
            this.Property(p => p.BuyAmount).HasPrecision(24, 4);
            this.Property(p => p.SellAmount).HasPrecision(24, 4);
            this.Property(p => p.DealAmount).HasPrecision(24, 4);
            this.Property(p => p.MarginAmount).HasPrecision(24, 4);
            this.Property(p => p.AccumulatedInterest).HasPrecision(24, 4);
            this.Property(p => p.YearInterest).HasPrecision(24, 4);
            this.Property(p => p.DayInterest).HasPrecision(24, 4);
            this.Property(p => p.AccumulatedProfit).HasPrecision(24, 4);
            this.Property(p => p.YearProfit).HasPrecision(24, 4);
            this.Property(p => p.DayProfit).HasPrecision(24, 4);
        }
    }
}