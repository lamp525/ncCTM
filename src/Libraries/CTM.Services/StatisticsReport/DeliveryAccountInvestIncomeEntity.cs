namespace CTM.Services.StatisticsReport
{
    public class DeliveryAccountInvestIncomeEntity
    {
        public string QueryPeriod { get; set; }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string SecurityCompnayName { get; set; }

        public string AttributeName { get; set; }

        public string AccountDetail { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public string StockDetail { get; set; }

        public decimal PositionValue { get; set; }

        public decimal HoldingVolume { get; set; }

        public decimal LatestPrice { get; set; }

        public decimal Profit { get; set; }

        public decimal AccumulatedProfit { get; set; }
    }
}