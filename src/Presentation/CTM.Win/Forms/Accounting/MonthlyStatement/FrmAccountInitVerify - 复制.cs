using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM.Core;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.MonthlyStatement;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraEditors.Controls;

namespace CTM.Win.Forms.Accounting.MonthlyStatement
{
    public partial class FrmAccountInitVerify2 : BaseForm
    {

        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IMonthlyStatementService _monthEndService;

        private IList<AccountEntity> _accountInfos = null;
        #endregion

        #region Constructors
        public FrmAccountInitVerify2(IDictionaryService dictionaryService,
            IAccountService accountService, 
            ICommonService commonService,
            IMonthlyStatementService monthEndService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._commonService = commonService;
            this._monthEndService = monthEndService;
        }

        #endregion

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
        }

        private void BindAccountList()
        {
            this.lbAccount.Items.Clear();

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

            this.lbAccount.DataSource = source;
            this.lbAccount.ValueMember = "AccountId";
            this.lbAccount.DisplayMember = "DisplayMember";
        }


        #endregion Utilities

        #region Events

        private void FrmAccountInitVerify_Load(object sender, EventArgs e)
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

        private void lbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #endregion


    }
}
