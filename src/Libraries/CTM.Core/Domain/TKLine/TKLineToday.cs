using System;

namespace CTM.Core.Domain.TKLine
{
    public class TKLineToday : BaseEntity
    {
        public string StockCode { get; set; }

        public DateTime TradeDate { get; set; }

        public decimal Close { get; set; }
    }
}