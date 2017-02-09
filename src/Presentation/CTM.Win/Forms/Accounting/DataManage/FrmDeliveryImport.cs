using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Forms.Common;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.DataManage
{
    public partial class FrmDeliveryImport : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IAccountService _accountService;
        private readonly IDeliveryRecordService _deliveryRecordService;
        private readonly IDataImportCommonService _dataImportCommonService;
        private readonly ICommonService _commonService;

        private EnumLibrary.SecurityAccount _securityAccount;
        private IniConfigHelper _iniConfigHelper;
        private bool _accountViewFirstDisplay = false;
        private IList<DataRow> _skippedRecords = null;

        #endregion Fields

        #region Constructors

        public FrmDeliveryImport
            (
            IDictionaryService dictionaryService,
            IAccountService accountService,
            IDeliveryRecordService deliveryRecordService,
            IDataImportCommonService dataImportCommonService,
            ICommonService commonService
            )
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
            this._accountService = accountService;
            this._deliveryRecordService = deliveryRecordService;
            this._dataImportCommonService = dataImportCommonService;
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
            }
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

        #endregion Utilities

        #region Events

        private void FrmDeliveryImport_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridViewAccount.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, rowIndicatorWidth: 30);
                this.gridViewPreview.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, rowIndicatorWidth: 50, columnAutoWidth: true);
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

        private void cbAccountAttribute_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.luSecurityCompany.SelectedValue()))
                BindAccountInfo();
        }

        private void btnExcelTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExcelTemplate.Enabled = false;

                var targetDirectoryName = "DataTemplate\\AccountingDelivery";

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

        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnFileSelect.Enabled = false;

                var myOpenFileDialog = this.openFileDialog1;
                // var defaultPath = this._iniConfigHelper.GetString("Investor", "TradeDataImportPath", null);
                //myOpenFileDialog.InitialDirectory = string.IsNullOrEmpty(defaultPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : defaultPath;
                myOpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                myOpenFileDialog.Filter = "Excel文件|*.xlsx";
                myOpenFileDialog.RestoreDirectory = false;
                myOpenFileDialog.FileName = string.Empty;

                if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtFilePath.Text = myOpenFileDialog.FileName;
                   // this._iniConfigHelper.WriteValue("Accounting", "TradeDataImportPath", Path.GetDirectoryName(myOpenFileDialog.FileName));

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
            try
            {
                this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.marqueeProgressBarControl1.Visible = true;
                this.marqueeProgressBarControl1.Text = "数据导入中...请稍后...";
                this.marqueeProgressBarControl1.Properties.ShowTitle = true;

                var bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(DataImportProcess);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DataImportCompleted);
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                this.PageFinish.AllowBack = true;
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 交易数据导入处理
        /// </summary>
        /// <returns></returns>
        private void DataImportProcess(object sender, DoWorkEventArgs e)
        {
            try
            {
                var source = this.gridControlPreview.DataSource as DataTable;

                if (source?.Rows.Count > 0)
                {
                    var accountId = int.Parse(this.gridViewAccount.GetRowCellValue(this.gridViewAccount.FocusedRowHandle, colAccountId).ToString());

                    var operationInfo = new RecordImportOperationEntity
                    {
                        AccountId = accountId,
                        OperatorCode = string.Empty,
                        ImportTime = _commonService.GetCurrentServerTime(),
                        ImportUserCode = LoginInfo.CurrentUser.UserCode,
                        DataType = EnumLibrary.DataType.Delivery,
                    };

                    _skippedRecords = null;
                    _deliveryRecordService.DataImportProcess(_securityAccount, source, operationInfo, out _skippedRecords);
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void DataImportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.marqueeProgressBarControl1.Properties.Stopped = true;
            this.marqueeProgressBarControl1.Visible = false;
            this.esiImportResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            if (e.Error == null && e.Result == null)
            {
                var allCount = gridViewPreview.DataRowCount;
                var skippedCount = _skippedRecords.Count;
                var succeedCount = allCount - skippedCount;

                this.esiImportResult.Text = $@"交易数据导入完成。 ( 记录总数：{allCount}    导入：{succeedCount}     忽略：{skippedCount} )";           
                this.esiImportResult.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
                this.PageFinish.AllowBack = false;

                if (_skippedRecords != null && _skippedRecords.Count > 0)
                {
                    this.lcgSkip.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.gridControlSkip.DataSource = _skippedRecords.CopyToDataTable();
                    this.gridViewSkip.PopulateColumns();
                    this.gridViewSkip.BestFitColumns();
                }
            }
            else
            {
                var msg = e.Error == null ? e.Result?.ToString() : e.Error.Message;
                this.esiImportResult.Text = msg;
                this.esiImportResult.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
                this.PageFinish.AllowBack = true;
            }
        }

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

                        BindDataImportInfo();
                        break;

                    case "PageImport":

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

                        this.esiImportResult.Text = string.Empty;
                        this.esiImportResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        this.lcgSkip.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        this.PageFinish.AllowBack = false;

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

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
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

        private void gridViewPreview_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewSkip_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
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