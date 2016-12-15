using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionApplication : BaseEntity
    {
        public string TradePlanNo { get; set; }

        public string ApplyNo { get; set; }

        public string ApplyUser { get; set; }

        public int DepartmentId { get; set; }

        public DateTime ApplyDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public int TradeType { get; set; }

        public int InitialDealFlag { get; set; }

        public decimal BuyVolume { get; set; }

        public decimal BuyAmount { get; set; }

        public decimal SellVolume { get; set; }

        public decimal SellAmount { get; set; }



        /// <summary>
        /// 投资决策单状态
        /// 0：进行中
        /// 1：完成
        /// </summary>
        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}