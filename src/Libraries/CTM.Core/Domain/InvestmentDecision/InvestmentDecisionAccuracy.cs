using System;

namespace CTM.Core.Domain.InvestmentDecision
{
    public class InvestmentDecisionAccuracy : BaseEntity
    {
        public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        public string UserCode { get; set; }

        public decimal Weight { get; set; }

        /// <summary>
        /// 判定标志
        /// 0：未判定
        /// 1：准确
        /// 2：不准确
        /// </summary>
        public int Flag { get; set; }

        public string Reason { get; set; }

        public DateTime? JudgeTime { get; set; }
    }
}