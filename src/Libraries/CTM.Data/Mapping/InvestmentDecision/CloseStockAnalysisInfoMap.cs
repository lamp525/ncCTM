using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class CloseStockAnalysisInfoMap : EntityTypeConfiguration<CloseStockAnalysisInfo>
    {
        public CloseStockAnalysisInfoMap()
        {
            this.ToTable(nameof(CloseStockAnalysisInfo));
            this.HasKey(p => p.Id);
        }
    }
}