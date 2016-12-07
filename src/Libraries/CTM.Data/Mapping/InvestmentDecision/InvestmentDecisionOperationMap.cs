using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionOperationMap : EntityTypeConfiguration<InvestmentDecisionOperation>
    {
        public InvestmentDecisionOperationMap()
        {
            this.ToTable(nameof(InvestmentDecisionOperation));
            this.HasKey(p => p.Id);

            this.Property(p => p.DealPrice).HasPrecision(18, 4);
            this.Property(p => p.PriceBound).HasPrecision(18, 4);
            this.Property(p => p.DealVolume).HasPrecision(24, 0);
            this.Property(p => p.DealAmount).HasPrecision(24, 4);
        }
    }
}