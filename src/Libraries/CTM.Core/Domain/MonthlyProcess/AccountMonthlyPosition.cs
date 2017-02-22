namespace CTM.Core.Domain.MonthlyProcess
{
    public class AccountMonthlyPosition : BaseEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public int YearMonth { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal PositionVolume { get; set; }
    }
}