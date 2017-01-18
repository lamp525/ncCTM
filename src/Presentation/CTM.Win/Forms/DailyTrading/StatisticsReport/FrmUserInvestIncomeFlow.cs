using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.StatisticsReport;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmUserInvestIncomeFlow : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IDepartmentService _departmentService;
        private readonly ITKLineService _tKLineService;
        private readonly IDailyStatisticsReportService _statisticsReportService;

        private Series _seriesPositionValue;
        private Series _seriesCurrentAsset;
        private Series _seriesCurrentActualProfit;
        private Series _seriesAccumulatedActualProfit;
        private Series _seriesCurrentIncomeRatio;
        private Series _seriesAccumulatedIncomeRatio;
        private Series _seriesAverageMarginAmount;
        private Series _seriesActualMarginAmount;

        private ConstantLine _lineZeroCoordinateX;
        private ConstantLine _lineInputAmountLine;
        private ConstantLine _lineBase;
        private ConstantLine _lineDailyRiskyControl;

        //风控线
        private decimal _dailyRiskControlValue;

        //单位：十万
        private const int _unitHundredThousand = (int)EnumLibrary.NumericUnit.HundredThousand;

        //单位：万
        private const int _unitTenThousand = (int)EnumLibrary.NumericUnit.TenThousand;

        //查询交易日数量
        private const int _tradeDateNumber = 25;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private string _reportType = null;

        private const string _layoutXmlName = "FrmUserInvestIncomeFlow";

        private bool _rankingFirstTime = true;

        private IList<UserInvestIncomeRankingModel> _dayRankingInfos = null;
        private IList<UserInvestIncomeRankingModel> _bandRankingInfos = null;
        private IList<UserInvestIncomeRankingModel> _targetRankingInfos = null;
        private IList<UserInvestIncomeRankingModel> _independenceRankingInfos = null;

        private IList<UserInfo> _investors = null;

        private bool _displayCurrentUserAccumulatedProfitRate = true;

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeFlow(
            IDailyRecordService dailyRecordService,
            IUserService userService,
            IDictionaryService dictionaryService,
            IDepartmentService departmentService,
            ITKLineService tKLineService,
            IDailyStatisticsReportService statisticsReportService
            )
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._departmentService = departmentService;
            this._tKLineService = tKLineService;
            this._statisticsReportService = statisticsReportService;
        }

        #endregion Constructors

        #region Utilities

        #region Invest Income Ranking

        /// <summary>
        /// 取得所有统计交易员信息
        /// </summary>
        /// <returns></returns>
        private IList<UserInfo> GetStatisticsUserInfos()
        {
            IList<int> deptIds = null;

            if (LoginInfo.CurrentUser.IsAdmin)
                deptIds = this._departmentService.GetAllAccountingDepartmentId().ToArray();
            else
                deptIds = new int[] { LoginInfo.CurrentUser.DepartmentId };

            var userInfos = this._userService.GetUserInfos(deptIds.ToArray());

            if (!LoginInfo.CurrentUser.IsAdmin)
                userInfos = userInfos.Where(x => x.IsDeleted == false).ToList();

            return userInfos;
        }

        /// <summary>
        /// 取得收益排行榜的统计日期区间
        /// </summary>
        /// <param name="rankingDate"></param>
        /// <returns></returns>
        private IList<DateTime> GetRankingStatisticalDates(DateTime rankingDate)
        {
            var result = new List<DateTime>();

            var mondayOfCurrentWeek = CommonHelper.GetCurrentMonday(rankingDate);
            var firstDayOfCurrentMonth = CommonHelper.GetFirstDayOfMonth(rankingDate);

            var dateFrom = firstDayOfCurrentMonth < mondayOfCurrentWeek ? firstDayOfCurrentMonth : mondayOfCurrentWeek;
            var dateTo = rankingDate;

            result.Add(dateFrom.AddDays(-1));
            result.AddRange(CommonHelper.GetAllWorkDays(dateFrom, dateTo));

            return result;
        }

        private UserInvestIncomeRankingModel GetInvestIncomeRankingInfo(IList<UserInvestIncomeEntity> currentIncomeInfo, UserInfo Investor, DateTime rankingDate)
        {
            var result = new UserInvestIncomeRankingModel();
            result.Investor = Investor;

            if (currentIncomeInfo != null && currentIncomeInfo.Any())
            {
                currentIncomeInfo = currentIncomeInfo.OrderByDescending(x => x.TradeTime).ToList();

                result.DayIncomeRate = currentIncomeInfo.First().CurrentIncomeRate;
                result.AccumulatedIncomeRate = currentIncomeInfo.First().AccumulatedIncomeRate;

                //本周
                var mondayOfCurrentWeek = CommonHelper.GetCurrentMonday(rankingDate);
                var currentWeekIncomeInfo = currentIncomeInfo.Where(x => x.TradeTime >= mondayOfCurrentWeek && x.TradeTime <= rankingDate).ToList();
                if (currentWeekIncomeInfo.Any())
                    result.AverageDayIncomeRateOfWeek = currentWeekIncomeInfo.CalculatePeriodAverageDayIncomeRate();

                //本月
                var firstDayOfCurrentMonth = CommonHelper.GetFirstDayOfMonth(rankingDate);
                var currentMonthIncomeInfo = currentIncomeInfo.Where(x => x.TradeTime >= firstDayOfCurrentMonth && x.TradeTime <= rankingDate).ToList();
                if (currentMonthIncomeInfo.Any())
                    result.AverageDayIncomeRateOfMonth = currentMonthIncomeInfo.CalculatePeriodAverageDayIncomeRate();
            }
            return result;
        }

        private void DisplayInvestIncomeRanking(DateTime rankingDate)
        {
            //交易员信息
            var investors = GetStatisticsUserInfos();

            var beneficiaries = investors.Select(x => x.Code).Distinct().ToArray();

            var tradeRecords = this._dailyRecordService.GetDailyRecords(beneficiaries: beneficiaries, tradeDateFrom: _initDate, tradeDateTo: rankingDate);

            if (tradeRecords == null || !tradeRecords.Any()) return;

            var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
            //所有交易日期
            var tradeDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), rankingDate);
            var stockClosePrices = this._tKLineService.GetStockClosePrices(tradeDates, stockFullCodes);

            var statisticalDates = this.GetRankingStatisticalDates(rankingDate);

            var allRankingInfos = new List<UserInvestIncomeRankingModel>();

            foreach (var user in investors)
            {
                var userRecords = tradeRecords.Where(x => x.Beneficiary == user.Code).ToList();
                var userStockCodes = userRecords.Select(x => x.StockCode).Distinct().ToList();
                var userStockClosePrices = stockClosePrices.Where(x => userStockCodes.Contains(x.StockCode.Trim())).ToList();
                var statisticalInvestorCodes = new List<string> { user.Code };
                var dailyInvestIncomes = this._statisticsReportService.CalculateUserInvestIncome(user, statisticalInvestorCodes, userRecords, statisticalDates, userStockClosePrices);

                var currentRankingModel = this.GetInvestIncomeRankingInfo(dailyInvestIncomes, user, rankingDate);

                allRankingInfos.Add(currentRankingModel);

                if (!LoginInfo.CurrentUser.IsAdmin && this._displayCurrentUserAccumulatedProfitRate && user.Id == LoginInfo.CurrentUser.UserId)
                {
                    this._displayCurrentUserAccumulatedProfitRate = false;
                    this.lblAccumulatedIncomeRate.Text = "本年度个人累计收益率： " + CommonHelper.ConvertToPercentage(currentRankingModel.AccumulatedIncomeRate);
                }
            }

            //短差部排行信息
            this._dayRankingInfos = allRankingInfos.Where(x => x.Investor.DepartmentId == (int)EnumLibrary.AccountingDepartment.Day).ToList();
            if (this._dayRankingInfos.Any())
            {
                this.lciDayRankingBoard.Visibility = LayoutVisibility.Always;
                this.CreateIncomeRankingBoard(this.groupControlDay, this._dayRankingInfos);
            }

            //波段部排行信息
            this._bandRankingInfos = allRankingInfos.Where(x => x.Investor.DepartmentId == (int)EnumLibrary.AccountingDepartment.Band).ToList();
            if (this._bandRankingInfos.Any())
            {
                this.lciBandRaningBoard.Visibility = LayoutVisibility.Always;
                this.CreateIncomeRankingBoard(this.groupControlBand, this._bandRankingInfos);
            }
            //目标部排行信息
            this._targetRankingInfos = allRankingInfos.Where(x => x.Investor.DepartmentId == (int)EnumLibrary.AccountingDepartment.Target).ToList();
            if (this._targetRankingInfos.Any())
            {
                this.lciTargetRankingBoard.Visibility = LayoutVisibility.Always;
                this.CreateIncomeRankingBoard(this.groupControlTarget, this._targetRankingInfos);
            }

            //独立核算部排行信息
            this._independenceRankingInfos = allRankingInfos.Where(x => x.Investor.DepartmentId == (int)EnumLibrary.AccountingDepartment.Independence).ToList();
            if (this._independenceRankingInfos.Any())
            {
                this.lciIndependenceRankingBoard.Visibility = LayoutVisibility.Always;
                this.CreateIncomeRankingBoard(this.groupControlIndependence, this._independenceRankingInfos);
            }
        }

        private void CreateIncomeRankingBoard(GroupControl groupControl, IList<UserInvestIncomeRankingModel> rankingInfos)
        {
            groupControl.Controls.Clear();

            var source = rankingInfos.OrderByDescending(x => x.DayIncomeRate)
                .ThenByDescending(x => x.AverageDayIncomeRateOfWeek)
                .ThenByDescending(x => x.AverageDayIncomeRateOfMonth)
                .ToArray();

            if (this.chkWorking.Checked)
            {
                source = source.Where(x => !x.Investor.IsDeleted).ToArray();
            }

            int lineHeight = 30;
            int colWidth = 15;

            int noWidth = 25;
            int nameWith = 40;
            int rateWith = 50;

            LabelControl lblTitleNO = new LabelControl();
            lblTitleNO.Text = "排名";
            lblTitleNO.Appearance.FontStyleDelta = FontStyle.Bold;
            lblTitleNO.Width = noWidth;
            lblTitleNO.Location = new Point(colWidth, lineHeight);
            groupControl.Controls.Add(lblTitleNO);

            LabelControl lblTitleName = new LabelControl();
            lblTitleName.Text = "投资人";
            lblTitleName.Appearance.FontStyleDelta = FontStyle.Bold;
            lblTitleName.Width = nameWith;
            lblTitleName.Location = new Point(colWidth + lblTitleNO.Location.X + noWidth, lineHeight);
            groupControl.Controls.Add(lblTitleName);

            LabelControl lblTitleDailyIncomeRate = new LabelControl();
            lblTitleDailyIncomeRate.Text = "本日";
            lblTitleDailyIncomeRate.Appearance.FontStyleDelta = FontStyle.Bold;
            lblTitleDailyIncomeRate.Width = rateWith;
            lblTitleDailyIncomeRate.Location = new Point(colWidth + lblTitleName.Location.X + nameWith, lineHeight);
            groupControl.Controls.Add(lblTitleDailyIncomeRate);

            LabelControl lblTitleWeeklyIncomeRate = new LabelControl();
            lblTitleWeeklyIncomeRate.Text = "周平均";
            lblTitleWeeklyIncomeRate.Appearance.FontStyleDelta = FontStyle.Bold;
            lblTitleWeeklyIncomeRate.Width = rateWith;
            lblTitleWeeklyIncomeRate.Location = new Point(colWidth + lblTitleDailyIncomeRate.Location.X + nameWith, lineHeight);
            groupControl.Controls.Add(lblTitleWeeklyIncomeRate);

            LabelControl lblTitleMonthlyIncomeRate = new LabelControl();
            lblTitleMonthlyIncomeRate.Text = "月平均";
            lblTitleMonthlyIncomeRate.Appearance.FontStyleDelta = FontStyle.Bold;
            lblTitleMonthlyIncomeRate.Width = rateWith;
            lblTitleMonthlyIncomeRate.Location = new Point(colWidth + lblTitleWeeklyIncomeRate.Location.X + nameWith, lineHeight);
            groupControl.Controls.Add(lblTitleMonthlyIncomeRate);

            LabelControl lblTitleAccumulatedIncomeRate = new LabelControl();
            lblTitleAccumulatedIncomeRate.Text = "累计";
            lblTitleAccumulatedIncomeRate.Appearance.FontStyleDelta = FontStyle.Bold;
            lblTitleAccumulatedIncomeRate.Width = rateWith;
            lblTitleAccumulatedIncomeRate.Location = new Point(colWidth + lblTitleMonthlyIncomeRate.Location.X + nameWith, lineHeight);
            groupControl.Controls.Add(lblTitleAccumulatedIncomeRate);

            for (var index = 0; index < source.Length; index++)
            {
                LabelControl lblNo = new LabelControl();
                lblNo.Text = (index + 1).ToString();
                lblNo.Width = noWidth;
                lblNo.Location = new Point(colWidth, (index + 2) * lineHeight);
                groupControl.Controls.Add(lblNo);

                if (index == 0)
                {
                    lblNo.Appearance.FontStyleDelta = FontStyle.Bold;
                    lblNo.Appearance.ForeColor = Color.Goldenrod;
                }
                else if (index == 1)
                {
                    lblNo.Appearance.FontStyleDelta = FontStyle.Bold;
                    lblNo.Appearance.ForeColor = Color.Silver;
                }
                else if (index == 2)
                {
                    lblNo.Appearance.FontStyleDelta = FontStyle.Bold;
                    lblNo.Appearance.ForeColor = Color.DarkGoldenrod;
                }

                LabelControl lblName = new LabelControl();
                lblName.Text = source[index].Investor.Name;
                lblName.Width = nameWith;
                lblName.Location = new Point(colWidth + lblNo.Location.X + noWidth, (index + 2) * lineHeight);
                groupControl.Controls.Add(lblName);

                if (source[index].Investor.IsDeleted)
                {
                    lblName.Appearance.FontStyleDelta = FontStyle.Strikeout;
                }

                LabelControl lblDailyIncomeRate = new LabelControl();
                lblDailyIncomeRate.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblDailyIncomeRate.Text = CommonHelper.ConvertToPercentage(source[index].DayIncomeRate);
                lblDailyIncomeRate.Width = rateWith;
                lblDailyIncomeRate.Location = new Point(colWidth + lblName.Location.X + nameWith, (index + 2) * lineHeight);
                groupControl.Controls.Add(lblDailyIncomeRate);

                LabelControl lblWeeklyIncomeRate = new LabelControl();
                lblWeeklyIncomeRate.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblWeeklyIncomeRate.Text = CommonHelper.ConvertToPercentage(source[index].AverageDayIncomeRateOfWeek);
                lblWeeklyIncomeRate.Width = rateWith;
                lblWeeklyIncomeRate.Location = new Point(colWidth + lblDailyIncomeRate.Location.X + nameWith, (index + 2) * lineHeight);
                groupControl.Controls.Add(lblWeeklyIncomeRate);

                LabelControl lblMonthlyIncomeRate = new LabelControl();
                lblMonthlyIncomeRate.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblMonthlyIncomeRate.Text = CommonHelper.ConvertToPercentage(source[index].AverageDayIncomeRateOfMonth);
                lblMonthlyIncomeRate.Width = rateWith;
                lblMonthlyIncomeRate.Location = new Point(colWidth + lblWeeklyIncomeRate.Location.X + nameWith, (index + 2) * lineHeight);
                groupControl.Controls.Add(lblMonthlyIncomeRate);

                LabelControl lblAccumulatedIncomeRate = new LabelControl();
                lblAccumulatedIncomeRate.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lblAccumulatedIncomeRate.Text = CommonHelper.ConvertToPercentage(source[index].AccumulatedIncomeRate);
                lblAccumulatedIncomeRate.Width = rateWith;
                lblAccumulatedIncomeRate.Location = new Point(colWidth + lblMonthlyIncomeRate.Location.X + nameWith, (index + 2) * lineHeight);
                groupControl.Controls.Add(lblAccumulatedIncomeRate);
            }
        }

        private void RefreshRankingBoard()
        {
            //短差部收益排行
            if (LoginInfo.CurrentUser.IsAdmin || LoginInfo.CurrentUser.DepartmentId == (int)EnumLibrary.AccountingDepartment.Day)
            {
                if (this._dayRankingInfos.Any())
                    this.CreateIncomeRankingBoard(this.groupControlDay, this._dayRankingInfos);
            }

            //波段部收益排行
            if (LoginInfo.CurrentUser.IsAdmin || LoginInfo.CurrentUser.DepartmentId == (int)EnumLibrary.AccountingDepartment.Band)
            {
                if (this._bandRankingInfos.Any())
                    this.CreateIncomeRankingBoard(this.groupControlBand, this._bandRankingInfos);
            }

            //目标部收益排行
            if (LoginInfo.CurrentUser.IsAdmin || LoginInfo.CurrentUser.DepartmentId == (int)EnumLibrary.AccountingDepartment.Target)
            {
                if (this._targetRankingInfos.Any())
                    this.CreateIncomeRankingBoard(this.groupControlTarget, this._targetRankingInfos);
            }

            //独立核算部排行
            if (LoginInfo.CurrentUser.IsAdmin || LoginInfo.CurrentUser.DepartmentId == (int)EnumLibrary.AccountingDepartment.Independence)
            {
                if (this._independenceRankingInfos.Any())
                    this.CreateIncomeRankingBoard(this.groupControlIndependence, this._independenceRankingInfos);
            }
        }

        #endregion Invest Income Ranking

        #region Invest Income Data

        private void BindSearchInfo()
        {
            //开始交易日
            this.deStartTradeDate.Properties.AllowNullInput = DefaultBoolean.False;
            this.deStartTradeDate.EditValue = _initDate;

            //截至交易日
            this.deEndTradeDate.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deEndTradeDate.EditValue = now.Date.AddDays(-1);
            else
                this.deEndTradeDate.EditValue = now.Date;

            //交易类别
            var tradeTypes = this._dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            this.cbTradeType.Initialize(tradeTypes, displayAdditionalItem: true);

            //报表类型
            var reportTypes = new List<ComboBoxItemModel>();

            var dayReport = new ComboBoxItemModel
            {
                Text = "日报表",
                Value = "日",
            };
            var weekReport = new ComboBoxItemModel
            {
                Text = "周报表",
                Value = "周",
            };
            var monthReport = new ComboBoxItemModel
            {
                Text = "月报表",
                Value = "月",
            };
            reportTypes.Add(dayReport);
            reportTypes.Add(weekReport);
            reportTypes.Add(monthReport);

            this.cbReportType.Initialize(reportTypes, displayAdditionalItem: false);
            this.cbReportType.DefaultSelected("日");

            //部门
            var deptInfos = this._departmentService.GetAllAccountingDepartmentInfo()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);
        }

        private void BindInvestor(bool isOnWorking)
        {
            if (this._investors == null || !this._investors.Any()) return;

            var source = this._investors.Where(x => x.IsDeleted == !isOnWorking).OrderBy(x => x.Code).ToList();

            luInvestor.Initialize(source, "Code", "Name", showHeader: true, enableSearch: true);
        }

        private void AccessControl()
        {
            this.tabPane1.SelectedPage = this.tabPageData;

            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.luInvestor.EditValue = string.Empty;
                this.cbDepartment.SelectedIndex = 0;
                this.lblAccumulatedIncomeRate.Text = string.Empty;
            }
            else
            {
                this.cbDepartment.ReadOnly = true;
                this.cbDepartment.DefaultSelected(LoginInfo.CurrentUser.DepartmentId.ToString());
                this.luInvestor.ReadOnly = true;

                var investors = luInvestor.Properties.DataSource as List<UserInfo>;
                if (investors != null && investors.Any())
                {
                    if (investors.Exists(x => x.Code == LoginInfo.CurrentUser.UserCode))
                        this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                    else
                        this.luInvestor.EditValue = string.Empty;
                }

                //查询条件
                this.lciOnWorkingTop.Visibility = LayoutVisibility.Never;
                this.lciQuitTop.Visibility = LayoutVisibility.Never;

                //排行榜
                this.lciCheckAll.Visibility = LayoutVisibility.Never;
                this.lciCheckWorking.Visibility = LayoutVisibility.Never;
                this.lciDayRankingBoard.Visibility = LayoutVisibility.Never;
                this.lciBandRaningBoard.Visibility = LayoutVisibility.Never;
                this.lciTargetRankingBoard.Visibility = LayoutVisibility.Never;
                this.lciIndependenceRankingBoard.Visibility = LayoutVisibility.Never;

                this.lblAccumulatedIncomeRate.Text = string.Empty;
            }
        }

        private void DisplayResultData(IList<UserInvestIncomeEntity> incomeInfos)
        {
            this.layoutControlGroupDetail.Text = "[ " + incomeInfos.First().TradeTime.ToString("yyyy年MM月dd日") + "-" + incomeInfos.Last().TradeTime.ToString("yyyy年MM月dd日") + " ]" + "投资收益明细（金额单位：万元）";

            this.gridControl1.DataSource = null;

            var tradeTimeFormat = string.Empty;
            if (this._reportType == "日")
                tradeTimeFormat = "yyyy-MM-dd";
            else if (this._reportType == "周")
                tradeTimeFormat = "yyyy-MM-dd";
            else if (this._reportType == "月")
                tradeTimeFormat = "yyyy年MM月";

            var deptName = this.cbDepartment.Text.Trim();
            var tradeTypeName = this.cbTradeType.Text.Trim();

            var source = incomeInfos.Select(x => new UserInvestIncomeEntity
            {
                AccumulatedMarginAmount = CommonHelper.SetDecimalDigits(x.AccumulatedMarginAmount / _unitTenThousand),
                AccumulatedIncomeRate = CommonHelper.SetDecimalDigits(x.AccumulatedIncomeRate, 4),
                AccumulatedInterest = CommonHelper.SetDecimalDigits(x.AccumulatedInterest / _unitTenThousand),
                AccumulatedActualProfit = CommonHelper.SetDecimalDigits(x.AccumulatedActualProfit / _unitTenThousand),
                AccumulatedProfit = CommonHelper.SetDecimalDigits(x.AccumulatedProfit / _unitTenThousand),
                ActualMarginAmount = CommonHelper.SetDecimalDigits(x.ActualMarginAmount / _unitTenThousand),
                AllotFund = CommonHelper.SetDecimalDigits(x.AllotFund / _unitTenThousand),
                AnnualIncomeRate = CommonHelper.SetDecimalDigits(x.AnnualIncomeRate, 4),
                AnnualInterest = CommonHelper.SetDecimalDigits(x.AnnualInterest / _unitTenThousand),
                AnnualActualProfit = CommonHelper.SetDecimalDigits(x.AnnualActualProfit / _unitTenThousand),
                AnnualProfit = CommonHelper.SetDecimalDigits(x.AnnualProfit / _unitTenThousand),
                AverageMarginAmount = CommonHelper.SetDecimalDigits(x.AverageMarginAmount / _unitTenThousand),
                CurrentAsset = CommonHelper.SetDecimalDigits(x.CurrentAsset / _unitTenThousand),
                CurrentIncomeRate = CommonHelper.SetDecimalDigits(x.CurrentIncomeRate, 4),
                CurrentInterest = CommonHelper.SetDecimalDigits(x.CurrentInterest / _unitTenThousand, 4),
                CurrentActualProfit = CommonHelper.SetDecimalDigits(x.CurrentActualProfit / _unitTenThousand),
                CurrentProfit = CommonHelper.SetDecimalDigits(x.CurrentProfit / _unitTenThousand),
                DealAmount = CommonHelper.SetDecimalDigits(x.DealAmount / _unitTenThousand),
                DepartmentName = deptName,           
                FundOccupyAmount = CommonHelper.SetDecimalDigits(x.FundOccupyAmount / _unitTenThousand),
                Investor = x.Investor,
                MondayPositionValue = CommonHelper.SetDecimalDigits(x.MondayPositionValue / _unitTenThousand),
                PositionValue = CommonHelper.SetDecimalDigits(x.PositionValue / _unitTenThousand),
                PositionRate = CommonHelper.SetDecimalDigits(x.PositionRate, 4),
                PlanMarginAmount = CommonHelper.SetDecimalDigits(x.PlanMarginAmount / _unitTenThousand),
                TradeTime = x.TradeTime,
                TradeTypeName = tradeTypeName,
            }
            );

            this.gridControl1.DataSource = source;
        }

        private void CustomDrawGridView()
        {
            //周一
            this.colMondayPositionValue.Visible = _reportType == "日" ? true : false;

            //净资产（当前实际融资融券额 + 累计净收益额）
            this.colCurrentAsset.ToolTip = this.colActualMarginAmount.Caption + " + " + this.colAnnualActualProfit.Caption;
            //累计净收益额（累计收益额 - 累计利息）
            this.colAnnualActualProfit.ToolTip = this.colAnnualProfit.Caption + " - " + this.colAnnualInterest.Caption;
            //累计收益率（累计净收益额 / 平均融资融券额）
            this.colAnnualIncomeRate.ToolTip = this.colAnnualActualProfit.Caption + " / " + this.colAverageMarginAmount.Caption;
            //当前净收益额（当前收益额 - 当前利息）
            this.colCurrentActualProfit.ToolTip = this.colCurrentProfit.Caption + " - " + this.colCurrentInterest.Caption;
            //当前收益率（当前净收益额 / 当前实际融资融券额）
            this.colCurrentIncomeRate.ToolTip = this.colCurrentActualProfit.Caption + " / " + this.colActualMarginAmount.Caption;
            //持仓仓位（持仓市值 / 当前实际融资融券额）
            this.colPositionRate.ToolTip = this.colPositionValue.Caption + " / " + this.colActualMarginAmount.Caption;

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.gridView1.Columns)
            {
                if (column.Tag != null)
                {
                    var tag = column.Tag.ToString().Trim();
                    if (tag.IndexOf("{0}") > -1)
                        column.Caption = string.Format(tag, this._reportType);
                }
            }
        }

        private void DisplayDefaultSearch()
        {
            SetRiskControlValue();

            GetSearchResult();
        }

        private void GetSearchResult()
        {
            //查询开始交易日
            var startDate = CommonHelper.StringToDateTime(deStartTradeDate.EditValue.ToString());

            //查询截至交易日
            var endDate = CommonHelper.StringToDateTime(deEndTradeDate.EditValue.ToString());

            //交易类别
            var tradeType = int.Parse(cbTradeType.SelectedValue());

            //投资人员
            var selectedInvestor = this.luInvestor.GetSelectedDataRow() as UserInfo;

            if (selectedInvestor == null) return;

            var investors = new List<UserInfo>();

            if (string.IsNullOrEmpty(selectedInvestor.Code))
            {
                investors = (this.luInvestor.Properties.DataSource as List<UserInfo>).Where(x => !string.IsNullOrEmpty(x.Code)).ToList();
            }
            else
                investors.Add(selectedInvestor);

            if (!investors.Any()) return;

            var statisticalInvestorCodes = investors.Select(x => x.Code).ToArray();

            //交易记录
            var tradeRecords = _dailyRecordService.GetDailyRecords(tradeType: tradeType, beneficiaries: statisticalInvestorCodes, tradeDateFrom: startDate, tradeDateTo: endDate).ToList();

            if (tradeRecords == null || !tradeRecords.Any()) return;

            var investorInfo = new UserInfo()
            {
                Code = selectedInvestor.Code,
                Name = this.luInvestor.Text.Trim(),
                AllotFund = investors.Sum(x => x.AllotFund),
            };

            var queryDates = new List<DateTime>();
            if (this._reportType == "日")
            {
                //取得26个交易日日期
                queryDates = CommonHelper.GetWorkdaysBeforeCurrentDay(endDate, _tradeDateNumber + 1).OrderBy(x => x).ToList();
            }
            else if (this._reportType == "周")
            {
                //取得26个周周末日期
                queryDates = CommonHelper.GetLastDateOfWeekBeforeCurrentDate(endDate, _tradeDateNumber + 1).OrderBy(x => x).ToList();
            }
            else if (this._reportType == "月")
            {
                //取得26个月月末日期
                queryDates = CommonHelper.GetLastDateOfMonthBeforeCurrentDate(endDate, _tradeDateNumber + 1).OrderBy(x => x).ToList();
            }

            //交易记录中的所有股票代码
            var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
            //所有交易日期
            var tradeDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), endDate);

            //各交易日所有股票收盘价
            var stockClosePrices = this._tKLineService.GetStockClosePrices(tradeDates, stockFullCodes);

            var queryResult = this._statisticsReportService.CalculateUserInvestIncome(investorInfo, statisticalInvestorCodes, tradeRecords, queryDates, stockClosePrices);

            if (queryResult != null && queryResult.Any())
            {
                DisplayResultData(queryResult);
                DisplayResultChart(queryResult);
            }
        }

        #endregion Invest Income Data

        #region Invest Income Chart

        private void SetRiskControlValue()
        {
            if (string.IsNullOrEmpty(luInvestor.SelectedValue()) || int.Parse(cbTradeType.SelectedValue()) != (int)EnumLibrary.TradeType.Day)
            {
                _dailyRiskControlValue = -0.05M;
            }
            else
            {
                _dailyRiskControlValue = -0.005M;
            }
        }

        private void DisplayResultChart(IList<UserInvestIncomeEntity> source)
        {
            if (source == null || source.Count == 0)
            {
                this.chartControl1.Visible = false;
                return;
            }

            this.chartControl1.Visible = true;

            SetChartTitle();

            SetSeries(source);

            SetConstantLine(source[0].AllotFund);

            SetAxisX(source);

            SetAxisY(source);

            SetSecondAxisY(source);

            SetDiagram();

            SetLegend();
        }

        private void SetLegend()
        {
            this.chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.LeftOutside;
            this.chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            this.chartControl1.Legend.UseCheckBoxes = true;
            this.chartControl1.Legend.BackColor = Color.Linen;
            this.chartControl1.Legend.VerticalIndent = 10;
            this.chartControl1.Legend.TextOffset = 7;
        }

        private void SetChartTitle()
        {
            var suffix = this.cbDepartment.Text.Trim() + "-" + this.luInvestor.Text.Trim() + "-" + this.cbTradeType.Text.Trim();

            var title = @"投资收益表({0}{1})- {2}";
            title = string.Format(title, _tradeDateNumber, _reportType, suffix);

            ChartTitle chartTitle = new ChartTitle();
            //标题内容
            chartTitle.Text = title;
            //字体颜色
            chartTitle.TextColor = System.Drawing.Color.Black;
            //字体类型字号
            chartTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //标题对齐方式
            chartTitle.Dock = ChartTitleDockStyle.Top;
            chartTitle.Alignment = StringAlignment.Center;

            chartControl1.Titles.Clear();
            chartControl1.Titles.Add(chartTitle);
        }

        private void SetConstantLine(decimal allotFund)
        {
            #region X轴0坐标线

            _lineZeroCoordinateX = new ConstantLine("X轴0坐标线", 0);
            _lineZeroCoordinateX.Title.Visible = false;
            _lineZeroCoordinateX.Color = Color.DimGray;
            _lineZeroCoordinateX.ShowBehind = true;
            _lineZeroCoordinateX.ShowInLegend = false;

            #endregion X轴0坐标线

            #region 投入资金线

            var inputAmount = CommonHelper.SetDecimalDigits(allotFund / _unitHundredThousand);
            _lineInputAmountLine = new ConstantLine("投入资金线", inputAmount);
            _lineInputAmountLine.LineStyle.Thickness = 2;
            _lineInputAmountLine.LineStyle.DashStyle = DashStyle.DashDot;
            _lineInputAmountLine.Color = Color.Navy;
            _lineInputAmountLine.Title.Alignment = ConstantLineTitleAlignment.Near;
            _lineInputAmountLine.Title.TextColor = _lineInputAmountLine.Color;
            _lineInputAmountLine.ShowBehind = true;
            _lineInputAmountLine.ShowInLegend = true;
            _lineInputAmountLine.LegendText = "投入资金线(单位:十万元)";

            #endregion 投入资金线

            #region 基准线

            _lineBase = new ConstantLine("基准", 0);
            _lineBase.LineStyle.Thickness = 1;
            _lineBase.LineStyle.DashStyle = DashStyle.Dash;
            _lineBase.Color = Color.Gold;
            _lineBase.Title.ShowBelowLine = false;
            _lineBase.Title.Alignment = ConstantLineTitleAlignment.Far;
            _lineBase.Title.TextColor = Color.OrangeRed;
            _lineBase.ShowBehind = true;
            _lineBase.ShowInLegend = false;

            #endregion 基准线

            #region 日内分控线

            _lineDailyRiskyControl = new ConstantLine("日内分控线," + CommonHelper.ConvertToPercentage(_dailyRiskControlValue), _dailyRiskControlValue);
            _lineDailyRiskyControl.LineStyle.DashStyle = DashStyle.Dot;
            _lineDailyRiskyControl.Color = Color.Red;
            _lineDailyRiskyControl.Title.ShowBelowLine = true;
            _lineDailyRiskyControl.Title.Alignment = ConstantLineTitleAlignment.Far;
            _lineDailyRiskyControl.Title.TextColor = _lineDailyRiskyControl.Color;
            _lineDailyRiskyControl.ShowBehind = true;
            _lineDailyRiskyControl.ShowInLegend = true;
            _lineDailyRiskyControl.LegendText = "日内分控线";

            #endregion 日内分控线
        }

        private void SetDiagram()
        {
        }

        private void SetSecondAxisY(IList<UserInvestIncomeEntity> source)
        {
            SecondaryAxisY myAxisY = new SecondaryAxisY();

            //标题
            myAxisY.Title.Visibility = DefaultBoolean.True;
            myAxisY.Title.Text = "收益及风控类";
            myAxisY.Title.Font = new Font("Tahoma", 10, FontStyle.Bold);
            myAxisY.Title.Alignment = StringAlignment.Center;

            //百分比显示
            myAxisY.Label.TextPattern = "{V:0.0%}";

            //刻度格式设置
            myAxisY.Tickmarks.MinorVisible = false;

            var maxAccumulatedIncomeRate = source.Max(x => x.AccumulatedIncomeRate);
            var minAccumulatedIncomeRate = source.Min(x => x.AccumulatedIncomeRate);
            var maxDailyIncomeRate = source.Max(x => x.CurrentIncomeRate);
            var minDailyIncomeRate = source.Min(x => x.CurrentIncomeRate);

            var maxValue = maxAccumulatedIncomeRate > maxDailyIncomeRate ? maxAccumulatedIncomeRate : maxDailyIncomeRate;
            if (maxValue <= 0)
                maxValue = 0;

            var minValue = minAccumulatedIncomeRate > minDailyIncomeRate ? minDailyIncomeRate : minAccumulatedIncomeRate;

            if (minValue > _dailyRiskControlValue)
                minValue = _dailyRiskControlValue;

            myAxisY.WholeRange.Auto = false;
            myAxisY.WholeRange.SetMinMaxValues(minValue, maxValue);

            myAxisY.VisualRange.Auto = true;
            //myAxisY.VisualRange.SetMinMaxValues(minValue, maxValue);

            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(myAxisY);

            ((LineSeriesView)_seriesCurrentIncomeRatio.View).AxisY = myAxisY;
            ((LineSeriesView)_seriesAccumulatedIncomeRatio.View).AxisY = myAxisY;

            myAxisY.ConstantLines.Clear();
            myAxisY.ConstantLines.Add(_lineBase);

            if (_reportType == "日")
            {
                myAxisY.ConstantLines.Add(_lineDailyRiskyControl);
            }
        }

        private void SetAxisY(IList<UserInvestIncomeEntity> source)
        {
            var max1 = source.Select(x => x.CurrentAsset);

            AxisY myAxisY = ((XYDiagram)chartControl1.Diagram).AxisY;

            //标题
            myAxisY.Title.Visibility = DefaultBoolean.True;
            myAxisY.Title.Text = "资产类";
            myAxisY.Title.Font = new Font("Tahoma", 10, FontStyle.Bold);
            myAxisY.Title.Alignment = StringAlignment.Center;

            //Y轴颜色
            myAxisY.Color = Color.Black;

            //Y轴粗细
            myAxisY.Thickness = 3;

            //网格线
            myAxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            myAxisY.GridLines.Color = Color.WhiteSmoke;

            //刻度格式设置
            myAxisY.Tickmarks.MinorVisible = false;

            myAxisY.ConstantLines.Clear();

            if (_reportType != "日" || int.Parse(this.cbTradeType.SelectedValue()) == (int)EnumLibrary.TradeType.Target)
                myAxisY.ConstantLines.Add(_lineInputAmountLine);
            myAxisY.ConstantLines.Add(_lineZeroCoordinateX);
        }

        private void SetAxisX(IList<UserInvestIncomeEntity> source)
        {
            var valueCount = source.Count;
            AxisX myAxisX = ((XYDiagram)this.chartControl1.Diagram).AxisX;
            myAxisX.Label.Staggered = false;

            //标签倾斜角度
            myAxisX.Label.Angle = -45;
            myAxisX.Label.EnableAntialiasing = DefaultBoolean.True;

            //刻度格式设置
            //myAxisX.Tickmarks.Visible = false;
            myAxisX.Tickmarks.MinorVisible = false;

            //显示效果设置
            var maxDate = source.Max(x => x.TradeTime);
            var minDate = source.Min(x => x.TradeTime);

            myAxisX.WholeRange.Auto = false;
            myAxisX.WholeRange.AutoSideMargins = false;
            myAxisX.WholeRange.SideMarginsValue = 1.5D;
            myAxisX.WholeRange.SetMinMaxValues(minDate, maxDate.AddDays(5));
        }

        private void SetSeries(IList<UserInvestIncomeEntity> source)
        {
            #region create series

            // create series
            _seriesActualMarginAmount = new Series(this.colActualMarginAmount.Caption.Trim(), ViewType.Bar);
            _seriesAverageMarginAmount = new Series(this.colAverageMarginAmount.Caption.Trim(), ViewType.Spline);
            _seriesPositionValue = new Series(this.colPositionValue.Caption.Trim(), ViewType.Bar);
            _seriesCurrentAsset = new Series(this.colCurrentAsset.Caption.Trim(), ViewType.Spline);
            _seriesCurrentActualProfit = new Series(this.colCurrentActualProfit.Caption.Trim(), ViewType.Spline);
            _seriesAccumulatedActualProfit = new Series(this.colAnnualActualProfit.Caption.Trim(), ViewType.Spline);
            _seriesCurrentIncomeRatio = new Series(this.colCurrentIncomeRate.Caption.Trim(), ViewType.Spline);
            _seriesAccumulatedIncomeRatio = new Series(this.colAnnualIncomeRate.Caption.Trim(), ViewType.Spline);

            #endregion create series

            #region set series appearance

            #region 当前融券额

            _seriesActualMarginAmount.LegendText = this._seriesActualMarginAmount.Name.Trim() + "(单位:十万元)";
            _seriesActualMarginAmount.ShowInLegend = true;
            _seriesActualMarginAmount.View.Color = Color.White;
            _seriesActualMarginAmount.ArgumentScaleType = ScaleType.Qualitative;
            _seriesActualMarginAmount.ValueScaleType = ScaleType.Numerical;
            ((BarSeriesView)_seriesActualMarginAmount.View).BarWidth = 0.2;
            ((BarSeriesView)_seriesActualMarginAmount.View).Border.Thickness = 1;
            ((BarSeriesView)_seriesActualMarginAmount.View).Border.Color = Color.LightSteelBlue;
            ((BarSeriesView)_seriesActualMarginAmount.View).FillStyle.FillMode = FillMode.Solid;

            #endregion 当前融券额

            #region 平均融券额

            _seriesAverageMarginAmount.LegendText = this._seriesAverageMarginAmount.Name.Trim() + "(单位:十万元)";
            _seriesAverageMarginAmount.View.Color = Color.Navy;
            _seriesAverageMarginAmount.ArgumentScaleType = ScaleType.Qualitative;
            _seriesAverageMarginAmount.ValueScaleType = ScaleType.Numerical;
            _seriesAverageMarginAmount.LabelsVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesAverageMarginAmount.View).MarkerVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesAverageMarginAmount.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)_seriesAverageMarginAmount.View).LineMarkerOptions.Size = 3;
            ((LineSeriesView)_seriesAverageMarginAmount.View).LineStyle.DashStyle = DashStyle.DashDot;

            #endregion 平均融券额

            #region 持仓市值

            _seriesPositionValue.LegendText = this._seriesPositionValue.Name.Trim() + "(单位:十万元)";
            _seriesPositionValue.ShowInLegend = true;
            _seriesPositionValue.View.Color = Color.White;
            _seriesPositionValue.ArgumentScaleType = ScaleType.Qualitative;
            _seriesPositionValue.ValueScaleType = ScaleType.Numerical;
            ((BarSeriesView)_seriesPositionValue.View).BarWidth = 0.2;
            ((BarSeriesView)_seriesPositionValue.View).Border.Thickness = 1;
            ((BarSeriesView)_seriesPositionValue.View).Border.Color = Color.LightSteelBlue;
            ((BarSeriesView)_seriesPositionValue.View).FillStyle.FillMode = FillMode.Solid;

            #endregion 持仓市值

            #region 净资产

            _seriesCurrentAsset.LegendText = _seriesCurrentAsset.Name.Trim() + "(单位:十万元)";
            _seriesCurrentAsset.View.Color = Color.SteelBlue;
            _seriesCurrentAsset.ArgumentScaleType = ScaleType.Qualitative;
            _seriesCurrentAsset.ValueScaleType = ScaleType.Numerical;
            _seriesCurrentAsset.LabelsVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesCurrentAsset.View).MarkerVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesCurrentAsset.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)_seriesCurrentAsset.View).LineMarkerOptions.Size = 4;

            #endregion 净资产

            #region 当前净收益额

            _seriesCurrentActualProfit.LegendText = _seriesCurrentActualProfit.Name.Trim() + "(单位:万元)";
            _seriesCurrentActualProfit.View.Color = Color.FromArgb(86, 156, 214);
            _seriesCurrentActualProfit.ArgumentScaleType = ScaleType.Qualitative;
            _seriesCurrentActualProfit.ValueScaleType = ScaleType.Numerical;
            _seriesCurrentActualProfit.LabelsVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesCurrentActualProfit.View).MarkerVisibility = DefaultBoolean.True;
            ((LineSeriesView)_seriesCurrentActualProfit.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)_seriesCurrentActualProfit.View).LineMarkerOptions.Size = 5;

            #endregion 当前净收益额

            #region 累计净收益额

            _seriesAccumulatedActualProfit.LegendText = _seriesAccumulatedActualProfit.Name.Trim() + "(单位:万元)";
            _seriesAccumulatedActualProfit.View.Color = Color.MidnightBlue;
            _seriesAccumulatedActualProfit.ArgumentScaleType = ScaleType.Qualitative;
            _seriesAccumulatedActualProfit.ValueScaleType = ScaleType.Numerical;
            _seriesAccumulatedActualProfit.LabelsVisibility = DefaultBoolean.False;

            ((LineSeriesView)_seriesAccumulatedActualProfit.View).MarkerVisibility = DefaultBoolean.True;
            ((LineSeriesView)_seriesAccumulatedActualProfit.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)_seriesAccumulatedActualProfit.View).LineMarkerOptions.Size = 6;

            #endregion 累计净收益额

            #region 当前收益率

            _seriesCurrentIncomeRatio.LegendText = _seriesCurrentIncomeRatio.Name.Trim();
            _seriesCurrentIncomeRatio.View.Color = Color.Orange;
            _seriesCurrentIncomeRatio.ArgumentScaleType = ScaleType.Qualitative;
            _seriesCurrentIncomeRatio.ValueScaleType = ScaleType.Numerical;
            _seriesCurrentIncomeRatio.LabelsVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesCurrentIncomeRatio.View).MarkerVisibility = DefaultBoolean.True;
            ((LineSeriesView)_seriesCurrentIncomeRatio.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)_seriesCurrentIncomeRatio.View).LineMarkerOptions.Size = 5;

            #endregion 当前收益率

            #region 累计收益率

            _seriesAccumulatedIncomeRatio.LegendText = _seriesAccumulatedIncomeRatio.Name.Trim();
            _seriesAccumulatedIncomeRatio.View.Color = Color.Coral;
            _seriesAccumulatedIncomeRatio.ArgumentScaleType = ScaleType.Qualitative;
            _seriesAccumulatedIncomeRatio.ValueScaleType = ScaleType.Numerical;
            _seriesAccumulatedIncomeRatio.LabelsVisibility = DefaultBoolean.False;
            ((LineSeriesView)_seriesAccumulatedIncomeRatio.View).MarkerVisibility = DefaultBoolean.True;
            ((LineSeriesView)_seriesAccumulatedIncomeRatio.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)_seriesAccumulatedIncomeRatio.View).LineMarkerOptions.Size = 8;

            #endregion 累计收益率

            #endregion set series appearance

            #region set series data

            foreach (var item in source)
            {
                string argument = string.Empty;

                if (this._reportType == "日")
                    argument = item.TradeTime.ToShortDateString().Substring(2);
                else if (this._reportType == "周")
                    argument = item.TradeTime.ToShortDateString().Substring(2);
                else if (this._reportType == "月")
                    argument = item.TradeTime.ToString("yyyy年MM月");

                //当前实际融资融券额（十万）
                var actualMarginAmount = CommonHelper.SetDecimalDigits(item.ActualMarginAmount / _unitHundredThousand);
                var pointActualMarginAmount = new SeriesPoint(argument, actualMarginAmount);
                if (item.TradeTime.DayOfWeek == DayOfWeek.Monday)
                {
                    pointActualMarginAmount.Color = ((BarSeriesView)_seriesActualMarginAmount.View).Border.Color;
                }
                _seriesActualMarginAmount.Points.Add(pointActualMarginAmount);

                //平均融资融券额（十万）
                var averageMarginAmount = CommonHelper.SetDecimalDigits(item.AverageMarginAmount / _unitHundredThousand);
                _seriesAverageMarginAmount.Points.Add(new SeriesPoint(argument, averageMarginAmount));

                //持仓市值（十万）
                var positionValue = CommonHelper.SetDecimalDigits(item.PositionValue / _unitHundredThousand);
                var pointPositionValue = new SeriesPoint(argument, positionValue);
                if (item.TradeTime.DayOfWeek == DayOfWeek.Monday)
                {
                    pointPositionValue.Color = ((BarSeriesView)_seriesPositionValue.View).Border.Color;
                }

                _seriesPositionValue.Points.Add(pointPositionValue);

                //净资产（十万）
                var currentAsset = CommonHelper.SetDecimalDigits(item.CurrentAsset / _unitHundredThousand);
                _seriesCurrentAsset.Points.Add(new SeriesPoint(argument, currentAsset));

                //当前净收益额（万）
                var currentActualProfit = CommonHelper.SetDecimalDigits(item.CurrentActualProfit / _unitTenThousand);
                _seriesCurrentActualProfit.Points.Add(new SeriesPoint(argument, currentActualProfit));

                //累计净收益额（万）
                var accumulatedActualProfit = CommonHelper.SetDecimalDigits(item.AccumulatedActualProfit / _unitTenThousand);
                _seriesAccumulatedActualProfit.Points.Add(new SeriesPoint(argument, accumulatedActualProfit));

                //当前收益率
                var currentIncomeRate = CommonHelper.SetDecimalDigits(item.CurrentIncomeRate, 4);
                _seriesCurrentIncomeRatio.Points.Add(new SeriesPoint(argument, currentIncomeRate));

                //累计收益率
                var accumulatedIncomeRate = CommonHelper.SetDecimalDigits(item.AccumulatedIncomeRate, 4);
                _seriesAccumulatedIncomeRatio.Points.Add(new SeriesPoint(argument, accumulatedIncomeRate));
            }

            #endregion set series data

            chartControl1.Series.Clear();

            switch ((EnumLibrary.TradeType)int.Parse(this.cbTradeType.SelectedValue()))
            {
                case EnumLibrary.TradeType.Day:

                    //chartControl1.Series.Add(_seriesPositionValue);
                    chartControl1.Series.Add(_seriesActualMarginAmount);
                    chartControl1.Series.Add(_seriesAverageMarginAmount);
                    //chartControl1.Series.Add(_seriesCurrentAsset);
                    chartControl1.Series.Add(_seriesCurrentActualProfit);
                    chartControl1.Series.Add(_seriesCurrentIncomeRatio);
                    chartControl1.Series.Add(_seriesAccumulatedActualProfit);
                    chartControl1.Series.Add(_seriesAccumulatedIncomeRatio);
                    break;

                case EnumLibrary.TradeType.Band:
                    chartControl1.Series.Add(_seriesActualMarginAmount);
                    chartControl1.Series.Add(_seriesAverageMarginAmount);
                    chartControl1.Series.Add(_seriesCurrentAsset);
                    chartControl1.Series.Add(_seriesCurrentActualProfit);
                    chartControl1.Series.Add(_seriesCurrentIncomeRatio);
                    chartControl1.Series.Add(_seriesAccumulatedActualProfit);
                    chartControl1.Series.Add(_seriesAccumulatedIncomeRatio);
                    break;

                case EnumLibrary.TradeType.Target:
                    chartControl1.Series.Add(_seriesActualMarginAmount);
                    chartControl1.Series.Add(_seriesAverageMarginAmount);
                    chartControl1.Series.Add(_seriesCurrentAsset);
                    chartControl1.Series.Add(_seriesCurrentActualProfit);
                    chartControl1.Series.Add(_seriesCurrentIncomeRatio);
                    chartControl1.Series.Add(_seriesAccumulatedActualProfit);
                    chartControl1.Series.Add(_seriesAccumulatedIncomeRatio);
                    break;

                case EnumLibrary.TradeType.All:
                    chartControl1.Series.Add(_seriesActualMarginAmount);
                    chartControl1.Series.Add(_seriesAverageMarginAmount);
                    chartControl1.Series.Add(_seriesCurrentAsset);
                    chartControl1.Series.Add(_seriesCurrentActualProfit);
                    chartControl1.Series.Add(_seriesCurrentIncomeRatio);
                    chartControl1.Series.Add(_seriesAccumulatedActualProfit);
                    chartControl1.Series.Add(_seriesAccumulatedIncomeRatio);
                    break;
            }
        }

        #endregion Invest Income Chart

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomFlow_Load(object sender, EventArgs e)
        {
            try
            {
                BindSearchInfo();

                AccessControl();

                CustomDrawGridView();

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showFilterPanel: true, showAutoFilterRow: true, showCheckBoxRowSelect: false);

                this.ActiveControl = this.btnSearch;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #region Chart

        private void chartControl1_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElementGroup elementGroup in e.CrosshairElementGroups)
            {
                foreach (CrosshairElement element in elementGroup.CrosshairElements)
                {
                    if (element.Series.Name == _seriesCurrentIncomeRatio.Name || element.Series.Name == _seriesAccumulatedIncomeRatio.Name)
                    {
                        SeriesPoint point = element.SeriesPoint;

                        var values = point.Values;
                        element.LabelElement.Text = string.Format(element.Series.Name + " : {0}", CommonHelper.ConvertToPercentage(values[0]));
                    }
                }
            }
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            //   e.SecondLabelText = "xxxxxx";
        }

        #endregion Chart

        #region Data

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            gridView1.SaveLayout(_layoutXmlName);
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._reportType = cbReportType.SelectedValue();
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.gridControl1.DataSource = null;

                if (this.tabPane1.SelectedPage == this.tabPageRanking)
                    this.tabPane1.SelectedPage = this.tabPageChart;

                this.CustomDrawGridView();

                this.SetRiskControlValue();

                this.GetSearchResult();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.gridControl1.DataSource = null;
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //投资人员
            var selectedDeptId = int.Parse(this.cbDepartment.SelectedValue());
            this._investors = this._userService.GetUserDetails(departmentId: selectedDeptId);
            var working = new UserInfo
            {
                Code = string.Empty,
                Name = "在职人员",
                IsDeleted = false,
            };
            var quit = new UserInfo
            {
                Code = string.Empty,
                Name = "离职人员",
                IsDeleted = true,
            };
            this._investors.Add(working);
            this._investors.Add(quit);

            var isOnWorking = this.chkQuit.Checked ? false : true;

            var souce = this._investors.Where(x => x.IsDeleted == !isOnWorking).OrderBy(x => x.Code).ToList();
            this.luInvestor.Initialize(souce, "Code", "Name", showHeader: true, enableSearch: true);

            var tradeType = (int)CTMHelper.GetTradeTypeByDepartment(selectedDeptId);

            this.cbTradeType.DefaultSelected(tradeType.ToString());
        }

        private void chkWorkingTop_CheckedChanged(object sender, EventArgs e)
        {
            this.chkQuit.Checked = !this.chkWorkingTop.Checked;

            if (this.chkWorkingTop.Checked)
                BindInvestor(true);
        }

        private void chkQuit_CheckedChanged(object sender, EventArgs e)
        {
            this.chkWorkingTop.Checked = !this.chkQuit.Checked;
            if (this.chkQuit.Checked)
                BindInvestor(false);
        }

        #endregion Data

        #region Ranking

        /// <summary>
        /// Tab 标签页切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            try
            {
                if (e.Page.Caption == tabPageRanking.Caption)
                {
                    if (_rankingFirstTime)
                    {
                        this.deRankingDate.Properties.AllowNullInput = DefaultBoolean.False;

                        this.deRankingDate.SetFormat("yyyy年MM月dd日");

                        var now = DateTime.Now;

                        var workDays = CommonHelper.GetWorkdaysBeforeCurrentDay(now.Date, 2).OrderBy(x => x).ToList();

                        if (now.Hour < 15)
                            this.deRankingDate.EditValue = workDays.First();
                        else
                            this.deRankingDate.EditValue = workDays.Last();

                        var rankingDate = CommonHelper.StringToDateTime(this.deRankingDate.EditValue.ToString());

                        // this.DisplayCurrentUserAccumulatedIncomeRate();

                        this.DisplayInvestIncomeRanking(rankingDate);

                        _rankingFirstTime = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 更新排行榜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateRanking_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnUpdateRanking.Enabled = false;

                this._dayRankingInfos = null;
                this._bandRankingInfos = null;
                this._targetRankingInfos = null;

                var rankingDate = CommonHelper.StringToDateTime(this.deRankingDate.EditValue.ToString());

                this.DisplayInvestIncomeRanking(rankingDate);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnUpdateRanking.Enabled = true;
            }
        }

        private void deRankingDate_EditValueChanged(object sender, EventArgs e)
        {
            var rankingDate = CommonHelper.StringToDateTime(this.deRankingDate.EditValue.ToString());

            if (rankingDate.DayOfWeek == DayOfWeek.Saturday)
                this.deRankingDate.EditValue = rankingDate.AddDays(-1);
            else if (rankingDate.DayOfWeek == DayOfWeek.Sunday)
                this.deRankingDate.EditValue = rankingDate.AddDays(-2);
        }

        private void chkWorking_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAll.Checked = !this.chkWorking.Checked;

            RefreshRankingBoard();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.chkWorking.Checked = !this.chkAll.Checked;

            RefreshRankingBoard();
        }

        #endregion Ranking

        #endregion Events
    }
}