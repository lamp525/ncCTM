using CTM.Core.Domain.MarginTrading;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

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