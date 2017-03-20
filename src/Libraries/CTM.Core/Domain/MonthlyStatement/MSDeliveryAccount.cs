namespace CTM.Core.Domain.MonthlyStatement
{
    public class MSDeliveryAccount : BaseEntity
    {
        public int YearMonth { get; set; }

        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        /// <summary>
        /// 总资产
        /// </summary>
        public decimal TotalAsset { get; set; }

        /// <summary>
        /// 可用资金
        /// </summary>
        public decimal AvailableFund { get; set; }

        /// <summary>
        /// 持仓市值
        /// </summary>
        public decimal PositionValue { get; set; }

        /// <summary>
        /// 可融资额
        /// </summary>
        public decimal FinancingLimit { get; set; }

        /// <summary>
        /// 已融资额
        /// </summary>
        public decimal FinancedAmount { get; set; }

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