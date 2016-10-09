using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TKLine;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.MarginTrading;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;

namespace CTM.Services.StatisticsReport
{
    public partial class DailyStatisticsReportService : IDailyStatisticsReportService
    {
        #region Fields

        private readonly ITKLineService _tKLineService;
        private readonly IDailyRecordService _dailyRecordService;
        private readonly IMarginTradingService _marginService;

        #endregion Fields

        #region Constructorss

        public DailyStatisticsReportService(
            IDailyRecordService dailyRecordService,
            ITKLineService tKLineService,
            IMarginTradingService marginService
            )
        {
            this._dailyRecordService = dailyRecordService;
            this._tKLineService = tKLineService;
            this._marginService = marginService;
        }

        #endregion Constructorss

        #region Utilities

        #region Day【短差】

        /// <summary>
        /// 取得短差每日融资融券信息
        /// </summary>
        /// <param name="investorCodes"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private IList<DailyMarginTradingInfoEntity> GetDayDailyMarginTradingInfo(IList<string> investorCodes, DateTime endDate)
        {
            var result = new List<DailyMarginTradingInfoEntity>();

            if (investorCodes?.Count == 0) return result;

            var marginInfos = this._marginService.GetUserInMarginTradingInfo(investorCodes.ToArray(), (int)EnumLibrary.TradeType.Day, null, endDate);

            if (marginInfos.Any())
            {
                var marginsByDay = marginInfos.GroupBy(x => x.MarginDate);

                var unit = (int)EnumLibrary.NumericUnit.TenThousand;

                foreach (var dayGroup in marginsByDay)
                {
                    var loanAmount = dayGroup.Where(x => x.IsFinancing == false).Sum(x => x.Amount) * unit;
                    var financingAmount = dayGroup.Where(x => x.IsFinancing == true).Sum(x => x.Amount) * unit;
                    var dailyEntity = new DailyMarginTradingInfoEntity
                    {
                        FinancingAmount = financingAmount,
                        isIn = true,
                        LoanAmount = loanAmount,
                        MarginDate = dayGroup.Key,
                        TotalAmount = loanAmount + financingAmount,
                    };

                    result.Add(dailyEntity);
                }
            }

            return result;
        }

        /// <summary>
        /// 用户每日短差融资融券及利息计算
        /// </summary>
        /// <param name="dailyCommonInfos"></param>
        /// <param name="tradeDates"></param>
        /// <param name="existedDayMarginInfos"></param>
        /// <returns></returns>
        private IList<UserMarginTradingInterestEntity> CalculateUserDayMarginTradingInterest(IList<InvestStatisticsCommonEntity> dailyCommonInfos, IList<DateTime> tradeDates, IList<DailyMarginTradingInfoEntity> existedDayMarginInfos)
        {
            var result = new List<UserMarginTradingInterestEntity>();

            if (tradeDates?.Count == 0) return result;

            decimal accumulatedMarginAmount = 0;
            decimal lastAverageMarginAmount = 0;
            decimal lastDayMarginAmount = 0;
            decimal accumulatedInterest = 0;
            var tradeDays = 0;
            foreach (var tradeDate in tradeDates)
            {
                var currentCommonInfo = dailyCommonInfos.SingleOrDefault(x => x.TradeDate == tradeDate) ?? new InvestStatisticsCommonEntity();
                var currentDayExistedMarginInfo = existedDayMarginInfos.Where(x => x.MarginDate == tradeDate).SingleOrDefault() ?? new DailyMarginTradingInfoEntity();

                var dailyModel = new UserMarginTradingInterestEntity();
                //融资融券日期
                dailyModel.TradeDate = tradeDate;
                //当日计划融资融券额
                dailyModel.PlanFinancingAmount = currentDayExistedMarginInfo.FinancingAmount;
                dailyModel.PlanLoanAmount = currentDayExistedMarginInfo.LoanAmount;
                dailyModel.PlanMarginAmount = currentDayExistedMarginInfo.TotalAmount;
                //当日成交额
                var dealAmount = currentCommonInfo.DealAmount;
                //当日实际融资融券额
                dailyModel.ActualMarginAmount = dealAmount > dailyModel.PlanMarginAmount ? dealAmount : dailyModel.PlanMarginAmount;
                dailyModel.ActualLoanAmount = dailyModel.PlanLoanAmount;
                dailyModel.ActualFinancingAmount = dailyModel.ActualMarginAmount - dailyModel.ActualLoanAmount;
                //前一日融资融券额
                dailyModel.LastDayMarginAmount = lastDayMarginAmount;
                lastDayMarginAmount = dailyModel.ActualMarginAmount;
                //累计融资融券额
                accumulatedMarginAmount += dailyModel.ActualMarginAmount;
                dailyModel.AccumulatedMarginAmount = accumulatedMarginAmount;
                //实际交易天数
                if (dealAmount > 0 || dailyModel.ActualMarginAmount > 0) tradeDays++;
                //平均融资融券额
                dailyModel.AverageMarginAmount = CommonHelper.CalculateRate(accumulatedMarginAmount, tradeDays);
                //前一日平均融资融券额
                lastAverageMarginAmount = dailyModel.AverageMarginAmount;
                //当日累计净收益
                var accumulatedNetProfit = currentCommonInfo.AccumulatedActualAmount;
                //当日利息
                dailyModel.CurrentInterest = ((dailyModel.ActualFinancingAmount - accumulatedNetProfit > 0 ? dailyModel.ActualFinancingAmount - accumulatedNetProfit : 0) + dailyModel.ActualLoanAmount) * AppConfigHelper.MarginTradingDPR;
                //累计利息
                accumulatedInterest += dailyModel.CurrentInterest;
                dailyModel.AccumulatedInterest = accumulatedInterest;

                result.Add(dailyModel);
            }

            return result;
        }

        /// <summary>
        /// 取得短差投资收益信息
        /// </summary>
        /// <param name="investorInfo"></param>
        /// <param name="statisticalInvestorCodes"></param>
        /// <param name="dayRecords"></param>
        /// <param name="statisticalDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        private IList<UserInvestIncomeEntity> GetUserDayInvestIncomeInfo(UserInfo investorInfo, IList<string> statisticalInvestorCodes, IList<DailyRecord> dayRecords, IList<DateTime> statisticalDates, IList<TKLineToday> stockClosePrices)
        {
            var dayIncomInfos = new List<UserInvestIncomeEntity>();

            if (dayRecords?.Count == 0 || statisticalDates?.Count == 0)
                return dayIncomInfos;

            //取得用户输入的短差融资融券信息
            var existedDayMarginInfos = this.GetDayDailyMarginTradingInfo(statisticalInvestorCodes, statisticalDates.Max());

            var firstMarginDate = existedDayMarginInfos?.Count > 0 ? existedDayMarginInfos.First().MarginDate : DateTime.MaxValue;

            var firstTradeDate = dayRecords.First().TradeDate;
            var startDate = firstTradeDate < firstMarginDate ? firstTradeDate : firstMarginDate;
            var endDate = statisticalDates.Last();

            var tradeDates = CommonHelper.GetAllWorkDays(startDate, endDate);

            //投资统计共通信息
            var dailyCommonInfos = dayRecords.GetDailyInvestStatisticsCommonInfo(tradeDates, stockClosePrices);

            //短差融资融券和利息信息
            var dayMarginInterestInfos = this.CalculateUserDayMarginTradingInterest(dailyCommonInfos, tradeDates, existedDayMarginInfos);

            //前一交易日的累计收益额
            var previousAccumulatedProfit = (dailyCommonInfos.SingleOrDefault(x => x.TradeDate == statisticalDates.First()) ?? new InvestStatisticsCommonEntity()).AccumulatedProfit;

            var allotFund = investorInfo.AllotFund;
            foreach (var date in statisticalDates)
            {
                if (date == statisticalDates.First()) continue;

                //当前日期的统计共通信息
                var currentStatisticsCommonInfo = dailyCommonInfos.Where(x => x.TradeDate <= date).LastOrDefault() ?? new InvestStatisticsCommonEntity();
                //当前日期的融资融券利息信息
                var currentMarginInterestInfo = dayMarginInterestInfos.Where(x => x.TradeDate <= date).LastOrDefault() ?? new UserMarginTradingInterestEntity();

                //当日收益额
                var currentProfit = currentStatisticsCommonInfo.AccumulatedProfit - previousAccumulatedProfit;
                //累计实际收益额
                var accumulatedActualProfit = currentStatisticsCommonInfo.AccumulatedProfit - currentMarginInterestInfo.AccumulatedInterest;

                var incomeModel = new UserInvestIncomeEntity()
                {
                    //投资人员
                    Investor = investorInfo.Name,
                    //交易日
                    TradeTime = date,
                    //交易类别
                    TradeType = EnumLibrary.TradeType.Day,
                    //成交额
                    DealAmount = currentStatisticsCommonInfo.DealAmount,
                    //分配资金
                    AllotFund = allotFund,
                    //资金占用额度
                    FundOccupyAmount = allotFund * (decimal)1.2,
                    //计划融资融券金额
                    PlanMarginAmount = currentMarginInterestInfo.PlanMarginAmount,
                    //平均融资融券金额
                    AverageMarginAmount = currentMarginInterestInfo.AverageMarginAmount,
                    //实际融资融券金额
                    ActualMarginAmount = currentMarginInterestInfo.ActualMarginAmount,
                    //累计融资融券金额
                    AccumulatedMarginAmount = currentMarginInterestInfo.AccumulatedMarginAmount,
                    //持仓市值
                    PositionValue = currentStatisticsCommonInfo.PositionValue,
                    //周一
                    MondayPositionValue = date.DayOfWeek == DayOfWeek.Monday ? currentStatisticsCommonInfo.PositionValue : 0,
                    //当日收益
                    CurrentProfit = currentProfit,
                    //累计收益额
                    AccumulatedProfit = currentStatisticsCommonInfo.AccumulatedProfit,
                    //当日利息
                    CurrentInterest = currentMarginInterestInfo.CurrentInterest,
                    //累计实际收益额
                    AccumulatedActualProfit = accumulatedActualProfit,
                    //累计利息
                    AccumulatedInterest = currentMarginInterestInfo.AccumulatedInterest,
                    //当日实际收益
                    CurrentActualProfit = currentProfit - currentMarginInterestInfo.CurrentInterest,
                    //当日资产（当日融资融券额 + 累计实际收益额）
                    CurrentAsset = currentMarginInterestInfo.ActualMarginAmount + accumulatedActualProfit,
                };

                //持仓仓位（当日持仓市值 / 当日实际融资融券额）
                incomeModel.PositionRate = CommonHelper.CalculateRate(incomeModel.PositionValue, incomeModel.ActualMarginAmount);
                //当日收益率（当日实际收益额 / 当日实际融资融券额）
                incomeModel.CurrentIncomeRate = CommonHelper.CalculateRate(incomeModel.CurrentActualProfit, incomeModel.ActualMarginAmount);
                //累计收益率（累计实际收益额 / 平均融资融券额）
                incomeModel.AccumulatedIncomeRate = CommonHelper.CalculateRate(incomeModel.AccumulatedActualProfit, incomeModel.AverageMarginAmount);

                //前一日累计收益额设为当日累计收益额
                previousAccumulatedProfit = incomeModel.AccumulatedProfit;

                dayIncomInfos.Add(incomeModel);
            }

            return dayIncomInfos;
        }

        #endregion Day【短差】

        #region Band【波段】

        /// <summary>
        /// 取得波段每日融资融券信息
        /// </summary>
        /// <param name="investorCodes"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private IList<DailyMarginTradingInfoEntity> GetBandDailyMarginTradingInfo(IList<string> investorCodes, DateTime endDate)
        {
            var result = new List<DailyMarginTradingInfoEntity>();

            if (investorCodes?.Count == 0) return result;

            var unit = (int)EnumLibrary.NumericUnit.TenThousand;

            var inMarginInfos = this._marginService.GetUserInMarginTradingInfo(investorCodes.ToArray(), (int)EnumLibrary.TradeType.Band, null, endDate);

            if (inMarginInfos.Any())
            {
                var marginDates = inMarginInfos.Select(x => x.MarginDate).Distinct();

                foreach (var date in marginDates)
                {
                    //融资融券信息
                    var currentBorrowInfo = inMarginInfos.Where(x => x.IsRepay == false && x.MarginDate < date.AddDays(1));
                    //还资还券信息
                    var currentRepayInfo = inMarginInfos.Where(x => x.IsRepay == true && x.MarginDate < date);

                    var loanAmount = (currentBorrowInfo.Where(x => x.IsFinancing == false).Sum(x => x.Amount) - currentRepayInfo.Where(x => x.IsFinancing == false).Sum(x => x.Amount)) * unit;
                    var financingAmount = (currentBorrowInfo.Where(x => x.IsFinancing == true).Sum(x => x.Amount) - currentRepayInfo.Where(x => x.IsFinancing == true).Sum(x => x.Amount)) * unit;
                    if (financingAmount < 0)
                        financingAmount = 0;
                    var dailyInEntity = new DailyMarginTradingInfoEntity
                    {
                        FinancingAmount = financingAmount,
                        isIn = true,
                        LoanAmount = loanAmount,
                        MarginDate = date,
                        TotalAmount = loanAmount + financingAmount,
                    };

                    result.Add(dailyInEntity);
                }
            }

            var outMarginInfos = this._marginService.GetUserOutMarginTradingInfo(investorCodes.ToArray(), (int)EnumLibrary.TradeType.Day, null, endDate);

            if (outMarginInfos.Any())
            {
                var marginsByDay = outMarginInfos.GroupBy(x => x.MarginDate);

                foreach (var dayGroup in marginsByDay)
                {
                    var loanAmount = dayGroup.Where(x => x.IsFinancing == false).Sum(x => x.Amount) * unit;
                    var financingAmount = 0;
                    var dailyOutEntity = new DailyMarginTradingInfoEntity
                    {
                        FinancingAmount = financingAmount,
                        isIn = false,
                        LoanAmount = loanAmount,
                        MarginDate = dayGroup.Key,
                        TotalAmount = loanAmount + financingAmount,
                    };

                    result.Add(dailyOutEntity);
                }
            }

            return result;
        }

        /// <summary>
        /// 用户每日波段融资融券及利息计算
        /// </summary>
        /// <param name="dailyCommonInfos"></param>
        /// <param name="tradeDates"></param>
        /// <param name="existedBandMarginInfos"></param>
        /// <returns></returns>
        private IList<UserMarginTradingInterestEntity> CalculateUserBandMarginTradingInterest(IList<InvestStatisticsCommonEntity> dailyCommonInfos, IList<DateTime> tradeDates, IList<DailyMarginTradingInfoEntity> existedBandMarginInfos)
        {
            var result = new List<UserMarginTradingInterestEntity>();

            if (tradeDates?.Count == 0) return result;

            decimal accumulatedMarginAmount = 0;
            decimal lastAverageMarginAmount = 0;
            decimal lastDayMarginAmount = 0;
            decimal lastDayInterest = 0;
            decimal accumulatedInterest = 0;
            var tradeDays = 0;
            foreach (var tradeDate in tradeDates)
            {
                var currentCommonInfo = dailyCommonInfos.SingleOrDefault(x => x.TradeDate == tradeDate) ?? new InvestStatisticsCommonEntity();
                var currentInMarginInfo = existedBandMarginInfos.Where(x => x.isIn == true && x.MarginDate == tradeDate).SingleOrDefault() ?? new DailyMarginTradingInfoEntity();
                var currentOutMarginInfo = existedBandMarginInfos.Where(x => x.isIn == false && x.MarginDate == tradeDate).SingleOrDefault() ?? new DailyMarginTradingInfoEntity();

                var dailyModel = new UserMarginTradingInterestEntity();
                //融资融券日期
                dailyModel.TradeDate = tradeDate;
                //当日计划融资融券额
                dailyModel.PlanFinancingAmount = currentInMarginInfo.FinancingAmount;
                dailyModel.PlanLoanAmount = currentInMarginInfo.LoanAmount;
                dailyModel.PlanMarginAmount = currentInMarginInfo.TotalAmount;
                //当日成交额
                var dealAmount = currentCommonInfo.DealAmount;
                //当日持仓额
                var positionValue = currentCommonInfo.PositionValue;
                //当日实际融资融券额
                dailyModel.ActualMarginAmount = positionValue > dailyModel.PlanMarginAmount ? positionValue : dailyModel.PlanMarginAmount;
                if (dealAmount > dailyModel.ActualMarginAmount)
                    dailyModel.ActualMarginAmount = dealAmount;
                dailyModel.ActualLoanAmount = dailyModel.PlanLoanAmount;
                dailyModel.ActualFinancingAmount = dailyModel.ActualMarginAmount - dailyModel.ActualLoanAmount;
                //前一日融资融券额
                dailyModel.LastDayMarginAmount = lastDayMarginAmount;
                lastDayMarginAmount = dailyModel.ActualMarginAmount;
                //累计融资融券额
                accumulatedMarginAmount += dailyModel.ActualMarginAmount;
                dailyModel.AccumulatedMarginAmount = accumulatedMarginAmount;
                //实际交易天数
                if (dealAmount > 0 || dailyModel.ActualMarginAmount > 0) tradeDays++;
                //平均融资融券额
                dailyModel.AverageMarginAmount = CommonHelper.CalculateRate(accumulatedMarginAmount, tradeDays);
                //前一日平均融资融券额
                lastAverageMarginAmount = dailyModel.AverageMarginAmount;
                //当日累计净收益
                var accumulatedNetProfit = currentCommonInfo.AccumulatedActualAmount;
                //当日利息
                var usedFinancingAmount = dailyModel.ActualFinancingAmount < accumulatedNetProfit ? 0 : dailyModel.ActualFinancingAmount - accumulatedNetProfit;
                var usedLoanAmount = dailyModel.ActualLoanAmount - currentOutMarginInfo.LoanAmount;
                dailyModel.CurrentInterest = (usedFinancingAmount + usedLoanAmount) * AppConfigHelper.MarginTradingDPR;
                //前一日利息
                dailyModel.LastDayInterest = lastDayInterest;
                lastDayInterest = dailyModel.CurrentInterest;
                //累计利息（周六周日利息核算）
                if (tradeDate.DayOfWeek == DayOfWeek.Monday)
                    accumulatedInterest += dailyModel.LastDayInterest * 2;
                accumulatedInterest += dailyModel.CurrentInterest;
                dailyModel.AccumulatedInterest = accumulatedInterest;

                result.Add(dailyModel);
            }

            return result;
        }

        /// <summary>
        /// 取得波段投资收益信息
        /// </summary>
        /// <param name="investorInfo"></param>
        /// <param name="statisticalInvestorCodes"></param>
        /// <param name="bandRecords"></param>
        /// <param name="statisticalDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        private IList<UserInvestIncomeEntity> GetUserBandInvestIncomeInfo(UserInfo investorInfo, IList<string> statisticalInvestorCodes, List<DailyRecord> bandRecords, IList<DateTime> statisticalDates, IList<TKLineToday> stockClosePrices)
        {
            var bandIncomInfos = new List<UserInvestIncomeEntity>();

            if (bandRecords?.Count == 0 || statisticalDates?.Count == 0)
                return bandIncomInfos;

            //取得用户输入的波段融资融券信息
            var existedBandMarginInfos = this.GetBandDailyMarginTradingInfo(statisticalInvestorCodes, statisticalDates.Max());

            var firstMarginDate = existedBandMarginInfos?.Count > 0 ? existedBandMarginInfos.First().MarginDate : DateTime.MaxValue;

            var firstTradeDate = bandRecords.First().TradeDate;
            var startDate = firstTradeDate < firstMarginDate ? firstTradeDate : firstMarginDate;
            var endDate = statisticalDates.Last();

            var tradeDates = CommonHelper.GetAllWorkDays(startDate, endDate);

            //投资统计共通信息
            var dailyCommonInfos = bandRecords.GetDailyInvestStatisticsCommonInfo(tradeDates, stockClosePrices);

            //波段融资融券和利息信息
            var bandMarginInterestInfos = this.CalculateUserBandMarginTradingInterest(dailyCommonInfos, tradeDates, existedBandMarginInfos);

            //前一交易日的累计收益额
            var previousAccumulatedProfit = (dailyCommonInfos.SingleOrDefault(x => x.TradeDate == statisticalDates.First()) ?? new InvestStatisticsCommonEntity()).AccumulatedProfit;

            var allotFund = investorInfo.AllotFund;
            foreach (var date in statisticalDates)
            {
                if (date == statisticalDates.First()) continue;

                //当前日期的统计共通信息
                var currentStatisticsCommonInfo = dailyCommonInfos.Where(x => x.TradeDate <= date).LastOrDefault() ?? new InvestStatisticsCommonEntity();
                //当前日期的融资融券利息信息
                var currentMarginInterestInfo = bandMarginInterestInfos.Where(x => x.TradeDate <= date).LastOrDefault() ?? new UserMarginTradingInterestEntity();

                //累计实际收益额
                var accumulatedActualProfit = currentStatisticsCommonInfo.AccumulatedProfit - currentMarginInterestInfo.AccumulatedInterest;
                //当日收益
                var currentProfit = currentStatisticsCommonInfo.AccumulatedProfit - previousAccumulatedProfit;

                var incomeModel = new UserInvestIncomeEntity()
                {
                    //投资人员
                    Investor = investorInfo.Name,
                    //交易日
                    TradeTime = date,
                    //交易类别
                    TradeType = EnumLibrary.TradeType.Band,
                    //成交额
                    DealAmount = currentStatisticsCommonInfo.DealAmount,
                    //分配资金
                    AllotFund = allotFund,
                    //资金占用额度
                    FundOccupyAmount = allotFund * 1.2M,
                    //计划融资融券金额
                    PlanMarginAmount = currentMarginInterestInfo.PlanMarginAmount,
                    //平均融资融券金额
                    AverageMarginAmount = currentMarginInterestInfo.AverageMarginAmount,
                    //实际融资融券金额
                    ActualMarginAmount = currentMarginInterestInfo.ActualMarginAmount,
                    //累计融资融券金额
                    AccumulatedMarginAmount = currentMarginInterestInfo.AccumulatedMarginAmount,
                    //持仓市值
                    PositionValue = currentStatisticsCommonInfo.PositionValue,
                    //周一
                    MondayPositionValue = date.DayOfWeek == DayOfWeek.Monday ? currentStatisticsCommonInfo.PositionValue : 0,
                    //当日收益
                    CurrentProfit = currentProfit,
                    //累计收益额
                    AccumulatedProfit = currentStatisticsCommonInfo.AccumulatedProfit,
                    //当日利息
                    CurrentInterest = currentMarginInterestInfo.CurrentInterest,
                    //累计实际收益额
                    AccumulatedActualProfit = accumulatedActualProfit,
                    //累计利息
                    AccumulatedInterest = currentMarginInterestInfo.AccumulatedInterest,
                    //当日实际收益
                    CurrentActualProfit = currentProfit - currentMarginInterestInfo.CurrentInterest,
                    //当日资产（当日融资融券额 + 累计实际收益额）
                    CurrentAsset = currentMarginInterestInfo.ActualMarginAmount + accumulatedActualProfit,
                };

                //持仓仓位（当日持仓市值 / 当日实际融资融券额）
                incomeModel.PositionRate = CommonHelper.CalculateRate(incomeModel.PositionValue, incomeModel.ActualMarginAmount);
                //当日收益率（当日实际收益额 / 当日实际融资融券额）
                incomeModel.CurrentIncomeRate = CommonHelper.CalculateRate(incomeModel.CurrentActualProfit, incomeModel.ActualMarginAmount);
                //累计收益率（累计实际收益额 / 平均融资融券额）
                incomeModel.AccumulatedIncomeRate = CommonHelper.CalculateRate(incomeModel.AccumulatedActualProfit, incomeModel.AverageMarginAmount);

                //前一日累计收益额设为当日累计收益额
                previousAccumulatedProfit = incomeModel.AccumulatedProfit;

                bandIncomInfos.Add(incomeModel);
            }

            return bandIncomInfos;
        }

        #endregion Band【波段】

        #region Target【目标】

        /// <summary>
        /// 取得目标每日融资融券信息
        /// </summary>
        /// <param name="investorCodes"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private IList<DailyMarginTradingInfoEntity> GetTargetDailyMarginTradingInfo(IList<string> investorCodes, DateTime endDate)
        {
            var result = new List<DailyMarginTradingInfoEntity>();

            if (investorCodes?.Count == 0) return result;

            var unit = (int)EnumLibrary.NumericUnit.TenThousand;

            var inMarginInfos = this._marginService.GetUserInMarginTradingInfo(investorCodes.ToArray(), (int)EnumLibrary.TradeType.Target, null, endDate);

            if (inMarginInfos.Any())
            {
                var marginDates = inMarginInfos.Select(x => x.MarginDate).Distinct();

                foreach (var date in marginDates)
                {
                    //融资融券信息
                    var currentBorrowInfo = inMarginInfos.Where(x => x.IsRepay == false && x.MarginDate < date.AddDays(1));
                    //还资还券信息
                    var currentRepayInfo = inMarginInfos.Where(x => x.IsRepay == true && x.MarginDate < date);

                    var loanAmount = 0;
                    var financingAmount = (currentBorrowInfo.Where(x => x.IsFinancing == true).Sum(x => x.Amount) - currentRepayInfo.Where(x => x.IsFinancing == true).Sum(x => x.Amount)) * unit;
                    if (financingAmount < 0)
                        financingAmount = 0;
                    var dailyInEntity = new DailyMarginTradingInfoEntity
                    {
                        FinancingAmount = financingAmount,
                        isIn = true,
                        LoanAmount = loanAmount,
                        MarginDate = date,
                        TotalAmount = loanAmount + financingAmount,
                    };

                    result.Add(dailyInEntity);
                }
            }

            var outMarginInfos = this._marginService.GetUserOutMarginTradingInfo(investorCodes.ToArray(), (int)EnumLibrary.TradeType.Band, null, endDate);

            if (outMarginInfos.Any())
            {
                var marginsByDay = outMarginInfos.GroupBy(x => x.MarginDate);

                foreach (var dayGroup in marginsByDay)
                {
                    var loanAmount = dayGroup.Where(x => x.IsFinancing == false).Sum(x => x.Amount) * unit;
                    var financingAmount = 0;
                    var dailyOutEntity = new DailyMarginTradingInfoEntity
                    {
                        FinancingAmount = financingAmount,
                        isIn = false,
                        LoanAmount = loanAmount,
                        MarginDate = dayGroup.Key,
                        TotalAmount = loanAmount + financingAmount,
                    };

                    result.Add(dailyOutEntity);
                }
            }

            return result;
        }

        /// <summary>
        /// 用户每日波段融资融券及利息计算
        /// </summary>
        /// <param name="dailyCommonInfos"></param>
        /// <param name="tradeDates"></param>
        /// <param name="existedTargetMarginInfos"></param>
        /// <returns></returns>
        private IList<UserMarginTradingInterestEntity> CalculateUserTargetMarginTradingInterest(IList<InvestStatisticsCommonEntity> dailyCommonInfos, IList<DateTime> tradeDates, IList<DailyMarginTradingInfoEntity> existedTargetMarginInfos)
        {
            var result = new List<UserMarginTradingInterestEntity>();

            if (tradeDates == null) return result;

            decimal accumulatedMarginAmount = 0;
            decimal lastAverageMarginAmount = 0;
            decimal lastDayMarginAmount = 0;
            decimal lastDayInterest = 0;
            decimal accumulatedInterest = 0;
            var tradeDays = 0;
            foreach (var tradeDate in tradeDates)
            {
                var currentCommonInfo = dailyCommonInfos.SingleOrDefault(x => x.TradeDate == tradeDate) ?? new InvestStatisticsCommonEntity();
                var currentInMarginInfo = existedTargetMarginInfos.Where(x => x.isIn == true && x.MarginDate == tradeDate).SingleOrDefault() ?? new DailyMarginTradingInfoEntity();
                var currentOutMarginInfo = existedTargetMarginInfos.Where(x => x.isIn == false && x.MarginDate == tradeDate).SingleOrDefault() ?? new DailyMarginTradingInfoEntity();

                var dailyModel = new UserMarginTradingInterestEntity();
                //融资融券日期
                dailyModel.TradeDate = tradeDate;
                //当日计划融资融券额
                dailyModel.PlanFinancingAmount = currentInMarginInfo.FinancingAmount;
                dailyModel.PlanLoanAmount = currentInMarginInfo.LoanAmount;
                dailyModel.PlanMarginAmount = currentInMarginInfo.TotalAmount;
                //当日成交额
                var dealAmount = currentCommonInfo.DealAmount;
                //当日持仓额
                var positionValue = currentCommonInfo.PositionValue;
                //当日实际融资融券额
                dailyModel.ActualMarginAmount = positionValue > dailyModel.PlanMarginAmount ? positionValue : dailyModel.PlanMarginAmount;
                if (dealAmount > dailyModel.ActualMarginAmount)
                    dailyModel.ActualMarginAmount = dealAmount;
                dailyModel.ActualLoanAmount = dailyModel.PlanLoanAmount;
                dailyModel.ActualFinancingAmount = dailyModel.ActualMarginAmount - dailyModel.ActualLoanAmount;
                //前一日融资融券额
                dailyModel.LastDayMarginAmount = lastDayMarginAmount;
                lastDayMarginAmount = dailyModel.ActualMarginAmount;
                //累计融资融券额
                accumulatedMarginAmount += dailyModel.ActualMarginAmount;
                dailyModel.AccumulatedMarginAmount = accumulatedMarginAmount;
                //实际交易天数
                if (dealAmount > 0 || dailyModel.ActualMarginAmount > 0) tradeDays++;
                //平均融资融券额
                dailyModel.AverageMarginAmount = CommonHelper.CalculateRate(accumulatedMarginAmount, tradeDays);
                //前一日平均融资融券额
                lastAverageMarginAmount = dailyModel.AverageMarginAmount;
                //当日累计净收益
                var accumulatedNetProfit = currentCommonInfo.AccumulatedActualAmount;
                //当日利息
                var usedFinancingAmount = dailyModel.ActualFinancingAmount - accumulatedNetProfit;
                var usedLoanAmount = dailyModel.ActualLoanAmount - currentOutMarginInfo.LoanAmount;
                dailyModel.CurrentInterest = (usedFinancingAmount + usedLoanAmount) * AppConfigHelper.MarginTradingDPR;
                //前一日利息
                dailyModel.LastDayInterest = lastDayInterest;
                lastDayInterest = dailyModel.CurrentInterest;
                //累计利息（周六周日利息核算）
                if (tradeDate.DayOfWeek == DayOfWeek.Monday)
                    accumulatedInterest += dailyModel.LastDayInterest * 2;
                accumulatedInterest += dailyModel.CurrentInterest;
                dailyModel.AccumulatedInterest = accumulatedInterest;

                result.Add(dailyModel);
            }

            return result;
        }

        /// <summary>
        /// 取得波段投资收益信息
        /// </summary>
        /// <param name="investorInfo"></param>
        /// <param name="statisticalInvestorCodes"></param>
        /// <param name="targetRecords"></param>
        /// <param name="statisticalDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        private IList<UserInvestIncomeEntity> GetUserTargetInvestIncomeInfo(UserInfo investorInfo, IList<string> statisticalInvestorCodes, List<DailyRecord> targetRecords, IList<DateTime> statisticalDates, IList<TKLineToday> stockClosePrices)
        {
            var bandIncomInfos = new List<UserInvestIncomeEntity>();

            if (targetRecords?.Count == 0 || statisticalDates?.Count == 0)
                return bandIncomInfos;

            //取得用户输入的波段融资融券信息
            var existedTargetMarginInfos = this.GetTargetDailyMarginTradingInfo(statisticalInvestorCodes, statisticalDates.Max());

            var firstMarginDate = existedTargetMarginInfos?.Count > 0 ? existedTargetMarginInfos.First().MarginDate : DateTime.MaxValue;

            var firstTradeDate = targetRecords.First().TradeDate;
            var startDate = firstTradeDate < firstMarginDate ? firstTradeDate : firstMarginDate;
            var endDate = statisticalDates.Last();

            var tradeDates = CommonHelper.GetAllWorkDays(startDate, endDate);

            //投资统计共通信息
            var dailyCommonInfos = targetRecords.GetDailyInvestStatisticsCommonInfo(tradeDates, stockClosePrices);

            //目标融资融券和利息信息
            var targetMarginInterestInfos = this.CalculateUserTargetMarginTradingInterest(dailyCommonInfos, tradeDates, existedTargetMarginInfos);

            //前一交易日的累计收益额
            var previousAccumulatedProfit = (dailyCommonInfos.SingleOrDefault(x => x.TradeDate == statisticalDates.First()) ?? new InvestStatisticsCommonEntity()).AccumulatedProfit;

            var allotFund = investorInfo.AllotFund;
            foreach (var date in statisticalDates)
            {
                if (date == statisticalDates.First()) continue;

                //当前日期的统计共通信息
                var currentStatisticsCommonInfo = dailyCommonInfos.Where(x => x.TradeDate <= date).LastOrDefault() ?? new InvestStatisticsCommonEntity();
                //当前日期的融资融券利息信息
                var currentMarginInterestInfo = targetMarginInterestInfos.Where(x => x.TradeDate <= date).LastOrDefault() ?? new UserMarginTradingInterestEntity();

                //累计实际收益额
                var accumulatedActualProfit = currentStatisticsCommonInfo.AccumulatedProfit - currentMarginInterestInfo.AccumulatedInterest;
                //当日收益
                var currentProfit = currentStatisticsCommonInfo.AccumulatedProfit - previousAccumulatedProfit;

                var incomeModel = new UserInvestIncomeEntity()
                {
                    //投资人员
                    Investor = investorInfo.Name,
                    //交易日
                    TradeTime = date,
                    //交易类别
                    TradeType = EnumLibrary.TradeType.Target,
                    //成交额
                    DealAmount = currentStatisticsCommonInfo.DealAmount,
                    //分配资金
                    AllotFund = allotFund,
                    //资金占用额度
                    FundOccupyAmount = allotFund * 1.2M,
                    //计划融资融券金额
                    PlanMarginAmount = currentMarginInterestInfo.PlanMarginAmount,
                    //平均融资融券金额
                    AverageMarginAmount = currentMarginInterestInfo.AverageMarginAmount,
                    //实际融资融券金额
                    ActualMarginAmount = currentMarginInterestInfo.ActualMarginAmount,
                    //累计融资融券金额
                    AccumulatedMarginAmount = currentMarginInterestInfo.AccumulatedMarginAmount,
                    //持仓市值
                    PositionValue = currentStatisticsCommonInfo.PositionValue,
                    //周一
                    MondayPositionValue = date.DayOfWeek == DayOfWeek.Monday ? currentStatisticsCommonInfo.PositionValue : 0,
                    //当日收益
                    CurrentProfit = currentProfit,
                    //累计收益额
                    AccumulatedProfit = currentStatisticsCommonInfo.AccumulatedProfit,
                    //当日利息
                    CurrentInterest = currentMarginInterestInfo.CurrentInterest,
                    //累计实际收益额
                    AccumulatedActualProfit = accumulatedActualProfit,
                    //累计利息
                    AccumulatedInterest = currentMarginInterestInfo.AccumulatedInterest,
                    //当日实际收益
                    CurrentActualProfit = currentProfit - currentMarginInterestInfo.CurrentInterest,
                    //当日资产（当日融资融券额 + 累计实际收益额）
                    CurrentAsset = currentMarginInterestInfo.ActualMarginAmount + accumulatedActualProfit,
                };

                //持仓仓位（当日持仓市值 / 当日实际融资融券额）
                incomeModel.PositionRate = CommonHelper.CalculateRate(incomeModel.PositionValue, incomeModel.ActualMarginAmount);
                //当日收益率（当日实际收益额 / 当日实际融资融券额）
                incomeModel.CurrentIncomeRate = CommonHelper.CalculateRate(incomeModel.CurrentActualProfit, incomeModel.ActualMarginAmount);
                //累计收益率（累计实际收益额 / 平均融资融券额）
                incomeModel.AccumulatedIncomeRate = CommonHelper.CalculateRate(incomeModel.AccumulatedActualProfit, incomeModel.AverageMarginAmount);

                //前一日累计收益额设为当日累计收益额
                previousAccumulatedProfit = incomeModel.AccumulatedProfit;

                bandIncomInfos.Add(incomeModel);
            }

            return bandIncomInfos;
        }

        #endregion Target【目标】

        #region Summation 【合算】

        /// <summary>
        /// 个人投资收益合算
        /// </summary>
        /// <param name="investorInfo"></param>
        /// <param name="statisticalDates"></param>
        /// <param name="dayInvestIncomeInfos"></param>
        /// <param name="bandInvestIncomeInfos"></param>
        /// <param name="targetInvestIncomeInfos"></param>
        /// <returns></returns>
        private IList<UserInvestIncomeEntity> GetUserTotalInvestIncome(UserInfo investorInfo, IList<DateTime> statisticalDates, IList<UserInvestIncomeEntity> dayInvestIncomeInfos, IList<UserInvestIncomeEntity> bandInvestIncomeInfos, IList<UserInvestIncomeEntity> targetInvestIncomeInfos)
        {
            IList<UserInvestIncomeEntity> result = new List<UserInvestIncomeEntity>();

            if (!dayInvestIncomeInfos.Any() && !bandInvestIncomeInfos.Any() && !targetInvestIncomeInfos.Any()) return result;

            foreach (var date in statisticalDates)
            {
                if (date == statisticalDates.First()) continue;

                var currentDayIncome = dayInvestIncomeInfos.SingleOrDefault(x => x.TradeTime == date) ?? new UserInvestIncomeEntity();
                var currentBandIncome = bandInvestIncomeInfos.SingleOrDefault(x => x.TradeTime == date) ?? new UserInvestIncomeEntity();
                var currentTargetIncome = targetInvestIncomeInfos.SingleOrDefault(x => x.TradeTime == date) ?? new UserInvestIncomeEntity();

                var incomeModel = new UserInvestIncomeEntity()
                {
                    AccumulatedActualProfit = currentDayIncome.AccumulatedActualProfit + currentBandIncome.AccumulatedActualProfit + currentTargetIncome.AccumulatedActualProfit,
                    AccumulatedInterest = currentDayIncome.AccumulatedInterest + currentBandIncome.AccumulatedInterest + currentTargetIncome.AccumulatedInterest,
                    AccumulatedMarginAmount = currentDayIncome.AccumulatedMarginAmount + currentBandIncome.AccumulatedMarginAmount + currentTargetIncome.AccumulatedMarginAmount,
                    AccumulatedProfit = currentDayIncome.AccumulatedProfit + currentBandIncome.AccumulatedProfit + currentTargetIncome.AccumulatedProfit,
                    ActualMarginAmount = currentDayIncome.ActualMarginAmount + currentBandIncome.ActualMarginAmount + currentTargetIncome.ActualMarginAmount,
                    AllotFund = investorInfo.AllotFund,
                    AverageMarginAmount = currentDayIncome.AverageMarginAmount + currentBandIncome.AverageMarginAmount + currentTargetIncome.AverageMarginAmount,
                    CurrentActualProfit = currentDayIncome.CurrentActualProfit + currentBandIncome.CurrentActualProfit + currentTargetIncome.CurrentActualProfit,
                    CurrentAsset = currentDayIncome.CurrentAsset + currentBandIncome.CurrentAsset + currentTargetIncome.CurrentAsset,
                    CurrentInterest = currentDayIncome.CurrentInterest + currentBandIncome.CurrentInterest + currentTargetIncome.CurrentInterest,
                    CurrentProfit = currentDayIncome.CurrentProfit + currentBandIncome.CurrentProfit + currentTargetIncome.CurrentProfit,
                    DealAmount = currentDayIncome.DealAmount + currentBandIncome.DealAmount + currentTargetIncome.DealAmount,
                    FundOccupyAmount = currentDayIncome.FundOccupyAmount + currentBandIncome.FundOccupyAmount + currentTargetIncome.FundOccupyAmount,
                    Investor = investorInfo.Name,
                    MondayPositionValue = currentDayIncome.MondayPositionValue + currentBandIncome.MondayPositionValue + currentTargetIncome.MondayPositionValue,
                    PlanMarginAmount = currentDayIncome.PlanMarginAmount + currentBandIncome.PlanMarginAmount + currentTargetIncome.PlanMarginAmount,
                    PositionValue = currentDayIncome.PositionValue + currentBandIncome.PositionValue + currentTargetIncome.PositionValue,
                    TradeTime = date,
                    TradeType = EnumLibrary.TradeType.All,
                };
                //持仓仓位（当日持仓市值 / 当日实际融资融券额）
                incomeModel.PositionRate = CommonHelper.CalculateRate(incomeModel.PositionValue, incomeModel.ActualMarginAmount);
                //当日收益率（当日实际收益额 / 当日实际融资融券额）
                incomeModel.CurrentIncomeRate = CommonHelper.CalculateRate(incomeModel.CurrentActualProfit, incomeModel.ActualMarginAmount);
                //累计收益率（累计实际收益额 / 平均融资融券额）
                incomeModel.AccumulatedIncomeRate = CommonHelper.CalculateRate(incomeModel.AccumulatedActualProfit, incomeModel.AverageMarginAmount);

                result.Add(incomeModel);
            }

            return result;
        }

        #endregion Summation 【合算】

        #endregion Utilities

        #region Methods

        /// <summary>
        /// 投资者投资收益计算
        /// </summary>
        /// <param name="investorInfo"></param>
        /// <param name="statisticalInvestorCodes"></param>
        /// <param name="dailyRecords"></param>
        /// <param name="statisticalDates"></param>
        /// <param name="stockClosePrices"></param>
        /// <returns></returns>
        public virtual IList<UserInvestIncomeEntity> CalculateUserInvestIncome(UserInfo investorInfo, IList<string> statisticalInvestorCodes, IList<DailyRecord> dailyRecords, IList<DateTime> statisticalDates, IList<TKLineToday> stockClosePrices)
        {
            IList<UserInvestIncomeEntity> result = new List<UserInvestIncomeEntity>();

            if (investorInfo == null || dailyRecords?.Count == 0 || statisticalDates?.Count == 0)
                return result;

            dailyRecords = dailyRecords.OrderBy(x => x.TradeDate).ToList();
            statisticalDates = statisticalDates.OrderBy(x => x).ToList();

            //日内每日投资收益统计
            IList<UserInvestIncomeEntity> dayInvestIncomeInfos = new List<UserInvestIncomeEntity>();
            //日内交易数据
            var dayRecords = dailyRecords.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Day).ToList();
            if (dayRecords.Any())
                dayInvestIncomeInfos = GetUserDayInvestIncomeInfo(investorInfo, statisticalInvestorCodes, dayRecords, statisticalDates, stockClosePrices);

            //波段每日投资收益统计
            IList<UserInvestIncomeEntity> bandInvestIncomeInfos = new List<UserInvestIncomeEntity>();
            //波段交易数据
            var bandRecords = dailyRecords.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Band).ToList();
            if (bandRecords.Any())
                bandInvestIncomeInfos = GetUserBandInvestIncomeInfo(investorInfo, statisticalInvestorCodes, bandRecords, statisticalDates, stockClosePrices);

            //目标每日投资收益统计
            IList<UserInvestIncomeEntity> targetInvestIncomeInfos = new List<UserInvestIncomeEntity>();
            //目标交易记录
            var targetRecords = dailyRecords.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Target).ToList();
            if (targetRecords.Any())
                targetInvestIncomeInfos = GetUserTargetInvestIncomeInfo(investorInfo, statisticalInvestorCodes, targetRecords, statisticalDates, stockClosePrices);

            //投资收益合算
            result = GetUserTotalInvestIncome(investorInfo, statisticalDates, dayInvestIncomeInfos, bandInvestIncomeInfos, targetInvestIncomeInfos);

            return result;
        }

        public virtual IList<AccountInvestIncomeEntity> CalculateAccountInvestIncome(IList<DailyRecord> records, IList<DateTime> queryDates, IList<TKLineToday> stockClosePrices, AccountEntity accountInfo)
        {
            var result = new List<AccountInvestIncomeEntity>();

            if (records == null || !records.Any() || stockClosePrices == null) return result;

            //帐户分配资金
            decimal allotFund = accountInfo.InvestFund;

            #region 统计日前一天

            var lastDate = queryDates.First();
            var lastRecords = records.Where(x => x.TradeDate <= lastDate).ToList();
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

                var currentRecords = records.Where(x => x.TradeDate < date).ToList();
                var currentDateClosePrices = stockClosePrices.Where(x => x.TradeDate == date).ToList();

                //当日投资收益信息
                var currentInvestIncomeInfo = currentRecords.GetInvestStatisticsCommonInfo(currentDateClosePrices);

                var incomeModel = new AccountInvestIncomeEntity()
                {
                    //账户名称
                    AccountName = accountInfo.Name,
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

        #endregion Methods
    }
}