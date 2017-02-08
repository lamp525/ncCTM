using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentPlanRecord:BaseEntity
    {
        public string SerialNo { get; set; }

        public string InvestorCode { get; set; }

        public DateTime AnalysisDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public int Trend { get; set; }

        public int Probability { get; set; }

        public string Logic { get; set; }

        public int Scheme { get; set; }

        public  int TradeType { get; set; }

        public int OperateMode { get; set; }

        public string Expected { get; set; }

        public string Unexpected { get; set; }

        public decimal PlanPrice { get; set; }

        public decimal PlanVolume { get; set; }

        public decimal PlanAmount { get; set; }

        public decimal LossPrice { get; set; }

        public decimal ProfitPrice { get; set; }

        public DateTime? DealDate { get; set; }

        public decimal DealPrice { get; set; }

        public decimal DealVolume { get; set; }

        public decimal DealAmount { get; set; }

        public string Summary { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
