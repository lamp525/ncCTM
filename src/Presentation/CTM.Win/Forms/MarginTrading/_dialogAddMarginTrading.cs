using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.MarginTrading;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.MarginTrading;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.Forms.MarginTrading
{
    public partial class _dialogAddMarginTrading : BaseForm
    {
        #region Fields

        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;
        private readonly IAccountService _accountService;
        private readonly IMarginTradingService _marginService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IDepartmentService _departmentService;

        private bool _isRepay = false;

        private const string _layoutXmlName = "_dialogAddMarginTrading";

        #endregion Fields

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogAddMarginTrading(
            IStockService stockService,
            IAccountService accountService,
            IMarginTradingService marginService,
            IUserService userService,
            IDictionaryService dictionaryService,
            IDepartmentService departmentService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._stockService = stockService;
            this._accountService = accountService;
            this._marginService = marginService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._departmentService = departmentService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Properties

        public bool IsRepay
        {
            get { return this._isRepay; }
            set { this._isRepay = value; }
        }

        #endregion Properties

        #region Utilities

        private void FormInit()
        {
            //还资还券
            if (this._isRepay)
            {
                this.lcgFinancing.Text = "还资信息";
                this.lciFinancingAmount.Text = "还资金额(万元)：";

                this.lcgLoan.Text = "还券信息";
                this.lciLoanOwner.Text = "还券所属：";
                this.lciLoanVolume.Text = "还券数量(手)：";
                lciLoanAmount.Text = "还券金额(万元)：";
            }
            //融资融券
            else
            {
                this.lcgFinancing.Text = "融资信息";
                this.lciFinancingAmount.Text = "融资金额(万元)：";

                this.lcgLoan.Text = "融券信息";
                this.lciLoanOwner.Text = "融券所属：";
                this.lciLoanVolume.Text = "融券数量(手)：";
                lciLoanAmount.Text = "融券金额(万元)：";
            }

            //交易日期
            this.deTradeDate.Properties.AllowNullInput = DefaultBoolean.False;
            this.deTradeDate.EditValue = this._commonService.GetCurrentServerTime().Date.ToString("yyyy-MM-dd");

            //交易类别
            var tradeTypes = this._dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            this.cbTradeType.Initialize(tradeTypes, displayAdditionalItem: false);

            var deptIds = this._departmentService.GetAllAccountingDepartmentId();
            var investors = this._userService.GetUserInfos(departmentIds: deptIds.ToArray()).Where(x => x.IsDeleted == false).OrderBy(x => x.Code).ToList();
            //投资人员
            this.luInvestor.Initialize(investors, "Code", "Name", enableSearch: true);
            if (!LoginInfo.CurrentUser.IsAdmin)
            {
                this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                this.luInvestor.ReadOnly = true;
            }

            //所属人
            this.luLoanOwner.Initialize(investors, "Code", "Name", enableSearch: true);

            //股票信息
            var stocks = _stockService.GetAllStocks(showDeleted: true)
                 .Select(x => new StockInfoModel
                 {
                     Id = x.Id,
                     Code = x.Code,
                     FullCode = x.FullCode,
                     Name = x.Name,
                     DisplayMember = x.FullCode + " - " + x.Name,
                 }
             ).ToList();

            this.luStock.Initialize(stocks, "Id", "DisplayMember", enableSearch: true, searchColumnIndex: 1);

            this.txtLoanAmount.SetNumericMask();
            this.txtFinancingAmount.SetNumericMask();

            this.btnDelete.Enabled = false;
        }

        private void BindTodayMarginInfo()
        {
            this.gridControl1.DataSource = null;

            var investorCode = this.luInvestor.SelectedValue();

            if (string.IsNullOrEmpty(investorCode)) return;

            var tradeDate = CommonHelper.StringToDateTime(this.deTradeDate.Text.Trim());

            var marginInfos = this._marginService.GetUserInMarginTradingDetails(investorCodes: new string[] { investorCode })
                .OrderByDescending(x => x.MarginDate)
                .ThenBy(x => x.AccountInfo)
                .ThenByDescending(x => x.Id)
                .ToList();

            this.gridControl1.DataSource = marginInfos;
        }

        private void AddMarginInfo(bool isFinancing)
        {
            if (!InputCheck(isFinancing)) return;

            var source = this.gridControl1.DataSource as List<MarginTradingEntity>;

            var investorInfo = this.luInvestor.GetSelectedDataRow() as UserInfo;

            var marginModel = new MarginTradingInfo
            {
                IsRepay = _isRepay,
                IsFinancing = isFinancing,
                AccountId = int.Parse(this.luAccount.SelectedValue()),
                DepartmentId = investorInfo.DepartmentId,
                InvestorCode = investorInfo.Code,
                MarginDate = CommonHelper.StringToDateTime(this.deTradeDate.EditValue.ToString()),
                TradeType = int.Parse(cbTradeType.SelectedValue()),
            };

            if (isFinancing)
            {
                marginModel.Amount = Math.Abs(decimal.Parse(this.txtFinancingAmount.Text.Trim()));
            }
            else
            {
                var stockInfo = this.luStock.GetSelectedDataRow() as StockInfoModel;
                marginModel.StockFullCode = stockInfo.FullCode;
                marginModel.StockName = stockInfo.Name;
                marginModel.LoanOwnerCode = this.luLoanOwner.SelectedValue();
                marginModel.LoanVolume = Math.Abs(int.Parse(this.txtLoanVolume.Text.Trim()));
                marginModel.Amount = Math.Abs(decimal.Parse(this.txtLoanAmount.Text.Trim()));
                if (source != null && source.Any())
                {
                    var existedInfo = source.SingleOrDefault(x =>
                    x.TradeType == marginModel.TradeType &&
                    x.IsRepay == _isRepay &&
                    x.InvestorCode == marginModel.InvestorCode &&
                    x.MarginDate == marginModel.MarginDate &&
                    x.AccountId == marginModel.AccountId &&
                    x.StockFullCode == marginModel.StockFullCode);

                    if (existedInfo != null)
                    {
                        DXMessage.ShowTips("该信息已经存在，请核对后重新输入！");
                        return;
                    }
                }
            }

            if (DXMessage.ShowYesNoAndTips("确定添加该信息么？") == System.Windows.Forms.DialogResult.Yes)
            {
                this._marginService.InsertMarginTradingInfo(marginModel);

                BindTodayMarginInfo();

                this.txtFinancingAmount.Text = null;

                this.luLoanOwner.EditValue = null;
                this.luStock.EditValue = null;
                this.txtLoanVolume.Text = null;
                this.txtLoanAmount.Text = null;
            }
        }

        private bool InputCheck(bool isFinancing)
        {
            if (LoginInfo.CurrentUser.IsAdmin)
            {
                if (string.IsNullOrEmpty(this.luInvestor.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择投资人员！");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(this.cbTradeType.SelectedValue()))
            {
                DXMessage.ShowTips("请选择交易类别！");
                return false;
            }

            if (string.IsNullOrEmpty(this.luAccount.SelectedValue()))
            {
                DXMessage.ShowTips("请选择账号信息！");
                return false;
            }

            if (isFinancing)
            {
                var repayMsg = this._isRepay ? "还资" : "融资";
                if (this.txtFinancingAmount.Text.Length == 0)
                {
                    DXMessage.ShowTips(string.Format("请输入{0}金额！", repayMsg));
                    this.txtFinancingAmount.Focus();
                    return false;
                }

                if (decimal.Parse(this.txtFinancingAmount.Text.Trim()) <= 0)
                {
                    DXMessage.ShowTips(string.Format("{0}金额应该大于0！", repayMsg));
                    this.txtFinancingAmount.Focus();
                    return false;
                }
            }
            else
            {
                var repayMsg = this._isRepay ? "还券" : "融券";
                if (string.IsNullOrEmpty(this.luLoanOwner.SelectedValue()))
                {
                    DXMessage.ShowTips(string.Format("请选择{0}所属人！", repayMsg));
                    return false;
                }

                if (this.luLoanOwner.SelectedValue().Equals(this.luInvestor.SelectedValue()))
                {
                    DXMessage.ShowTips(string.Format("投资人员和{0}所属人不能相同！", repayMsg));
                    return false;
                }

                if (string.IsNullOrEmpty(this.luStock.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择股票信息！");
                    return false;
                }

                if (this.txtLoanVolume.Text.Length == 0)
                {
                    DXMessage.ShowTips(string.Format("请输入{0}数量！", repayMsg));
                    this.txtLoanVolume.Focus();
                    return false;
                }

                if (int.Parse(this.txtLoanVolume.Text.Trim()) < 1)
                {
                    DXMessage.ShowTips(string.Format("{0}数量应该大于0！", repayMsg));
                    this.txtLoanVolume.Focus();
                    return false;
                }

                if (this.txtLoanAmount.Text.Length == 0)
                {
                    DXMessage.ShowTips(string.Format("请输入{0}金额！", repayMsg));
                    this.txtLoanAmount.Focus();
                    return false;
                }

                if (decimal.Parse(this.txtLoanAmount.Text.Trim()) <= 0)
                {
                    DXMessage.ShowTips(string.Format("{0}金额应该大于0！", repayMsg));
                    this.txtLoanAmount.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion Utilities

        #region Events

        private void _dialogAddMarginTrading_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showAutoFilterRow: false, showGroupPanel: true);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbTradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cbTradeType.SelectedValue())) return;

            var tradeType = int.Parse(this.cbTradeType.SelectedValue());

            switch ((EnumLibrary.TradeType)tradeType)
            {
                case EnumLibrary.TradeType.Target:
                    this.lcgFinancing.Enabled = true;
                    this.lcgLoan.Enabled = false;
                    break;

                case EnumLibrary.TradeType.Band:
                    this.lcgFinancing.Enabled = true;
                    this.lcgLoan.Enabled = true;
                    break;

                case EnumLibrary.TradeType.Day:
                    if (this._isRepay)
                    {
                        this.lcgFinancing.Enabled = false;
                        this.lcgLoan.Enabled = false;
                    }
                    else
                    {
                        this.lcgFinancing.Enabled = true;
                        this.lcgLoan.Enabled = true;
                    }
                    break;
            }
        }

        private void deTradeDate_EditValueChanged(object sender, EventArgs e)
        {
            BindTodayMarginInfo();
        }

        private void luInvestor_EditValueChanged(object sender, EventArgs e)
        {
            var investor = this.luInvestor.GetSelectedDataRow() as UserInfo;

            if (investor == null) return;

            var defaultTradeType = (int)CTMHelper.GetTradeTypeByDepartment(investor.DepartmentId);

            this.cbTradeType.DefaultSelected(defaultTradeType.ToString());

            if (investor.DepartmentId == (int)EnumLibrary.AccountingDepartment.Independence)
                this.cbTradeType.ReadOnly = false;
            else
                this.cbTradeType.ReadOnly = true;

            //账户信息
            var accountIds = _accountService.GetAccountIdByOperatorId(investor.Id);

            if (accountIds == null || !accountIds.Any()) return;

            var accounts = _accountService.GetAccountDetails(accountIds: accountIds.ToArray(), tableNoTracking: true).OrderBy(x => x.Name).ToList();

            luAccount.Initialize(accounts, "Id", "DisplayMember", enableSearch: true);

            BindTodayMarginInfo();
        }

        private void btnAddFinancing_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAddFinancing.Enabled = false;

                AddMarginInfo(true);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAddFinancing.Enabled = true;
            }
        }

        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAddLoan.Enabled = false;

                AddMarginInfo(false);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAddLoan.Enabled = true;
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length > 0)
                this.btnDelete.Enabled = true;
            else
                this.btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                var myView = this.gridView1;
                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除所选的信息么？") == System.Windows.Forms.DialogResult.Yes)
                {
                    var ids = new List<int>();
                    foreach (var rowhandle in selectedHandles)
                    {
                        ids.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    this._marginService.DeleteMarginTradingInfo(ids);

                    BindTodayMarginInfo();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnDelete.Enabled = true;
            }
        }

        private void _dialogAddMarginTrading_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.RefreshEvent?.Invoke();
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