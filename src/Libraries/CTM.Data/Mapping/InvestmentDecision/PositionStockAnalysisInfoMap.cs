using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class PositionStockAnalysisInfoMap : EntityTypeConfiguration<PositionStockAnalysisInfo>
    {
        public PositionStockAnalysisInfoMap()
        {
            this.ToTable(nameof(PositionStockAnalysisInfo));
            this.HasKey(p => p.Id);
        }
    }
}