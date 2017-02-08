using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentPlanRecordMap : EntityTypeConfiguration<InvestmentPlanRecord>
    {
        public InvestmentPlanRecordMap()
        {
            this.ToTable(nameof(InvestmentPlanRecord));
            this.HasKey(p => p.Id);

            this.Property(p => p.PlanPrice).HasPrecision(18, 4);
            this.Property(p => p.PlanVolume ).HasPrecision(24, 0);
            this.Property(p => p.PlanAmount ).HasPrecision(24, 4);
            this.Property(p => p.ProfitPrice ).HasPrecision(18, 4);
            this.Property(p => p.LossPrice).HasPrecision(18, 4);
            this.Property(p => p.DealPrice ).HasPrecision(18, 4);
            this.Property(p => p.DealVolume ).HasPrecision(24, 0);
            this.Property(p => p.DealAmount ).HasPrecision(24, 4);

        }
    }
}