using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionStockPoolLog : BaseEntity
    {
        public string StockCode { get; set; }

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
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 目标负责人代码
        /// </summary>
        public string Principal { get; set; }
    }
}