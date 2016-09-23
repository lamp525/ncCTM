using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.Models
{
    public class UserInvestIncomeRetracementModel
    {
        public string TradeDate { get; set; }

        public string DepartmentName { get; set; }

        public string Investor { get; set; }

        public bool IsOnWorking { get; set; }

        public decimal PeriodProfit { get; set; }

        public decimal PeriodAverageMarginAmount { get; set; }

        public decimal PeriodInterest { get; set; }

        public decimal PeriodActualProfit { get; set; }

        public decimal PeriodMaxRetracementAmount { get; set; }

        public decimal AccumulatedProfit { get; set; }

        public decimal AverageMarginAmount { get; set; }

        public decimal AccumulatedInterest { get; set; }

        public decimal AccumulatedActualProfit { get; set; }

        public decimal MaxRetracementAmount { get; set; }
    }
}