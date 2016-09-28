using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Dictionary;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Accounting.DataManage
{
    public partial class FrmTradeDataVerify : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IDataVerifyService _dataVerifyService;

        private IList<AccountInfoModel> _accounts;

        private const string _layoutXmlName = "FrmTradeDataVerify";

        #endregion Fields

        #region Constructors

        public FrmTradeDataVerify
            (
             IDictionaryService dictionaryService,
             IAccountService accountService,
             IDataVerifyService dataVerifyService
            )
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._dataVerifyService = dataVerifyService;
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
            _accounts = _accountService.GetAccountDetails(onlyNeedAccounting: true, showDisabled: true)
                 .Select(x => new AccountInfoModel
                 {
                     AccountId = x.Id,
                     AccountName = x.Name,
                     AttributeCode = x.AttributeCode,
                     AttributeName = x.AttributeName,
                     SecurityCompanyCode = x.SecurityCompanyCode,
                     SecurityCompanyName = x.SecurityCompanyName,
                     TypeName = x.TypeName,
                     DisplayMember = x.Name + " - " + x.SecurityCompanyName + " - " + x.AttributeName + " - " + x.TypeName,
                 }
                )
                .OrderBy(x=>x.AccountName )
               .ToList();

            this.luAccount.Initialize(_accounts, "AccountId", "DisplayMember", showHeader: true, enableSearch: true);

            var now = DateTime.Now.Date;

            //开始时间
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(now);

            //结束时间
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = CommonHelper.GetLastDayOfMonth(now);
        }

        #endregion Utilities

        #region Events

        private void FrmTradeDataVerify_Load(object sender, EventArgs e)
        {
            try
            {
                BindSearchInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbAccountAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountFilter();
        }

        private void AccountFilter()
        {
            if (!string.IsNullOrEmpty(this.cbAccountAttribute.SelectedValue()) || !string.IsNullOrEmpty(this.cbSecurity.SelectedValue()))
            {
                var source = new List<AccountInfoModel>();
                source.AddRange(_accounts);

                if (!string.IsNullOrEmpty(this.cbAccountAttribute.SelectedValue()))
                    source = source?.Where(x => x.AttributeCode == int.Parse(this.cbAccountAttribute.SelectedValue())).ToList();

                if (!string.IsNullOrEmpty(this.cbSecurity.SelectedValue()))
                    source = source?.Where(x => x.SecurityCompanyCode == int.Parse(this.cbSecurity.SelectedValue())).ToList();

                this.luAccount.Properties.DataSource = source;
                this.luAccount.Properties.DropDownRows = source.Count;
            }
        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountFilter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                if (string.IsNullOrEmpty(this.luAccount.SelectedValue()))
                {
                    DXMessage.ShowTips("请选择账号信息！");
                    return;
                }

                var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
                var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());
                var accountId = int.Parse(this.luAccount.SelectedValue());

                var diffInfos = _dataVerifyService.GetDiffBetweenDeliveryAndDailyData(accountId, dateFrom, dateTo);

                this.gridControl1.DataSource = diffInfos;
                this.gridView1.PopulateColumns();
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