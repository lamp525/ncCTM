using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class MarketTrendForecastDetail : BaseEntity
    {
        public string SerialNo { get; set; }

        public DateTime ForecastDate { get; set; }

        public string InvestorCode { get; set; }

        public decimal Weight { get; set; }

        public DateTime? AcquaintanceGraphDate { get; set; }

        public string Trend { get; set; }

        public string Open { get; set; }

        public string Forenoon { get; set; }

        public string Afternoon { get; set; }

        public string Close { get; set; }

        public string Reason { get; set; }

        public string Accuracy { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remarks { get; set; }
    }
}