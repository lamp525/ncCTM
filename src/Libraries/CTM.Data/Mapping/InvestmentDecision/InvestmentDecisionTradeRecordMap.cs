using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionTradeRecordMap : EntityTypeConfiguration<InvestmentDecisionTradeRecord>
    {
        public InvestmentDecisionTradeRecordMap()
        {
            this.ToTable(nameof(InvestmentDecisionTradeRecord));
            this.HasKey(p => p.Id);
        }
    }
}