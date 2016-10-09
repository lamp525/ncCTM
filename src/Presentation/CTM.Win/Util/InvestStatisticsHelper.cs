using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;
using CTM.Services.Account;
using CTM.Services.StatisticsReport;
using CTM.Win.Models;

namespace CTM.Win.Util
{
    public class InvestStatisticsHelper
    {
        /// <summary>
        /// 每日收益率计算（目标）
        /// </summary>
        /// <param name="dailyProfit"></param>
        /// <param name="allotFund"></param>
        /// <returns></returns>
        public static decimal CalculateDailyIncomeRateTarget(decimal dailyProfit, decimal allotFund)
        {
            decimal result = 0;

            result = allotFund == 0 ? 0 : dailyProfit / allotFund;

            return result;
        }

        /// <summary>
        /// 累计收益率计算（目标）
        /// </summary>
        /// <param name="dailyProfit"></param>
        /// <param name="allotFund"></param>
        /// <returns></returns>
        public static decimal CalculateAccumulatedIncomeRateTarget(decimal accumulatedProfit, decimal allotFund)
        {
            decimal result = 0;

            result = allotFund == 0 ? 0 : accumulatedProfit / allotFund;

            return result;
        }

        /// <summary>
        /// 持仓率计算
        /// </summary>
        /// <param name="positionValue"></param>
        /// <param name="currentAsset"></param>
        /// <returns></returns>
        public static decimal CalculatePositionRate(decimal positionValue, decimal currentAsset)
        {
            decimal result = 0;

            result = currentAsset == 0 ? 0 : positionValue / currentAsset;

            return result;
        }

        /// <summary>
        /// 成交额计算
        /// </summary>
        /// <param name="tradeRecordDaily"></param>
        /// <returns></returns>
        public static decimal CalculateDealAmount(IList<DailyRecord> tradeRecordDaily)
        {
            decimal dealAmount = 0;

            if (tradeRecordDaily != null && tradeRecordDaily.Any())
            {
                var sellAmount = tradeRecordDaily.Where(x => x.DealFlag == false).Sum(x => x.DealAmount);
                var buyAmount = tradeRecordDaily.Where(x => x.DealFlag == true).Sum(x => x.DealAmount);

                dealAmount = sellAmount > buyAmount ? sellAmount : buyAmount;
            }

            return dealAmount;
        }

        /// <summary>
        /// 取得交易记录的投资统计共通信息
        /// </summary>
        /// <param name="tradeRecords"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        public static InvestStatisticsCommonModel GetInvestStatisticsCommonInfo(IList<DailyRecord> tradeRecords, DataTable stockClosePrices)
        {
            var result = new InvestStatisticsCommonModel();

            if (tradeRecords == null || !tradeRecords.Any() || stockClosePrices == null) return result;

            //持仓市值
            decimal positionValue = 0;
            //持仓收益
            decimal positionProfit = 0;
            //累计发生金额
            decimal accumulatedActualAmount = 0;
            //累计收益额
            decimal accumulatedProfit = 0;

            var recordsByStock = tradeRecords.GroupBy(x => x.StockCode);
            foreach (var stockGroup in recordsByStock)
            {
                //各只股票发生金额
                decimal actualAmountPerStock = stockGroup.Sum(x => x.ActualAmount);
                accumulatedActualAmount += actualAmountPerStock;

                //各只股票的持股数
                decimal holdingVolume = stockGroup.Sum(x => x.DealVolume);
                decimal closePrice = holdingVolume == 0 ? 0 : stockClosePrices.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockGroup.Key).Select(x => x.Field<decimal>("Close")).SingleOrDefault();

                //持仓市值
                positionValue += Math.Abs(holdingVolume) * closePrice;

                //持仓收益
                positionProfit += holdingVolume * closePrice;
            }
            accumulatedProfit = accumulatedActualAmount + positionProfit;

            result.PositionValue = positionValue;
            result.AccumulatedActualAmount = accumulatedActualAmount;
            result.AccumulatedProfit = accumulatedProfit;

            return result;
        }

        /// <summary>
        /// 帐户投资收益计算
        /// </summary>
        /// <param name="tradeRecords"></param>
        /// <param name="queryDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <param name="accountDetailInfo"></param>
        /// <returns></returns>
        public static IList<AccountInvestIncomeEntity> CalculateAccountInvestIncome(IList<DailyRecord> tradeRecords, IList<DateTime> queryDates, DataSet stockClosePrices, AccountEntity accountDetailInfo)
        {
            var result = new List<AccountInvestIncomeEntity>();

            if (tradeRecords == null || !tradeRecords.Any() || stockClosePrices == null) return result;

            //帐户分配资金
            decimal allotFund = accountDetailInfo.InvestFund;

            #region 统计日前一天

            var lastDate = queryDates.First();
            var lastRecords = tradeRecords.Where(x => x.TradeDate <= lastDate).ToList();
            var lastDateClosePrices = stockClosePrices.Tables[lastDate.ToString()];

            //前一天的累计收益额
            decimal previousAccumulatedProfit = GetInvestStatisticsCommonInfo(lastRecords, lastDateClosePrices).AccumulatedProfit;

            #endregion 统计日前一天

            //所有统计日收益信息计算
            var tradeDates = new List<DateTime>();
            tradeDates.AddRange(queryDates);
            tradeDates.RemoveAt(0);
            foreach (var date in tradeDates)
            {
                #region 当前统计日

                var currentRecords = tradeRecords.Where(x => x.TradeDate < date).ToList();
                var currentDateClosePrices = stockClosePrices.Tables[date.ToString()];

                //当日投资收益信息
                var currentInvestIncomeInfo = GetInvestStatisticsCommonInfo(currentRecords, currentDateClosePrices);

                //当日成交额
                var dealAmount = CalculateDealAmount(tradeRecords.Where(x => x.TradeDate == date).ToList());

                var incomeModel = new AccountInvestIncomeEntity()
                {
                    //账户名称
                    AccountName = accountDetailInfo.AccountName,
                    //账户属性
                    AccountAttributeName = accountDetailInfo.AttributeName,
                    //账户类别
                    AccountTypeName = accountDetailInfo.TypeName,
                    //累计收益额
                    AccumulatedProfit = currentInvestIncomeInfo.AccumulatedProfit,
                    //分配资金
                    AllotFund = allotFund,
                    //当日资产
                    CurrentAsset = allotFund + currentInvestIncomeInfo.AccumulatedProfit,
                    //当日收益
                    CurrentProfit = currentInvestIncomeInfo.AccumulatedProfit - previousAccumulatedProfit,
                    //资金占用额度
                    FundOccupyAmount = allotFund * (decimal)1.2,
                    //周一
                    MondayPositionValue = date.DayOfWeek == DayOfWeek.Monday ? currentInvestIncomeInfo.PositionValue : 0,
                    //持仓市值
                    PositionValue = currentInvestIncomeInfo.PositionValue,
                    //开户券商
                    SecurityCompanyName = accountDetailInfo.SecurityCompanyName,
                    //交易日
                    TradeTime = date,
                };

                //持仓仓位
                incomeModel.PositionRate = CalculatePositionRate(incomeModel.PositionValue, incomeModel.CurrentAsset);

                //当日收益率
                incomeModel.CurrentIncomeRate = CalculateDailyIncomeRateTarget(incomeModel.CurrentProfit, incomeModel.AllotFund);

                //累计收益率
                incomeModel.AccumulatedIncomeRate = CalculateAccumulatedIncomeRateTarget(incomeModel.AccumulatedProfit, incomeModel.AllotFund);

                #endregion 当前统计日

                //前一日累计收益额设为当日累计收益额
                previousAccumulatedProfit = incomeModel.AccumulatedProfit;

                result.Add(incomeModel);
            }
            return result;
        }
    }
}