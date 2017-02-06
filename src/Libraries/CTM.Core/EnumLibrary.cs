using System.ComponentModel;

namespace CTM.Core
{
    public static class EnumLibrary
    {
        /// <summary>
        /// 投资决策单投票类别
        /// </summary>
        public enum IDVoteType
        {
            /// <summary>
            /// 申请人
            /// </summary>
            [Description("申请人")]
            Applicant = 1,

            /// <summary>
            /// 决策委员会成员
            /// </summary>
            [Description("决策委员会成员")]
            Committee = 2,

            /// <summary>
            /// 普通交易员
            /// </summary>
            [Description("普通交易员")]
            Nomal = 3,

            /// <summary>
            /// 一票否决
            /// </summary>
            [Description("一票否决")]
            OneVoteVeto = 99,
        }

        /// <summary>
        /// 投资决策单投票标志
        /// </summary>
        public enum IDVoteFlag
        {
            /// <summary>
            /// 未投票
            /// </summary>
            [Description("未投票")]
            None = 0,

            /// <summary>
            /// 赞同
            /// </summary>
            [Description("赞同")]
            Approval = 1,

            /// <summary>
            /// 反对
            /// </summary>
            [Description("反对")]
            Oppose = 2,

            /// <summary>
            /// 弃权
            /// </summary>
            [Description("弃权")]
            Abstain = 3,
        }

        /// <summary>
        /// 投资决策单状态
        /// </summary>
        public enum IDApplicationStatus
        {
            /// <summary>
            /// 进行中
            /// </summary>
            [Description("进行中")]
            Proceed = 0,

            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Done = 99,
        }

        /// <summary>
        /// 投资决策交易申请类别
        /// </summary>
        public enum IDOperationApplyType
        {
            /// <summary>
            /// 不可操作
            /// </summary>
            [Description("不可操作")]
            None = 0,

            /// <summary>
            /// 买入
            /// </summary>
            [Description("买入")]
            Buy = 1,

            /// <summary>
            /// 卖出
            /// </summary>
            [Description("卖出")]
            Sell = 2,

            /// <summary>
            /// 买卖
            /// </summary>
            [Description("买卖")]
            Both = 99,
        }

        /// <summary>
        /// 决策操作记录投票状态
        /// </summary>
        public enum IDOperationVoteStatus
        {
            /// <summary>
            ///待决策
            /// </summary>
            [Description("待决策")]
            None = 1,

            /// <summary>
            /// 决策中
            /// </summary>
            [Description("决策中")]
            Proceed = 2,

            /// <summary>
            /// 通过
            /// </summary>
            [Description("通过")]
            Passed = 3,

            /// <summary>
            /// 不通过
            /// </summary>
            [Description("不通过")]
            Denied = 4,
        }

        public enum IDOperationExecuteStatus
        {
            /// <summary>
            /// 待确认
            /// </summary>
            [Description("待确认")]
            None = 1,

            /// <summary>
            /// 已执行
            /// </summary>
            [Description("已执行")]
            Executed = 2,

            /// <summary>
            /// 未执行
            /// </summary>
            [Description("未执行")]
            Unexecuted = 3
        }

        /// <summary>
        /// 决策操作记录准确度状态
        /// </summary>
        public enum IDOperationAccuracyStatus
        {
            /// <summary>
            /// 待评定
            /// </summary>
            [Description("待评定")]
            None = 1,

            /// <summary>
            /// 评定中
            /// </summary>
            [Description("评定中")]
            Proceed = 2,

            /// <summary>
            /// 准确
            /// </summary>
            [Description("准确")]
            Accurate = 3,

            /// <summary>
            /// 不准确
            /// </summary>
            [Description("不准确")]
            Inaccurate = 4,
        }

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
            [Description("未知")]
            Unknown,

            /// <summary>
            /// 中银国际（信用）
            /// </summary>
            [Description("中银国际（信用）")]
            BOCI_C,

            /// <summary>
            /// 中银国际（普通）
            /// </summary>
            [Description("中银国际（普通）")]
            BOCI_N,

            /// <summary>
            /// 财通证券（信用）
            /// </summary>
            [Description("财通证券（信用）")]
            CaiTong_C,

            /// <summary>
            /// 财通证券（普通）
            /// </summary>
            [Description("财通证券（普通）")]
            CaiTong_N,

            /// <summary>
            /// 中信证券（信用）
            /// </summary>
            [Description("中信证券（信用）")]
            CITIC_C,

            /// <summary>
            /// 中信证券（普通）
            /// </summary>
            [Description("中信证券（普通）")]
            CITIC_N,

            /// <summary>
            /// 安信证券（普通）
            /// </summary>
            [Description("安信证券（普通）")]
            ESSENCE_N,

            /// <summary>
            /// 安信证券（信用）
            /// </summary>
            [Description("安信证券（信用）")]
            ESSENCE_C,

            /// <summary>
            /// 方正证券（信用）
            /// </summary>
            [Description("方正证券（信用）")]
            Founder_C,

            /// <summary>
            /// 方正证券（普通）
            /// </summary>
            [Description("方正证券（普通）")]
            Founder_N,

            /// <summary>
            /// 银河证券（普通）
            /// </summary>
            [Description("银河证券（普通）")]
            Galaxy_N,

            /// <summary>
            /// 国泰君安（信用）
            /// </summary>
            [Description(" 国泰君安（信用）")]
            GuoTai_C,

            /// <summary>
            /// 国泰君安（普通）
            /// </summary>
            [Description("国泰君安（普通）")]
            GuoTai_N,

            /// <summary>
            /// 海通证券（普通）
            /// </summary>
            [Description("海通证券（普通）")]
            HaiTong_N,

            /// <summary>
            /// 华泰证券（普通）
            /// </summary>
            [Description("华泰证券（普通）")]
            HuaTai_N,

            /// <summary>
            /// 华泰证券（信用）
            /// </summary>
            [Description("华泰证券（信用）")]
            HuaTai_C,

            /// <summary>
            /// 申万证券（普通）
            /// </summary>
            [Description("申万证券（普通）")]
            ShenWan_N,

            /// <summary>
            /// 国金证券（普通）
            /// </summary>
            [Description("国金证券（普通）")]
            SinoLink_N,

            /// <summary>
            /// 招商证券（普通）
            /// </summary>
            [Description("招商证券（普通）")]
            ZhaoShang_N,

            /// <summary>
            /// 浙商证券（信用）
            /// </summary>
            [Description("浙商证券（信用）")]
            ZheShang_C,

            /// <summary>
            /// 浙商证券（普通）
            /// </summary>
            [Description("浙商证券（普通）")]
            ZheShang_N,
        }
    }
}