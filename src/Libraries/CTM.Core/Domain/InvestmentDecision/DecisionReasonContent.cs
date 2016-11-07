namespace CTM.Core.Domain.InvestmentDecision
{
    public class DecisionReasonContent : BaseEntity
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Remarks { get; set; }
    }
}