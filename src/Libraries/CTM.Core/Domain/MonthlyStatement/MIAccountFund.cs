namespace CTM.Core.Domain.MonthlyStatement
{
    /// <summary>
    /// 每月期初账户资金
    /// </summary>
    public class MIAccountFund : BaseEntity
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
    }
}