using System;

namespace CTM.Services.StatisticsReport
{
    public class UserMarginTradingInterestEntity
    {
        public string InvestorCode { get; set; }

        public DateTime TradeDate { get; set; }

        public decimal PlanLoanAmount { get; set; }

        public decimal PlanFinancingAmount { get; set; }

        public decimal ActualLoanAmount { get; set; }

        public decimal ActualFinancingAmount { get; set; }

        public decimal PlanMarginAmount { get; set; }

        public decimal ActualMarginAmount { get; set; }

        public decimal AverageMarginAmount { get; set; }

        public decimal LastDayMarginAmount { get; set; }

        public decimal AccumulatedMarginAmount { get; set; }

        public decimal CurrentInterest { get; set; }

        public decimal LastDayInterest { get; set; }

        public decimal AccumulatedInterest { get; set; }
    }
}