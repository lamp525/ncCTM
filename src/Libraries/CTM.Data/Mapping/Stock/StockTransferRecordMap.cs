using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Stock;

namespace CTM.Data.Mapping.Stock
{
    public partial class StockTransferRecordMap : EntityTypeConfiguration<StockTransferRecord>
    {
        public StockTransferRecordMap()
        {
            this.ToTable("StockTransferRecord");
            this.HasKey(p => p.Id);
        }
    }
}