using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;

namespace CTM.Services.TradeRecord
{
    public class RecordImportOperationEntity
    {
        public EnumLibrary.DataType DataType { get; set; }

        public DateTime? TradeDate { get; set; }

        public int AccountId { get; set; }

        public string OperatorCode { get; set; }

        public DateTime ImportTime { get; set; }

        public string ImportUserCode { get; set; }
    }
}