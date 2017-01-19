namespace CTM.Win.Models
{
    public class UserInvestIncomeSummaryModel
    {
        /// <summary>
        /// 统计类型
        /// 0：投资人各股票
        /// 1：投资人小计
        /// 2：投资人合计
        /// </summary>
        public int Type { get; set; }

        public string Investor { get; set; }

        public bool IsOnWorking { get; set; }

        public int TradeType { get; set; }

        public string TradeTypeName { get; set; }

        public string StockFullCode { get; set; }

        public string StockName { get; set; }

        public decimal AllotFund { get; set; }

        public decimal AccumulatedProfit { get; set; }

        public decimal AccumulatedIncomeRate { get; set; }

        public decimal InitAsset { get; set; }

        public decimal InitPositionValue { get; set; }

        public decimal InitHoldingVolume { get; set; }

        public decimal InitProfit { get; set; }

        public decimal CurrentAsset { get; set; }

        public decimal CurrentPositionValue { get; set; }

        public decimal CurrentHoldingVolume { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal CurrentProfit { get; set; }

        public decimal CurrentIncomeRate { get; set; }

        public decimal AnnualProfit { get; internal set; }

        public decimal AnnualIncomeRate { get; internal set; }

        public int UniqueSerialNo { get; internal set; }
    }
}