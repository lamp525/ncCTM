using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.Models
{
    public class TradeRecordSearchModel
    {
        public int DataType { get; set; }

        public string StockCode { get; set; }

        public int AccountId { get; set; }

        public string Beneficiary { get; set; }

        public int TradeType { get; set; }

        public bool? DealFlag { get; set; }

        public string Operator { get; set; }

        public DateTime? TradeDateFrom { get; set; }

        public DateTime? TradeDateTo { get; set; }

        public string ImportUser { get; set; }

        public DateTime? ImportDateFrom { get; set; }

        public DateTime? ImportDateTo { get; set; }
    }
}