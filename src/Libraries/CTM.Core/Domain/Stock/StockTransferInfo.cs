using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Core.Domain.Stock
{
    public class StockTransferInfo : BaseEntity
    {
        public int HolderAccountId { get; set; }

        public string HolderAccountInfo { get; set; }

        public int ReceivedAccountId { get; set; }

        public string ReceivedAccountInfo { get; set; }

        public string StockFullCode { get; set; }

        public string StockName { get; set; }

        public string Holder { get; set; }

        public string HolderName { get; set; }

        public string Receiver { get; set; }

        public string ReceiverName { get; set; }

        public string OperatorCode { get; set; }

        public string OperatorName { get; set; }

        public int TransferVolume { get; set; }

        public decimal TransferPrice { get; set; }

        public DateTime TransferTime { get; set; }
    }
}