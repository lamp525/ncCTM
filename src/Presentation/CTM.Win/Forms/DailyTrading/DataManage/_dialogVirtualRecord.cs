using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.MarginTrading;
using CTM.Services.Stock;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.Forms.DailyTrading.DataManage
{
    public partial class _dialogVirtualRecord : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;
        private readonly IAccountService _accountService;
        private readonly IMarginTradingService _marginService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IDepartmentService _departmentService;

        private const string _layoutXmlName = "_dialogVirtualRecord";

        #endregion Fields

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogVirtualRecord(
            IDailyRecordService dailyRecordService,
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

            this._dailyRecordService = dailyRecordService;
            this._stockService = stockService;
            this._accountService = accountService;
            this._marginService = marginService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._departmentService = departmentService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.txtDataType.Text = "虚拟交易记录";
            this.chkBuy.Checked = true;

            this.txtDealPrice.SetNumericMask();

            this.txtDealPrice.Properties.MaxLength = 9;
            this.txtDealVolume.Properties.MaxLength = 9;

            this.txtDealAmount.EditValue = 0;
            this.txtActualAmount.EditValue = 0;

            //交易日期
            this.deTradeDate.SetFormat("yyyy-MM-dd HH:mm:ss");
            this.deTradeDate.Properties.AllowNullInput = DefaultBoolean.False;
            this.deTradeDate.EditValue = this._commonService.GetCurrentServerTime();

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

            //实际受益人
            this.luBeneficiary.Initialize(investors, "Code", "Name", enableSearch: true);

            if (!LoginInfo.CurrentUser.IsAdmin)
            {
                this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                this.luInvestor.ReadOnly = true;

                this.luBeneficiary.EditValue = LoginInfo.CurrentUser.UserCode;
            }

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

            this.btnDelete.Enabled = false;
        }

        private void BindVirtualRecord()
        {
            var investorCode = this.luInvestor.SelectedValue();

            var virtualRecords = this._dailyRecordService.GetDailyRecordsDetail(operatorCode: investorCode, beneficiary: investorCode, dataType: (int)EnumLibrary.DataType.Virtual)
                .Select(x => new TradeRecordModel
                {
                    RecordId = x.Id,
                    AccountId = x.AccountId,
                    AccountName = x.AccountName,
                    ActualAmount = CommonHelper.SetDecimalDigits(x.ActualAmount),
                    AuditFlag = x.AuditFlag,
                    AuditNo = x.AuditNo,
                    AuditTime = x.AuditTime,
                    Beneficiary = x.Beneficiary,
                    BeneficiaryName = x.BeneficiaryName,
                    Commission = CommonHelper.SetDecimalDigits(x.Commission),
                    ContractNo = x.ContractNo,
                    DataType = x.DataType,
                    DataTypeName = CTMHelper.GetDataTypeName(x.DataType),
                    DealAmount = CommonHelper.SetDecimalDigits(x.DealAmount),
                    DealFlag = x.DealFlag,
                    DealFlagName = x.DealFlag ? "买入" : "卖出",
                    DealNo = x.DealNo,
                    DealPrice = x.DealPrice,
                    DealVolume = x.DealVolume,
                    ImportTime = x.ImportTime,
                    ImportUser = x.ImportUser,
                    ImportUserName = x.ImportUserName,
                    Incidentals = CommonHelper.SetDecimalDigits(x.Incidentals),
                    OperatorCode = x.OperatorCode,
                    OperatorName = x.OperatorName,
                    Remarks = x.Remarks,
                    SplitNo = x.SplitNo,
                    StampDuty = CommonHelper.SetDecimalDigits(x.StampDuty),
                    StockCode = x.StockCode,
                    StockHolderCode = x.StockHolderCode,
                    StockName = x.StockName,
                    TradeDate = x.TradeDate,
                    TradeTime = x.TradeTime,
                    TradeType = x.TradeType,
                    TradeTypeName = CTMHelper.GetTradeTypeName(x.TradeType),
                    UpdateTime = x.UpdateTime,
                    UpdateUser = x.UpdateUser,
                    UpdateUserName = x.UpdateUserName,
                }
                ).ToList();

            this.gridControl1.DataSource = virtualRecords;
        }

        private void AddVirtualRecordProcess()
        {
            if (!InputCheck()) return;

            var now = this._commonService.GetCurrentServerTime();
            var stockInfo = this.luStock.GetSelectedDataRow() as StockInfoModel;
            var dealVolume = int.Parse(this.txtDealVolume.Text.Trim());

            var tradeTime = CommonHelper.StringToDateTime(this.deTradeDate.Text.Trim());
            var virtualRecord = new DailyRecord
            {
                AccountId = int.Parse(this.luAccount.SelectedValue()),
                ActualAmount = decimal.Parse(txtActualAmount.Text.Trim()),
                AuditFlag = false,
                Beneficiary = this.luBeneficiary.SelectedValue(),
                DataType = (int)EnumLibrary.DataType.Virtual,
                DealAmount = decimal.Parse(this.txtDealAmount.Text.Trim()),
                DealFlag = this.chkBuy.Checked,
                DealPrice = decimal.Parse(this.txtDealPrice.Text.Trim()),
                DealVolume = this.chkBuy.Checked ? dealVolume : -dealVolume,
                ImportTime = now,
                ImportUser = LoginInfo.CurrentUser.UserCode,
                OperatorCode = this.luInvestor.SelectedValue(),
                StockCode = stockInfo.FullCode,
                StockName = stockInfo.Name,
                TradeDate = tradeTime.Date,
                TradeTime = tradeTime.ToLongTimeString(),
                TradeType = int.Parse(this.cbTradeType.SelectedValue()),
                UpdateTime = now,
                UpdateUser = LoginInfo.CurrentUser.UserCode,
            };

            this._dailyRecordService.InsertDailyRecord(virtualRecord);
        }

        private bool InputCheck()
        {
            if (LoginInfo.CurrentUser.IsAdmin)
            {
                if (string.IsNullOrEmpty(this.luInvestor.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择投资人员！");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(this.luAccount.SelectedValue()))
            {
                DXMessage.ShowTips("请选择账号信息！");
                return false;
            }

            if (string.IsNullOrEmpty(this.cbTradeType.SelectedValue()))
            {
                DXMessage.ShowTips("请选择交易类别！");
                return false;
            }

            if (string.IsNullOrEmpty(this.luBeneficiary.SelectedValue()))
            {
                DXMessage.ShowTips("请选择实际收益人！");
                return false;
            }

            if (string.IsNullOrEmpty(this.luStock.SelectedValue()))
            {
                DXMessage.ShowTips("请选择股票信息！");
                return false;
            }

            if (this.txtDealVolume.Text.Length == 0)
            {
                DXMessage.ShowTips("请输入成交数量数量！");
                this.txtDealVolume.Focus();
                return false;
            }

            if (int.Parse(this.txtDealVolume.Text.Trim()) < 1)
            {
                DXMessage.ShowTips("成交数量应该大于0！");
                this.txtDealVolume.Focus();
                return false;
            }

            if (this.txtDealPrice.Text.Length == 0)
            {
                DXMessage.ShowTips("请输入成交价格！");
                this.txtDealPrice.Focus();
                return false;
            }

            if (decimal.Parse(this.txtDealPrice.Text.Trim()) <= 0)
            {
                DXMessage.ShowTips("成交价格应该大于0！");
                this.txtDealPrice.Focus();
                return false;
            }

            return true;
        }

        private void CalculateDealAmount()
        {
            if (!string.IsNullOrEmpty(this.txtDealVolume.Text.Trim()) && !string.IsNullOrEmpty(this.txtDealPrice.Text.Trim()))
            {
                decimal dealAmount = int.Parse(this.txtDealVolume.Text.Trim()) * decimal.Parse(this.txtDealPrice.Text.Trim());

                this.txtDealAmount.Text = dealAmount.ToString();

                decimal actualAmount = this.chkBuy.Checked ? -dealAmount : dealAmount;

                this.txtActualAmount.Text = actualAmount.ToString();
            }
        }

        #endregion Utilities

        #region Events

        private void _dialogVirtualRecord_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showAutoFilterRow: false);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                AddVirtualRecordProcess();

                BindVirtualRecord();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAdd.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除所选的交易记录吗？") == DialogResult.Yes)
                {
                    var recordIds = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        recordIds.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colRecordId).ToString()));
                    }

                    this._dailyRecordService.DeleteDailyRecords(recordIds.ToArray());

                    myView.DeleteSelectedRows();
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

        private void luInvestor_EditValueChanged(object sender, EventArgs e)
        {
            var investor = this.luInvestor.GetSelectedDataRow() as UserInfo;

            if (investor == null) return;

            var defaultTradeType = (int)CTMHelper.GetTradeTypeByDepartment(investor.DepartmentId);

            this.cbTradeType.DefaultSelected(defaultTradeType.ToString());

            //if (investor.DepartmentId == (int)EnumLibrary.AccountingDepartment.Independence)
            //    this.cbTradeType.ReadOnly = false;
            //else
            //    this.cbTradeType.ReadOnly = true;

            //账户信息
            var accountIds = _accountService.GetAccountIdByOperatorId(investor.Id);

            if (accountIds == null || !accountIds.Any()) return;

            var accounts = _accountService.GetAccountDetails(accountIds: accountIds.ToArray(), tableNoTracking: true)
                .OrderBy(x => x.Name).ToList();

            luAccount.Initialize(accounts, "Id", "DisplayMember", enableSearch: true);

            BindVirtualRecord();
        }

        private void chkSell_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSell.Checked)
                chkBuy.Checked = false;
        }

        private void chkBuy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBuy.Checked)
                chkSell.Checked = false;

            CalculateDealAmount();
        }

        private void txtDealVolume_EditValueChanged(object sender, EventArgs e)
        {
            CalculateDealAmount();
        }

        private void txtDealPrice_EditValueChanged(object sender, EventArgs e)
        {
            CalculateDealAmount();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 0)
                this.btnDelete.Enabled = false;
            else
                this.btnDelete.Enabled = true;
        }

        private void _dialogVirtualRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshEvent?.Invoke();
        }

        #endregion Events
    }
}