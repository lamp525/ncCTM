using System;
using System.Collections.Generic;
using CTM.Services.Account;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.AccountManage
{
    public partial class FrmAccountFundMonthlyStatements : BaseForm
    {
        #region Fields

        private readonly IAccountService _accountService;

        #endregion Fields

        #region Constructors

        public FrmAccountFundMonthlyStatements(IAccountService accountService)
        {
            InitializeComponent();

            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        private void BindOperationInfo()
        {
            this.lblNotSettled.Text = string.Empty;
            this.lblSettled.Text = string.Empty;
            this.btnSettle.Enabled = false;
            this.btnRevoke.Enabled = false;

            KeyValuePair<int, bool> info = _accountService.GetLatestAccountFundInitialInfo();

            if (info.Key > 0)
            {
                this.btnSettle.Enabled = true;
                if (info.Value == true)
                    this.btnRevoke.Enabled = true;

                int notSettledYear = info.Key / 100;
                int notSettledMonth = info.Key % 100;
                int settledYear = notSettledYear;
                int settledMonth = notSettledMonth - 1;

                if (settledMonth == 0)
                {
                    settledMonth = 12;
                    notSettledYear += 1;
                }
                this.lblSettled.Text = $@"已结月份：{settledYear }年{settledMonth}月";
                this.lblNotSettled.Text = $@"未结月份：{notSettledYear }年{notSettledMonth}月";
            }
        }

        #endregion Utilities

        #region Events

        private void FrmAccountFundSettleMonthly_Load(object sender, EventArgs e)
        {
            try
            {
                BindOperationInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRevoke.Enabled = false;

                _accountService.AccountFundRevokeProcess();

                BindOperationInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnRevoke.Enabled = true;
            }
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSettle.Enabled = false;

                _accountService.AccountFundSettleProcess();

                BindOperationInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSettle.Enabled = true;
            }
        }

        #endregion Events
    }
}