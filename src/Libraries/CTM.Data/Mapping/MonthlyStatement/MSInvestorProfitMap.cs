using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyStatement;

namespace CTM.Data.Mapping.MonthlyStatement
{
    public partial class MSInvestorProfitMap : EntityTypeConfiguration<MSInvestorProfit>
    {
        public MSInvestorProfitMap()
        {
            this.ToTable(nameof(MSInvestorProfit));
            this.HasKey(p => p.Id);

            this.Property(p => p.AccumulatedProfit).HasPrecision(24, 4);
            this.Property(p => p.YearProfit).HasPrecision(24, 4);
            this.Property(p => p.MonthProfit).HasPrecision(24, 4);
            this.Property(p => p.WithDrawAmount).HasPrecision(24, 4);
        }
    }
}