using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class InvestmentPlanRecordSummaryMap : EntityTypeConfiguration<InvestmentPlanRecordSummary>
    {
        public InvestmentPlanRecordSummaryMap()
        {
            this.ToTable(nameof(InvestmentPlanRecordSummary));
            this.HasKey(p => p.Id);
        }
    }
}