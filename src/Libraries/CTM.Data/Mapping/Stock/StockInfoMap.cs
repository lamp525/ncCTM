using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Stock;

namespace CTM.Data.Mapping.Stock
{
    public partial class StockInfoMap : EntityTypeConfiguration<StockInfo>
    {
        public StockInfoMap()
        {
            this.ToTable("StockInfo");
            this.HasKey(p => p.Id);

            this.Property(p => p.Code).HasMaxLength(20);
            this.Property(p => p.FullCode).HasMaxLength(20);
            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.Remarks).HasMaxLength(200);

            this.Ignore(p => p.IsInPool);
        }
    }
}