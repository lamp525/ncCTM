namespace CTM.Win.Models
{
    public class UserInvestIncomeAccountModel
    {
        public string QueryPeriod { get; set; }

        public string InvestorName { get; set; }

        public string InvestorCode { get; set; }

        public bool IsOnWorking { get; set; }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string SecurityCompnayName { get; set; }

        public string AttributeName { get; set; }

        public string AccountDetail { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public string StockDetail { get; set; }

        public decimal Profit { get; set; }

        public decimal AccumulatedProfit { get; set; }
    }
}