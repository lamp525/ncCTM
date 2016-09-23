using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Core.Domain.MarginTrading
{
    public class MarginTradingInfo : BaseEntity
    {
        public string InvestorCode { get; set; }

        public DateTime MarginDate { get; set; }

        public int TradeType { get; set; }

        /// <summary>
        /// 还资还券标志
        /// True ：还资还券
        /// Flase：融资融券
        /// </summary>
        public bool IsRepay { get; set; }

        /// <summary>
        /// 资金证券标志
        /// True：资金
        /// False：证券
        /// </summary>
        public bool IsFinancing { get; set; }

        public int DepartmentId { get; set; }

        public int AccountId { get; set; }

        public string StockFullCode { get; set; }

        public string StockName { get; set; }

        public string LoanOwnerCode { get; set; }

        public int LoanVolume { get; set; }

        public decimal Amount { get; set; }
    }
}