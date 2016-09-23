using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.Models
{
    public class AccountInfoModel
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public int IndustryId { get; set; }

        public string IndustryName { get; set; }

        public int TypeCode { get; set; }

        public string TypeName { get; set; }

        public int AttributeCode { get; set; }

        public string AttributeName { get; set; }

        public int PlanCode { get; set; }

        public string PlanName { get; set; }

        public int SecurityCompanyCode { get; set; }

        public string SecurityCompanyName { get; set; }

        /// <summary>
        /// 总资产
        /// </summary>
        public decimal TotalFund { get; set; }

        /// <summary>
        /// 投入资金
        /// </summary>
        public decimal InvestFund { get; set; }

        /// <summary>
        /// 融资额
        /// </summary>
        public decimal FinancingAmount { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 盈亏比率
        /// </summary>
        public decimal IncomeRate { get; set; }

        /// <summary>
        /// 印花税率
        /// </summary>
        public decimal StampDutyRate { get; set; }

        /// <summary>
        /// 佣金率
        /// </summary>
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 其他费率
        /// </summary>
        public decimal IncidentalsRate { get; set; }

        /// <summary>
        /// 核算
        /// True：需要核算
        /// Flase：不核算
        /// </summary>
        public bool NeedAccounting { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public string OperatorNames { get; set; }

        public string DisplayMember { get; set; }
    }
}