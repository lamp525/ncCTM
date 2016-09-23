using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Stock;

namespace CTM.Data.Mapping.Stock
{
    public partial class StockPoolEntryMap : EntityTypeConfiguration<StockPoolEntry>
    {
        public StockPoolEntryMap()
        {
            this.ToTable("StockPoolEntry");
            this.HasKey(p => p.Id);
        }
    }
}