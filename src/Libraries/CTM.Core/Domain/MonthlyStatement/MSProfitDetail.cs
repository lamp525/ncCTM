namespace CTM.Core.Domain.MonthlyStatement
{
    public class MSProfitDetail : BaseEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public string InvestorCode { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal PositionVolume { get; set; }

        /// <summary>
        /// 累计收益
        /// </summary>
        public decimal AccumulatedProfit { get; set; }

        /// <summary>
        /// 年度收益
        /// </summary>
        public decimal YearProfit { get; set; }

        /// <summary>
        /// 月度收益
        /// </summary>
        public decimal MonthProfit { get; set; }
    }
}