using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Dictionary;
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

        #endregion Fields

        #region Constructors

        public FrmTradeDataVerify()
        {
            InitializeComponent();
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

            this.cbSecurity.Initialize(accountAttributes);

            //账户信息
            var accounts = _accountService.GetAccountInfos(onlyNeedAccounting: true, showDisabled: true)
                 .Select(x => new AccountInfoModel
                 {
                     AccountId = x.Id,
                     AccountName = x.Name,
                     AttributeName = x.AttributeName,
                     SecurityCompanyName = x.SecurityCompanyName,
                     TypeName = x.TypeName,
                     DisplayMember = x.Name + " - " + x.SecurityCompanyName + " - " + x.AttributeName + " - " + x.TypeName,
                 }
                )
               .ToList();

            this.luAccount.Initialize(accounts, "AccountId", "DispalyMember", showHeader: true, enableSearch: true);


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
            BindSearchInfo();
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

            }
            catch (Exception ex)
            {

                DXMessage.ShowError (ex.Message     );
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }

        }



        #endregion Events


    }
}