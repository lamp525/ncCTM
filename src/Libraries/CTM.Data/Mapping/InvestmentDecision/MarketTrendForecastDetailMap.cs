using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class MarketTrendForecastDetailMap : EntityTypeConfiguration<MarketTrendForecastDetail>
    {
        public MarketTrendForecastDetailMap()
        {
            this.ToTable(nameof(MarketTrendForecastDetail));
            this.HasKey(p => p.Id);

            this.Property(p => p.Weight).HasPrecision(18, 4);
        }
    }
}