using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.TKLine;

namespace CTM.Data.Mapping.TKLine
{
    public partial class TKLineTodayMap : EntityTypeConfiguration<TKLineToday>
    {
        public TKLineTodayMap()
        {
            this.ToTable("TKLineToday");
            this.HasKey(p => p.Id);

            this.Property(p => p.StockCode).HasMaxLength(16);
            this.Property(p => p.Close).HasPrecision(18, 4);
        }
    }
}