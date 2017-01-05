using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Industry;

namespace CTM.Data.Mapping.Industry
{
    public partial class InvestmentSubjectMap:EntityTypeConfiguration <InvestmentSubject >
    {
        public InvestmentSubjectMap ()
        {
            this.ToTable(nameof(InvestmentSubject));
            this.HasKey(p => p.Id);

            this.Property(p => p.TotalFund).HasPrecision(24, 4);
            this.Property(p => p.InvestFund).HasPrecision(24, 4);
            this.Property(p => p.NetAsset).HasPrecision(24, 4);
            this.Property(p => p.FinancingAmount).HasPrecision(24, 4);
        }
    }
}
