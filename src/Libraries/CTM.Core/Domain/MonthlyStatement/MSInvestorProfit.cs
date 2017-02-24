namespace CTM.Core.Domain.MonthlyStatement
{
    public class MSInvestorProfit : BaseEntity
    {
        public string InvestorCode { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

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