using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionCommitteeMap : EntityTypeConfiguration<InvestmentDecisionCommittee>
    {
        public InvestmentDecisionCommitteeMap()
        {
            this.ToTable(nameof(InvestmentDecisionCommittee));
            this.HasKey(p => p.Id);
        }
    }
}