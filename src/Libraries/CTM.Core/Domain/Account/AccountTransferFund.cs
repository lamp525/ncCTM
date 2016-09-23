using System;

namespace CTM.Core.Domain.Account
{
    public class AccountTransferFund : BaseEntity
    {
        public string AccountCode { get; set; }

        public DateTime Time { get; set; }

        public string Operator { get; set; }

        public int TransferType { get; set; }

        public bool FlowType { get; set; }

        public string ObjectAccountCode { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public string Remarks { get; set; }
    }
}