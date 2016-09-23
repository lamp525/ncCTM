using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.Models
{
    public class TradeRecordModel
    {
        public int RecordId { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TradeDate { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public string TradeTime { get; set; }

        /// <summary>
        /// 账号ID
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 证券代码
        /// </summary>
        public string StockCode { get; set; }

        /// <summary>
        /// 证券名称
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// 买卖标志
        /// True：买入
        /// False：卖出
        /// </summary>
        public bool DealFlag { get; set; }

        public string DealFlagName { get; set; }

        /// <summary>
        /// 交易编号
        /// </summary>
        public string DealNo { get; set; }

        /// <summary>
        /// 成交价格
        /// </summary>
        public decimal DealPrice { get; set; }

        /// <summary>
        /// 成交数量
        /// </summary>
        public int DealVolume { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        public decimal DealAmount { get; set; }

        /// <summary>
        /// 发生金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 印花税
        /// </summary>
        public decimal StampDuty { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 杂费（其它）
        /// </summary>
        public decimal Incidentals { get; set; }

        /// <summary>
        /// 股东代码
        /// </summary>
        public string StockHolderCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 数据导入用户代码
        /// </summary>
        public string ImportUser { get; set; }

        /// <summary>
        /// 导入时间
        /// </summary>
        public DateTime ImportTime { get; set; }

        /// <summary>
        /// 更新用户代码
        /// </summary>
        public string UpdateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 钩稽状态
        /// True：已钩稽
        /// False：未钩稽
        /// </summary>
        public bool AuditFlag { get; set; }

        /// <summary>
        /// 钩稽单号
        /// </summary>
        public int? AuditNo { get; set; }

        /// <summary>
        /// 钩稽时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 数据类别
        /// 1：当日成交
        /// 2：交割单
        /// </summary>
        public int DataType { get; set; }

        public string DataTypeName { get; set; }

        /// <summary>
        /// 交易类别
        /// 1：目标
        /// 2：波段
        /// 3：日内（短差）
        /// </summary>
        public int TradeType { get; set; }

        public string TradeTypeName { get; set; }

        /// <summary>
        /// 交易员代码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 实际受益人
        /// </summary>
        public string Beneficiary { get; set; }

        /// <summary>
        /// 拆单编号
        /// </summary>
        public string SplitNo { get; set; }

        public virtual string AccountName { get; set; }

        public virtual string BeneficiaryName { get; set; }

        public virtual string ImportUserName { get; set; }

        public virtual string OperatorName { get; set; }

        public virtual string UpdateUserName { get; set; }
    }
}