using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Core.Domain.InvestmentDecision
{
  public   class InvestmentDecisionTradeRecord:BaseEntity
    {
     public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        public int DailyRecordId { get; set; }
    }
}
