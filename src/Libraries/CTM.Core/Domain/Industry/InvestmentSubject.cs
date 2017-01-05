using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Core.Domain.Industry
{
 public   class InvestmentSubject:BaseEntity
    {
        public int IndustryId { get; set; }

        public decimal TotalFund { get; set; }

        public decimal InvestFund { get; set; }

        public decimal FinancingAmount { get; set; }

        public string Remarks { get; set; }
    }
}
