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

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmUserInvestIncomeDaily : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly ITKLineService _tKLineService;
        private readonly IDailyStatisticsReportService _statisticsReportService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmUserInvestIncomeDaily";

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeDaily(
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
            //交易日
            this.deTradeDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;

            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTradeDate.EditValue = now.Date.AddDays(-1);
            else
                this.deTradeDate.EditValue = now.Date;

            //部门
            var deptInfos = this._departmentService.GetAllAccountingDepartmentInfo()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);

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

        private IList<UserInvestIncomeEntity> CalculateUserDailyInvestIncome(DateTime endDate, int deptId)
        {
            var result = new List<UserInvestIncomeEntity>();

            IList<UserInfo> beneficiaryInfos = new List<UserInfo>();

            if (LoginInfo.CurrentUser.IsAdmin)
                beneficiaryInfos = this._userService.GetUserInfos(departmentIds: new int[] { deptId }).Where(x => x.IsDeleted == false).ToList();
            else
                beneficiaryInfos.Add(_userService.GetUserInfoById(LoginInfo.CurrentUser.UserId));

            var beneficiaries = beneficiaryInfos.Select(x => x.Code).Distinct().ToArray();

            //交易记录
            var tradeRecords = _dailyRecordService.GetDailyRecords(beneficiaries: beneficiaries, tradeDateFrom: _initDate, tradeDateTo: endDate).ToList();

            if (!tradeRecords.Any()) return result;

            //所有股票收盘价
            var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
            var queryDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), endDate);

            var stockClosePrices = this._tKLineService.GetStockClosePrices(queryDates, stockFullCodes);

            var statisticalDates = CommonHelper.GetWorkdaysBeforeCurrentDay(endDate, 2);

            foreach (var user in beneficiaryInfos)
            {
                //当前用户交易记录
                var userRecords = tradeRecords.Where(x => x.Beneficiary == user.Code).ToList();
                var userStockCodes = userRecords.Select(x => x.StockCode).Distinct().ToList();
                var userStockClosePrices = stockClosePrices.Where(x => userStockCodes.Contains(x.StockCode.Trim())).ToList();
                var statisticalInvestorCodes = new List<string> { user.Code };

                var dailyInvestIncomes = this._statisticsReportService.CalculateUserInvestIncome(user, statisticalInvestorCodes, userRecords, statisticalDates, userStockClosePrices);

                if (dailyInvestIncomes.Any())
                    result.Add(dailyInvestIncomes.Single());
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
                    AnnualActualProfit = CommonHelper.SetDecimalDigits(x.AnnualActualProfit / unit),
                    AnnualProfit = CommonHelper.SetDecimalDigits(x.AnnualProfit / unit),
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
            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
            this.gridView1.SetColumnHeaderAppearance();

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.gridControl1.DataSource = null;

                //查询截至交易日
                var endDate = CommonHelper.GetPreviousWorkDay(CommonHelper.StringToDateTime(this.deTradeDate.EditValue.ToString()));
                this.deTradeDate.EditValue = endDate;

                //部门ID
                var deptId = int.Parse(this.cbDepartment.SelectedValue());

                var investIncomes = this.DataFormat(this.CalculateUserDailyInvestIncome(endDate, deptId)).OrderBy(x => x.Investor);

                this.gridControl1.DataSource = investIncomes;
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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colAnnualProfit
                || e.Column == this.colCurrentProfit
                || e.Column == this.colCurrentActualProfit
                || e.Column == this.colCurrentIncomeRate)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        #endregion Events
    }
}