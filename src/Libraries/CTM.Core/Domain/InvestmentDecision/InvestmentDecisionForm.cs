using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionForm : BaseEntity
    {
        public string SerialNo { get; set; }

        public int Status { get; set; }

        public int Point { get; set; }

        public DateTime ApplyDate { get; set; }

        public string ApplyUser { get; set; }

        public string StockFullCode { get; set; }

        public string StockName { get; set; }

        public int TradeType { get; set; }

        public bool DealFlag { get; set; }

        public decimal Price { get; set; }

        public decimal Volume { get; set; }

        public decimal Profit { get; set; }

        public string RelateTradePlanNo { get; set; }

        public DateTime CreateTime { get; set; }
    }
}