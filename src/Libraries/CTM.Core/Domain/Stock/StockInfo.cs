using System.Collections.Generic;

namespace CTM.Core.Domain.Stock
{
    public class StockInfo : BaseEntity
    {
        public string Code { get; set; }

        public string FullCode { get; set; }

        public string Name { get; set; }

        public string Remarks { get; set; }

        public bool IsDeleted { get; set; }

        public virtual bool IsInPool { get; set; }

        public virtual StockPoolInfo StockPoolInfo { get; set; }
    }
}