using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionAccuracyMap : EntityTypeConfiguration<InvestmentDecisionOperationAccuracy>
    {
        public InvestmentDecisionAccuracyMap()
        {
            this.ToTable(nameof(InvestmentDecisionOperationAccuracy));
            this.HasKey(p => p.Id);

            this.Property(p => p.Weight).HasPrecision(18, 4);
        }
    }
}