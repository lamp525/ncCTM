namespace CTM.Services.Account
{
    public class AccountEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Owner { get; set; }

        public string OwnerName { get; set; }

        /// <summary>
        /// 开户所在地
        /// </summary>
        public string Location { get; set; }

        public int IndustryId { get; set; }

        public string IndustryName { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        public int TypeCode { get; set; }

        public string TypeName { get; set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        public int AttributeCode { get; set; }

        public string AttributeName { get; set; }

        /// <summary>
        /// 规划编码
        /// </summary>
        public int PlanCode { get; set; }

        public string PlanName { get; set; }

        /// <summary>
        /// 开户券商编码
        /// </summary>
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
        /// 禁用标志
        /// True：禁用
        /// False：启用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remarks { get; set; }

        public string OperatorNames { get; set; }

        public string DisplayMember { get; set; }
    }
}