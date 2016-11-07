using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class DecisionReasonContentMap : EntityTypeConfiguration<DecisionReasonContent>
    {
        public DecisionReasonContentMap()
        {
            this.ToTable(nameof(DecisionReasonContent));
            this.HasKey(p => p.Id);
        }
    }
}