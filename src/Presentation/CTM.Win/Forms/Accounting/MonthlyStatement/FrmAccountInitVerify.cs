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
    public partial class FrmAccountInitVerify : BaseForm
    {

        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly ICommonService _commonService;
        private readonly IMonthlyStatementService _statementService;

        private IList<AccountEntity> _accountInfos = null;

        #endregion

        #region Constructors
        public FrmAccountInitVerify(IDictionaryService dictionaryService,
            IAccountService accountService, 
            ICommonService commonService,
            IMonthlyStatementService statementService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._commonService = commonService;
            this._statementService = statementService;
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

        private void DisplayProfitInfoList()
        {
   

         
        }

        private void DisplayPositionInfoList()
        {
         
        }


        #endregion Utilities

        #region Events

        private void FrmAccountInitVerify_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                this.xtraTabControl1.SelectedTabPage = this.pagePosition;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
   
        }

        private void cbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if (e.Page == this.pagePosition)
                    DisplayPositionInfoList();
                else
                    DisplayProfitInfoList();
            }
            catch (Exception ex)
            {

                DXMessage.ShowError(ex.Message);
            }
        }

  

        #endregion


    }
}
