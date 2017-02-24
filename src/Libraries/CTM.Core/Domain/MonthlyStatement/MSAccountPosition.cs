namespace CTM.Core.Domain.MonthlyStatement
{
    public class MSAccountPosition : BaseEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal PositionVolume { get; set; }

        public decimal CostPrice { get; set; }
    }
}