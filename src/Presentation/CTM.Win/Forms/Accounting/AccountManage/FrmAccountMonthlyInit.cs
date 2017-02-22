using System;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Services.Account;
using CTM.Services.Dictionary;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.AccountManage
{
    public partial class FrmAccountMonthlyInit : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;

        #endregion Fields

        #region Constructors

        public FrmAccountMonthlyInit(
            IDictionaryService dictionaryService,
            IAccountService accountService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            //账户名称
            var accountNames = _accountService.GetAllAccountNames(false).ToList();
            this.cbAccount.Properties.Items.AddRange(accountNames);

            //账户属性
            var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
            .Select(x => new ComboBoxItemModel
            {
                Value = x.Code.ToString(),
                Text = x.Name
            }).ToList();
            this.cbAccount.Initialize(accountAttributes);

            //证券公司
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay)

                        .Select(x => new ComboBoxItemModel
                        {
                            Value = x.Code.ToString(),
                            Text = x.Name
                        }).OrderBy(x => x.Text).ToList();

            this.cbSecurity.Initialize(securityCompanys);
        }

        private void BindAccountList()
        {
            throw new NotImplementedException();
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

        }

        private void cbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }


        ·   

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        #endregion Events


    }
}