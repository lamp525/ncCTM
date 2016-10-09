using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Department;
using CTM.Services.StatisticsReport;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Function.StatisticsReport
{
    public partial class FrmUserInvestIncomeMonthly : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly ITKLineService _tKLineService;
        private readonly IDailyStatisticsReportService _statisticsReportService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmUserInvestIncomeMonthly";

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeMonthly(
            IDailyRecordService dailyRecordService,
            IUserService userService,
            IDepartmentService departmentService,
            ITKLineService tKLineService,
            IDailyStatisticsReportService statisticsReportService
            )
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._userService = userService;
            this._departmentService = departmentService;
            this._tKLineService = tKLineService;
            this._statisticsReportService = statisticsReportService;
        }

        #endregion Constructors

        #region Utilities

        private void BindSearchInfo()
        {
            //交易月份
            this.deTradeDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTradeDate.EditValue = DateTime.Now.Date;
            this.deTradeDate.SetFormat("yyyy年MM月");

            //部门
            var deptInfos = this._departmentService.GetAllAccountingDepartmentInfo()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);
            this.cbDepartment.DefaultSelected(((int)EnumLibrary.AccountingDepartment.Day).ToString());

            //利率信息
            var apr = AppConfigHelper.MarginTradingAPR;
            this.lblInterest.Text = "融资融券年利率： " + CommonHelper.ConvertToPercentage(apr);
        }

        private void AccessControl()
        {
            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.cbDepartment.SelectedIndex = 0;
            }
            else
            {
                this.cbDepartment.DefaultSelected(LoginInfo.CurrentUser.DepartmentId.ToString());
                this.lciDepartment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private IList<UserInvestIncomeEntity> CalculateUserDailyInvestIncome(DateTime searchDate, int deptId)
        {
            var result = new List<UserInvestIncomeEntity>();

            var firstDay = CommonHelper.GetFirstDayOfMonth(searchDate);
            var lastDay = CommonHelper.GetLastDayOfMonth(searchDate);

            if (lastDay > DateTime.Now.Date)
                lastDay = DateTime.Now.Date;

            IList<UserInfo> beneficiaryInfos = new List<UserInfo>();

            if (LoginInfo.CurrentUser.IsAdmin)
                beneficiaryInfos = this._userService.GetUserInfos(departmentIds: new int[] { deptId }).Where(x => x.IsDeleted == false).ToList();
            else
                beneficiaryInfos.Add(_userService.GetUserInfoById(LoginInfo.CurrentUser.UserId));

            var beneficiaries = beneficiaryInfos.Select(x => x.Code).Distinct().ToArray();

            //交易记录
            var tradeRecords = _dailyRecordService.GetDailyRecords(beneficiaries: beneficiaries, tradeDateFrom: _initDate, tradeDateTo: lastDay).ToList();

            if (!tradeRecords.Any()) return result;

            //所有股票收盘价
            var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
            var queryDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), lastDay);
            var stockClosePrices = this._tKLineService.GetStockClosePrices(queryDates, stockFullCodes);

            var statisticalDates = CommonHelper.GetAllWorkDays(firstDay.AddDays(-1), lastDay);

            foreach (var user in beneficiaryInfos)
            {
                //当前用户交易记录
                var userRecords = tradeRecords.Where(x => x.Beneficiary == user.Code).ToList();
                var userStockCodes = userRecords.Select(x => x.StockCode).Distinct().ToList();
                var userStockClosePrices = stockClosePrices.Where(x => userStockCodes.Contains(x.StockCode.Trim())).ToList();
                var statisticalInvestorCodes = new List<string> { user.Code };

                var dailyInvestIncomes = this._statisticsReportService.CalculateUserInvestIncome(user, statisticalInvestorCodes, userRecords, statisticalDates, userStockClosePrices);

                if (dailyInvestIncomes.Any())
                {
                    //月日均融资融券额
                    // var currentAverageMarginAmount = dailyInvestIncomes.Sum(x => x.ActualMarginAmount) / dailyInvestIncomes.Count;
                    var currentAverageMarginAmount = dailyInvestIncomes.CalculatePeriodAverageMarginAmount();
                    //月实际收益额
                    var currentActualProfit = dailyInvestIncomes.Sum(x => x.CurrentActualProfit);

                    //月收益率
                    var currentIncomeRate = CommonHelper.CalculateRate(currentActualProfit, currentAverageMarginAmount);

                    var currentUserIncome = new UserInvestIncomeEntity
                    {
                        //月末累计实际收益额
                        AccumulatedActualProfit = dailyInvestIncomes.Last().AccumulatedActualProfit,
                        //月末累计收益率
                        AccumulatedIncomeRate = dailyInvestIncomes.Last().AccumulatedIncomeRate,
                        //月末累计利息
                        AccumulatedInterest = dailyInvestIncomes.Last().AccumulatedInterest,
                        //月末累计融资融券额
                        AccumulatedMarginAmount = dailyInvestIncomes.Last().AccumulatedMarginAmount,
                        //月末累计收益额
                        AccumulatedProfit = dailyInvestIncomes.Last().AccumulatedProfit,
                        //本月累计实际融资融券额
                        ActualMarginAmount = dailyInvestIncomes.Sum(x => x.ActualMarginAmount),
                        AllotFund = dailyInvestIncomes.Last().AllotFund,
                        //本月日均融资融券额
                        AverageMarginAmount = currentAverageMarginAmount,
                        //本月实际收益额
                        CurrentActualProfit = currentActualProfit,
                        //月末资产
                        CurrentAsset = dailyInvestIncomes.Last().CurrentAsset,
                        //本月收益率
                        CurrentIncomeRate = currentIncomeRate,
                        //本月累计利息
                        CurrentInterest = dailyInvestIncomes.Sum(x => x.CurrentInterest),
                        //本月累计收益额
                        CurrentProfit = dailyInvestIncomes.Sum(x => x.CurrentProfit),
                        //本月累计成交额
                        DealAmount = dailyInvestIncomes.Sum(x => x.DealAmount),
                        FundOccupyAmount = dailyInvestIncomes.Last().FundOccupyAmount,
                        Investor = user.Name,
                        //本月累计计划融资融券额
                        PlanMarginAmount = dailyInvestIncomes.Sum(x => x.PlanMarginAmount),
                        //月末持仓率
                        PositionRate = dailyInvestIncomes.Last().PositionRate,
                        //月末持仓市值
                        PositionValue = dailyInvestIncomes.Last().PositionValue,
                        TradeTime = dailyInvestIncomes.Last().TradeTime,
                    };

                    result.Add(currentUserIncome);
                }
            }

            return result;
        }

        private IList<UserInvestIncomeEntity> DataFormat(IList<UserInvestIncomeEntity> source)
        {
            var result = new List<UserInvestIncomeEntity>();

            if (source != null || source.Any())
            {
                decimal unit = (int)EnumLibrary.NumericUnit.TenThousand;
                var deptName = this.cbDepartment.Text.Trim();

                result = source.Select(x => new UserInvestIncomeEntity
                {
                    AccumulatedMarginAmount = CommonHelper.SetDecimalDigits(x.AccumulatedMarginAmount / unit),
                    AccumulatedIncomeRate = CommonHelper.SetDecimalDigits(x.AccumulatedIncomeRate, 4),
                    AccumulatedInterest = CommonHelper.SetDecimalDigits(x.AccumulatedInterest / unit),
                    AccumulatedActualProfit = CommonHelper.SetDecimalDigits(x.AccumulatedActualProfit / unit),
                    AccumulatedProfit = CommonHelper.SetDecimalDigits(x.AccumulatedProfit / unit),
                    ActualMarginAmount = CommonHelper.SetDecimalDigits(x.ActualMarginAmount / unit),
                    AllotFund = CommonHelper.SetDecimalDigits(x.AllotFund / unit),
                    AverageMarginAmount = CommonHelper.SetDecimalDigits(x.AverageMarginAmount / unit),
                    CurrentAsset = CommonHelper.SetDecimalDigits(x.CurrentAsset / unit),
                    CurrentIncomeRate = CommonHelper.SetDecimalDigits(x.CurrentIncomeRate, 4),
                    CurrentInterest = CommonHelper.SetDecimalDigits(x.CurrentInterest),
                    CurrentActualProfit = CommonHelper.SetDecimalDigits(x.CurrentActualProfit / unit),
                    CurrentProfit = CommonHelper.SetDecimalDigits(x.CurrentProfit / unit),
                    DealAmount = CommonHelper.SetDecimalDigits(x.DealAmount / unit),
                    DepartmentName = deptName,
                    FundOccupyAmount = CommonHelper.SetDecimalDigits(x.FundOccupyAmount / unit),
                    Investor = x.Investor,
                    PositionValue = CommonHelper.SetDecimalDigits(x.PositionValue / unit),
                    PositionRate = CommonHelper.SetDecimalDigits(x.PositionRate, 4),
                    PlanMarginAmount = CommonHelper.SetDecimalDigits(x.PlanMarginAmount / unit),
                    TradeTime = x.TradeTime,
                }).ToList();
            }

            return result;
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeDaily_Load(object sender, EventArgs e)
        {
            BindSearchInfo();
            AccessControl();

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showCheckBoxRowSelect: false);

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                //查询日期
                var searchDate = CommonHelper.StringToDateTime(this.deTradeDate.EditValue.ToString());

                //部门ID
                var deptId = int.Parse(this.cbDepartment.SelectedValue());

                var investIncomes = this.DataFormat(this.CalculateUserDailyInvestIncome(searchDate, deptId)).OrderBy(x => x.Investor);

                this.gridControl1.DataSource = investIncomes;
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
            this.gridView1.SaveLayout(_layoutXmlName);
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

        #endregion Events
    }
}