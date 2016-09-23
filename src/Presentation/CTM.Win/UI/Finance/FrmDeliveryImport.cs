using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.MarginTrading;
using CTM.Services.Stock;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Finance
{
    public partial class FrmDeliveryImport : BaseForm 
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        private readonly IDailyRecordService _tradeRecordService;
        private readonly IMarginTradingService _marginService;

        private readonly ICommonService _commonService;

        #endregion


        #region Constructors
        public FrmDeliveryImport()
        {
            InitializeComponent();
        }
        #endregion

        #region Utilities

        /// <summary>
        /// 绑定账户属性
        /// </summary>
        private void BindAccountAttribute()
        {
            //取得账户属性信息
            var accountAttributes = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.AccountAttribute)
                .Select(x => new ComboBoxItemModel
                {
                    Value = x.Code.ToString(),
                    Text = x.Name
                }).ToList();

            this.cbAccountAttribute.Initialize(accountAttributes);
        }

        /// <summary>
        ///绑定证券公司信息
        /// </summary>
        private void BindSecurityCompany()
        {
            //取得证券公司信息
            var securityCompanys = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.SecurityCompay).OrderBy(x => x.Name).ToList();

            this.luSecurityCompany.Initialize(securityCompanys, "Code", "Name", enableSearch: true);
        }

        /// <summary>
        /// 绑定账户信息
        /// </summary>
        private void BindAccountInfo()
        {
            //证券公司
            var securityCompanyName = this.luSecurityCompany.Text.Trim();
            var securityCompanyCode = int.Parse(this.luSecurityCompany.EditValue.ToString().Trim());

            //账户属性
            var accountAttribute = this.cbAccountAttribute.SelectedItem as ComboBoxItemModel;
            var accountAttributeName = accountAttribute.Text.Trim();
            var accountAttributeCode = int.Parse(accountAttribute.Value.Trim());

            //检查选中的证券公司和账户属性是否支持导入处理
            if (!CheckSeletedObjectImportFunciton(securityCompanyName, accountAttributeName))
            {
                DXMessage.ShowTips(string.Format("证券公司【{0}】的【{1}】账户暂不支持数据导入功能，请联系管理员！", securityCompanyName, accountAttributeName));

                return;
            }

            var accounts = _accountService.GetAccountDetails(securityCompanyCode: securityCompanyCode, attributeCode: accountAttributeCode);

            if (accounts.Count > 0)
            {
                this.gridControlAccount.DataSource = accounts;
                this.gridViewAccount.SelectRow(0);
            }
            else
            {
                DXMessage.ShowTips(string.Format("证券公司【{0}】没有账户属性为【{1}】的账户信息，请重新选择！", securityCompanyName, accountAttributeName));
            }
        }

        #endregion

        #region Events
        private void FrmDeliveryImport_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridViewAccount.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
               //this.gridViewPreview.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);

                BindAccountAttribute();
                BindSecurityCompany();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luSecurityCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cbAccountAttribute.SelectedIndex != -1)
                BindAccountInfo();
        }

        private void cbAccountAttribute_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.luSecurityCompany.SelectedValue()))
                BindAccountInfo();
        }

        private void btnExcelTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnFileSelect_Click(object sender, EventArgs e)
        {

        }

        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            var pageName = e.Page.Name.Trim();

            try
            {
                switch (pageName)
                {
                    #region 账户选择画面

                    case "PageAccount":

                        if (this.luSecurityCompany.EditValue == null || this.luSecurityCompany.EditValue.ToString() == "nulltext")
                        {
                            DXMessage.ShowTips("请选择证券公司！");
                            e.Handled = true;
                            return;
                        }

                        //账户属性
                        if (this.cbAccountAttribute.SelectedIndex == -1)
                        {
                            DXMessage.ShowTips("请选择账户属性！");
                            e.Handled = true;
                            return;
                        }

                        if (this.gridViewAccount.SelectedRowsCount != 1)
                        {
                            DXMessage.ShowTips("请选择一个账户！");
                            e.Handled = true;
                            return;
                        }
                        else
                        {
                            var rowHandles = this.gridViewAccount.GetSelectedRows();

                            var operators = this.gridViewAccount.GetRowCellValue(rowHandles[0], colOperatorNames);

                            if (operators == null || string.IsNullOrEmpty(operators.ToString()))
                            {
                                DXMessage.ShowTips("该账号未设置操作人员，请联系管理员！");
                                e.Handled = true;
                                return;
                            }
                            else
                                BindDataImportInfo();
                        }
                        break;

                    #endregion 账户选择画面

                    #region 导入数据选择画面

                    case "PageSelectFile":

                        //交易员
                        if (string.IsNullOrEmpty(this.luOperator.SelectedValue()))
                        {
                            DXMessage.ShowTips("请选择交易员！");
                            e.Handled = true;
                            return;
                        }

                        //导入的Excel文件路径
                        string importFileName = this.txtImportFileName.Text.Trim();
                        if (string.IsNullOrEmpty(importFileName))
                        {
                            DXMessage.ShowTips("请选择要导入的交易数据Excel文件！");
                            e.Handled = true;
                            return;
                        }

                        //导入的Excel是否存在
                        if (!System.IO.File.Exists(importFileName))
                        {
                            DXMessage.ShowTips("该Excel文件不存在！");
                            e.Handled = true;
                            return;
                        }

                        //导入数据预览
                        BindPreviewData(importFileName);

                        break;

                    #endregion 导入数据选择画面

                    #region 数据预览画面

                    case "PagePreview":

                        if (this.gridViewPreview.DataRowCount == 0)
                        {
                            this.PagePreview.AllowNext = false;
                        }
                        else
                        {
                            //数据导入
                            if (!DataImportProcess())
                            {
                                e.Handled = true;
                                return;
                            }
                        }

                        break;

                        #endregion 数据预览画面
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                e.Handled = true;
                return;
            }
        }

        #endregion


    }
}
