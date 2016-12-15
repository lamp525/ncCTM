using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionApplicationMap : EntityTypeConfiguration<InvestmentDecisionApplication>
    {
        public InvestmentDecisionApplicationMap()
        {
            this.ToTable(nameof(InvestmentDecisionApplication));
            this.HasKey(p => p.Id);

            this.Property(p => p.BuyAmount).HasPrecision(24, 4);
            this.Property(p => p.BuyVolume).HasPrecision(24, 4);
            this.Property(p => p.SellAmount).HasPrecision(24, 4);
            this.Property(p => p.SellVolume).HasPrecision(24, 4);
        }
    }
}