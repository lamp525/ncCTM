using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionVoteMap : EntityTypeConfiguration<InvestmentDecisionVote>
    {
        public InvestmentDecisionVoteMap()
        {
            this.ToTable(nameof(InvestmentDecisionVote));
            this.HasKey(p => p.Id);
        }
    }
}