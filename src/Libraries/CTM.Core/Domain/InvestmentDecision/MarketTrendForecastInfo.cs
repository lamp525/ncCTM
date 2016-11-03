using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class MarketTrendForecastInfo : BaseEntity
    {
        public string SerialNo { get; set; }

        public DateTime ForecastDate { get; set; }

        public DateTime CreateTime { get; set; }

        public string Result { get; set; }
    }
}