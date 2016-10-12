using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.Services.Account
{
  public   class AccountFundTransferEntity
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public string AccountDetail { get; set; }

        public DateTime TransferDate { get; set; }

        public int TransferType { get; set; }

        public decimal TransferAmount { get; set; }

        public bool FlowFlag { get; set; }

        public int TargetAccountId { get; set; }

        public string TargetAccountCode { get; set; }

        public decimal Balance { get; set; }

        public DateTime OperateTime { get; set; }

        public string Operator { get; set; }

        public string Remarks { get; set; }
    }
}
