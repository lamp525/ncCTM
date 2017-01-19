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
    public partial class FrmUserInvestIncomeSummary : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _tradeRecordService;
        private readonly ITKLineService _TKLineService;
        private readonly IUserService _userService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;
        private IList<UserInvestIncomeSummaryModel> _queryResult = null;
        private const string _layoutXmlName = "FrmUserInvestIncomeSummary";

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeSummary(IDailyRecordService tradeRecordService, ITKLineService TKLineService, IUserService userService)
        {
            InitializeComponent();

            this._tradeRecordService = tradeRecordService;
            this._TKLineService = TKLineService;
            this._userService = userService;
        }

        #endregion Constructors

        #region Utilities

        private void DisplaySearchResult(bool isSearch)
        {
            this.gridControl1.DataSource = null;

            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            if (isSearch)
                _queryResult = CalculateUserIncomeSummary(dateFrom.AddDays(-1), dateTo);

            if (_queryResult == null) return;

            var currentResult = this.chkOnWorking.Checked ? _queryResult.Where(x => x.IsOnWorking).ToList() : _queryResult;

            var totalSummary = CalculateTotalInvestIncome(currentResult);

            var result = DataFormat(totalSummary);

            this.gridControl1.DataSource = result;
        }

        private IList<UserInvestIncomeSummaryModel> DataFormat(IList<UserInvestIncomeSummaryModel> source)
        {
            var result = new List<UserInvestIncomeSummaryModel>();
            int unit = (int)EnumLibrary.NumericUnit.TenThousand;

            result = source.Select(x => new UserInvestIncomeSummaryModel()
            {
                Type = x.Type,

                Investor = x.Investor,

                TradeTypeName = CTMHelper.GetTradeTypeName(x.TradeType),

                StockFullCode = x.StockFullCode,
                StockName = x.StockName,

                AllotFund = x.Type == 0 ? 0 : CommonHelper.SetDecimalDigits(x.AllotFund / unit),
                AccumulatedProfit = CommonHelper.SetDecimalDigits(x.AccumulatedProfit / unit),
                AccumulatedIncomeRate = CommonHelper.SetDecimalDigits(x.AccumulatedIncomeRate, 4),

                InitAsset = x.Type == 0 ? 0 : CommonHelper.SetDecimalDigits(x.InitAsset / unit),
                InitHoldingVolume = x.Type == 0 ? x.InitHoldingVolume : 0,
                InitPositionValue = CommonHelper.SetDecimalDigits(x.InitPositionValue / unit),
                InitProfit = CommonHelper.SetDecimalDigits(x.InitProfit / unit),

                CurrentAsset = x.Type == 0 ? 0 : CommonHelper.SetDecimalDigits(x.CurrentAsset / unit),
                CurrentHoldingVolume = x.Type == 0 ? x.CurrentHoldingVolume : 0,
                CurrentPositionValue = CommonHelper.SetDecimalDigits(x.CurrentPositionValue / unit),
                CurrentPrice = x.Type == 0 ? CommonHelper.SetDecimalDigits(x.CurrentPrice) : 0,
                CurrentProfit = CommonHelper.SetDecimalDigits(x.CurrentProfit / unit),
                CurrentIncomeRate = CommonHelper.SetDecimalDigits(x.CurrentIncomeRate, 4),

                AnnualProfit = CommonHelper.SetDecimalDigits(x.AnnualProfit / unit),
                AnnualIncomeRate = CommonHelper.SetDecimalDigits(x.AnnualIncomeRate, 4),
            }
            ).OrderBy(x => x.Investor).ThenBy(x => x.StockFullCode).ThenBy(x => x.StockName).ToList();

            int serialNo = 0;
            string previousInvestor = null;
            foreach (var item in result)
            {
                if (!item.Investor.Equals(previousInvestor))
                    serialNo++;

                item.UniqueSerialNo = serialNo;
                previousInvestor = item.Investor;
            }

            return result;
        }

        private IList<UserInvestIncomeSummaryModel> CalculateTotalInvestIncome(IList<UserInvestIncomeSummaryModel> queryResult)
        {
            var totalSummaryRecords = new List<UserInvestIncomeSummaryModel>();

            if (queryResult == null) return totalSummaryRecords;

            totalSummaryRecords.AddRange(queryResult);

            //投资人小计
            var recordsByInvestor = queryResult.GroupBy(x => x.Investor);

            foreach (var investorGroup in recordsByInvestor)
            {
                var firstRecord = investorGroup.First();
                var subTotalAccumulatedProfit = investorGroup.Sum(x => x.AccumulatedProfit);
                var subTotalInitProfit = investorGroup.Sum(x => x.InitProfit);
                var subTotalCurrentProfit = investorGroup.Sum(x => x.CurrentProfit);
                var subTotalAnnualProfit = investorGroup.Sum(x => x.AnnualProfit);

                var subTotalModel = new UserInvestIncomeSummaryModel
                {
                    Type = 1,

                    Investor = firstRecord.Investor,

                    StockFullCode = string.Empty,
                    StockName = "小    计：",

                    AllotFund = firstRecord.AllotFund,
                    AccumulatedProfit = subTotalAccumulatedProfit,
                    AccumulatedIncomeRate = CommonHelper.CalculateRate(subTotalAccumulatedProfit, firstRecord.AllotFund),

                    InitAsset = subTotalInitProfit + firstRecord.AllotFund,
                    InitHoldingVolume = 0,
                    InitPositionValue = investorGroup.Sum(x => x.InitPositionValue),
                    InitProfit = investorGroup.Sum(x => x.InitProfit),

                    CurrentAsset = subTotalAccumulatedProfit + firstRecord.AllotFund,
                    CurrentHoldingVolume = 0,
                    CurrentPositionValue = investorGroup.Sum(x => x.CurrentPositionValue),
                    CurrentPrice = 0,
                    CurrentProfit = subTotalCurrentProfit,
                    CurrentIncomeRate = CommonHelper.CalculateRate(subTotalCurrentProfit, firstRecord.AllotFund),

                    AnnualProfit = subTotalAnnualProfit,
                    AnnualIncomeRate = CommonHelper.CalculateRate(subTotalAnnualProfit, firstRecord.AllotFund),

                    TradeType = 0,
                };

                totalSummaryRecords.Add(subTotalModel);
            }

            var allSubTotalRecords = totalSummaryRecords.Where(x => x.Type == 1);

            var totalModel = new UserInvestIncomeSummaryModel();

            totalModel.Type = 2;

            totalModel.Investor = " 总    计： ";

            totalModel.StockName = string.Empty;
            totalModel.StockFullCode = string.Empty;

            totalModel.AllotFund = allSubTotalRecords.Sum(x => x.AllotFund);
            totalModel.AccumulatedProfit = allSubTotalRecords.Sum(x => x.AccumulatedProfit);
            totalModel.AccumulatedIncomeRate = CommonHelper.CalculateRate(totalModel.AccumulatedProfit, totalModel.AllotFund);

            totalModel.InitAsset = allSubTotalRecords.Sum(x => x.InitAsset);
            totalModel.InitHoldingVolume = 0;
            totalModel.InitPositionValue = allSubTotalRecords.Sum(x => x.InitPositionValue);
            totalModel.InitProfit = allSubTotalRecords.Sum(x => x.InitProfit);

            totalModel.CurrentAsset = allSubTotalRecords.Sum(x => x.CurrentAsset);
            totalModel.CurrentHoldingVolume = 0;
            totalModel.CurrentPositionValue = allSubTotalRecords.Sum(x => x.CurrentPositionValue);
            totalModel.CurrentPrice = 0;
            totalModel.CurrentProfit = allSubTotalRecords.Sum(x => x.CurrentProfit);
            totalModel.CurrentIncomeRate = CommonHelper.CalculateRate(totalModel.CurrentProfit, totalModel.AllotFund);

            totalModel.AnnualProfit = allSubTotalRecords.Sum(x => x.AnnualProfit);
            totalModel.AnnualIncomeRate = CommonHelper.CalculateRate(totalModel.AnnualProfit, totalModel.AllotFund);

            totalModel.TradeType = 0;

            totalSummaryRecords.Add(totalModel);

            return totalSummaryRecords;
        }

        private IList<UserInvestIncomeSummaryModel> CalculateUserIncomeSummary(DateTime startDate, DateTime endDate)
        {
            var result = new List<UserInvestIncomeSummaryModel>();
            var beneficiaries = new string[1];

            if (LoginInfo.CurrentUser.IsAdmin)
                beneficiaries = null;
            else
                beneficiaries[0] = LoginInfo.CurrentUser.UserCode;

            var allRecords = _tradeRecordService.GetDailyRecords(beneficiaries: beneficiaries, tradeDateFrom: _initDate, tradeDateTo: endDate);

            if (!allRecords.Any()) return result;

            var baseDate = (new DateTime(endDate.Year, 1, 1)).AddDays(-1);
            var queryDates = new List<DateTime> { baseDate > startDate ? startDate : baseDate, endDate };
            var stockFullCodes = allRecords.Select(x => x.StockCode).Distinct().ToArray();
            var stockClosePrices = _TKLineService.GetStockClosePrices(queryDates, stockFullCodes);
            var baseDateClosePrices = stockClosePrices.Where(x => x.TradeDate == baseDate).ToList();
            var initDateClosePrices = stockClosePrices.Where(x => x.TradeDate == startDate).ToList();
            var currentDateClosePrices = stockClosePrices.Where(x => x.TradeDate == endDate).ToList();
            var allBeneficiaries = allRecords.Select(x => x.Beneficiary).Distinct().ToArray();
            var allBeneficiaryInfos = _userService.GetUserInfoByCode(allBeneficiaries);

            //受益人分组记录
            var recordsByBeneficiary = allRecords.GroupBy(x => x.Beneficiary);

            foreach (var beneficiaryGroup in recordsByBeneficiary)
            {
                var beneficiaryInfo = allBeneficiaryInfos.SingleOrDefault(x => x.Code == beneficiaryGroup.Key);
                if (beneficiaryInfo == null) continue;

                var allotFund = beneficiaryInfo.AllotFund;
                //股票分组记录
                var recordsByStockCode = beneficiaryGroup.GroupBy(x => x.StockCode);

                foreach (var stockGroup in recordsByStockCode)
                {
                    var stockFullCode = stockGroup.Key;
                    var stockName = stockGroup.First().StockName;
                    var recordsByTradeType = stockGroup.GroupBy(x => x.TradeType);

                    //年度基准日股票收盘价格
                    decimal baseClosePrice = (baseDateClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;
                    //期初股票收盘价格
                    decimal initClosePrice = (initDateClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;
                    //期末股票收盘价格
                    decimal currentClosePrice = (currentDateClosePrices.LastOrDefault(x => x.StockCode.Trim() == stockGroup.Key) ?? new TKLineToday()).Close;

                    foreach (var tradeTypeGroup in recordsByTradeType)
                    {
                        var tradeType = tradeTypeGroup.Key;

                        #region 基准日处理

                        //截至基准日的交易记录
                        var baseRecords = tradeTypeGroup.Where(x => x.TradeDate <= baseDate);
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
                        var initRecords = tradeTypeGroup.Where(x => x.TradeDate <= startDate);
                        //发生金额
                        decimal initActualAmount = initRecords.Sum(x => x.ActualAmount);
                        //股票的持股数
                        decimal initHoldingVolume = initRecords.Sum(x => x.DealVolume);
                        //持仓市值
                        decimal initPositionValue = Math.Abs(initHoldingVolume) * initClosePrice;
                        //累计收益额
                        decimal initAccumulatedProfit = initActualAmount + initHoldingVolume * initClosePrice;

                        #endregion 期初处理

                        #region 期末处理

                        //截至期末的交易记录
                        var currentRecords = tradeTypeGroup;

                        //股票持股数
                        decimal currentHoldingVolume = currentRecords.Sum(x => x.DealVolume);
                        //发生金额
                        decimal currentActualAmount = currentRecords.Sum(x => x.ActualAmount);
                        //持仓市值
                        decimal currentPositionValue = Math.Abs(currentHoldingVolume) * currentClosePrice;
                        //累计收益额
                        decimal currentAccumulatedProfit = currentActualAmount + currentHoldingVolume * currentClosePrice;
                        //累计收益率
                        decimal currentAccumulatedIncomeRate = 0.00M;
                        //本期收益
                        decimal currentProfit = currentAccumulatedProfit - initAccumulatedProfit;
                        //本期收益率
                        decimal currentIncomeRate = 0.00M;
                        //本年收益额
                        decimal annualProfit = currentAccumulatedProfit - baseAccumulatedProfit;
                        //本年收益率
                        decimal annualIncomeRate = 0.00M;

                        if (tradeType == (int)EnumLibrary.TradeType.Day)
                        {
                            currentIncomeRate = CommonHelper.CalculateRate(currentProfit, currentPositionValue > allotFund ? currentPositionValue : allotFund);
                            currentAccumulatedIncomeRate = CommonHelper.CalculateRate(currentAccumulatedProfit, currentPositionValue > allotFund ? currentPositionValue : allotFund);
                            annualIncomeRate = CommonHelper.CalculateRate(annualProfit, currentPositionValue > allotFund ? currentPositionValue : allotFund);
                        }
                        else
                        {
                            currentIncomeRate = CommonHelper.CalculateRate(currentProfit, allotFund);
                            currentAccumulatedIncomeRate = CommonHelper.CalculateRate(currentAccumulatedProfit, allotFund);
                            annualIncomeRate = CommonHelper.CalculateRate(annualProfit, allotFund);
                        }

                        #endregion 期末处理

                        var tradeTypeSummaryModel = new UserInvestIncomeSummaryModel()
                        {
                            Type = 0,

                            Investor = beneficiaryInfo.Name,
                            IsOnWorking = !beneficiaryInfo.IsDeleted,

                            StockFullCode = stockFullCode,
                            StockName = stockName,

                            AllotFund = allotFund,
                            AccumulatedProfit = currentAccumulatedProfit,
                            AccumulatedIncomeRate = currentAccumulatedIncomeRate,

                            InitAsset = allotFund + initAccumulatedProfit,
                            InitHoldingVolume = initHoldingVolume,
                            InitPositionValue = initPositionValue,
                            InitProfit = initAccumulatedProfit,

                            TradeType = tradeType,
                            CurrentAsset = allotFund + currentAccumulatedProfit,
                            CurrentPositionValue = currentPositionValue,
                            CurrentHoldingVolume = currentHoldingVolume,
                            CurrentPrice = currentClosePrice,
                            CurrentProfit = currentProfit,
                            CurrentIncomeRate = currentIncomeRate,

                            AnnualProfit = annualProfit,
                            AnnualIncomeRate = annualIncomeRate,
                        };

                        result.Add(tradeTypeSummaryModel);
                    }
                }
            }
            return result;
        }

        private void FilterSearchResult()
        {
            DisplaySearchResult(false);
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeSummary_Load(object sender, EventArgs e)
        {
            this.deFrom.Properties.AllowNullInput = DefaultBoolean.False;
            this.deFrom.EditValue = _initDate;

            this.deTo.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTo.EditValue = now.Date.AddDays(-1);
            else
                this.deTo.EditValue = now.Date;

            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.lciCheckAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciCheckOnWorking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciCheckAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciCheckOnWorking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            this.bandedGridView1.LoadLayout(_layoutXmlName);
            this.bandedGridView1.SetLayout(showCheckBoxRowSelect: false, showFilterPanel: true, showGroupPanel: true);
            this.bandedGridView1.SetColumnHeaderAppearance();

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                DisplaySearchResult(true);
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

        private void chkOnWorking_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAll.Checked = !this.chkOnWorking.Checked;

            if (this.chkOnWorking.Checked)
                FilterSearchResult();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.chkOnWorking.Checked = !this.chkAll.Checked;

            if (this.chkAll.Checked)
                FilterSearchResult();
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.bandedGridView1.SaveLayout(_layoutXmlName);
        }

        private void bandedGridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var currentUniqueSerialNo = int.Parse(this.bandedGridView1.GetRowCellValue(e.RowHandle, this.colUniqueSerialNo).ToString());
            if (currentUniqueSerialNo % 2 == 1)
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(225, 244, 255);

            var stockCode = this.bandedGridView1.GetRowCellValue(e.RowHandle, this.colStockFullCode).ToString();
            if (string.IsNullOrEmpty(stockCode))
                e.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            e.HighPriority = true;
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