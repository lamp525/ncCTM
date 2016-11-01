using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionFormMap : EntityTypeConfiguration<InvestmentDecisionForm>
    {
        public InvestmentDecisionFormMap()
        {
            this.ToTable(nameof(InvestmentDecisionForm));
            this.HasKey(p => p.Id);

            this.Property(p => p.Price).HasPrecision(18, 4);
            this.Property(p => p.PriceBound).HasPrecision(18, 4);
            this.Property(p => p.Volume).HasPrecision(24, 0);
            this.Property(p => p.Amount).HasPrecision(24, 4);
            this.Property(p => p.Profit).HasPrecision(24, 4);
            //this.Property(p => p.LossBound).HasPrecision(18, 4);
            //this.Property(p => p.LossPrice).HasPrecision(18, 4);
            //this.Property(p => p.ProfitBound).HasPrecision(18, 4);
            //this.Property(p => p.ProfitPrice).HasPrecision(18, 4);
        }
    }
}