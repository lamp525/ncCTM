using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionOperationVote : BaseEntity
    {
        public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        public string UserCode { get; set; }

        /// <summary>
        /// 投票类别
        /// 1：申请人
        /// 2：决策委员会
        /// 3：普通交易员
        /// 99：一票否决
        /// </summary>
        public int Type { get; set; }

        public int AuthorityLevel { get; set; }

        public decimal Weight { get; set; }

        /// <summary>
        /// 投票标志
        /// 0：未投票
        /// 1：同意
        /// 2：反对
        /// 3：弃权
        /// </summary>
        public int Flag { get; set; }

        public int ReasonCategoryId { get; set; }

        public string ReasonContent { get; set; }

        public DateTime? VoteTime { get; set; }
    }
}