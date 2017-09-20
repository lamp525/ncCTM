using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.MonthlyStatement;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.MonthlyStatement
{
    public partial class FrmAccountInitVerify : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IMonthlyStatementService _statementService;

        private string _currentAccountIds = null;
        private int _currentYear;
        private int _currentMonth;
        private bool _isExpanded = true;
        private bool _isSearched = false;

        #endregion Fields

        #region Constructors

        public FrmAccountInitVerify(
            IDictionaryService dictionaryService,
            IAccountService accountService,
            ICommonService commonService,
            IMonthlyStatementService statementService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._commonService = commonService;
            this._statementService = statementService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            //期初日期
            this.deYearMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deYearMonth.SetFormat("yyyy年MM月");
            var now = _commonService.GetCurrentServerTime().Date;
            this.deYearMonth.EditValue = now;

            //账户名称
            var accountNames = _accountService.GetAllAccountNames(false).ToList();
            this.cbAccount.Initialize(accountNames, displayAdditionalItem: true);

            //账户属性
            var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
            .Select(x => new ComboBoxItemModel
            {
                Value = x.Code.ToString(),
                Text = x.Name
            }).ToList();
            this.cbAttribute.Initialize(accountAttributes, displayAdditionalItem: true);

            //证券公司
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)
                        .Select(x => new ComboBoxItemModel
                        {
                            Value = x.Code.ToString(),
                            Text = x.Name
                        }).OrderBy(x => x.Text).ToList();

            this.cbSecurity.Initialize(securityCompanys, displayAdditionalItem: true);

            this.gvPosition.SetLayout(showGroupPanel: true, showCheckBoxRowSelect: false);
            this.gvPosition.SetColumnHeaderAppearance();

            this.gvAccountProfit.SetLayout(showGroupPanel: true, showCheckBoxRowSelect: false);
            this.gvAccountProfit.SetColumnHeaderAppearance();

            this.gvStockProfit.SetLayout(showCheckBoxRowSelect: false, showFooter: true);
            this.gvStockProfit.SetColumnHeaderAppearance();
        }

        private string GetSelectedAccountIds()
        {
            string accountName = this.cbAccount.SelectedItem as string;
            if (accountName == "全部")
                accountName = null;
            int securityCode = string.IsNullOrEmpty(this.cbSecurity.SelectedValue()) ? 0 : int.Parse(this.cbSecurity.SelectedValue());
            int attributeCode = string.IsNullOrEmpty(this.cbAttribute.SelectedValue()) ? 0 : int.Parse(this.cbAttribute.SelectedValue());

            IList<int> accountIds = _accountService.GetAccountIds(accountName, securityCode, attributeCode);

            return CommonHelper.ArrayListToSqlConditionString(accountIds);
        }

        private void LoadSelectedPage()
        {
            if (this.xtraTabControl1.SelectedTabPage == this.pagePosition)
                DisplayPositionInfoList();
            else
                DisplayAccountProfitInfoList();
        }

        private void DisplayPositionInfoList()
        {
            this.gcPosition.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_GetAccountPositionContrastData] @Year={_currentYear}, @Month={_currentMonth}, @AccountIds='{_currentAccountIds}'";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            this.gcPosition.DataSource = ds.Tables[0];

            this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";

            if (_isExpanded)
                this.gvPosition.ExpandAllGroups();
            else
                this.gvPosition.CollapseAllGroups();
        }

        private void DisplayAccountProfitInfoList()
        {
            this.gcAccountProfit.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_GetAccountProfitContrastData] @Year={_currentYear}, @Month={_currentMonth}, @AccountIds='{_currentAccountIds}'";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            this.gcAccountProfit.DataSource = ds.Tables[0];
        }

        private void DisplayStockProfitInfoList(int accountId)
        {
            this.gcStockProfit.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_GetStockProfitContrastData] @Year={_currentYear }, @Month={_currentMonth}, @AccountId={accountId}";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            this.gcStockProfit.DataSource = ds.Tables[0];
        }

        #endregion Utilities

        #region Events

        private void FrmAccountInitVerify_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                this.xtraTabControl1.SelectedTabPage = this.pagePosition;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                var searhDate = CommonHelper.StringToDateTime(this.deYearMonth.EditValue.ToString());
                _currentYear = searhDate.Year;
                _currentMonth = searhDate.Month;
                _currentAccountIds = GetSelectedAccountIds();
                _isSearched = true;

                LoadSelectedPage();
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

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if (_isSearched)
                    LoadSelectedPage();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #region PagePosition

        private void btnExpandOrCollapse_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExpandOrCollapse.Enabled = false;
                this._isExpanded = !_isExpanded;

                if (_isExpanded)
                    this.gvPosition.ExpandAllGroups();
                else
                    this.gvPosition.CollapseAllGroups();

                this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
            }
            finally
            {
                this.btnExpandOrCollapse.Enabled = true;
            }
        }

        private void gvPosition_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvPosition_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colDeliveryDifference_V || e.Column == this.colDailyDifference_V)
            {
                var cellValue = Convert.ToDecimal(e.CellValue);

                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        #endregion PagePosition

        #region PageProfit

        private void btnAccountAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAccountAdjust.Enabled = false;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAccountAdjust.Enabled = true;
            }
        }

        private void gvAccountProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvAccountProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colDeliveryDifference_A1 || e.Column == this.colDailyDifference_A1)
            {
                var cellValue = Convert.ToDecimal(e.CellValue);

                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void gvAccountProfit_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle < 0) return;

                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                this.esiStockProfitTitle.Text = @"当前账户：" + gv.GetRowCellValue(e.FocusedRowHandle, this.colAccountName_A1).ToString() + " - " + gv.GetRowCellValue(e.FocusedRowHandle, this.colSecurityCompanyName_A1) + " - " + gv.GetRowCellValue(e.FocusedRowHandle, this.colAttributeName_A1);

                var accountId = Convert.ToInt32(gv.GetRowCellValue(e.FocusedRowHandle, this.colAccountId_A1));
                DisplayStockProfitInfoList(accountId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnStockAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnStockAdjust.Enabled = false;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnStockAdjust.Enabled = true;
            }
        }

        private void gvStockProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvStockProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colProfitDifference_A2)
            {
                var cellValue = Convert.ToDecimal(e.CellValue);

                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        #endregion PageProfit

        #endregion Events
    }
}