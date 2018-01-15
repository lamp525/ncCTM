using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.Stock;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CTM.Win.Forms.DailyTrading.DataManage
{
    public partial class FrmTradeDataManage : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IStockService _stockService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly ICommonService _commonService;

        private SearchModel _searchCondition;

        private const string _layoutXmlName = "FrmTradeDataManage";

        #endregion Fields

        #region Nested

        private class SearchModel
        {
            public int DataType { get; set; }

            public string StockCode { get; set; }

            public int AccountId { get; set; }

            public string Beneficiary { get; set; }

            public int TradeType { get; set; }

            public bool? DealFlag { get; set; }

            public string Operator { get; set; }

            public DateTime? TradeDateFrom { get; set; }

            public DateTime? TradeDateTo { get; set; }

            public string ImportUser { get; set; }

            public DateTime? ImportDateFrom { get; set; }

            public DateTime? ImportDateTo { get; set; }
        }

        #endregion Nested

        #region Constructors

        public FrmTradeDataManage(
            IDailyRecordService tradeRecordService,
            IStockService stockService,
            IAccountService accountService,
            IUserService userService,
            IDictionaryService dictionaryService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._dailyRecordService = tradeRecordService;
            this._accountService = accountService;
            this._stockService = stockService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        /// <summary>
        /// 绑定查询信息
        /// </summary>
        private void BindSearchInfo()
        {
            //股票
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

            var allStockModel = new StockInfoModel
            {
                Id = 0,
                FullCode = "000000",
                Name = "全部",
                DisplayMember = "000000 - 全部",
            };
            stocks.Add(allStockModel);
            stocks = stocks.OrderBy(x => x.FullCode).ToList();
            luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

            //账户
            BindAccount();

            //数据类型
            var dataTypes = new List<ComboBoxItemModel>();

            var entrustModel = new ComboBoxItemModel
            {
                Text = "当日委托",
                Value = "1",
            };
            dataTypes.Add(entrustModel);

            var dailyModel = new ComboBoxItemModel
            {
                Text = "当日成交",
                Value = "3",
            };
            dataTypes.Add(dailyModel);

            var deliveryModel = new ComboBoxItemModel
            {
                Text = "交割单",
                Value = "2",
            };
            dataTypes.Add(deliveryModel);

            var virtualModel = new ComboBoxItemModel
            {
                Text = "虚拟交易",
                Value = "77",
            };
            dataTypes.Add(virtualModel);

            var stockTransferModel = new ComboBoxItemModel
            {
                Text = "股票转移",
                Value = "88",
            };
            dataTypes.Add(stockTransferModel);

            var oldSystemModel = new ComboBoxItemModel
            {
                Text = "旧系统",
                Value = "99",
            };
            dataTypes.Add(oldSystemModel);

            this.cbDataType.Initialize(dataTypes, displayAdditionalItem: true);

            //交易类别
            var tradeTypes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            cbTradeType.Initialize(tradeTypes, displayAdditionalItem: true);

            var allUserModel = new UserInfo
            {
                Code = string.Empty,
                Name = "全部",
            };

            var dealers = _userService.GetAllOperators(showDeleted: true);
            dealers.Add(allUserModel);
            dealers = dealers.OrderBy(x => x.Code).ToList();

            //受益人
            luBeneficiary.Initialize(dealers, "Code", "Name", showHeader: true, enableSearch: true);

            var admins = _userService.GetAllAdmins(showDeleted: true);
            var importors = new List<UserInfo>();
            importors.AddRange(dealers);
            importors.AddRange(admins);

            importors = importors.Distinct().OrderBy(x => x.Code).ToList();

            //导入人
            luImport.Initialize(importors, "Code", "Name", showHeader: true, enableSearch: true);
        }

        private void BindAccount()
        {
            IList<AccountEntity> accounts = null;

            if (LoginInfo.CurrentUser.IsAdmin || this.radioGroup1.SelectedIndex == 0)
            {
                //账户
                accounts = _accountService.GetAccountDetails(showDisabled: true).ToList();

                var allAccountModel = new AccountEntity
                {
                    Id = 0,
                    Name = "  全部  ",
                    AttributeName = "  全部  ",
                    SecurityCompanyName = "  全部  ",
                    DisplayMember = "  全部  ",
                };
                accounts.Add(allAccountModel);
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                var operateAccountIds = _accountService.GetAccountIdByOperatorId(LoginInfo.CurrentUser.UserId);
                accounts = _accountService.GetAccountDetails(accountIds: operateAccountIds.ToArray());
            }

            accounts = accounts.OrderBy(x => x.Name).ThenBy(x => x.SecurityCompanyName).ThenBy(x => x.AttributeName).ToList();
            luAccount.Initialize(accounts, "Id", "DisplayMember", enableSearch: true);
        }

        private void SetDefaultSearchInfo()
        {
            var today = _commonService.GetCurrentServerTime().Date;

            //交易时间（开始）
            deTradeFrom.EditValue = today.AddMonths(-1);
            //交易时间（结束）
            deTradeTo.EditValue = today;

            //导入时间（开始）
            deImportFrom.EditValue = today.AddDays(-1);
            //导入事件（结束）
            deImportTo.EditValue = today;

            //买卖标志
            this.chkBuy.Checked = false;
            this.chkSell.Checked = false;

            luStock.EditValue = null;
            luAccount.EditValue = null;

            cbTradeType.EditValue = null;

            //导入人
            luImport.EditValue = null;

            luBeneficiary.EditValue = null;

            if (LoginInfo.CurrentUser.IsAdmin || this.radioGroup1.SelectedIndex == 1)
            {
                luBeneficiary.EditValue = null;
                luBeneficiary.ReadOnly = false;
                luImport.EditValue = null;
                luImport.ReadOnly = false;
            }
            else if (this.radioGroup1.SelectedIndex == 0)
            {
                luBeneficiary.EditValue = LoginInfo.CurrentUser.UserCode;
                luBeneficiary.ReadOnly = true;
                luImport.EditValue = LoginInfo.CurrentUser.UserCode;
                luImport.ReadOnly = true;
            }
        }

        /// <summary>
        /// 绑定交易记录列表
        /// </summary>
        private void BindTradeRecord(SearchModel seachCondition)
        {
            this.gridControl1.DataSource = null;

            var today = _commonService.GetCurrentServerTime().Date;

            if (seachCondition == null)
            {
                seachCondition = new SearchModel
                {
                    DataType = -1,
                    ImportDateFrom = today.AddDays(-1),
                    ImportDateTo = today,
                    ImportUser = LoginInfo.CurrentUser.UserCode,
                    Beneficiary = LoginInfo.CurrentUser.IsAdmin ? null : LoginInfo.CurrentUser.UserCode,
                    Operator = LoginInfo.CurrentUser.IsAdmin ? null : LoginInfo.CurrentUser.UserCode,
                };
            }

            var records = _dailyRecordService.GetDailyRecordsDetailBySearchCondition(
                IsAdmin: LoginInfo.CurrentUser.IsAdmin,
                stockCode: seachCondition.StockCode,
                accountId: seachCondition.AccountId,
                dataType: seachCondition.DataType,
                tradeType: seachCondition.TradeType,
                dealFlag: seachCondition.DealFlag,
                beneficiary: seachCondition.Beneficiary,
                operatorCode: seachCondition.Operator,
                tradeDateFrom: seachCondition.TradeDateFrom,
                tradeDateTo: seachCondition.TradeDateTo,
                importUserCode: seachCondition.ImportUser,
                importDateFrom: seachCondition.ImportDateFrom,
                importDateTo: seachCondition.ImportDateTo
                )
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

            records = records.OrderBy(x => x.TradeDate).ThenBy(x => x.TradeTime).ToList();

            this.gridControl1.DataSource = records;
        }

        private void DisplayEditDialog(List<TradeRecordModel> selectedRecords)
        {
            var dialog = this.CreateDialog<_dialogDailyRecordEdit>();
            dialog.RefreshEvent += new _dialogDailyRecordEdit.RefreshParentForm(RefreshForm);
            dialog.Records = selectedRecords;
            dialog.Text = "交易数据修改";

            dialog.ShowDialog();
        }

        private void DisplaySplitDialog(TradeRecordModel selectedRecord)
        {
            var dialog = this.CreateDialog<_dialogDailyRecordSplit>();
            dialog.RefreshEvent += new _dialogDailyRecordSplit.RefreshParentForm(RefreshForm);
            dialog.Record = selectedRecord;
            dialog.Text = "交易记录拆单";

            dialog.ShowDialog();
        }

        private void DispalyVirtualRecordDialog()
        {
            var dialog = this.CreateDialog<_dialogVirtualRecord>();
            dialog.RefreshEvent += new _dialogVirtualRecord.RefreshParentForm(RefreshForm);
            dialog.Text = "添加虚拟交易记录";

            dialog.ShowDialog();
        }

        private void DispalyStockTransferForm()
        {
            this.DisplayForm<FrmStockTransfer>("股票持仓转移");
        }

        private void RefreshForm()
        {
            try
            {
                BindTradeRecord(_searchCondition);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Utilities

        #region Events

        private void FrmTradeDataManage_Load(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnSplit.Enabled = false;
                this.btnAddVirtualRecord.Enabled = LoginInfo.CurrentUser.IsAdmin;

                if (LoginInfo.CurrentUser.IsAdmin)
                    this.lciQueryMode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                else
                    this.lciQueryMode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                this.gridView1.LoadLayout(_layoutXmlName);

                this.gridView1.SetLayout(showGroupPanel: true, showFilterPanel: true, rowIndicatorWidth: 60, showCheckBoxRowSelect: true);

                BindSearchInfo();

                SetDefaultSearchInfo();

                this.ActiveControl = this.btnSearch;

                BindTradeRecord(null);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 重置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            SetDefaultSearchInfo();
        }

        /// <summary>
        /// 查询交易数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.btnClear.Enabled = false;

                _searchCondition = new SearchModel();

                //股票代码
                var stockInfo = this.luStock.GetSelectedDataRow() as StockInfoModel;
                _searchCondition.StockCode = (stockInfo == null || stockInfo.Id == 0) ? string.Empty : this.luStock.SelectedValue();

                //账号ID
                _searchCondition.AccountId = luAccount.EditValue == null ? 0 : int.Parse(luAccount.EditValue.ToString());

                //受益人
                _searchCondition.Beneficiary = luBeneficiary.EditValue == null ? null : luBeneficiary.EditValue.ToString();

                //导入人
                _searchCondition.ImportUser = luImport.EditValue == null ? null : luImport.EditValue.ToString();

                //交易类型
                _searchCondition.TradeType = cbTradeType.EditValue == null ? 0 : int.Parse(cbTradeType.SelectedValue());

                //数据类型
                _searchCondition.DataType = cbDataType.EditValue == null ? -1 : int.Parse(cbDataType.SelectedValue());

                //买卖标志
                if (chkBuy.Checked)
                    _searchCondition.DealFlag = true;
                else if (chkSell.Checked)
                    _searchCondition.DealFlag = false;

                //导入日期
                if (deImportFrom.EditValue == null)
                    _searchCondition.ImportDateFrom = null;
                else
                    _searchCondition.ImportDateFrom = CommonHelper.StringToDateTime(deImportFrom.EditValue.ToString());

                if (deImportTo.EditValue == null)
                    _searchCondition.ImportDateTo = null;
                else
                    _searchCondition.ImportDateTo = CommonHelper.StringToDateTime(deImportTo.EditValue.ToString());

                //交易日期
                if (deTradeFrom.EditValue == null)
                    _searchCondition.TradeDateFrom = null;
                else
                    _searchCondition.TradeDateFrom = CommonHelper.StringToDateTime(deTradeFrom.EditValue.ToString());

                if (deTradeTo.EditValue == null)
                    _searchCondition.TradeDateTo = null;
                else
                    _searchCondition.TradeDateTo = CommonHelper.StringToDateTime(deTradeTo.EditValue.ToString());

                BindTradeRecord(_searchCondition);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSearch.Enabled = true;
                this.btnClear.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        /// <summary>
        /// 修改交易记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                var selectedRecords = new List<TradeRecordModel>();
                for (var index = 0; index < selectedHandles.Length; index++)
                {
                    var record = myView.GetRow(selectedHandles[index]) as TradeRecordModel;
                    selectedRecords.Add(record);
                }

                DisplayEditDialog(selectedRecords);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnEdit.Enabled = true;
            }
        }

        /// <summary>
        /// 删除交易记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 交易记录拆单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSplit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSplit.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                var record = myView.GetRow(selectedHandles[0]) as TradeRecordModel;
                DisplaySplitDialog(record);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSplit.Enabled = true;
            }
        }

        /// <summary>
        /// 添加虚拟交易记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddVirtualRecord_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddVirtualRecord.Enabled = false;

                DispalyVirtualRecordDialog();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAddVirtualRecord.Enabled = true;
            }
        }

        /// <summary>
        /// 股票移仓
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStockTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                btnStockTransfer.Enabled = false;

                DispalyStockTransferForm();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnStockTransfer.Enabled = true;
            }
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
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnSplit.Enabled = false;
            }
            else if (selectedHandles.Length > 0)
            {
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;

                if (selectedHandles.Length == 1)
                    this.btnSplit.Enabled = true;
                else
                    this.btnSplit.Enabled = false;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.radioGroup1.Enabled = false;

                BindAccount();
                SetDefaultSearchInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.radioGroup1.Enabled = true;
            }
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gridView1.DrawRowIndicator(e);
        }

        #endregion Events
    }
}