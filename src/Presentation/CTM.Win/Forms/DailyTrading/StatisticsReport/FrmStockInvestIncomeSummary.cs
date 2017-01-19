using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.TKLine;
using CTM.Core.Util;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmStockInvestIncomeSummary : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly ITKLineService _TKLineService;
        private readonly IUserService _userService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmStockInvestIncomeSummary";

        #endregion Fields

        #region Constructors

        public FrmStockInvestIncomeSummary(IDailyRecordService dailyRecordService, ITKLineService TKLineService, IUserService userService)
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._TKLineService = TKLineService;
            this._userService = userService;
        }

        #endregion Constructors

        #region Utilities

        private void DisplaySearchResult()
        {
            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            var queryResult = CalculateStockIncomeSummary(dateFrom, dateTo);

            var totalSummary = CalculateTotalInvestIncome(queryResult);

            var result = DataFormat(totalSummary);

            this.gridControl1.DataSource = result;
        }

        private IList<StockInvestIncomeSummaryModel> DataFormat(IList<StockInvestIncomeSummaryModel> source)
        {
            var result = new List<StockInvestIncomeSummaryModel>();

            var unit = (int)EnumLibrary.NumericUnit.TenThousand;

            result = source.Select(x => new StockInvestIncomeSummaryModel
            {
                StockFullCode = x.StockFullCode,
                StockName = x.StockName,

                AccumulatedProfit = CommonHelper.SetDecimalDigits(x.AccumulatedProfit / unit),
                AnnualProfit = CommonHelper.SetDecimalDigits(x.AnnualProfit / unit),

                BandActualProfit = CommonHelper.SetDecimalDigits(x.BandActualProfit / unit),
                BandFoatingProfit = CommonHelper.SetDecimalDigits(x.BandFoatingProfit / unit),
                BandHoldingVolume = x.Type == 0 ? x.BandHoldingVolume : 0,
                BandPositionValue = CommonHelper.SetDecimalDigits(x.BandPositionValue / unit),
                BandTotalProfit = CommonHelper.SetDecimalDigits(x.BandTotalProfit / unit),

                CurrentHoldingVolume = x.Type == 0 ? x.CurrentHoldingVolume : 0,
                CurrentPositionValue = CommonHelper.SetDecimalDigits(x.CurrentPositionValue / unit),
                CurrentPrice = x.Type == 0 ? CommonHelper.SetDecimalDigits(x.CurrentPrice) : 0,
                CurrentProfit = CommonHelper.SetDecimalDigits(x.CurrentProfit / unit),

                DayActualProfit = CommonHelper.SetDecimalDigits(x.DayActualProfit / unit),
                DayFoatingProfit = CommonHelper.SetDecimalDigits(x.DayFoatingProfit / unit),
                DayHoldingVolume = x.Type == 0 ? x.DayHoldingVolume : 0,
                DayPositionValue = CommonHelper.SetDecimalDigits(x.DayPositionValue / unit),
                DayTotalProfit = CommonHelper.SetDecimalDigits(x.DayTotalProfit / unit),

                InitHoldingVolume = x.Type == 0 ? x.InitHoldingVolume : 0,
                InitPositionValue = CommonHelper.SetDecimalDigits(x.InitPositionValue / unit),
                InitProfit = CommonHelper.SetDecimalDigits(x.InitProfit / unit),
                InitPrice = x.Type == 0 ? CommonHelper.SetDecimalDigits(x.InitPrice) : 0,

                TargetActualProfit = CommonHelper.SetDecimalDigits(x.TargetActualProfit / unit),
                TargetFoatingProfit = CommonHelper.SetDecimalDigits(x.TargetFoatingProfit / unit),
                TargetHoldingVolume = x.Type == 0 ? x.TargetHoldingVolume : 0,
                TargetPositionValue = CommonHelper.SetDecimalDigits(x.TargetPositionValue / unit),
                TargetTotalProfit = CommonHelper.SetDecimalDigits(x.TargetTotalProfit / unit),
            }
            ).OrderBy(x => x.StockFullCode).ThenBy(x => x.StockName).ToList();

            return result;
        }

        private IList<StockInvestIncomeSummaryModel> CalculateTotalInvestIncome(IList<StockInvestIncomeSummaryModel> queryResult)
        {
            var result = new List<StockInvestIncomeSummaryModel>();
            result.AddRange(queryResult);

            var totalSummaryModel = new StockInvestIncomeSummaryModel();

            totalSummaryModel.Type = 1;

            totalSummaryModel.StockFullCode = " 合计： ";
            totalSummaryModel.StockName = string.Empty;

            totalSummaryModel.AccumulatedProfit = queryResult.Sum(x => x.AccumulatedProfit);
            totalSummaryModel.AnnualProfit = queryResult.Sum(x => x.AnnualProfit);

            totalSummaryModel.BandActualProfit = queryResult.Sum(x => x.BandActualProfit);
            totalSummaryModel.BandFoatingProfit = queryResult.Sum(x => x.BandFoatingProfit);
            totalSummaryModel.BandHoldingVolume = 0;
            totalSummaryModel.BandPositionValue = queryResult.Sum(x => x.BandPositionValue);
            totalSummaryModel.BandTotalProfit = queryResult.Sum(x => x.BandTotalProfit);

            totalSummaryModel.CurrentPositionValue = queryResult.Sum(x => x.CurrentPositionValue);
            totalSummaryModel.CurrentHoldingVolume = 0;
            totalSummaryModel.CurrentPrice = 0;
            totalSummaryModel.CurrentProfit = queryResult.Sum(x => x.CurrentProfit);

            totalSummaryModel.DayActualProfit = queryResult.Sum(x => x.DayActualProfit);
            totalSummaryModel.DayFoatingProfit = queryResult.Sum(x => x.DayFoatingProfit);
            totalSummaryModel.DayHoldingVolume = 0;
            totalSummaryModel.DayPositionValue = queryResult.Sum(x => x.DayPositionValue);
            totalSummaryModel.DayTotalProfit = queryResult.Sum(x => x.DayTotalProfit);

            totalSummaryModel.InitHoldingVolume = 0;
            totalSummaryModel.InitPositionValue = queryResult.Sum(x => x.InitPositionValue);
            totalSummaryModel.InitProfit = queryResult.Sum(x => x.InitProfit);
            totalSummaryModel.InitPrice = 0;

            totalSummaryModel.TargetActualProfit = queryResult.Sum(x => x.TargetActualProfit);
            totalSummaryModel.TargetFoatingProfit = queryResult.Sum(x => x.TargetFoatingProfit);
            totalSummaryModel.TargetHoldingVolume = 0;
            totalSummaryModel.TargetPositionValue = queryResult.Sum(x => x.TargetPositionValue);
            totalSummaryModel.TargetTotalProfit = queryResult.Sum(x => x.TargetTotalProfit);

            result.Add(totalSummaryModel);

            return result;
        }

        private IList<StockInvestIncomeSummaryModel> CalculateStockIncomeSummary(DateTime startDate, DateTime endDate)
        {
            var result = new List<StockInvestIncomeSummaryModel>();

            var allRecords = _dailyRecordService.GetDailyRecords(tradeDateFrom: _initDate, tradeDateTo: endDate);

            if (!allRecords.Any()) return result;

            var stockFullCodes = allRecords.Select(x => x.StockCode).Distinct().ToArray();
            var baseDate = (new DateTime(endDate.Year, 1, 1)).AddDays(-1);
            var queryDates = new List<DateTime> { baseDate, startDate, endDate };
            var stockClosePrices = _TKLineService.GetStockClosePrices(queryDates, stockFullCodes);
            var baseDateClosePrices = stockClosePrices.Where(x => x.TradeDate == baseDate).ToList();
            var initDateClosePrices = stockClosePrices.Where(x => x.TradeDate == startDate).ToList();
            var currentDateClosePrices = stockClosePrices.Where(x => x.TradeDate == endDate).ToList();

            //股票分组记录
            var recordsByStockCode = allRecords.GroupBy(x => x.StockCode);

            foreach (var stockGroup in recordsByStockCode)
            {
                var stockFullCode = stockGroup.Key;
                var stockName = stockGroup.First().StockName;

                var recordsByTradeType = stockGroup.GroupBy(x => x.TradeType);

                //基准日股票收盘价
                decimal baseClosePrice = (baseDateClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;
                //期初股票收盘价格
                decimal initClosePrice = (initDateClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;
                //期末股票收盘价格
                decimal currentClosePrice = (currentDateClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;

                #region 基准日处理

                //截至基准日的交易记录
                var baseRecords = stockGroup.Where(x => x.TradeDate <= baseDate);
                //发生金额
                decimal baseActualAmount = baseRecords.Sum(x => x.ActualAmount);
                //股票的持股数
                decimal baseHoldingVolume = baseRecords.Sum(x => x.DealVolume);
                //持仓市值
                decimal basePositionValue = Math.Abs(baseHoldingVolume) * initClosePrice;
                //累计收益额
                decimal baseAccumulatedProfit = baseActualAmount + baseHoldingVolume * initClosePrice;

                #endregion 基准日处理

                #region 期初处理

                //截至期初的交易记录
                var initRecords = stockGroup.Where(x => x.TradeDate <= startDate);
                //发生金额
                decimal initActualAmount = initRecords.Sum(x => x.ActualAmount);
                //股票的持股数
                decimal initHoldingVolume = initRecords.Sum(x => x.DealVolume);
                //持仓市值
                decimal initPositionValue = Math.Abs(initHoldingVolume) * initClosePrice;
                //累计收益额
                decimal initAccumulatedProfit = initActualAmount + initHoldingVolume * initClosePrice;
                //目标累计收益额
                var initTargetRecords = initRecords.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Target);
                decimal initTargetAccumulatedProfit = initTargetRecords.Sum(x => x.ActualAmount) + Math.Abs(initTargetRecords.Sum(x => x.DealVolume)) * initClosePrice;
                //波段累计收益额
                var initBandRecords = initRecords.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Target);
                decimal initBandAccumulatedProfit = initBandRecords.Sum(x => x.ActualAmount) + Math.Abs(initBandRecords.Sum(x => x.DealVolume)) * initClosePrice;
                //日内累计收益额
                var initDayRecords = initRecords.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Target);
                decimal initDayAccumulatedProfit = initDayRecords.Sum(x => x.ActualAmount) + Math.Abs(initDayRecords.Sum(x => x.DealVolume)) * initClosePrice;

                #endregion 期初处理

                #region 期末处理

                //截至期末的交易记录
                var currentRecords = stockGroup;
                //股票持股数
                decimal currentHoldingVolume = currentRecords.Sum(x => x.DealVolume);
                //发生金额
                decimal currentActualAmount = currentRecords.Sum(x => x.ActualAmount);
                //持仓市值
                decimal currentPositionValue = Math.Abs(currentHoldingVolume) * currentClosePrice;
                //累计收益额
                decimal currentAccumulatedProfit = currentActualAmount + currentHoldingVolume * currentClosePrice;
                //本期收益
                decimal currentProfit = currentAccumulatedProfit - initAccumulatedProfit;

                #region 目标统计

                //目标交易记录
                var targetRecords = stockGroup.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Target);
                //目标股票持股数
                decimal targetHoldingVolume = targetRecords.Sum(x => x.DealVolume);
                //目标发生金额
                decimal targetActualAmount = targetRecords.Sum(x => x.ActualAmount);
                //目标持仓市值
                decimal targetPositionValue = Math.Abs(targetHoldingVolume) * currentClosePrice;
                //目标累计收益额
                decimal targetAccumulatedProfit = targetActualAmount + targetHoldingVolume * currentClosePrice;
                //目标本期收益
                decimal targetTotalProfit = targetAccumulatedProfit - initTargetAccumulatedProfit;
                //目标浮动收益
                decimal targetFloatingProfit = targetHoldingVolume * (currentClosePrice - initClosePrice);

                #endregion 目标统计

                #region 波段统计

                //波段交易记录
                var bandRecords = stockGroup.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Band);
                //波段股票持股数
                decimal bandHoldingVolume = bandRecords.Sum(x => x.DealVolume);
                //波段发生金额
                decimal bandActualAmount = bandRecords.Sum(x => x.ActualAmount);
                //波段持仓市值
                decimal bandPositionValue = Math.Abs(bandHoldingVolume) * currentClosePrice;
                //波段累计收益额
                decimal bandAccumulatedProfit = bandActualAmount + bandHoldingVolume * currentClosePrice;
                //波段本期收益
                decimal bandTotalProfit = bandAccumulatedProfit - initBandAccumulatedProfit;
                //波段浮动收益
                decimal bandFloatingProfit = bandHoldingVolume * (currentClosePrice - initClosePrice);

                #endregion 波段统计

                #region 日内统计

                //日内交易记录
                var dayRecords = stockGroup.Where(x => x.TradeType == (int)EnumLibrary.TradeType.Day);
                //日内股票持股数
                decimal dayHoldingVolume = dayRecords.Sum(x => x.DealVolume);
                //日内发生金额
                decimal dayActualAmount = dayRecords.Sum(x => x.ActualAmount);
                //日内持仓市值
                decimal dayPositionValue = Math.Abs(dayHoldingVolume) * currentClosePrice;
                //日内累计收益额
                decimal dayAccumulatedProfit = dayActualAmount + dayHoldingVolume * currentClosePrice;
                //日内本期收益
                decimal dayTotalProfit = dayAccumulatedProfit - initDayAccumulatedProfit;
                //日内浮动收益
                decimal dayFloatingProfit = dayHoldingVolume * (currentClosePrice - initClosePrice);

                #endregion 日内统计

                #endregion 期末处理

                var stockSummaryModel = new StockInvestIncomeSummaryModel()
                {
                    Type = 0,

                    StockFullCode = stockFullCode,
                    StockName = stockName,

                    AccumulatedProfit = currentAccumulatedProfit,
                    AnnualProfit = currentAccumulatedProfit - baseAccumulatedProfit,

                    BandActualProfit = bandActualAmount,
                    BandFoatingProfit = bandFloatingProfit,
                    BandHoldingVolume = bandHoldingVolume,
                    BandPositionValue = bandPositionValue,
                    BandTotalProfit = bandTotalProfit,

                    CurrentPositionValue = currentPositionValue,
                    CurrentHoldingVolume = currentHoldingVolume,
                    CurrentPrice = currentClosePrice,
                    CurrentProfit = currentProfit,

                    DayActualProfit = dayActualAmount,
                    DayFoatingProfit = dayFloatingProfit,
                    DayHoldingVolume = dayHoldingVolume,
                    DayPositionValue = dayPositionValue,
                    DayTotalProfit = dayTotalProfit,

                    InitHoldingVolume = initHoldingVolume,
                    InitPositionValue = initPositionValue,
                    InitProfit = initAccumulatedProfit,
                    InitPrice = initClosePrice,

                    TargetActualProfit = targetActualAmount,
                    TargetFoatingProfit = targetFloatingProfit,
                    TargetHoldingVolume = targetHoldingVolume,
                    TargetPositionValue = targetPositionValue,
                    TargetTotalProfit = targetTotalProfit,
                };

                result.Add(stockSummaryModel);
            }

            return result;
        }

        #endregion Utilities

        #region Events

        private void FrmStockInvestIncomeSummary_Load(object sender, EventArgs e)
        {
            this.deFrom.Properties.AllowNullInput = DefaultBoolean.False;
            this.deTo.Properties.AllowNullInput = DefaultBoolean.False;
            this.deFrom.EditValue = _initDate;

            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTo.EditValue = now.Date.AddDays(-1);
            else
                this.deTo.EditValue = now.Date;

            this.bandedGridView1.LoadLayout(_layoutXmlName);
            this.bandedGridView1.SetLayout(showFilterPanel: true, showCheckBoxRowSelect: false, rowIndicatorWidth: 50);

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                DisplaySearchResult();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.bandedGridView1.SaveLayout(_layoutXmlName);
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}