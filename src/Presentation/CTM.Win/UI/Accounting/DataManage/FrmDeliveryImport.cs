using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.Account;
using CTM.Services.Account;
using CTM.Services.Common;
using CTM.Services.Dictionary;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.UI.Common;
using CTM.Win.Util;

namespace CTM.Win.UI.Accounting.DataManage
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
                DXMessage.ShowTips(string.Format("证券公司【{0}】的【{1}】账户暂不支持数据导入功能，请联系管理员！", securityCompanyName, accountAttributeName));

                return;
            }

            var accounts = _accountService.GetAccountDetails(securityCompanyCode: securityCompanyCode, attributeCode: accountAttributeCode)
                                    .OrderBy(x => x.Name)
                                    .ToList();

            if (accounts.Any())
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
            int selectedHandle = this.gridViewAccount.GetSelectedRows()[0];

            var selectedAccountInfo = this.gridViewAccount.GetRow(selectedHandle) as AccountInfo;

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

            var deliveryData = _dataImportCommonService.GetImportDataFromExcel(importFileName);

            this.gridControlPreview.DataSource = deliveryData;
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
                this.gridViewAccount.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
                this.gridViewPreview.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, rowIndicatorWidth: 60);

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

                myOpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                myOpenFileDialog.Filter = "Excel文件|*.xlsx";
                myOpenFileDialog.RestoreDirectory = false;
                myOpenFileDialog.FileName = string.Empty;

                if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = myOpenFileDialog.FileName;

                    this.txtFilePath.Text = myOpenFileDialog.FileName;

                    //导入数据预览
                    BindPreviewData(fileName);
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
                this.lblFinish.Visible = false;
                this.PageFinish.AllowBack = false;

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
                    int selectedHandle = this.gridViewAccount.GetSelectedRows()[0];

                    var accountInfo = this.gridViewAccount.GetRow(selectedHandle) as AccountInfo;

                    var operationInfo = new RecordImportOperationEntity
                    {
                        AccountId = accountInfo.Id,
                        OperatorCode = string.Empty,
                        ImportTime = _commonService.GetCurrentServerTime(),
                        ImportUserCode = LoginInfo.CurrentUser.UserCode,
                        DataType = EnumLibrary.DataType.Delivery,
                    };
                    _deliveryRecordService.DataImportProcess(_securityAccount, source, operationInfo);
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void DataImportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.marqueeProgressBarControl1.Properties.Stopped = true;
            this.marqueeProgressBarControl1.Visible = false;
            this.lblFinish.Visible = true;

            if (e.Error == null && e.Result == null)
            {
                this.lblFinish.Text = "数据导入完成！";
                this.lblFinish.ForeColor = System.Drawing.Color.Black;
                this.PageFinish.AllowBack = false;
            }
            else
            {
                var msg = e.Error == null ? e.Result?.ToString() : e.Error.Message;
                this.lblFinish.Text = msg;
                this.lblFinish.ForeColor = System.Drawing.Color.Red;
                this.PageFinish.AllowBack = true;
            }
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            if (DXMessage.ShowYesNoAndTips("确定取消本次数据导入操作么？") == DialogResult.Yes)
                this.Close();
        }

        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            var pageName = e.Page.Name.Trim();

            try
            {
                switch (pageName)
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

                    case "PageImport":

                        //导入的Excel文件路径
                        string importFileName = this.txtFilePath.Text.Trim();
                        if (string.IsNullOrEmpty(importFileName))
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

        private void wizardControl1_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }

        private void gridViewAccount_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewPreview_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}