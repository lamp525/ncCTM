using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Accounting.AccountManage
{
    public partial class FrmAccountFundTransfer : BaseForm
    {
        #region Fields

        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IDictionaryService _dictionaryService;

        private IList<AccountEntity> _accounts;

        private const string _layoutXmlName = "FrmAccountFundTransfer";

        #endregion Fields

        #region Constructors

        public FrmAccountFundTransfer(IAccountService accountService, ICommonService commonService, IDictionaryService dictionaryService)
        {
            InitializeComponent();

            this._accountService = accountService;
            this._commonService = commonService;
            this._dictionaryService = dictionaryService;
        }

        #endregion Constructors

        #region Utilities

        private void BindSearchInfo()
        {
            //账户属性信息
            var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
                .Select(x => new ComboBoxItemModel
                {
                    Value = x.Code.ToString(),
                    Text = x.Name
                }).ToList();
            this.cbAccountAttribute.Initialize(accountAttributes);

            //证券公司信息
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)

                        .Select(x => new ComboBoxItemModel
                        {
                            Value = x.Code.ToString(),
                            Text = x.Name
                        }).OrderBy(x => x.Text).ToList();

            this.cbSecurity.Initialize(securityCompanys);

            //账户信息
            _accounts = _accountService.GetAccountDetails(onlyNeedAccounting: true, showDisabled: true).OrderBy(x => x.Name).ToList();

            this.luAccount.Initialize(_accounts, "Id", "DisplayMember", showHeader: true, enableSearch: true);

            var now = DateTime.Now.Date;

            //开始时间
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(now);

            //结束时间
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = CommonHelper.GetLastDayOfMonth(now);
        }

        private void AccountFilter()
        {
            if (!string.IsNullOrEmpty(this.cbAccountAttribute.SelectedValue()) || !string.IsNullOrEmpty(this.cbSecurity.SelectedValue()))
            {
                var source = new List<AccountEntity>();
                source.AddRange(_accounts);

                if (!string.IsNullOrEmpty(this.cbAccountAttribute.SelectedValue()))
                    source = source?.Where(x => x.AttributeCode == int.Parse(this.cbAccountAttribute.SelectedValue())).ToList();

                if (!string.IsNullOrEmpty(this.cbSecurity.SelectedValue()))
                    source = source?.Where(x => x.SecurityCompanyCode == int.Parse(this.cbSecurity.SelectedValue())).ToList();

                this.luAccount.Properties.DataSource = source;
                this.luAccount.Properties.DropDownRows = source.Count;
            }
        }

        private void RefreshForm()
        {
            BindFundTranserInfo(0, null, null, _commonService.GetCurrentServerTime().Date);
        }

        private void BindFundTranserInfo(int accountId, DateTime? dateFrom, DateTime? dateTo, DateTime? operateDate)
        {
            var source = _accountService.GetAccountFundTransferInfo(accountId, dateFrom, dateTo, operateDate).OrderByDescending(x => x.TransferDate).ToList();

            this.gridControl1.DataSource = source;

        }

        #endregion Utilities

        #region Events

        private void FrmAccountFund_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showCheckBoxRowSelect: true, showFilterPanel: true, showAutoFilterRow: false, rowIndicatorWidth: 50);
                BindSearchInfo();

                this.btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountFilter();
        }

        private void cbAccountAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountFilter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
                var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());
                var accountId = string.IsNullOrEmpty(this.luAccount.SelectedValue()) ? 0 : int.Parse(this.luAccount.SelectedValue());

                BindFundTranserInfo(accountId, dateFrom, dateTo, null);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;
                var dialog = this.CreateDialog<_dialogAccountFundTransfer>();
                dialog.RefreshEvent += new _dialogAccountFundTransfer.RefreshParentForm(RefreshForm);
                dialog.Text = "账户资金调拨";
                dialog.AccountId = string.IsNullOrEmpty(this.luAccount.SelectedValue()) ? 0 : int.Parse(this.luAccount.SelectedValue());

                dialog.ShowDialog();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的明细吗？") == DialogResult.Yes)
                {
                    var ids = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        ids.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    this._accountService.DisableAccount(ids.ToArray());

                    RefreshForm();
                }
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