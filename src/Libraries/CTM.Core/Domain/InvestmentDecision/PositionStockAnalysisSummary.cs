using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Core.Domain.InvestmentDecision
{
   public  class PositionStockAnalysisSummary: BaseEntity
    {
        public string SerialNo { get; set; }

        public string Principal { get; set; }

        public DateTime AnalysisDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public int TradeType { get; set; }

        public string Decision { get; set; }

        public string PriceRange { get; set; }

        public string Reason { get; set; }

        public string Accuracy { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remarks { get; set; }
    }
}
