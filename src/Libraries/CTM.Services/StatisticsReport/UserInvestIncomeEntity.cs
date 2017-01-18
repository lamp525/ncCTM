using System;
using CTM.Core;
using CTM.Core.Util;

namespace CTM.Services.StatisticsReport
{
    public class UserInvestIncomeEntity
    {
        /// <summary>
        /// 投资人
        /// </summary>
        public string Investor { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TradeTime { get; set; }

        /// <summary>
        /// 交易类别
        /// </summary>
        public EnumLibrary.TradeType TradeType { get; set; }

        /// <summary>
        /// 交易类别名称
        /// </summary>
        public string TradeTypeName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 周一持仓市值
        /// </summary>
        public decimal MondayPositionValue { get; set; }

        /// <summary>
        /// 当前总资产
        /// </summary>
        public decimal CurrentAsset { get; set; }

        /// <summary>
        /// 累计收益额
        /// </summary>
        public decimal AccumulatedProfit { get; set; }

        /// <summary>
        /// 累计收益率
        /// </summary>
        public decimal AccumulatedIncomeRate { get; set; }

        /// <summary>
        /// 当前收益额
        /// </summary>
        public decimal CurrentProfit { get; set; }

        /// <summary>
        /// 当前收益率
        /// </summary>
        public decimal CurrentIncomeRate { get; set; }

        /// <summary>
        /// 持仓市值
        /// </summary>
        public decimal PositionValue { get; set; }

        /// <summary>
        /// 分配资金
        /// </summary>
        public decimal AllotFund { get; set; }

        /// <summary>
        /// 资金占用额度
        /// </summary>
        public decimal FundOccupyAmount { get; set; }

        /// <summary>
        /// 持仓仓位
        /// </summary>
        public decimal PositionRate { get; set; }

        /// <summary>
        /// 成交额
        /// </summary>
        public decimal DealAmount { get; set; }

        /// <summary>
        /// 计划融资融券额
        /// </summary>
        public decimal PlanMarginAmount { get; set; }

        /// <summary>
        /// 平均融资融券额
        /// </summary>
        public decimal AverageMarginAmount { get; set; }

        /// <summary>
        /// 实际融资融券额
        /// </summary>
        public decimal ActualMarginAmount { get; set; }

        /// <summary>
        /// 累计融资融券额
        /// </summary>
        public decimal AccumulatedMarginAmount { get; set; }

        /// <summary>
        /// 当前利息
        /// </summary>
        public decimal CurrentInterest { get; set; }

        /// <summary>
        /// 当前实际收益额（扣息后）
        /// </summary>
        public decimal CurrentActualProfit { get; set; }

        /// <summary>
        /// 累计利息
        /// </summary>
        public decimal AccumulatedInterest { get; set; }

        /// <summary>
        /// 累计实际收益额（扣息后）
        /// </summary>
        public decimal AccumulatedActualProfit { get; set; }

        /// <summary>
        /// 年度收益
        /// </summary>
        public decimal AnnualProfit { get; set; }

        /// <summary>
        /// 年度利息
        /// </summary>
        public decimal AnnualInterest { get; set; }

        /// <summary>
        /// 年度实际收益（扣息后）
        /// </summary>
        public decimal AnnualActualProfit { get; set; }

        /// <summary>
        /// 年度收益率
        /// </summary>
        public decimal AnnualIncomeRate { get; set; }
    }

    public static class UserInvestIncomeEntityExtensions
    {
        /// <summary>
        /// 计算持仓仓位（当日持仓市值 / 当日实际融资融券额）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculatePositionRate(this UserInvestIncomeEntity source)
        {
            return CommonHelper.CalculateRate(source.PositionValue, source.ActualMarginAmount);
        }

        /// <summary>
        /// 计算当日收益率（当日实际收益额 / 当日实际融资融券额）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculateCurrentIncomeRate(this UserInvestIncomeEntity source)
        {
            return CommonHelper.CalculateRate(source.CurrentActualProfit, source.ActualMarginAmount);
        }

        /// <summary>
        /// 计算累计收益率（累计实际收益额 / 平均融资融券额）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal CalculateAccumulatedIncomeRate(this UserInvestIncomeEntity source)
        {
            return CommonHelper.CalculateRate(source.AccumulatedActualProfit, source.AverageMarginAmount);
        }
    }
}