using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentDecisionStockPoolLogMap : EntityTypeConfiguration<InvestmentDecisionStockPoolLog>
    {
        public InvestmentDecisionStockPoolLogMap()
        {
            this.ToTable(nameof(InvestmentDecisionStockPoolLog));
            this.HasKey(p => p.Id);
        }
    }
}