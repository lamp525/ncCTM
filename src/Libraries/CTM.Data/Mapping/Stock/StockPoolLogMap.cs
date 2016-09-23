using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Stock;

namespace CTM.Data.Mapping.Stock
{
    public partial class StockPoolLogMap : EntityTypeConfiguration<StockPoolLog>
    {
        public StockPoolLogMap()
        {
            this.ToTable("StockPoolLog");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.BandPricipalName);
            this.Ignore(p => p.TargetPricipalName);
            this.Ignore(p => p.StockFullCode);
            this.Ignore(p => p.StockName);
            this.Ignore(p => p.OperatorName);
        }
    }
}