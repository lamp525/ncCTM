using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class MarketTrendForecastInfo : BaseEntity
    {
        public string SerialNo { get; set; }

        public int Status { get; set; }

        public string ApplyUser { get; set; }

        public DateTime ApplyDate { get; set; }

        public DateTime CreateTime { get; set; }
    }
}