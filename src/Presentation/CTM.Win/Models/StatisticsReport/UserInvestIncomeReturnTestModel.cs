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

        public decimal AnnualProfit { get; set; }

        public decimal AverageMarginAmount { get; set; }

        public decimal AnnualInterest { get; set; }

        public decimal AnnualActualProfit { get; set; }

        public decimal AnnualRetracementAmount { get; set; }
    }
}