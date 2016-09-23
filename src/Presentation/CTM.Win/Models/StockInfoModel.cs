using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.Models
{
    public class StockInfoModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string FullCode { get; set; }

        public string Name { get; set; }

        public string Remarks { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsInPool { get; set; }

        public string DisplayMember { get; set; }
    }
}