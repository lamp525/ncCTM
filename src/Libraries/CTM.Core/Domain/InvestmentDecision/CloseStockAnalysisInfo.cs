using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class CloseStockAnalysisInfo : BaseEntity
    {
        public string SerialNo { get; set; }

        public string InvestorCode { get; set; }

        public DateTime JudgmentDate { get; set; }

        public DateTime CreateTime { get; set; }

        public string Result { get; set; }
    }
}