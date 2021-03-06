﻿using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Dictionary;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.DailyTrading.DataManage
{
    public partial class _dialogDailyRecordEdit : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _tradeRecordService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;

        private IList<TradeRecordModel> _records;
        private bool _saveSucceed = false;

        private const string _layoutXmlName = "_dialogDailyRecordEdit";

        #endregion Fields

        #region Properties

        public IList<TradeRecordModel> Records
        {
            // get { return this._records ?? (new List<TradeRecordModel>()); }
            set { this._records = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogDailyRecordEdit(
            IDailyRecordService tradeRecordService,
            IUserService userService,
            IDictionaryService dictionaryService,
            IAccountService accountService
            )
        {
            InitializeComponent();

            this._tradeRecordService = tradeRecordService;
            this._dictionaryService = dictionaryService;
            this._userService = userService;
            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        /// <summary>
        /// 更新GirdView
        /// </summary>
        /// <param name="tradeDate"></param>
        /// <param name="accountId"></param>
        /// <param name="tradeType"></param>
        /// <param name="beneficiary"></param>
        private void UpdateGridView(string tradeDate, int accountId, string tradeType, string beneficiary)
        {
            this.gridControl1.DataSource = null;

            foreach (var record in _records)
            {
                if (CommonHelper.IsDate(tradeDate))
                {
                    record.TradeDate = CommonHelper.StringToDateTime(tradeDate);
                }
                if (accountId > 0)
                {
                    record.AccountId = accountId;
                    record.AccountName = this.luAccount.Text.Trim();
                }

                if (!string.IsNullOrEmpty(tradeType))
                {
                    record.TradeType = int.Parse(tradeType);
                    record.TradeTypeName = this.cbTradeType.SelectedText;
                }

                if (!string.IsNullOrEmpty(beneficiary))
                {
                    record.Beneficiary = beneficiary;
                    record.BeneficiaryName = this.luBeneficiary.SelectedText;
                }
            }

            this.gridControl1.DataSource = _records;
        }

        /// <summary>
        /// 更新交易记录
        /// </summary>
        /// <param name="tradeDate"></param>
        /// <param name="accountId"></param>
        /// <param name="tradeType"></param>
        /// <param name="beneficiary"></param>
        private void UpdateRecords(string tradeDate, int accountId, string tradeType, string beneficiary)
        {
            var recordIds = _records.Select(x => x.RecordId).ToArray();

            var dailyRecords = _tradeRecordService.GetDailyRecordsByIds(recordIds);

            foreach (var record in dailyRecords)
            {
                if (CommonHelper.IsDate(tradeDate))
                    record.TradeDate = CommonHelper.StringToDateTime(tradeDate);

                if (accountId > 0)
                    record.AccountId = accountId;

                if (!string.IsNullOrEmpty(tradeType))
                    record.TradeType = int.Parse(tradeType);

                if (!string.IsNullOrEmpty(beneficiary))
                    record.Beneficiary = beneficiary;
            }

            _tradeRecordService.UpdateDailyRecords(dailyRecords);
        }

        #endregion Utilities

        #region Events

        private void _dialogDailyRecordEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //账户信息
                IList<AccountEntity> accounts = null;

                if (LoginInfo.CurrentUser.IsAdmin)
                {
                    accounts = _accountService.GetAccountDetails(showDisabled: true).ToList();
                }
                else
                {
                    var operateAccountIds = _accountService.GetAccountIdByOperatorId(LoginInfo.CurrentUser.UserId);
                    accounts = _accountService.GetAccountDetails(accountIds: operateAccountIds.ToArray());
                }

                accounts = accounts.OrderBy(x => x.Name).ThenBy(x => x.SecurityCompanyName).ThenBy(x => x.AttributeName).ToList();
                luAccount.Initialize(accounts, "Id", "DisplayMember", enableSearch: true);

                //交易类别
                var tradeTypes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                    .Select(x => new ComboBoxItemModel
                    {
                        Text = x.Name,
                        Value = x.Code.ToString(),
                    }).ToList();

                cbTradeType.Initialize(tradeTypes, displayAdditionalItem: true, additionalItemText: "请选择...", additionalItemValue: "");

                //受益人
                var beneficiaries = this._userService.GetAllOperators(true);
                luBeneficiary.Initialize(beneficiaries, "Code", "Name", showHeader: false, enableSearch: true);

                this.gridView1.LoadLayout(_layoutXmlName);
                this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false,columnAutoWidth:true);
                this.gridControl1.DataSource = _records;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var tradeDate = this.deTradeDate.Text.Trim();
                var accountId = string.IsNullOrEmpty(luAccount.SelectedValue()) ? 0 : int.Parse(this.luAccount.SelectedValue());
                var tradeType = this.cbTradeType.SelectedValue();
                var beneficiary = this.luBeneficiary.SelectedValue();

                if (CommonHelper.IsDate(tradeDate) || accountId > 0 || !string.IsNullOrEmpty(tradeType) || !string.IsNullOrEmpty(beneficiary))
                {
                    this.btnSave.Enabled = false;

                    UpdateGridView(tradeDate, accountId, tradeType, beneficiary);

                    UpdateRecords(tradeDate, accountId, tradeType, beneficiary);

                    this._saveSucceed = true;

                    DXMessage.ShowTips("交易数据修改成功！");
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                return;
            }
            finally
            {
                this.btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this._saveSucceed)
                RefreshEvent?.Invoke();

            this.Close();
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