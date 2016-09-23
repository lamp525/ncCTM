using System;

namespace CTM.Core.Domain.Account
{
    public class AccountTradeFund : BaseEntity
    {
        public string AccountCode { get; set; }

        public DateTime Time { get; set; }

        public string Operator { get; set; }

        public int TradeType { get; set; }

        public bool FlowType { get; set; }

        public string ObjectAccountCode { get; set; }

        public int Volume { get; set; }

        public decimal TradePrice { get; set; }

        public decimal CostPrice { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public string Remarks { get; set; }
    }
}