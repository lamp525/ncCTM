using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionOperationVoteMap : EntityTypeConfiguration<InvestmentDecisionOperationVote>
    {
        public InvestmentDecisionOperationVoteMap()
        {
            this.ToTable(nameof(InvestmentDecisionOperationVote));
            this.HasKey(p => p.Id);

            this.Property(p => p.Weight).HasPrecision(18, 4);
        }
    }
}