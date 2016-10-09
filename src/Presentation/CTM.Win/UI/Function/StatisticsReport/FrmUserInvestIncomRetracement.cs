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
    public partial class FrmUserInvestIncomeRetracement : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IDailyStatisticsReportService _statisticsReportService;
        private readonly ITKLineService _tKLineService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmUserInvestIncomeRetracement";

        private IList<UserInvestIncomeRetracementModel> _retracementInfos = null;

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeRetracement(
            IDailyRecordService dailyRecordService,
            IUserService userService,
            IDepartmentService departmentService,
            IDailyStatisticsReportService statisticsReportService,
            ITKLineService tKLineService
            )
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._userService = userService;
            this._departmentService = departmentService;
            this._statisticsReportService = statisticsReportService;
            this._tKLineService = tKLineService;
        }

        #endregion Constructors

        #region Utilities

        private void BindSearchInfo()
        {
            //查询时间
            this.deStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deStartDate.SetFormat("yyyy/MM/dd");
            this.deEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEndDate.SetFormat("yyyy/MM/dd");

            var now = DateTime.Now;
            this.deStartDate.EditValue = CommonHelper.GetFirstDayOfMonth(now.Date);

            if (now.Hour < 15)
                this.deEndDate.EditValue = now.Date.AddDays(-1);
            else
                this.deEndDate.EditValue = now.Date;

            var deptInfos = _departmentService.GetAllAccountingDepartmentInfo()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);
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
                this.lciAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciOnWorking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private IList<UserInvestIncomeRetracementModel> CalculateUserRetracement(DateTime startDate, DateTime endDate, int deptId)
        {
            var result = new List<UserInvestIncomeRetracementModel>();

            IList<UserInfo> beneficiaryInfos = new List<UserInfo>();

            if (LoginInfo.CurrentUser.IsAdmin)
                beneficiaryInfos = _userService.GetUserInfos(departmentIds: new int[] { deptId });
            else
                beneficiaryInfos.Add(_userService.GetUserInfoByCode(LoginInfo.CurrentUser.UserCode));

            var beneficiaryCodes = beneficiaryInfos.Select(x => x.Code).ToArray();

            //交易记录
            var tradeRecords = _dailyRecordService.GetDailyRecords(beneficiaries: beneficiaryCodes, tradeDateFrom: _initDate, tradeDateTo: endDate).ToList();

            if (!tradeRecords.Any()) return result;

            //股票收盘价
            var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
            var queryDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), endDate);

            var a = DateTime.Now;
            var stockClosePrices = this._tKLineService.GetStockClosePrices(queryDates, stockFullCodes);
            var b = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("【" + _layoutXmlName + "】股票价格查询时间： " + (b - a));

            foreach (var user in beneficiaryInfos)
            {
                //当前用户交易记录
                var userRecords = tradeRecords.Where(x => x.Beneficiary == user.Code).ToList();
                var userStockCodes = userRecords.Select(x => x.StockCode).Distinct().ToList();
                var userStockClosePrices = stockClosePrices.Where(x => userStockCodes.Contains(x.StockCode.Trim())).ToList();
                var statisticalInvestorCodes = new List<string> { user.Code };
                var dailyInvestIncomes = this._statisticsReportService.CalculateUserInvestIncome(user, statisticalInvestorCodes, userRecords, queryDates, userStockClosePrices);
                if (!dailyInvestIncomes.Any()) continue;

                //计算最大回撤金额
                decimal maxRetracementAmount = 0;
                foreach (var dayIncome in dailyInvestIncomes)
                {
                    if (dayIncome.CurrentProfit < 0)
                    {
                        var previousMaxAccumulatedProfit = dailyInvestIncomes.Where(x => x.TradeTime < dayIncome.TradeTime.AddDays(1)).Max(x => x.AccumulatedProfit);
                        var currentRetracementAmount = previousMaxAccumulatedProfit > dayIncome.AccumulatedProfit ? dayIncome.AccumulatedProfit - previousMaxAccumulatedProfit : dayIncome.CurrentProfit;
                        if (currentRetracementAmount < maxRetracementAmount)
                            maxRetracementAmount = currentRetracementAmount;
                    }
                }

                var periodDailyInvestIncomes = dailyInvestIncomes.Where(x => x.TradeTime > startDate.AddDays(-1) && x.TradeTime < endDate.AddDays(1)).ToList();

                //计算区间最大回撤金额
                decimal periodMaxRetracementAmount = 0;
                foreach (var dayIncome in periodDailyInvestIncomes)
                {
                    if (dayIncome.CurrentProfit < 0)
                    {
                        var previousMaxAccumulatedProfit = periodDailyInvestIncomes.Where(x => x.TradeTime < dayIncome.TradeTime.AddDays(1)).Max(x => x.AccumulatedProfit);
                        var currentRetracementAmount = previousMaxAccumulatedProfit > dayIncome.AccumulatedProfit ? dayIncome.AccumulatedProfit - previousMaxAccumulatedProfit : dayIncome.CurrentProfit;
                        if (currentRetracementAmount < periodMaxRetracementAmount)
                            periodMaxRetracementAmount = currentRetracementAmount;
                    }
                }

                //期末收益信息
                var endDailyInvestIncome = dailyInvestIncomes.Last();

                var userRetracementInfo = new UserInvestIncomeRetracementModel
                {
                    Investor = user.Name,
                    DepartmentName = this.cbDepartment.Text.Trim(),
                    IsOnWorking = !user.IsDeleted,
                    TradeDate = this.deStartDate.Text.Trim() + " - " + this.deEndDate.Text.Trim(),
                    PeriodProfit = CommonHelper.SetDecimalDigits(periodDailyInvestIncomes.Sum(x => x.CurrentProfit)),
                    PeriodAverageMarginAmount = CommonHelper.SetDecimalDigits(periodDailyInvestIncomes.CalculatePeriodAverageMarginAmount()),
                    PeriodInterest = CommonHelper.SetDecimalDigits(periodDailyInvestIncomes.Sum(x => x.CurrentInterest)),
                    PeriodActualProfit = CommonHelper.SetDecimalDigits(periodDailyInvestIncomes.Sum(x => x.CurrentInterest)),
                    PeriodMaxRetracementAmount = CommonHelper.SetDecimalDigits(periodMaxRetracementAmount),
                    AccumulatedProfit = CommonHelper.SetDecimalDigits(endDailyInvestIncome.AccumulatedProfit),
                    AverageMarginAmount = CommonHelper.SetDecimalDigits(endDailyInvestIncome.AverageMarginAmount),
                    AccumulatedInterest = CommonHelper.SetDecimalDigits(endDailyInvestIncome.AccumulatedInterest),
                    AccumulatedActualProfit = CommonHelper.SetDecimalDigits(endDailyInvestIncome.AccumulatedActualProfit),
                    MaxRetracementAmount = CommonHelper.SetDecimalDigits(maxRetracementAmount),
                };

                result.Add(userRetracementInfo);
            }

            return result;
        }

        private void RefreshRetracementInfo()
        {
            if (_retracementInfos == null) return;

            var source = this.chkOnWorking.Checked ? _retracementInfos.Where(x => x.IsOnWorking).ToList() : _retracementInfos;

            this.gridControl1.DataSource = source;
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeRetracement_Load(object sender, EventArgs e)
        {
            try
            {
                BindSearchInfo();

                AccessControl();

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showCheckBoxRowSelect: false);

                this.ActiveControl = this.btnSearch;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                _retracementInfos = null;

                //查询日期
                var startDate = CommonHelper.StringToDateTime(this.deStartDate.EditValue.ToString());
                var endDate = CommonHelper.StringToDateTime(this.deEndDate.EditValue.ToString());

                //部门ID
                var deptId = int.Parse(cbDepartment.SelectedValue());

                _retracementInfos = CalculateUserRetracement(startDate, endDate, deptId).OrderBy(x => x.Investor).ToList();

                var source = this.chkOnWorking.Checked ? _retracementInfos.Where(x => x.IsOnWorking).ToList() : _retracementInfos;

                this.gridControl1.DataSource = source;
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

        private void deEndDate_EditValueChanged(object sender, EventArgs e)
        {
            var now = DateTime.Now.Date;

            var endDate = CommonHelper.StringToDateTime(this.deEndDate.EditValue.ToString());

            if (endDate > now)
                this.deEndDate.EditValue = now;
            else if (endDate < _initDate)
                this.deEndDate.EditValue = _initDate;
        }

        private void deStartDate_EditValueChanged(object sender, EventArgs e)
        {
            var now = DateTime.Now.Date;

            var startDate = CommonHelper.StringToDateTime(this.deStartDate.EditValue.ToString());

            if (startDate > now)
                this.deStartDate.EditValue = now;
            else if (startDate < _initDate)
                this.deStartDate.EditValue = _initDate;
        }

        private void chkOnWorking_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAll.Checked = !this.chkOnWorking.Checked;

            RefreshRetracementInfo();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.chkOnWorking.Checked = !this.chkAll.Checked;

            RefreshRetracementInfo();
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