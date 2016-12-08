using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionOperation : BaseEntity
    {
        public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string OperateUser { get; set; }

        public DateTime OperateDate { get; set; }

        /// <summary>
        /// 初始操作标志
        /// true：初始操作
        /// false：对应操作
        /// </summary>
        public bool InitialFlag { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        /// <summary>
        /// 买卖标志
        /// true：买入
        /// false：卖出
        /// </summary>
        public bool DealFlag { get; set; }

        public decimal DealPrice { get; set; }

        public decimal PriceBound { get; set; }

        public decimal DealVolume { get; set; }

        public decimal DealAmount { get; set; }

        public int ReasonCategoryId { get; set; }

        public string ReasonContent { get; set; }

        /// <summary>
        /// 投票状态
        /// 1：未投票
        /// 2：投票中
        /// 3：通过
        /// 4：不通过
        /// </summary>
        public int VoteStatus { get; set; }

        public int VotePoint { get; set; }

        /// <summary>
        /// 执行标志
        /// true：已执行
        /// false：未执行
        /// </summary>
        public bool ExecuteFlag { get; set; }

        /// <summary>
        /// 交易记录关联标志
        /// true：已关联
        /// false：未关联
        /// </summary>
        public bool TradeRecordRelateFlag { get; set; }

        /// <summary>
        /// 准确性判定状态
        /// 1：未投票
        /// 2：投票中
        /// 3：准确
        /// 4：不准确
        /// </summary>
        public int AccuracyStatus { get; set; }

        public int AccuracyPoint { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}