using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Core.Domain.InvestmentDecision
{
   public  class MarketTrendForecast:BaseEntity 
    {
        public string SerialNo { get; set; }

        public string InvestorCode { get; set; }

        public DateTime AcquaintanceGraphDate { get; set; }

        public string Trend { get; set; }

        public string Open { get; set; }

        public string Forenoon { get; set; }

        public string Afrernoon { get; set; }

        public string Close { get; set; }

        public string Reason { get; set; }

        public string Accuracy { get; set; }
    }
}
