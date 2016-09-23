using System.Collections.Generic;

namespace CTM.Core.Domain.Account
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public class AccountInfo : BaseEntity
    {
        private ICollection<AccountOperator> _operators;

        /// <summary>
        /// 产业分类ID
        /// </summary>
        public int IndustryId { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        public int TypeCode { get; set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        public int AttributeCode { get; set; }

        /// <summary>
        /// 规划编码
        /// </summary>
        public int PlanCode { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
       //public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开户券商编码
        /// </summary>
        public int SecurityCompanyCode { get; set; }

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
        /// 禁用标志
        /// True：禁用
        /// False：启用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remarks { get; set; }

        public string AttributeName { get; set; }

        public string TypeName { get; set; }

        public string PlanName { get; set; }

        public string SecurityCompanyName { get; set; }

        public virtual string OperatorNames { get; set; }

        public virtual string IndustryName { get; set; }

        public virtual ICollection<AccountOperator> AccountOperators
        {
            get { return _operators ?? (_operators = new List<AccountOperator>()); }
            protected set { _operators = value; }
        }
    }
}