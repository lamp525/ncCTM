using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Account;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.MonthlyProcess
{
    public partial class _dialogAccountFundTransfer : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IAccountService _accountService;

        #endregion Fields

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogAccountFundTransfer(ICommonService commonService, IAccountService accountService)
        {
            InitializeComponent();

            this._accountService = accountService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Properties

        public int AccountId { get; set; }

        #endregion Properties

        #region Utilities

        private void FormInit()
        {
            //操作日期
            this.deTransfer.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTransfer.EditValue = this._commonService.GetCurrentServerTime().Date.ToString("yyyy-MM-dd");

            //账户信息
            var accounts = _accountService.GetAccountDetails(onlyNeedAccounting: true, showDisabled: true).OrderBy(x => x.Name).ToList();
            this.luAccount.Initialize(accounts, "Id", "DisplayMember", enableSearch: true);
            if (this.AccountId > 0)
                this.luAccount.EditValue = this.AccountId;

            //操作金额
            this.txtAmount.SetNumericMask(6);
            this.btnDelete.Enabled = false;
        }

        private void BindFundTransferInfo()
        {
            var source = _accountService.GetAccountFundTransferInfo(operateDate: _commonService.GetCurrentServerTime().Date);

            this.gridControl1.DataSource = source;
        }

        #endregion Utilities

        #region Events

        private void _dialogAccountFundTransfer_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindFundTransferInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chkIn_CheckedChanged(object sender, EventArgs e)
        {
            this.chkOut.Checked = !this.chkIn.Checked;
        }

        private void chkOut_CheckedChanged(object sender, EventArgs e)
        {
            this.chkIn.Checked = !this.chkOut.Checked;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                if (string.IsNullOrEmpty(this.luAccount.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择账号信息！");
                    return;
                }

                if (this.txtAmount.Text.Trim().Length == 0)
                {
                    DXMessage.ShowTips("请输入操作金额！");
                    this.txtAmount.Focus();
                    return;
                }

                if (decimal.Parse(this.txtAmount.Text.Trim()) <= 0)
                {
                    DXMessage.ShowTips("操作金额应该大于0！");
                    this.txtAmount.Focus();
                    return;
                }

                var transferDate = CommonHelper.StringToDateTime(this.deTransfer.EditValue.ToString());
                var transferAmount = Math.Abs(decimal.Parse(this.txtAmount.Text.Trim()));
                var account = this.luAccount.GetSelectedDataRow() as AccountEntity;

                if (account == null) return;

                var info = new AccountFundTransfer
                {
                    AccountCode = account.Code,
                    AccountId = account.Id,
                    FlowFlag = this.chkIn.Checked,
                    OperateTime = _commonService.GetCurrentServerTime(),
                    Operator = LoginInfo.CurrentUser.UserCode,
                    TransferAmount = this.chkIn.Checked ? transferAmount : -transferAmount,
                    TransferDate = transferDate,
                };

                this._accountService.AddAccuntFundTransfer(info);

                BindFundTransferInfo();

                this.txtAmount.Text = null;
                this.chkIn.Checked = true;
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
                this.btnDelete.Enabled = false;

                var myView = this.gridView1;
                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除所选的信息么？") == System.Windows.Forms.DialogResult.Yes)
                {
                    var ids = new List<int>();
                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        ids.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    this._accountService.DeleteAccountFundTransfer(ids);
                    BindFundTransferInfo();
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

        private void _dialogAccountFundTransfer_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.RefreshEvent?.Invoke();
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