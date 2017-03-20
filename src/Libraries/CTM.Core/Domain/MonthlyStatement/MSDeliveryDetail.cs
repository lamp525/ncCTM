namespace CTM.Core.Domain.MonthlyStatement
{
    public class MSDeliveryDetail : BaseEntity
    {
        public int YearMonth { get; set; }

        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal PositionVolume { get; set; }

        public decimal PositionValue { get; set; }

        public decimal CostPrice { get; set; }

        public decimal AccumulatedProfit { get; set; }

        public decimal YearProfit { get; set; }

        public decimal MonthProfit { get; set; }
    }
}