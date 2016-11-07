using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class DecisionReasonCategoryMap : EntityTypeConfiguration<DecisionReasonCategory>
    {
        public DecisionReasonCategoryMap()
        {
            this.ToTable(nameof(DecisionReasonCategory));
            this.HasKey(p => p.Id);
        }
    }
}