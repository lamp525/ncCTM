using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Invest;

namespace CTM.Data.Mapping.Invest
{
    public partial class InvestTypeMap : EntityTypeConfiguration<InvestType>
    {
        public InvestTypeMap()
        {
            this.ToTable("InvestType");
            this.HasKey(p => p.Id);
        }
    }
}