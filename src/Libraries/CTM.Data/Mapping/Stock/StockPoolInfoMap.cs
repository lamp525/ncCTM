using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Stock;

namespace CTM.Data.Mapping.Stock
{
    public partial class StockPoolInfoMap : EntityTypeConfiguration<StockPoolInfo>
    {
        public StockPoolInfoMap()
        {
            this.ToTable("StockPoolInfo");
            this.HasKey(p => p.StockId);

            this.Property(p => p.StockId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Ignore(p => p.Id);
            this.Ignore(p => p.TargetName);
            this.Ignore(p => p.BandName);

            this.HasRequired(p => p.StockInfo)
                .WithOptional(sp => sp.StockPoolInfo);
        }
    }
}