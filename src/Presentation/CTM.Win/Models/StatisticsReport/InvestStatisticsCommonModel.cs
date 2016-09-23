using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.Models
{
    public class InvestStatisticsCommonModel
    {
        /// <summary>
        /// 交易日
        /// </summary>
        public DateTime TradeDate { get; internal set; }

        /// <summary>
        /// 成交额
        /// </summary>
        public decimal DealAmount { get; set; }

        /// <summary>
        /// 持仓市值
        /// </summary>
        public decimal PositionValue { get; set; }

        /// <summary>
        /// 持仓收益
        /// </summary>
        public decimal PositionProfit { get; set; }

        /// <summary>
        /// 累计发生金额（累计实际收益不含持仓收益）
        /// </summary>
        public decimal AccumulatedActualAmount { get; set; }

        /// <summary>
        /// 累计收益（包含持仓收益）
        /// </summary>
        public decimal AccumulatedProfit { get; set; }
    }
}