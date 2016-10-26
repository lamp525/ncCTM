using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class MarketTrendForecastInfoMap : EntityTypeConfiguration<MarketTrendForecastInfo>
    {
        public MarketTrendForecastInfoMap()
        {
            this.ToTable(nameof(MarketTrendForecastInfo));
            this.HasKey(p => p.Id);
        }
    }
}