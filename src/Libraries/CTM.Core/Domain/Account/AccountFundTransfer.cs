using System;

namespace CTM.Core.Domain.Account
{
    public class AccountFundTransfer : BaseEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public DateTime Time { get; set; }

        public string Operator { get; set; }

        public int TransferType { get; set; }

        public bool FlowType { get; set; }

        public int TargetAccountId { get; set; }

        public string TargetAccountCode { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public string Remarks { get; set; }
    }
}