using System;

namespace CTM.Core.Domain.Stock
{
    public class StockPoolLog : BaseEntity
    {
        public int StockId { get; set; }

        /// <summary>
        /// 操作类型
        /// 1：添加
        /// 2：修改
        /// 3：删除
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatorTime { get; set; }

        /// <summary>
        /// 目标负责人代码
        /// </summary>
        public string TargetPrincipal { get; set; }

        /// <summary>
        /// 波段负责人代码
        /// </summary>
        public string BandPrincipal { get; set; }

        public virtual string StockFullCode { get; set; }

        public virtual string StockName { get; set; }

        public virtual string OperatorName { get; set; }

        public virtual string BandPricipalName { get; set; }

        public virtual string TargetPricipalName { get; set; }
    }
}