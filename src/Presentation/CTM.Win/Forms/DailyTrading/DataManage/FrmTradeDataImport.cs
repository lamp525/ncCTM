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
using CTM.Win.Forms.Common;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.DailyTrading.DataManage
{
    public partial class FrmTradeDataImport : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        private readonly IDailyRecordService _dailyRecordService;
        private readonly IDataImportCommonService _dataImportCommonService;
        private readonly IMarginTradingService _marginService;
        private readonly ICommonService _commonService;

        private EnumLibrary.SecurityAccount _securityAccount;
        private IniConfigHelper _iniConfigHelper;
        private bool _accountViewFirstDisplay = false;
        private IList<DataRow> _skippedRecords = null;

        #endregion Fields

        #region Constructors

        public FrmTradeDataImport(
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
            this._dataImportCommonService = dataImportService;
            this._marginService = marginService;
            this._commonService = commonService;

            //string configFilePath = System.Configuration.ConfigurationManager.AppSettings["ConfigFilePath"].ToString();

            //configFilePath = Path.Combine(Application.StartupPath, configFilePath);

            //this._iniConfigHelper = string.IsNullOrEmpty(configFilePath) ? new IniConfigHelper() : new IniConfigHelper(configFilePath);
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
            _securityAccount = _dataImportCommonService.GetSelectedSecurityCompanyEnum(securityCompanyName, accountAttributeName);
            if (_securityAccount == EnumLibrary.SecurityAccount.Unknown)
            {
                DXMessage.ShowTips($"证券公司【{securityCompanyName}】的【{accountAttributeName}】账户暂不支持数据导入功能，请联系管理员！");
                return;
            }

            var accounts = _accountService.GetAccountDetails(securityCompanyCode: securityCompanyCode, attributeCode: accountAttributeCode).OrderBy(x => x.Name).ToList();
            this.gridControlAccount.DataSource = accounts;

            if (accounts.Count == 0)
            {
                DXMessage.ShowTips($"证券公司【{securityCompanyName}】没有账户属性为【{accountAttributeName}】的账户信息，请重新选择！");
            }
            else if (accounts.Count > 1)
            {
                this.gridViewAccount.ClearSelection();
                _accountViewFirstDisplay = true;
            }
        }

        /// <summary>
        /// 绑定数据导入信息
        /// </summary>
        private void BindDataImportInfo()
        {
            var selectedAccountInfo = this.gridViewAccount.GetFocusedRow() as AccountEntity;

            if (selectedAccountInfo != null)
            {
                this.txtAccountInfo.Text = selectedAccountInfo.Name + " - " + selectedAccountInfo.SecurityCompanyName + " - " + selectedAccountInfo.AttributeName + " - " + selectedAccountInfo.TypeName + " - " + selectedAccountInfo.PlanName;

                //交易员
                var dealers = _accountService.GetAccountOperatorsByAccountId(selectedAccountInfo.Id);

                this.luOperator.Initialize(dealers, "Code", "Name", showHeader: false, showFooter: false);

                if (dealers.Select(x => x.Code).Contains(LoginInfo.CurrentUser.UserCode))
                    this.luOperator.EditValue = LoginInfo.CurrentUser.UserCode;
                else
                    this.luOperator.ItemIndex = 0;
            }

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
        }

        /// <summary>
        /// 绑定预览数据
        /// </summary>
        /// <param name="importFileName"></param>
        private void BindPreviewData(string importFileName)
        {
            this.gridControlPreview.DataSource = null;

            var tradeData = _dataImportCommonService.GetImportDataFromExcel(importFileName);

            this.gridControlPreview.DataSource = tradeData;
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

        /// <summary>
        /// 交易数据导入处理
        /// </summary>
        /// <returns></returns>
        private bool DataImportProcess()
        {
            bool result = false;

            try
            {
                var source = this.gridControlPreview.DataSource as DataTable;

                if (source?.Rows.Count > 0)
                {
                    var accountId = int.Parse(this.gridViewAccount.GetRowCellValue(this.gridViewAccount.FocusedRowHandle, colAccountId).ToString());
                    var operationInfo = new RecordImportOperationEntity
                    {
                        AccountId = accountId,
                        OperatorCode = this.luOperator.SelectedValue(),
                        ImportTime = _commonService.GetCurrentServerTime(),
                        ImportUserCode = LoginInfo.CurrentUser.UserCode,
                        DataType = this.chkDelivery.Checked ? EnumLibrary.DataType.Delivery : EnumLibrary.DataType.Entrust,
                        BandPrincipal = this.luBandPrincipal.SelectedValue(),
                        TargetPrincipal = this.luTargetPrincipal.SelectedValue(),
                    };

                    _skippedRecords = null;
                    _dailyRecordService.DataImportProcess(_securityAccount, source, operationInfo, out _skippedRecords);
                }

                result = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }

            return result;
        }

        #endregion Utilities

        #region Events

        private void FrmTradeDataImportWizard_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridViewAccount.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, rowIndicatorWidth: 30);
                this.gridViewPreview.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, columnAutoWidth: true);
                this.gridViewSkip.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, columnAutoWidth: true);

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
            switch (e.Page.Name.Trim())
            {
                case "PageFinish":
                    this.txtFilePath.Text = string.Empty;
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
            try
            {
                switch (e.Page.Name.Trim())
                {
                    case "PageAccount":

                        if (string.IsNullOrEmpty(this.luSecurityCompany.SelectedValue()))
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
                            var operators = this.gridViewAccount.GetRowCellValue(this.gridViewAccount.FocusedRowHandle, colOperatorNames);

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

                    case "PageImport":

                        //交易员
                        if (string.IsNullOrEmpty(this.luOperator.SelectedValue()))
                        {
                            DXMessage.ShowTips("请选择交易员！");
                            e.Handled = true;
                            return;
                        }

                        //导入的Excel文件路径
                        if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
                        {
                            DXMessage.ShowTips("请选择要导入的交易数据Excel文件！");
                            e.Handled = true;
                            return;
                        }

                        if (this.gridViewPreview.DataRowCount == 0)
                        {
                            DXMessage.ShowTips("该Excel文件不存在交易记录！");
                            e.Handled = true;
                            return;
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

                        this.lcgSkip.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                        break;
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
        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnFileSelect.Enabled = false;

                var myOpenFileDialog = this.openFileDialog1;
                //var defaultPath = this._iniConfigHelper.GetString("Investor", "TradeDataImportPath", null);
                //myOpenFileDialog.InitialDirectory = string.IsNullOrEmpty(defaultPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : defaultPath;
                myOpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                myOpenFileDialog.Filter = "Excel文件|*.xlsx";
                myOpenFileDialog.RestoreDirectory = false;
                myOpenFileDialog.FileName = string.Empty;

                if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtFilePath.Text = myOpenFileDialog.FileName;
                    //this._iniConfigHelper.WriteValue("Investor", "TradeDataImportPath", Path.GetDirectoryName(myOpenFileDialog.FileName));

                    //导入数据预览
                    BindPreviewData(myOpenFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnFileSelect.Enabled = true;
            }
        }

        private void PageFinish_PageInit(object sender, EventArgs e)
        {
            this.esiImportResult.Text = "交易数据导入完成。";

            if (_skippedRecords != null && _skippedRecords.Count > 0)
            {
                this.lcgSkip.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.gridControlSkip.DataSource = _skippedRecords.CopyToDataTable();
                this.gridViewSkip.PopulateColumns();
                this.gridViewSkip.BestFitColumns();
            }
        }

        private void wizardControl1_CancelClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DXMessage.ShowYesNoAndTips("确定取消本次数据导入操作么？") == DialogResult.Yes)
                this.Close();
        }

        private void wizardControl1_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.wizardControl1.SelectedPage == PageFinish)
                this.Close();
            else
            {
                if (DXMessage.ShowYesNoAndTips("确定取消本次数据导入操作么？") == DialogResult.Yes)
                    this.Close();
            }
        }

        private void chkDelivery_CheckedChanged(object sender, EventArgs e)
        {
            chkEntrust.Checked = !chkDelivery.Checked;
        }

        private void chkEntrust_CheckedChanged(object sender, EventArgs e)
        {
            chkDelivery.Checked = !chkEntrust.Checked;
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
                if (this._accountViewFirstDisplay)
                    e.Info.ImageIndex = -1;
            }
        }

        private void gridViewAccount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this._accountViewFirstDisplay = false;
            this.gridViewAccount.UnselectRow(e.PrevFocusedRowHandle);
            this.gridViewAccount.SelectRow(e.FocusedRowHandle);
        }

        private void gridViewSkip_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            (sender as DevExpress.XtraGrid.Views.Grid.GridView).DrawRowIndicator(e);
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
                else if (chkDelivery.Checked)
                    targetDirectoryName += "Delivery";

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

        private void btnExportSkip_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExportSkip.Enabled = false;
                var fileName = Path.GetFileNameWithoutExtension(this.txtFilePath.Text) + " （未导入）";
                this.gridViewSkip.ExportToExcelAndOpen(fileName);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnExportSkip.Enabled = true;
            }
        }

        #endregion Events
    }
}