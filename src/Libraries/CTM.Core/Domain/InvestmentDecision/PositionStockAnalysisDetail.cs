using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class PositionStockAnalysisDetail : BaseEntity
    {
        public string SerialNo { get; set; }

        public string InvestorCode { get; set; }

        public DateTime AnalysisDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public int TradeType { get; set; }

        public string Decision { get; set; }

        public decimal DealRange { get; set; }

        public decimal DealAmount { get; set; }

        public string PriceRange { get; set; }

        public string Reason { get; set; }

        public string Accuracy { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remarks { get; set; }
    }
}