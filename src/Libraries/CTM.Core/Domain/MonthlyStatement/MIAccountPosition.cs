namespace CTM.Core.Domain.MonthlyStatement
{
    /// <summary>
    /// 每月期初账户持仓
    /// </summary>
    public class MIAccountPosition : BaseEntity
    {
        public int YearMonth { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal PositionVolume { get; set; }
    }
}