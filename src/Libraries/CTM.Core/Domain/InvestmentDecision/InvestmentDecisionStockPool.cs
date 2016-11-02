namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionStockPool : BaseEntity
    {
        public string StockCode { get; set; }

        public string StockName { get; set; }

        public string Principal { get; set; }

        public string Remarks { get; set; }
    }
}