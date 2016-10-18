using System.ComponentModel;

namespace CTM.Core
{
    public static class EnumLibrary
    {
        /// <summary>
        /// 投资决策单状态
        /// </summary>
        public enum IDFormStatus
        {
            [Description("已提交")]
            Submited = 1,

            [Description("进行中")]
            Proceed = 2,

            [Description("申请通过")]
            Passed = 3,

            [Description("申请不通过")]
            Denied = 4,
        }

        public enum ReportType
        {
            [Description("日报表")]
            Day = 1,

            [Description("周报表")]
            Week = 2,

            [Description("月报表")]
            Month = 3,

            [Description("季报表")]
            Quarter = 4,

            [Description("年报表")]
            Year = 5
        }

        /// <summary>
        /// 数值单位
        /// </summary>
        public enum NumericUnit
        {
            /// <summary>
            /// 十
            /// </summary>
            Ten = 10,

            /// <summary>
            /// 百
            /// </summary>
            Hundred = 100,

            /// <summary>
            /// 千
            /// </summary>
            Thousand = 1000,

            /// <summary>
            /// 万
            /// </summary>
            TenThousand = 10000,

            /// <summary>
            /// 十万
            /// </summary>
            HundredThousand = 100000,

            /// <summary>
            /// 百万
            /// </summary>
            Million = 1000000,

            /// <summary>
            /// 千万
            /// </summary>
            TenMillion = 10000000,

            /// <summary>
            /// 亿
            /// </summary>
            HundredMillion = 100000000,

            /// <summary>
            /// 十亿
            /// </summary>
            Billion = 1000000000,
        }

        /// <summary>
        /// 投资核算部门
        /// </summary>
        public enum AccountingDepartment
        {
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")]
            All = 0,

            /// <summary>
            /// 短差部
            /// </summary>
            [Description("短差部")]
            Day = 2,

            /// <summary>
            /// 波段部
            /// </summary>
            [Description("波段部")]
            Band = 3,

            /// <summary>
            /// 目标部
            /// </summary>
            [Description("目标部")]
            Target = 4,

            /// <summary>
            /// 独立核算部
            /// </summary>
            [Description("独立核算部")]
            Independence = 5,
        }

        /// <summary>
        /// 页面状态
        /// </summary>
        public enum PageMode
        {
            /// <summary>
            /// 0：默认
            /// </summary>
            Default = 0,

            /// <summary>
            /// 1：当前用户
            /// </summary>
            CurrentUser = 1,

            /// <summary>
            /// 2：其他
            /// </summary>
            Other = 2,
        }

        /// <summary>
        /// 字典类别
        /// </summary>
        public enum DictionaryType
        {
            /// <summary>
            /// 证券公司
            /// </summary>
            SecurityCompay = 1,

            /// <summary>
            /// 账户属性
            /// </summary>
            AccountAttribute = 2,

            /// <summary>
            /// 职位信息
            /// </summary>
            PositionInfo = 3,

            /// <summary>
            /// 交易类别
            /// </summary>
            TradeType = 4,

            /// <summary>
            /// 账户规划
            /// </summary>
            AccountPlan = 5,

            /// <summary>
            /// 账户类型
            /// </summary>
            AccountType = 6
        }

        /// <summary>
        /// 交易类别
        /// </summary>
        public enum TradeType
        {
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")]
            All = 0,

            /// <summary>
            /// 目标
            /// </summary>
            [Description("目标")]
            Target = 1,

            /// <summary>
            /// 波段
            /// </summary>
            [Description("波段")]
            Band = 2,

            /// <summary>
            /// 日内
            /// </summary>
            [Description("日内")]
            Day = 3,
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")]
            All = 0,

            /// <summary>
            /// 当日委托
            /// </summary>
            [Description("当日委托")]
            Entrust = 1,

            /// <summary>
            /// 交割单
            /// </summary>
            [Description("交割单")]
            Delivery = 2,

            /// <summary>
            /// 当日成交
            /// </summary>
            [Description("当日成交")]
            Daily = 3,

            /// <summary>
            /// 虚拟
            /// </summary>
            [Description("虚拟交易")]
            Virtual = 77,

            /// <summary>
            /// 股票转移
            /// </summary>
            [Description("股票转移")]
            StockTransfer = 88,

            /// <summary>
            /// 旧系统
            /// </summary>
            [Description("旧系统")]
            History = 99
        }

        /// <summary>
        /// 账户属性
        /// </summary>
        public enum AccountAttribute
        {
            /// <summary>
            /// 普通
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 信用
            /// </summary>
            Credit = 2
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum OperateType
        {
            /// <summary>
            /// 添加
            /// </summary>
            Add = 1,

            /// <summary>
            /// 修改
            /// </summary>
            Edit = 2,

            /// <summary>
            /// 删除
            /// </summary>
            Delete = 3,
        }

        public enum SecurityAccount
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown,

            /// <summary>
            /// 中银国际（信用）
            /// </summary>
            BOCI_C,

            /// <summary>
            /// 中银国际（普通）
            /// </summary>
            BOCI_N,

            /// <summary>
            /// 财通证券（信用）
            /// </summary>
            CaiTong_C,

            /// <summary>
            /// 财通证券（普通）
            /// </summary>
            CaiTong_N,

            /// <summary>
            /// 中信证券（信用）
            /// </summary>
            CITIC_C,

            /// <summary>
            /// 中信证券（普通）
            /// </summary>
            CITIC_N,

            /// <summary>
            /// 方正证券（信用）
            /// </summary>
            Founder_C,

            /// <summary>
            /// 方正证券（普通）
            /// </summary>
            Founder_N,

            /// <summary>
            /// 银河证券（普通）
            /// </summary>
            Galaxy_N,

            /// <summary>
            /// 国泰君安（信用）
            /// </summary>
            GuoTai_C,

            /// <summary>
            /// 国泰君安（普通）
            /// </summary>
            GuoTai_N,

            /// <summary>
            /// 华泰证券（普通）
            /// </summary>
            HuaTai_N,

            /// <summary>
            /// 华泰证券（信用）
            /// </summary>
            HuaTai_C,

            /// <summary>
            /// 申万证券（普通）
            /// </summary>
            ShenWan_N,

            /// <summary>
            /// 国金证券（普通）
            /// </summary>
            SinoLink_N,

            /// <summary>
            /// 招商证券（普通）
            /// </summary>
            ZhaoShang_N,

            /// <summary>
            /// 浙商证券（信用）
            /// </summary>
            ZheShang_C,

            /// <summary>
            /// 浙商证券（普通）
            /// </summary>
            ZheShang_N,
        }
    }
}