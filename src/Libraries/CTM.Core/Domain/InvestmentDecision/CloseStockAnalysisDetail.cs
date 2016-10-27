using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class CloseStockAnalysisDetail : BaseEntity
    {
        public string SerialNo { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public int TradeType { get; set; }

        public string Decision { get; set; }

        public string PriceRange { get; set; }

        public string Reason { get; set; }

        public string Accuracy { get; set; }

        public DateTime? AnalysisTime { get; set; }
    }
}