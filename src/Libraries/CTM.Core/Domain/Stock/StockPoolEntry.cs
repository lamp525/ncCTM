using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Core.Domain.Stock
{
    public class StockPoolEntry : BaseEntity
    {
        /// <summary>
        /// 股票ID
        /// </summary>
        public int StockId { get; set; }

        /// <summary>
        /// 添加标志
        /// True：添加
        /// False：修改
        /// </summary>
        public bool AddFlag { get; set; }

        /// <summary>
        /// 目标负责人代码
        /// </summary>
        public string TargetPrincipal { get; set; }

        /// <summary>
        /// 波段负责人代码
        /// </summary>
        public string BandPrincipal { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? ToDate { get; set; }

        public string Remarks { get; set; }
    }
}