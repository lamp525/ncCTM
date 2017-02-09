using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentPlanRecordSummary : BaseEntity
    {
        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        public DateTime CreateTime { get; set; }
    }
}