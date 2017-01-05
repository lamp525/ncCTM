namespace CTM.Core.Domain.Industry
{
    public class InvestmentSubject : BaseEntity
    {
        public int IndustryId { get; set; }

        public decimal TotalFund { get; set; }

        public decimal InvestFund { get; set; }

        public decimal NetAsset { get; set; }

        public decimal FinancingAmount { get; set; }

        public string Remarks { get; set; }
    }
}