using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.User;
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

namespace CTM.Win.UI.Function.DataImport
{
    public partial class FrmTradeDataImportWizard : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        private readonly IDailyRecordService _dailyRecordService;
        private readonly IDataImportCommonService _dataImportService;
        private readonly IMarginTradingService _marginService;

        private readonly ICommonService _commonService;

        private DataTable _importDataTable;

        private EnumLibrary.SecurityAccount _securityAccount;

        #endregion Fields

        #region Constructors

        public FrmTradeDataImportWizard(
            IDictionaryService dictionaryService,
            IAccountService accountService,
            IUserService userService,
            IStockService stockService,
            IDailyRecordService dailyRecordService,
            IDataImportCommonService dataImportService,
            IMarginTradingService marginService,
            ICommonService commonService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._userService = userService;
            this._stockService = stockService;
            this._dailyRecordService = dailyRecordService;
            this._dataImportService = dataImportService;
            this._marginService = marginService;
            this._commonService = commonService;
        }

        #endregion Constructors

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

        /// <summary>
        /// 绑定数据导入信息
        /// </summary>
        private void BindDataImportInfo()
        {
            var myGridView = this.gridViewAccount;

            //Get the selected row's handle
            int selectedHandle = myGridView.GetSelectedRows()[0];

            ////所属产业
            //txtIndustryName.Text = myGridView.GetRowCellValue(selectedHandle, colIndustryName).ToString();

            //账户名称
            txtAccountName.Text = myGridView.GetRowCellValue(selectedHandle, colAccountName).ToString();
            txtAccountName.Tag = myGridView.GetRowCellValue(selectedHandle, colAccountId);

            //账户属性
            txtAccountAttribute.Text = myGridView.GetRowCellValue(selectedHandle, colAttributeName).ToString();

            //账户类型
            txtAccountType.Text = myGridView.GetRowCellValue(selectedHandle, colTypeName).ToString();

            //账户规划
            txtAccountPlan.Text = myGridView.GetRowCellValue(selectedHandle, colPlanName).ToString();

            //开户券商
            txtSecurityCompany.Text = myGridView.GetRowCellValue(selectedHandle, colSecurityCompanyName).ToString();

            //数据导入人
            txtImportUser.Text = LoginInfo.CurrentUser.UserName;

            //交易员
            var dealers = _accountService.GetAccountOperatorsByAccountId(int.Parse(myGridView.GetRowCellValue(selectedHandle, colAccountId).ToString()));

            this.luOperator.Initialize(dealers, "Code", "Name", showHeader: false, showFooter: false);

            if (dealers.Select(x => x.Code).Contains(LoginInfo.CurrentUser.UserCode))
                this.luOperator.EditValue = LoginInfo.CurrentUser.UserCode;
            else
                this.luOperator.ItemIndex = 0;
        }

        /// <summary>
        /// 绑定预览数据
        /// </summary>
        /// <param name="importFileName"></param>
        private void BindPreviewData(string importFileName)
        {
            var noneModel = new UserInfo()
            {
                Code = string.Empty,
                Name = "无",
            };

            //波段负责人
            var bandOperatorInfos = _userService.GetUserInfos(new int[] { (int)EnumLibrary.AccountingDepartment.Band, (int)EnumLibrary.AccountingDepartment.Independence }).Where(x => x.IsDeleted == false).ToList();
            bandOperatorInfos.Add(noneModel);
            bandOperatorInfos = bandOperatorInfos.OrderBy(x => x.Code).ToList();
            this.luBandPrincipal.Initialize(bandOperatorInfos, "Code", "Name", enableSearch: true);

            //目标负责人
            var targetOperatorInfos = _userService.GetUserInfos(new int[] { (int)EnumLibrary.AccountingDepartment.Target, (int)EnumLibrary.AccountingDepartment.Independence }).Where(x => x.IsDeleted == false).ToList();
            targetOperatorInfos.Add(noneModel);
            targetOperatorInfos = targetOperatorInfos.OrderBy(x => x.Code).ToList();
            this.luTargetPrincipal.Initialize(targetOperatorInfos, "Code", "Name", enableSearch: true);

            if (this._importDataTable != null) this._importDataTable.Clear();

            this._importDataTable = _dataImportService.GetImportDataFromExcel(importFileName);

            this.gridControlPreview.DataSource = this._importDataTable;
            this.gridViewPreview.PopulateColumns();
            this.gridViewPreview.BestFitColumns(true);
        }

        private void DisplayTemplateDialog(string templateFilePath)
        {
            var dialog = this.CreateDialog<_dialogImportDataTemplate>(FormBorderStyle.FixedSingle);
            dialog.TemplateFilePath = templateFilePath;
            dialog.Text = "导入数据模板Excel预览";
            dialog.Show();
        }

        #endregion Utilities

        #region Events

        private void FrmTradeDataImportWizard_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridViewAccount.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
                this.gridViewPreview.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);

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

        private void cbAccountAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.luSecurityCompany.SelectedValue()))
                BindAccountInfo();
        }

        /// <summary>
        /// 引导各画面的上一步按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControl1_PrevClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            var pageName = e.Page.Name.Trim();

            switch (pageName)
            {
                case "PageFinish":
                    this.txtImportFileName.Text = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 引导各画面的下一步按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 选择导入文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnBrowse.Enabled = false;

                var myOpenFileDialog = this.openFileDialog1;

                myOpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                myOpenFileDialog.Filter = "Excel文件|*.xlsx";
                myOpenFileDialog.RestoreDirectory = false;
                myOpenFileDialog.FileName = string.Empty;

                if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = myOpenFileDialog.FileName;

                    this.txtImportFileName.Text = myOpenFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnBrowse.Enabled = true;
            }
        }

        private void PageFinish_PageInit(object sender, EventArgs e)
        {
            this.lblImportStatus.Text = "交易数据导入成功。";
        }

        private void wizardControl1_CancelClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DXMessage.ShowYesNoAndTips("确定取消本次数据导入操作么？") == DialogResult.Yes)
                this.Close();
        }

        private void wizardControl1_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 当日成交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDay_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkDaily.Checked)
            //{
            //    this.checkDelivery.Checked = false;
            //    this.chkEntrust.Checked = false;
            //}
            //else
            //{
            //    this.chkEntrust.Checked = false;
            //    this.checkDelivery.Checked = true;
            //}
        }

        /// <summary>
        /// 交割单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkDelivery.Checked)
                this.chkEntrust.Checked = false;
            else
                this.chkEntrust.Checked = true;
        }

        /// <summary>
        /// 委托数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEntrust_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEntrust.Checked)
                this.checkDelivery.Checked = false;
            else
                this.checkDelivery.Checked = true;
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewPreview_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewAccount_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 查看导入Excel模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExcelTemplate.Enabled = false;

                var targetDirectoryName = "DataTemplate\\";

                if (chkEntrust.Checked)
                    targetDirectoryName += "Entrust";
                else if (checkDelivery.Checked)
                    targetDirectoryName += "Delivery";
                else if (checkDaily.Checked)
                    targetDirectoryName += "Daily";

                var fileName = _securityAccount.ToString() + ".xlsx";

                var templateFilePath = Path.Combine(Application.StartupPath, targetDirectoryName, fileName);

                if (!File.Exists(templateFilePath))
                {
                    DXMessage.ShowTips("Sorry！未找到对应的Excel模板文件！");
                    return;
                }

                //弹出显示模板Excel
                DisplayTemplateDialog(templateFilePath);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnExcelTemplate.Enabled = true;
            }
        }

        #endregion Events
    }
}