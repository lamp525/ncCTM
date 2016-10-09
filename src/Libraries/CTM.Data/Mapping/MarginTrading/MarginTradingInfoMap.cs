using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MarginTrading;

namespace CTM.Data.Mapping.MarginTrading
{
    public partial class MarginTradingInfoMap : EntityTypeConfiguration<MarginTradingInfo>
    {
        public MarginTradingInfoMap()
        {
            this.ToTable("MarginTradingInfo");
            this.HasKey(p => p.Id);

            this.Property(p => p.Amount).HasPrecision(24, 4);
        }
    }
}