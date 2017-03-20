using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class MSDailyInvestorMap : EntityTypeConfiguration<MSDailyInvestor>
    {
        public MSDailyInvestorMap()
        {
            this.ToTable(nameof(MSDailyInvestor));
            this.HasKey(p => p.Id);

            this.Property(p => p.InvestorCode).HasMaxLength(20);

            this.Property(p => p.PositionValue).HasPrecision(24, 4);
            this.Property(p => p.BuyAmount).HasPrecision(24, 4);
            this.Property(p => p.SellAmont).HasPrecision(24, 4);
            this.Property(p => p.DealAmount).HasPrecision(24, 4);
            this.Property(p => p.MarginAmount).HasPrecision(24, 4);
            this.Property(p => p.AccumulatedInterest).HasPrecision(24, 4);
            this.Property(p => p.YearInterest).HasPrecision(24, 4);
            this.Property(p => p.MonthInterest).HasPrecision(24, 4);
            this.Property(p => p.AccumulatedProfit).HasPrecision(24, 4);
            this.Property(p => p.YearProfit).HasPrecision(24, 4);
            this.Property(p => p.MonthProfit).HasPrecision(24, 4);
            this.Property(p => p.WithDrawAmount).HasPrecision(24, 4);
        }
    }
}