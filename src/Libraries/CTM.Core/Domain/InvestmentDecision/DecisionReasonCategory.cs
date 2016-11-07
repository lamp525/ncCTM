namespace CTM.Core.Domain.InvestmentDecision
{
    public class DecisionReasonCategory : BaseEntity
    {
        public int ParentId { get; set; }

        public string Name { get; set; }

        public string Remarks { get; set; }
    }
}