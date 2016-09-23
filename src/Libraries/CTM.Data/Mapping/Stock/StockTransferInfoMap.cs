using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using CTM.Core.Domain.Stock;

namespace CTM.Data.Mapping.Stock
{
    public partial class StockTransferInfoMap : EntityTypeConfiguration<StockTransferInfo>
    {
        public StockTransferInfoMap()
        {
            this.ToTable("StockTransferInfo");
            this.HasKey(p => p.Id);

            this.Property(p => p.TransferPrice).HasPrecision(18, 4);
        }
    }
}