using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
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
    public partial class FrmDailyReportExport : BaseForm
    {
        #region Fields

        private readonly int _WanYuan = (int)EnumLibrary.NumericUnit.TenThousand;

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IDepartmentService _departmentService;
        private readonly IDailyStatisticsReportService _statisticsReportService;
        private readonly ITKLineService _tKLineService;
        private readonly IUserService _userService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        #endregion Fields

        #region Constructors

        public FrmDailyReportExport(
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

            //投资小组
            string sqlText = @" SELECT TeamId,TeamName FROM TeamInfo WHERE IsDeleted = 0 ORDER BY TeamId ";

            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);

            if (ds == null || ds.Tables.Count == 0) return;

            var teamInfos = ds.Tables[0].AsEnumerable()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Field<string>("TeamName"),
                    Value = x.Field<int>("TeamId").ToString(),
                }).ToList();
            this.cbDepartment.Initialize(teamInfos, displayAdditionalItem: false);
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

        private string GetReportTemplateFilePath(int teamId)
        {
            string directoryName = "ReportTemplate\\DailyProfit";
            string fileName = AppConfig._ReportTemplateTradeTypeProfit;

            return Path.Combine(Application.StartupPath, directoryName, fileName);
        }

        private string GetReportDestinyFilePath(int teamId, string savePath)
        {
            string directoryName = savePath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //投资小组
            string sqlText = $@" SELECT TeamName FROM TeamInfo WHERE TeamId = {teamId} ";

            string teamName = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText).ToString();

            var fileName = $"证券投资部收益报表({teamName})" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            return Path.Combine(directoryName, fileName);
        }

        private void CreateReport(DateTime endDate, int teamId, string reportType, string savePath)
        {
            // var tradeType = CTMHelper.GetTradeTypeByDepartment(deptId);
            var templateFilePath = GetReportTemplateFilePath(teamId);

            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException("报表模板Excel文件不存在！");
            }

            var destinyFileName = GetReportDestinyFilePath(teamId, savePath);

            var reportData = GetReportData(endDate, teamId, reportType);

            if (!reportData.Any())
                throw new Exception("收益报表数据读取失败！");

            WriteDataToExcel(reportData, teamId, templateFilePath, destinyFileName);
        }

        private void WriteDataToExcel(IList<TradeTypeProfitEntity> reportData, int teamId, string templateFilePath, string destinyFilePath)
        {
            if (reportData == null)
                throw new NullReferenceException(nameof(reportData));

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = null;      

            try
            {
                workbook = excelApp.Workbooks.Open(templateFilePath);
                if (workbook == null)
                    throw new Exception("报表模板文件打开失败！");

                Excel.Worksheet sheetSummary = workbook.Sheets["Summary"];
                if (sheetSummary == null)
                    throw new Exception("报表模板文件缺少投资汇总Sheet！");

            

                            
                GenerateSubjectSheet(reportData, workbook);

                //GenerateSummarySheet();


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

        private void GenerateSubjectSheet(IList<TradeTypeProfitEntity> reportData, Excel.Workbook workbook)
        {
            Excel.Worksheet sheetSubject = workbook.Sheets["Subject"];
            if (sheetSubject == null)
                throw new Exception("报表模板文件缺少投资对象收益Sheet！");

            foreach (var group in reportData.GroupBy(x=>x.InvestorName ))
            {
                sheetSubject.Name = group.Key;

                //日总收益数据
                var totalProfitData = group.Where(x => x.TradeType == (int)EnumLibrary.TradeType.All).OrderBy(x => x.TradeDate).ToList();
                int totalProfitStartRow = 34;
                for (int i = 0; i < totalProfitData.Count; i++)
                {
                    int index = totalProfitStartRow + i;
                    var data = totalProfitData[i];

                    //序号
                    sheetSubject.Cells["A" + index.ToString()] = index.ToString();
                    //日期
                    sheetSubject.Cells["B" + index.ToString()] = data.TradeDate;
                    //周一市值
                    sheetSubject.Cells["C" + index.ToString()] = data.MondayValue / _WanYuan;
                    //净资产
                    sheetSubject.Cells["D" + index.ToString()] = (data.YearProfit + Math.Abs(data.CurValue)) / _WanYuan;
                    //本年收益额
                    sheetSubject.Cells["E" + index.ToString()] = data.YearProfit / _WanYuan;
                    //当日收益率
                    sheetSubject.Cells["E" + index.ToString()] = data.DayRate;
                    //当日收益额
                    sheetSubject.Cells["G" + index.ToString()] = data.DayProfit / _WanYuan;
                    //本年收益率
                    sheetSubject.Cells["H" + index.ToString()] = data.YearRate;
                    //持仓市值
                    sheetSubject.Cells["I" + index.ToString()] = data.CurValue / _WanYuan;
                    //投入资金线
                    sheetSubject.Cells["J" + index.ToString()] = data.YearAvgFund / _WanYuan;
                    //资金可用额度
                    sheetSubject.Cells["K" + index.ToString()] = data.YearAvgFund * 1.2 / _WanYuan;
                    //持仓仓位
                    sheetSubject.Cells["L" + index.ToString()] = data.YearAvgFund== 0 ? 0.0 : data.CurValue / data.YearAvgFund ;

                }

            }


        }

        private void GenerateSummarySheet()
        {
            throw new NotImplementedException();
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

        private List<TradeTypeProfitEntity> GetReportData(DateTime endDate, int teamId, string reportType)
        {
            List<TradeTypeProfitEntity> result = new List<TradeTypeProfitEntity>();

            var queryDates = new List<DateTime>();

            //日报表
            if (reportType == EnumLibrary.ReportType.Day.ToString())
                //取得26个交易日日期
                queryDates = CommonHelper.GetWorkdaysBeforeCurrentDay(endDate, 26).OrderBy(x => x).ToList();
            else
                return result;


            result = this._statisticsReportService.CalculateTradeTypeProfit(teamId, queryDates.Min(), queryDates.Max()).ToList();

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

                var teamId = int.Parse(this.cbDepartment.SelectedValue());

                var reportType = this.cbReportType.SelectedValue();

                var savePath = this.txtSavePath.Text.Trim();

                CreateReport(endDate, teamId, reportType, savePath);
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