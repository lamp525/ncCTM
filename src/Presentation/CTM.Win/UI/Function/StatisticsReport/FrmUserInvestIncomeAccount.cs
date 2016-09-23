using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.UI.Function.StatisticsReport
{
    public partial class FrmUserInvestIncomeAccount : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IDepartmentService _deptService;
        private readonly ITKLineService _tKLineService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmUserInvestIncomeAccount";

        private IList<UserInvestIncomeAccountModel> _investIncomeInfos = null;

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeAccount(
            IDailyRecordService dailyRecordService,
            IAccountService accountService,
            IUserService userService,
            IDictionaryService dictionaryService,
            IDepartmentService deptService,
            ITKLineService tKLineService
            )
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._accountService = accountService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._deptService = deptService;
            this._tKLineService = tKLineService;
        }

        #endregion Constructors

        #region Utilities

        private void RefreshInvestIncomeInfo()
        {
            if (_investIncomeInfos == null) return;

            var source = this.chkOnWorking.Checked ? _investIncomeInfos.Where(x => x.IsOnWorking).ToList() : _investIncomeInfos;

            this.gridControl1.DataSource = source;
        }

        private void DisplaySearchResult()
        {
            _investIncomeInfos = null;

            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            _investIncomeInfos = CalculateInvestIncome(dateFrom, dateTo).OrderBy(x => x.InvestorName).ThenBy(x => x.AccountName).ToList();

            var source = this.chkOnWorking.Checked ? _investIncomeInfos.Where(x => x.IsOnWorking).ToList() : _investIncomeInfos;
            this.gridControl1.DataSource = source;
        }

        private IList<UserInvestIncomeAccountModel> CalculateInvestIncome(DateTime fromDate, DateTime toDate)
        {
            var result = new List<UserInvestIncomeAccountModel>();

            IList<UserInfo> investors = new List<UserInfo>();

            if (LoginInfo.CurrentUser.IsAdmin)
            {
                // 投资人员信息
                var deptIds = this._deptService.GetAllAccountingDepartmentId();

                investors = this._userService.GetUserInfos(departmentIds: deptIds.ToArray());
            }
            else
            {
                investors.Add(this._userService.GetUserInfoById(LoginInfo.CurrentUser.UserId));
            }
            var investorCodes = investors.Select(x => x.Code).Distinct().ToArray();

            var tradeRecords = _dailyRecordService.GetDailyRecords(beneficiaries: investorCodes, tradeDateFrom: _initDate, tradeDateTo: toDate);

            if (!tradeRecords.Any()) return result;

            //var unit = (int)EnumLibrary.NumericUnit.TenThousand;

            var queryPeriod = fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();

            //所有交易帐户信息
            var accountIds = tradeRecords.Select(x => x.AccountId).Distinct().ToArray();
            var accounts = this._accountService.GetAccountInfos(accountIds: accountIds);

            //所有交易股票的收盘价格
            var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
            var queryDates = new List<DateTime> { fromDate.AddDays(-1), toDate };
            var stockClosePrices = this._tKLineService.GetStockClosePrices(queryDates, stockFullCodes);

            var recordsByInvestor = tradeRecords.GroupBy(x => x.Beneficiary);
            foreach (var investorRecords in recordsByInvestor)
            {
                //当前投资人员信息
                var currentInvest = investors.SingleOrDefault(x => x.Code == investorRecords.Key);

                if (currentInvest == null)
                {
                    System.Diagnostics.Debug.WriteLine("Investor :" + investorRecords.Key);
                    continue;
                }

                var recordsByAccount = investorRecords.GroupBy(x => x.AccountId);
                foreach (var accountRecords in recordsByAccount)
                {
                    //当前帐户信息
                    var currentAccount = accounts.SingleOrDefault(x => x.Id == accountRecords.Key);

                    if (currentAccount == null)
                    {
                        System.Diagnostics.Debug.WriteLine("Account :" + accountRecords.Key);
                        continue;
                    }

                    var recordsByStock = accountRecords.GroupBy(x => x.StockCode);
                    foreach (var stockRecords in recordsByStock)
                    {
                        if (string.IsNullOrEmpty(stockRecords.Key)) continue;

                        var investStatisticsInfoPerStock = stockRecords.ToList().GetDailyInvestStatisticsCommonInfo(queryDates, stockClosePrices);

                        //期初收益信息
                        var startStatisticsInfo = investStatisticsInfoPerStock.First();
                        var endStatisticsInfo = investStatisticsInfoPerStock.Last();

                        var investIncomePerStockModel = new UserInvestIncomeAccountModel
                        {
                            AccountDetail = currentAccount.Name + " - " + currentAccount.SecurityCompanyName + " - " + currentAccount.AttributeName,
                            AccountId = currentAccount.Id,
                            AccountName = currentAccount.Name,
                            AccumulatedProfit = CommonHelper.SetDecimalDigits(endStatisticsInfo.AccumulatedProfit),
                            AttributeName = currentAccount.AttributeName,
                            InvestorCode = currentInvest.Code,
                            InvestorName = currentInvest.Name,
                            IsOnWorking = !currentInvest.IsDeleted,
                            Profit = CommonHelper.SetDecimalDigits((endStatisticsInfo.AccumulatedProfit - startStatisticsInfo.AccumulatedProfit)),
                            QueryPeriod = queryPeriod,
                            SecurityCompnayName = currentAccount.SecurityCompanyName,
                            StockCode = stockRecords.Key,
                            StockDetail = stockRecords.Key + " - " + stockRecords.First().StockName,
                            StockName = stockRecords.First().StockName,
                        };

                        result.Add(investIncomePerStockModel);
                    }
                }
            }

            return result;
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeAccount_Load(object sender, EventArgs e)
        {
            if (!LoginInfo.CurrentUser.IsAdmin)
            {
                this.lciAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciOnWorking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            this.deFrom.Properties.AllowNullInput = DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(DateTime.Now.Date);

            this.deTo.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTo.EditValue = now.Date.AddDays(-1);
            else
                this.deTo.EditValue = now.Date;

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showGroupPanel: true, showFilterPanel: true, showCheckBoxRowSelect: false);

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.gridControl1.DataSource = null;

                DisplaySearchResult();
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

        private void chkOnWorking_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAll.Checked = !this.chkOnWorking.Checked;

            RefreshInvestIncomeInfo();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.chkOnWorking.Checked = !this.chkAll.Checked;

            RefreshInvestIncomeInfo();
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