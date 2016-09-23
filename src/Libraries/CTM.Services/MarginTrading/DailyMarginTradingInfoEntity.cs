using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Services.MarginTrading
{
    public class DailyMarginTradingInfoEntity
    {
        /// <summary>
        /// 借入借出标志
        /// True： 借入
        /// False：借出
        /// </summary>
        public bool isIn { get; set; }

        public string InvestorCode { get; set; }

        public DateTime MarginDate { get; set; }

        public decimal FinancingAmount { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}