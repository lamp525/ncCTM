using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Core.Domain.TradeRecord
{
    public class DailyRecord : BaseRecord
    {
        /// <summary>
        /// 数据类别
        /// 0：委托记录
        /// 1：当日成交
        /// 2：交割单
        /// 88：股票转移
        /// 99：旧系统数据
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 交易类别
        /// 1：目标
        /// 2：波段
        /// 3：日内（短差）
        /// </summary>
        public int TradeType { get; set; }

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