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

            this.Property(p => p.StopProfitPrice).HasPrecision(18, 4);
            this.Property(p => p.StopProfitBound).HasPrecision(18, 4);
            this.Property(p => p.StopLossPrice).HasPrecision(18, 4);
            this.Property(p => p.StopLossBound).HasPrecision(18, 4);
        }
    }
}