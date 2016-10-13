using System;

namespace CTM.Core.Domain.Account
{
    public class AccountFundTransfer : BaseEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public DateTime TransferDate { get; set; }

        public int TransferType { get; set; }

        public decimal TransferAmount { get; set; }

        public bool FlowFlag { get; set; }

        public int TargetAccountId { get; set; }

        public string TargetAccountCode { get; set; } 

        public DateTime OperateTime { get; set; }

        public string Operator { get; set; }

        public string Remarks { get; set; }
    }
}