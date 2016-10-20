using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionVote : BaseEntity
    {
        public string FormSerialNo { get; set; }

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

        public int Weight { get; set; }

        /// <summary>
        /// 投票标志
        /// 0：未投票
        /// 1：同意
        /// 2：反对
        /// 3：弃权
        /// </summary>
        public int Flag { get; set; }

        public string Reason { get; set; }

        public DateTime? VoteTime { get; set; }
    }
}