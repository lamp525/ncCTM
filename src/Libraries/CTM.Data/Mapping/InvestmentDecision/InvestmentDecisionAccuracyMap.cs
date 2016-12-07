using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionAccuracyMap : EntityTypeConfiguration<InvestmentDecisionAccuracy>
    {
        public InvestmentDecisionAccuracyMap()
        {
            this.ToTable(nameof(InvestmentDecisionAccuracy));
            this.HasKey(p => p.Id);

            this.Property(p => p.Weight).HasPrecision(18, 4);
        }
    }
}