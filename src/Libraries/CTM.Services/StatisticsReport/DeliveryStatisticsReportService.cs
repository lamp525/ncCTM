using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.TradeRecord;

namespace CTM.Services.StatisticsReport
{
    public partial class DeliveryStatisticsReportService : IDeliveryStatisticsReportService
    {
        /// <summary>
        /// 帐户投资收益计算
        /// </summary>
        /// <param name="deliveryRecords"></param>
        /// <param name="queryDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public virtual IList<AccountInvestIncomeEntity> CalculateAccountInvestIncome(IList<DeliveryRecord> deliveryRecords, IList<DateTime> queryDates, IList<TKLineToday> stockClosePrices, AccountEntity accountInfo)
        {
            var result = new List<AccountInvestIncomeEntity>();

            if (deliveryRecords == null || !deliveryRecords.Any() || stockClosePrices == null) return result;

            //帐户分配资金
            decimal allotFund = accountInfo.InvestFund;

            #region 统计日前一天

            var lastDate = queryDates.First();
            var lastRecords = deliveryRecords.Where(x => x.TradeDate <= lastDate).ToList();
            var lastDateClosePrices = stockClosePrices.Where(x => x.TradeDate == lastDate).ToList();

            //前一天的累计收益额
            decimal previousAccumulatedProfit = lastRecords.GetInvestStatisticsCommonInfo(lastDateClosePrices).AccumulatedProfit;

            #endregion 统计日前一天

            //所有统计日收益信息计算
            var tradeDates = new List<DateTime>();
            tradeDates.AddRange(queryDates);
            tradeDates.RemoveAt(0);
            foreach (var date in tradeDates)
            {
                #region 当前统计日

                var currentRecords = deliveryRecords.Where(x => x.TradeDate < date).ToList();
                var currentDateClosePrices = stockClosePrices.Where(x => x.TradeDate == date).ToList();

                //当日投资收益信息
                var currentInvestIncomeInfo = currentRecords.GetInvestStatisticsCommonInfo(currentDateClosePrices);

                var incomeModel = new AccountInvestIncomeEntity()
                {
                    //账户名称
                    AccountName = accountInfo.AccountName,
                    //账户属性
                    AccountAttributeName = accountInfo.AttributeName,
                    //账户类别
                    AccountTypeName = accountInfo.TypeName,
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
                    SecurityCompanyName = accountInfo.SecurityCompanyName,
                    //交易日
                    TradeTime = date,
                };

                //持仓仓位
                incomeModel.PositionRate = CommonHelper.CalculateRate(incomeModel.PositionValue, incomeModel.CurrentAsset);

                //当日收益率
                incomeModel.CurrentIncomeRate = CommonHelper.CalculateRate(incomeModel.CurrentProfit, incomeModel.AllotFund);

                //累计收益率
                incomeModel.AccumulatedIncomeRate = CommonHelper.CalculateRate(incomeModel.AccumulatedProfit, incomeModel.AllotFund);

                #endregion 当前统计日

                //前一日累计收益额设为当日累计收益额
                previousAccumulatedProfit = incomeModel.AccumulatedProfit;

                result.Add(incomeModel);
            }
            return result;
        }
    }
}