using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Core.Domain.TradeRecord
{
    public class DeliveryRecord : BaseRecord
    {
        public virtual string AccountName { get; set; }

        public virtual string ImportUserName { get; set; }

        public virtual string UpdateUserName { get; set; }
    }
}