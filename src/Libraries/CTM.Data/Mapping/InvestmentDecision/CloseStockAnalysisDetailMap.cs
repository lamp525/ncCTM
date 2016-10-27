using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class CloseStockAnalysisDetailMap : EntityTypeConfiguration<CloseStockAnalysisDetail>
    {
        public CloseStockAnalysisDetailMap()
        {
            this.ToTable(nameof(CloseStockAnalysisDetail));
            this.HasKey(p => p.Id);
        }
    }
}