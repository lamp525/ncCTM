namespace CTM.Core.Domain.MonthlyStatement
{
    public class MSDailyInvestor : BaseEntity
    {
        public int YearMonth { get; set; }

        public string InvestorCode { get; set; }

        public decimal PositionValue { get; set; }

        public decimal BuyAmount { get; set; }

        public decimal SellAmount { get; set; }

        public decimal DealAmount { get; set; }

        public decimal MarginAmount { get; set; }

        public decimal AccumulatedInterest { get; set; }

        public decimal YearInterest { get; set; }

        public decimal MonthInterest { get; set; }

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

        /// <summary>
        /// 已提取盈利额
        /// </summary>
        public decimal WithDrawAmount { get; set; }
    }
}