using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Core.Domain.TradeRecord
{
    public class DeliveryRecord : BaseEntity
    {
        /// <summary>
        /// 数据类别
        /// </summary>
        public int DataType { get; set; }

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
        /// 备注说明
        /// </summary>
        public string Remarks { get; set; }

        public virtual string AccountName { get; set; }

        public virtual string ImportUserName { get; set; }

        public virtual string UpdateUserName { get; set; }
    }
}