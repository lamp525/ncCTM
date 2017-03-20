using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.MonthlyStatement;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.MonthlyStatement;
using CTM.Services.Stock;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.MonthlyStatement
{
    public partial class FrmAccountMonthlyInit : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IStockService _stockService;
        private readonly ICommonService _commonService;
        private readonly IMonthlyStatementService _statementService;

        private IList<AccountEntity> _accountInfos = null;

        private int _currentAccountId;
        private string _currentAccountCode;
        private string _currentAccountInfo;
        private int _currentYear;
        private int _currentMonth;

        #endregion Fields

        #region Constructors

        public FrmAccountMonthlyInit(
            IDictionaryService dictionaryService,
            IAccountService accountService,
            IStockService stockService,
            ICommonService commonService,
            IMonthlyStatementService statementService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._stockService = stockService;
            this._commonService = commonService;
            this._statementService = statementService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
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

            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);

            //期初日期
            this.deInit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deInit.SetFormat("yyyy年MM月");
            var now = _commonService.GetCurrentServerTime().Date;
            if (now.AddDays(1).Month == now.Month + 1)
                now = now.AddMonths(1);
            this.deInit.EditValue = now;

            this.txtTotalAsset.SetNumericMask(2);
            this.txtAvailableFund.SetNumericMask(2);
            this.txtPositionValue.SetNumericMask(2);
            this.txtFinancingLimit.SetNumericMask(2);
            this.txtFinancedAmount.SetNumericMask(2);

            this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            this.btnDelete.Enabled = false;

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
           ).OrderBy(x => x.FullCode).ToList();
            this.luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

            this.btnAdd.Enabled = false;

            this.gridView2.SetLayout(showAutoFilterRow: true, showCheckBoxRowSelect: true, editable: true, readOnly: false);

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.gridView2.Columns)
            {
                if (column.Name == this.colPositionVolume.Name || column.Name == this.colOperate.Name)
                    column.OptionsColumn.AllowEdit = true;
                else
                    column.OptionsColumn.AllowEdit = false;
            }
        }

        private void BindAccountList()
        {
            this.gridControl1.DataSource = null;

            if (_accountInfos == null)
                _accountInfos = _accountService.GetAccountDetails(showDisabled: false)
                   .OrderBy(x => x.Name).ThenBy(x => x.SecurityCompanyName).ThenBy(x => x.AttributeName).ToList();

            IList<AccountEntity> source = _accountInfos;

            var accountName = this.cbAccount.SelectedItem as string;
            if (!string.IsNullOrEmpty(accountName) && accountName != "全部")
                source = source.Where(x => x.Name == accountName).ToList();

            var securityCode = this.cbSecurity.SelectedValue();
            if (!string.IsNullOrEmpty(securityCode) && securityCode != "0")
                source = source.Where(x => x.SecurityCompanyCode == int.Parse(securityCode)).ToList();

            var attributeCode = this.cbAttribute.SelectedValue();
            if (!string.IsNullOrEmpty(attributeCode) && attributeCode != "0")
                source = source.Where(x => x.AttributeCode == int.Parse(attributeCode)).ToList();

            this.gridControl1.DataSource = source;
        }

        private void BindAMIFund()
        {
            this.txtAccountInfo.EditValue = _currentAccountInfo;

            var fundInfo = _statementService.GetMIAccountFund(_currentAccountId, _currentYear, _currentMonth);

            if (fundInfo != null)
            {
                this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                this.txtTotalAsset.ReadOnly = true;
                this.txtAvailableFund.ReadOnly = true;
                this.txtPositionValue.ReadOnly = true;
                this.txtFinancingLimit.ReadOnly = true;
                this.txtFinancedAmount.ReadOnly = true;

                this.txtTotalAsset.EditValue = CommonHelper.SetDecimalDigits(fundInfo.TotalAsset, 2);
                this.txtAvailableFund.EditValue = CommonHelper.SetDecimalDigits(fundInfo.AvailableFund, 2);
                this.txtPositionValue.EditValue = CommonHelper.SetDecimalDigits(fundInfo.PositionValue, 2);
                this.txtFinancingLimit.EditValue = CommonHelper.SetDecimalDigits(fundInfo.FinancingLimit, 2);
                this.txtFinancedAmount.EditValue = CommonHelper.SetDecimalDigits(fundInfo.FinancedAmount, 2);
            }
            else
            {
                this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                this.txtTotalAsset.ReadOnly = false;
                this.txtAvailableFund.ReadOnly = false;
                this.txtPositionValue.ReadOnly = false;
                this.txtFinancingLimit.ReadOnly = false;
                this.txtFinancedAmount.ReadOnly = false;

                this.txtTotalAsset.Text = string.Empty;
                this.txtAvailableFund.Text = string.Empty;
                this.txtPositionValue.Text = string.Empty;
                this.txtFinancingLimit.Text = string.Empty;
                this.txtFinancedAmount.Text = string.Empty;

                this.txtTotalAsset.Focus();
            }
        }

        private void BindAMIPosition()
        {
            this.gridControl2.DataSource = null;

            var source = _statementService.GetMIAccountPosition(_currentAccountId, _currentYear, _currentMonth).OrderBy(x => x.StockCode).ToList();

            this.gridControl2.DataSource = source;
        }

        private void SaveAMIFund()
        {
            if (string.IsNullOrEmpty(txtTotalAsset.Text.Trim()))
            {
                DXMessage.ShowTips("请输入总资产！");
                txtTotalAsset.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtAvailableFund.Text.Trim()))
            {
                DXMessage.ShowTips("请输入可用资金！");
                txtAvailableFund.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPositionValue.Text.Trim()))
            {
                DXMessage.ShowTips("请输入持仓市值！");
                txtPositionValue.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFinancingLimit.Text.Trim()))
            {
                DXMessage.ShowTips("请输入可融资额！");
                txtFinancingLimit.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFinancedAmount.Text.Trim()))
            {
                DXMessage.ShowTips("请输入已融资额！");
                txtFinancedAmount.Focus();
                return;
            }

            var fundInfo = new MIAccountFund
            {
                AccountCode = _currentAccountCode,
                AccountId = _currentAccountId,
                YearMonth = _currentYear * 100 + _currentMonth,
                AvailableFund = Convert.ToDecimal(this.txtAvailableFund.Text.Trim()),
                FinancedAmount = Convert.ToDecimal(this.txtFinancedAmount.Text.Trim()),
                FinancingLimit = Convert.ToDecimal(this.txtFinancingLimit.Text.Trim()),
                PositionValue = Convert.ToDecimal(this.txtPositionValue.Text.Trim()),
                TotalAsset = Convert.ToDecimal(this.txtTotalAsset.Text.Trim()),
            };

            _statementService.SaveMIAccountFund(fundInfo);

            DXMessage.ShowTips("保存成功！");

            BindAMIFund();
        }

        #endregion Utilities

        #region Events

        private void FrmAccountMonthlyInit_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindAccountList();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAccountList();
        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAccountList();
        }

        private void cbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAccountList();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                var row = gv.GetRow(gv.FocusedRowHandle) as AccountEntity;

                if (row == null) return;

                _currentAccountId = row.Id;
                _currentAccountCode = row.Code;
                _currentAccountInfo = row.DisplayMember;

                var initDate = CommonHelper.StringToDateTime(this.deInit.EditValue.ToString());
                _currentYear = initDate.Year;
                _currentMonth = initDate.Month;

                this.luStock.EditValue = null;

                BindAMIFund();

                BindAMIPosition();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void deInit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView1.GetFocusedDataSourceRowIndex() < 0) return;

                var initDate = CommonHelper.StringToDateTime(this.deInit.EditValue.ToString());
                _currentYear = initDate.Year;
                _currentMonth = initDate.Month;

                BindAMIFund();

                BindAMIPosition();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.lciEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            this.txtTotalAsset.ReadOnly = false;
            this.txtAvailableFund.ReadOnly = false;
            this.txtPositionValue.ReadOnly = false;
            this.txtFinancingLimit.ReadOnly = false;
            this.txtFinancedAmount.ReadOnly = false;

            this.txtTotalAsset.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSave.Enabled = false;

                SaveAMIFund();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BindAMIFund();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luStock_EditValueChanged(object sender, EventArgs e)
        {
            var stockCode = luStock.SelectedValue();

            this.btnAdd.Enabled = !string.IsNullOrEmpty(stockCode);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                var stockInfo = luStock.GetSelectedDataRow() as StockInfoModel;

                if (stockInfo == null) return;

                this.luStock.EditValue = null;

                _statementService.AddMIAccountPosition(_currentAccountId, _currentAccountCode, _currentYear, _currentMonth, stockInfo.FullCode, stockInfo.Name);

                BindAMIPosition();

                var source = this.gridControl2.DataSource as List<MIAccountPosition>;

                if (source != null && source.Any())
                {
                    for (int i = 0; i < source.Count; i++)
                    {
                        if (source[i].StockCode == stockInfo.FullCode)
                        {
                            this.gridView2.SelectRow(this.gridView2.GetRowHandle(i));
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                if (DXMessage.ShowYesNoAndTips("确认删除选择的股票持仓信息么？") == System.Windows.Forms.DialogResult.Yes)
                {
                    IList<int> positionIds = null;
                    foreach (var handle in this.gridView2.GetSelectedRows())
                    {
                        positionIds.Add(Convert.ToInt32(this.gridView2.GetRowCellValue(handle, this.colPositionId)));
                    }
                    _statementService.DeleteMIAccountPosition(positionIds);

                    BindAMIPosition();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnImport.Enabled = false;

                var source = this.gridControl2.DataSource as List<MIAccountPosition>;

                bool clearExisted = false;
                if (source != null && source.Any())
                {
                    if (DXMessage.ShowYesNoAndWarning("从交割单导入持仓将会清空现有的持仓信息。是否继续？") == System.Windows.Forms.DialogResult.No)
                        return;
                    else
                        clearExisted = true;
                }

                _statementService.ImportPositionInfoFromDelivery(_currentAccountId, _currentYear, _currentMonth, clearExisted);

                BindAMIPosition();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnImport.Enabled = true;
            }
        }

        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                var selectedHandles = gv.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                btnDelete.Enabled = selectedHandles.Any();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var currentRow = e.Row as MIAccountPosition;

                _statementService.UpdateMIAccountPosition(currentRow.Id, currentRow.PositionVolume);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            var dr = myView.GetRow(e.RowHandle) as MIAccountPosition;

            if (dr == null) return;

            //操作
            if (e.Column.Name == colOperate.Name)
            {
                DevExpress.XtraEditors.ViewInfo.ButtonEditViewInfo buttonVI = (DevExpress.XtraEditors.ViewInfo.ButtonEditViewInfo)((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell).ViewInfo;

                buttonVI.RightButtons[0].Button.Enabled = true;
                buttonVI.RightButtons[0].State = DevExpress.Utils.Drawing.ObjectState.Normal;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.gridView2;

                if (myView.FocusedRowHandle < 0) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (!string.IsNullOrEmpty(buttonTag) && buttonTag == "Delete")
                {
                    if (DXMessage.ShowYesNoAndWarning("确定删除该股票持仓信息吗？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        var positionId = Convert.ToInt32(myView.GetRowCellValue(myView.FocusedRowHandle, colPositionId));

                        this._statementService.DeleteMIAccountPosition(new int[] { positionId });

                        BindAMIPosition();
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                e.Button.Enabled = true;
            }
        }

        #endregion Events
    }
}