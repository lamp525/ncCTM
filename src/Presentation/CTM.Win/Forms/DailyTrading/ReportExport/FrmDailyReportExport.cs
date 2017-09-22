using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
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

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IDepartmentService _departmentService;
        private readonly IDailyStatisticsReportService _statisticsReportService;
        private readonly ITKLineService _tKLineService;
        private readonly IUserService _userService;
        private readonly ExcelHelper _excelEdit = new ExcelHelper();

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

            //保存路径
            this.txtSavePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private string GetReportTemplateFilePath(int teamId)
        {
            string directoryName = "ReportTemplate";
            string fileName = AppConfig._ReportTemplateTradeTypeProfit;

            return Path.Combine(Application.StartupPath, directoryName, fileName);
        }

        private string GetReportDestinyFilePath(int teamId, string savePath)
        {
            string directoryName = savePath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //投资小组
            string sqlText = $@" SELECT TeamName FROM TeamInfo WHERE TeamId = {teamId} ";

            string teamName = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText).ToString();

            var fileName = $"日收益报表({teamName})" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            return Path.Combine(directoryName, fileName);
        }

        private void CreateReport(DateTime endDate, int teamId, string savePath)
        {
            // var tradeType = CTMHelper.GetTradeTypeByDepartment(deptId);
            var templateFilePath = GetReportTemplateFilePath(teamId);

            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException("报表模板Excel文件不存在！");
            }

            var destinyFileName = GetReportDestinyFilePath(teamId, savePath);

            if (File.Exists(destinyFileName))
                File.Delete(destinyFileName);

            var reportData = GetReportData(endDate, teamId);

            if (!reportData.Any())
                throw new Exception("收益报表数据读取失败！");

            WriteDataToExcel(reportData, teamId, templateFilePath, destinyFileName);
        }

        private void WriteDataToExcel(IList<TradeTypeProfitEntity> reportData, int teamId, string templateFileName, string exportFileName)
        {
            if (reportData == null)
                throw new NullReferenceException(nameof(reportData));

            try
            {
                _excelEdit.Open(templateFileName);

                //投资主体模板Sheet
                Excel.Worksheet subjectSheet = _excelEdit.GetSheet("Subject");
                if (subjectSheet != null)
                    GenerateSubjectSheet(reportData, subjectSheet);

                //汇总图表模板Sheet
                Excel.Worksheet summarySheet = _excelEdit.GetSheet("Summary");
                if (summarySheet != null)
                    GenerateSummarySheet(summarySheet);

                //删除投资主体模板Sheet
                _excelEdit.DeleteSheet(subjectSheet.Name);

                _excelEdit.SaveAs(exportFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _excelEdit.Close();
            }
        }

        private void GenerateSubjectSheet(IList<TradeTypeProfitEntity> reportData, Excel.Worksheet subjectSheet)
        {
            //投资对象数据
            var subjectDataList = reportData.GroupBy(x => x.InvestorName);
            foreach (var subjectData in subjectDataList)
            {
                _excelEdit.CopySheetToEnd(subjectSheet, subjectData.Key);

                Excel.Worksheet curSheet = _excelEdit.GetSheet(subjectData.Key);

                //交易类别数据
                var tradeTypeDataList = subjectData.GroupBy(x => x.TradeType);
                foreach (var tradeTypeData in tradeTypeDataList)
                {
                    int startRow = GetStartRowIndex(tradeTypeData.Key);

                    for (int i = 0; i < tradeTypeData.Count(); i++)
                    {
                        TradeTypeProfitEntity data = tradeTypeData.ElementAt(i);
                        int rowIndex = startRow + i;

                        //序号
                        _excelEdit.SetCellValue(curSheet, rowIndex, 1, i + 1);

                        //日期
                        _excelEdit.SetCellValue(curSheet, rowIndex, 2, data.TradeDate);

                        //周一市值
                        _excelEdit.SetCellValue(curSheet, rowIndex, 3, data.MondayValue);

                        //净资产
                        _excelEdit.SetCellValue(curSheet, rowIndex, 4, data.YearProfit + Math.Abs(data.CurValue));

                        //本年收益额
                        _excelEdit.SetCellValue(curSheet, rowIndex, 5, data.YearProfit);

                        //当日收益率
                        _excelEdit.SetCellValue(curSheet, rowIndex, 6, data.DayRate);

                        //当日收益额
                        _excelEdit.SetCellValue(curSheet, rowIndex, 7, data.DayProfit);

                        //本年收益率
                        _excelEdit.SetCellValue(curSheet, rowIndex, 8, data.YearRate);

                        //持仓市值
                        _excelEdit.SetCellValue(curSheet, rowIndex, 9, data.CurValue);

                        //投入资金线
                        // _excelEdit.SetCellValue(curSheet, rowIndex, 10, data.YearAvgFund);

                        //资金可用额度
                        //_excelEdit.SetCellValue(curSheet, rowIndex, 11, data.YearAvgFund * (decimal)1.2);

                        //持仓仓位
                        //_excelEdit.SetCellValue(curSheet, rowIndex, 12, data.CurValue / (Math.Abs(data.YearProfit) + Math.Abs(data.CurValue)));
                    }
                }
            }
        }

        private static int GetStartRowIndex(int tradeType)
        {
            int startRow = 0;

            switch (tradeType)
            {
                case (int)EnumLibrary.TradeType.All:
                    startRow = 34;
                    break;

                case (int)EnumLibrary.TradeType.Target:
                    startRow = 62;
                    break;

                case (int)EnumLibrary.TradeType.Band:
                    startRow = 90;
                    break;

                case (int)EnumLibrary.TradeType.Day:
                    startRow = 118;
                    break;

                default:
                    throw new Exception("收益报表数据中的交易类别有误！");
            }

            return startRow;
        }

        private void GenerateSummarySheet(Excel.Worksheet summarySheet)
        {
            return;
        }

        private List<TradeTypeProfitEntity> GetReportData(DateTime endDate, int teamId)
        {
            List<TradeTypeProfitEntity> result = new List<TradeTypeProfitEntity>();

            //取得25个交易日日期
            var queryDates = CommonHelper.GetWorkdaysBeforeCurrentDay(endDate).OrderBy(x => x).ToList();

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

                var savePath = this.txtSavePath.Text.Trim();

                CreateReport(endDate, teamId, savePath);
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