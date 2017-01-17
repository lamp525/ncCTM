using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Department;
using CTM.Services.StatisticsReport;
using CTM.Services.TKLine;
using CTM.Services.TradeRecord;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using Excel = Microsoft.Office.Interop.Excel;

namespace CTM.Win.Forms.DailyTrading.ReportExport
{
    public partial class FrmInvestIncomeReportExport : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IDepartmentService _departmentService;
        private readonly IDailyStatisticsReportService _statisticsReportService;
        private readonly ITKLineService _tKLineService;
        private readonly IUserService _userService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        #endregion Fields

        #region Constructors

        public FrmInvestIncomeReportExport(
            IDailyRecordService dailyRecordService,
            IDepartmentService departmentService,
            IDailyStatisticsReportService statisticsReportService,
            ITKLineService tKLineService,
            IUserService userService
            )
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
            this._departmentService = departmentService;
            this._statisticsReportService = statisticsReportService;
            this._tKLineService = tKLineService;
            this._userService = userService;
        }

        #endregion Constructors

        #region Utilities

        private void BindSearchInfo()
        {
            //截至交易日
            this.deEnd.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deEnd.EditValue = now.Date.AddDays(-1);
            else
                this.deEnd.EditValue = now.Date;

            //部门
            var deptInfos = this._departmentService.GetAllAccountingDepartmentInfo()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);
            this.cbDepartment.DefaultSelected(((int)EnumLibrary.AccountingDepartment.Day).ToString());

            //报表类型
            var reportTypes = new List<ComboBoxItemModel>
            {
                new ComboBoxItemModel { Text = CTMHelper.GetReportTypeName ((int)EnumLibrary.ReportType.Day), Value = EnumLibrary.ReportType.Day.ToString() },
                new ComboBoxItemModel { Text = CTMHelper.GetReportTypeName ((int)EnumLibrary.ReportType.Week), Value = EnumLibrary.ReportType.Week.ToString() },
                new ComboBoxItemModel { Text = CTMHelper.GetReportTypeName ((int)EnumLibrary.ReportType.Month), Value = EnumLibrary.ReportType.Month.ToString() },
            };

            this.cbReportType.Initialize(reportTypes, displayAdditionalItem: false);
            this.cbReportType.DefaultSelected(EnumLibrary.ReportType.Day.ToString());

            //保存路径
            this.txtSavePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private string GetReportTemplateFilePath(int deptId)
        {
            string directoryName = "ReportTemplate\\UserDailyProfitFlow";
            string fileName = string.Empty;

            switch ((EnumLibrary.AccountingDepartment)deptId)
            {
                case EnumLibrary.AccountingDepartment.Independence:
                    fileName = "IndependenceReport.xlsx";
                    break;

                case EnumLibrary.AccountingDepartment.Target:
                    fileName = "TargetReport.xlsx";
                    break;

                case EnumLibrary.AccountingDepartment.Band:
                    fileName = "BandReport.xlsx";
                    break;

                case EnumLibrary.AccountingDepartment.Day:
                    fileName = "DayReport.xlsx";
                    break;

                default:
                    break;
            }

            return Path.Combine(Application.StartupPath, directoryName, fileName);
        }

        private string GetReportDestinyFilePath(int deptId, string savePath)
        {
            string directoryName = savePath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string typeName = string.Empty;

            switch ((EnumLibrary.AccountingDepartment)deptId)
            {
                case EnumLibrary.AccountingDepartment.Independence:
                    typeName = "独立核算";
                    break;

                case EnumLibrary.AccountingDepartment.Target:
                    typeName = "目标";
                    break;

                case EnumLibrary.AccountingDepartment.Band:
                    typeName = "波段";
                    break;

                case EnumLibrary.AccountingDepartment.Day:
                    typeName = "短差";
                    break;

                default:
                    break;
            }

            var fileName = $"证券投资部收益报表({typeName})" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            return Path.Combine(directoryName, fileName);
        }

        private void CreateReport(DateTime endDate, int deptId, string reportType, string savePath)
        {
            // var tradeType = CTMHelper.GetTradeTypeByDepartment(deptId);
            var templateFilePath = GetReportTemplateFilePath(deptId);

            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException("报表模板Excel文件不存在！");
            }

            var destinyFileName = GetReportDestinyFilePath(deptId, savePath);

            var reportData = GetReportData(endDate, deptId, reportType);

            WriteDataToExcel(reportData, deptId, templateFilePath, destinyFileName);
        }

        private void WriteDataToExcel(IDictionary<string, IList<UserInvestIncomeEntity>> reportData, int deptId, string templateFilePath, string destinyFilePath)
        {
            if (reportData == null)
                throw new NullReferenceException(nameof(reportData));

            Excel.Application excelApp = new Excel.Application();

            Excel.Workbook workbook = excelApp.Workbooks.Open(templateFilePath);

            try
            {
                foreach (var item in reportData)
                {
                    var investorName = item.Key;
                    Excel._Worksheet worksheet = null;

                    foreach (Excel.Worksheet sheet in workbook.Sheets)
                    {
                        if (sheet.Name == investorName)
                            worksheet = sheet;
                    }

                    if (worksheet == null) continue;

                    WorksheetFormatting(worksheet);

                    var startRowIndex = 34;

                    var data = item.Value;

                    for (int i = 0; i < data.Count; i++)
                    {
                        var investInfo = data[i];

                        if (deptId != (int)EnumLibrary.AccountingDepartment.Day)
                        {
                            //周一（万元）
                            worksheet.Cells[startRowIndex + i, 3] = investInfo.MondayPositionValue / (int)EnumLibrary.NumericUnit.TenThousand;

                            //净资产（万元）
                            worksheet.Cells[startRowIndex + i, 4] = investInfo.CurrentAsset / (int)EnumLibrary.NumericUnit.TenThousand;

                            //持仓市值（万元）
                            worksheet.Cells[startRowIndex + i, 9] = investInfo.PositionValue / (int)EnumLibrary.NumericUnit.TenThousand;

                            //持仓仓位
                            worksheet.Cells[startRowIndex + i, 12] = investInfo.PositionRate;
                        }

                        //日期
                        worksheet.Cells[startRowIndex + i, 2] = investInfo.TradeTime;

                        //累计收益额（万元）
                        worksheet.Cells[startRowIndex + i, 5] = investInfo.AccumulatedActualProfit / (int)EnumLibrary.NumericUnit.TenThousand;

                        //当日收益率
                        worksheet.Cells[startRowIndex + i, 6] = investInfo.CurrentIncomeRate;

                        //日收益额（万元）
                        worksheet.Cells[startRowIndex + i, 7] = investInfo.CurrentActualProfit / (int)EnumLibrary.NumericUnit.TenThousand;

                        //累计收益率
                        worksheet.Cells[startRowIndex + i, 8] = investInfo.AccumulatedIncomeRate;
                    }
                }

                workbook.SaveAs(destinyFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                workbook = null;
                excelApp.Quit();
                excelApp = null;
            }
        }

        private void WorksheetFormatting(Excel._Worksheet worksheet)
        {
            //日期
            Excel.Range rngColB = worksheet.Columns["B", Type.Missing];
            rngColB.NumberFormatLocal = @"yy/mm/dd";

            //周一（万元）
            Excel.Range rngColC = worksheet.Columns["C", Type.Missing];
            rngColC.NumberFormatLocal = @"0";

            //净资产（万元）
            Excel.Range rngColD = worksheet.Columns["D", Type.Missing];
            rngColD.NumberFormatLocal = @"0.00";

            //累计收益额（万元）
            Excel.Range rngColE = worksheet.Columns["E", Type.Missing];
            rngColE.NumberFormatLocal = @"0.00";

            ////当日收益率
            //Excel.Range rngColF = worksheet.Columns["F", Type.Missing];
            //rngColF.NumberFormatLocal = @"0.00";

            //日收益额（万元）
            Excel.Range rngColG = worksheet.Columns["G", Type.Missing];
            rngColG.NumberFormatLocal = @"0.00";

            ////累计收益率
            //Excel.Range rngColH = worksheet.Columns["H", Type.Missing];
            //rngColH.NumberFormatLocal = @"0.00";

            ////持仓市值（万元）
            //Excel.Range rngColI = worksheet.Columns["I", Type.Missing];
            //rngColI.NumberFormatLocal = @"0.0";

            ////持仓仓位
            //Excel.Range rngColL = worksheet.Columns["L", Type.Missing];
            //rngColL.NumberFormatLocal = @"0.00";
        }

        private IDictionary<string, IList<UserInvestIncomeEntity>> GetReportData(DateTime endDate, int deptId, string reportType)
        {
            IDictionary<string, IList<UserInvestIncomeEntity>> result = new Dictionary<string, IList<UserInvestIncomeEntity>>();

            IList<UserInfo> investors = _userService.GetUserInfos(departmentIds: new int[] { deptId }).Where(x => x.IsDeleted == false).ToList();

            //合计
            var summation = new UserInfo
            {
                Code = string.Empty,
                Name = "合计",
                AllotFund = investors.Sum(x => x.AllotFund),
            };

            investors.Add(summation);

            var queryDates = new List<DateTime>();

            //日报表
            if (reportType == EnumLibrary.ReportType.Day.ToString())
                //取得26个交易日日期
                queryDates = CommonHelper.GetWorkdaysBeforeCurrentDay(endDate, 26).OrderBy(x => x).ToList();
            else
                return result;

            foreach (var investor in investors)
            {
                var statisticalInvestorCodes = new List<string>();

                if (string.IsNullOrEmpty(investor.Code))

                    statisticalInvestorCodes = investors.Where(x => !string.IsNullOrEmpty(x.Code)).Select(x => x.Code).ToList();
                else
                    statisticalInvestorCodes.Add(investor.Code);

                //交易记录
                var tradeRecords = _dailyRecordService.GetDailyRecords(tradeType: 0, beneficiaries: statisticalInvestorCodes.ToArray(), tradeDateFrom: _initDate, tradeDateTo: endDate).ToList();

                IList<UserInvestIncomeEntity> investIncome = new List<UserInvestIncomeEntity>();
                if (tradeRecords.Any())
                {
                    //交易记录中的所有股票代码
                    var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
                    //所有交易日期
                    var tradeDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), endDate);

                    //各交易日所有股票收盘价
                    var stockClosePrices = this._tKLineService.GetStockClosePrices(tradeDates, stockFullCodes);

                    investIncome = this._statisticsReportService.CalculateUserInvestIncome(investor, statisticalInvestorCodes, tradeRecords, queryDates, stockClosePrices);
                }

                result.Add(new KeyValuePair<string, IList<UserInvestIncomeEntity>>(investor.Name, investIncome));
            }

            return result;
        }

        #endregion Utilities

        #region Events

        private void FrmInvestIncomeReportExport_Load(object sender, EventArgs e)
        {
            try
            {
                BindSearchInfo();

                this.mpbUserInvestIncomeFlow.Enabled = false;
                this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnChangeSavePath_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnChangeSavePath.Enabled = false;

                var mySaveFolderDialog = new FolderBrowserDialog();
                mySaveFolderDialog.Description = "请选择保存目录";

                if (mySaveFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtSavePath.Text = mySaveFolderDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnChangeSavePath.Enabled = true;
            }
        }

        private void btnExport2Excel_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExport2Excel.Enabled = false;

                if (!Directory.Exists(this.txtSavePath.Text.Trim()))
                    throw new DirectoryNotFoundException("保存路径不存在！");

                this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.mpbUserInvestIncomeFlow.Enabled = true;
                this.mpbUserInvestIncomeFlow.Properties.Stopped = false;
                this.mpbUserInvestIncomeFlow.Text = "报表生成中...请稍后...";
                this.mpbUserInvestIncomeFlow.Properties.ShowTitle = true;

                var bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(Export2ExcelProcess);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Export2ExcelCompleted);
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void Export2ExcelProcess(object sender, DoWorkEventArgs e)
        {
            try
            {
                //查询截至交易日
                var endDate = CommonHelper.StringToDateTime(this.deEnd.EditValue.ToString());

                var deptId = int.Parse(this.cbDepartment.SelectedValue());

                var reportType = this.cbReportType.SelectedValue();

                var savePath = this.txtSavePath.Text.Trim();

                CreateReport(endDate, deptId, reportType, savePath);
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void Export2ExcelCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.mpbUserInvestIncomeFlow.Properties.Stopped = true;
            this.mpbUserInvestIncomeFlow.Enabled = false;

            var msg = string.Empty;
            if (e.Error == null && e.Result == null)
                msg = "报表导出成功！";
            else
                msg = e.Error == null ? e.Result?.ToString() : e.Error.Message;

            DXMessage.ShowTips(msg);

            this.btnExport2Excel.Enabled = true;
        }

        #endregion Events
    }
}