namespace CTM.Services.InvestmentDecision
{
    public class DecisionReasonCategoryEntity
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Remarks { get; set; }
    }
}