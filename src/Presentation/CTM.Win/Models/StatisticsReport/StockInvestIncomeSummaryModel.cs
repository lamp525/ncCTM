namespace CTM.Win.Models
{
    public class StockInvestIncomeSummaryModel
    {
        /// <summary>
        /// 统计类型
        /// 0：各股票
        /// 1：合计
        /// </summary>
        public int Type { get; set; }

        public string StockFullCode { get; set; }

        public string StockName { get; set; }

        public decimal AccumulatedProfit { get; set; }

        public decimal AnnualProfit { get; set; }

        //public decimal AccumulatedIncomeRate { get; set; }

        public decimal InitPositionValue { get; set; }

        public decimal InitHoldingVolume { get; set; }

        public decimal InitPrice { get; set; }

        public decimal InitProfit { get; set; }

        public decimal CurrentPositionValue { get; set; }

        public decimal CurrentHoldingVolume { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal CurrentProfit { get; set; }

        //  public decimal CurrentIncomeRate { get; set; }

        public decimal TargetPositionValue { get; set; }

        public decimal TargetHoldingVolume { get; set; }

        public decimal TargetTotalProfit { get; set; }

        //  public decimal TargetIncomeRate { get; set; }

        public decimal TargetActualProfit { get; set; }

        public decimal TargetFoatingProfit { get; set; }

        public decimal BandPositionValue { get; set; }

        public decimal BandHoldingVolume { get; set; }

        public decimal BandTotalProfit { get; set; }

        // public decimal BandIncomeRate { get; set; }

        public decimal BandActualProfit { get; set; }

        public decimal BandFoatingProfit { get; set; }

        public decimal DayPositionValue { get; set; }

        public decimal DayHoldingVolume { get; set; }

        public decimal DayTotalProfit { get; set; }

        //   public decimal DayIncomeRate { get; set; }

        public decimal DayActualProfit { get; set; }

        public decimal DayFoatingProfit { get; set; }
    }
}