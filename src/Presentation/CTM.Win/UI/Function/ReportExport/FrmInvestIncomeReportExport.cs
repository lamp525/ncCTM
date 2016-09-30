using System;
using System.Collections.Generic;
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

namespace CTM.Win.UI.Function.ReportExport
{
    public partial class FrmInvestIncomeReportExport : BaseForm
    {
        #region Fields

        private readonly IDailyRecordService _dailyRecordService;
        private readonly IDepartmentService _departmentService;
        private readonly IStatisticsReportService _statisticsReportService;
        private readonly ITKLineService _tKLineService;
        private readonly IUserService _userService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        #endregion Fields

        #region Constructors

        public FrmInvestIncomeReportExport(
            IDailyRecordService dailyRecordService,
            IDepartmentService departmentService,
            IStatisticsReportService statisticsReportService,
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
                .Where(x => x.Id != (int)EnumLibrary.AccountingDepartment.Independence)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            this.cbDepartment.Initialize(deptInfos, displayAdditionalItem: false);

            //报表类型
            var reportTypes = new List<ComboBoxItemModel>
            {
                new ComboBoxItemModel { Text = CTMHelper.GetReportTypeName ((int)EnumLibrary.ReportType.Day), Value = EnumLibrary.ReportType.Day.ToString() },
                new ComboBoxItemModel { Text = CTMHelper.GetReportTypeName ((int)EnumLibrary.ReportType.Week), Value = EnumLibrary.ReportType.Week.ToString() },
                new ComboBoxItemModel { Text = CTMHelper.GetReportTypeName ((int)EnumLibrary.ReportType.Month) , Value = EnumLibrary.ReportType.Month.ToString() },
            };

            this.cbReportType.Initialize(reportTypes, displayAdditionalItem: false);
            this.cbReportType.DefaultSelected(EnumLibrary.ReportType.Day.ToString());
        }

        private string GetReportTemplateFilePath(int deptId)
        {
            string templateFilePath = string.Empty;
            EnumLibrary.TradeType tradeType = CTMHelper.GetTradeTypeByDepartment(deptId);

            string directoryName = "ReportTemplate\\UserDailyProfitFlow";
            string fileName = string.Empty;

            switch (tradeType)
            {
                case EnumLibrary.TradeType.Target:
                    break;

                case EnumLibrary.TradeType.Band:
                    break;

                case EnumLibrary.TradeType.Day:
                    fileName = "DayReport.xlsx";
                    break;

                default:
                    break;
            }

            return Path.Combine(Application.StartupPath, directoryName, fileName);
        }

        private void CreateReport(DateTime endDate, int deptId, string reportType)
        {
            var reportData = GetReportData(endDate, deptId, reportType);
            var templateFileName = GetReportTemplateFilePath(deptId);

            if (!File.Exists(templateFileName))
            {
                DXMessage.ShowError("报表模板Excel文件不存在！");
                return;
            }

            WriteDataToExcel(reportData, templateFileName);
        }

        private void WriteDataToExcel(IList<KeyValuePair<string, IList<UserInvestIncomeEntity>>> reportData, string templateFileName)
        {
            if (!File.Exists(templateFileName))
                throw new FileNotFoundException("文件不存在");

            Excel.Application excelApp = new Excel.Application();

            Excel.Workbook workbook = excelApp.Workbooks.Open(templateFileName);

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

                var startRowIndex = 34;

                var data = item.Value;

                for (int i = 0; i < data.Count; i++)
                {
                    var investInfo = data[i];

                    //日期
                    worksheet.Cells[startRowIndex + i, 2] = investInfo.TradeTime.ToShortDateString();

                    //累计收益额（万元）
                    worksheet.Cells[startRowIndex + i, 5] = CommonHelper.SetDecimalDigits(investInfo.AccumulatedActualProfit / (int)EnumLibrary.NumericUnit.TenThousand);

                    //当日收益率
                    worksheet.Cells[startRowIndex + i, 6] = CommonHelper.ConvertToPercentage(investInfo.CurrentIncomeRate);

                    //日收益额（万元）
                    worksheet.Cells[startRowIndex + i, 7] = CommonHelper.SetDecimalDigits(investInfo.CurrentActualProfit / (int)EnumLibrary.NumericUnit.TenThousand);

                    //累计收益率
                    worksheet.Cells[startRowIndex + i, 8] = CommonHelper.ConvertToPercentage(investInfo.AccumulatedIncomeRate);
                }
            }

            var destinyFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "证券投资部收益报表(短差)" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");

            workbook.SaveAs(destinyFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            workbook = null;
            excelApp.Quit();
            excelApp = null;

            if (File.Exists(destinyFileName))
                DXMessage.ShowTips("报表导出成功！");
        }

        private IList<KeyValuePair<string, IList<UserInvestIncomeEntity>>> GetReportData(DateTime endDate, int deptId, string reportType)
        {
            IList<KeyValuePair<string, IList<UserInvestIncomeEntity>>> result = new List<KeyValuePair<string, IList<UserInvestIncomeEntity>>>();

            EnumLibrary.TradeType tradeType = CTMHelper.GetTradeTypeByDepartment(deptId);

            IList<UserInfo> investors = _userService.GetUserInfos(departmentIds: new int[] { deptId }).Where(x => x.IsDeleted == false).ToList();

            var queryDates = new List<DateTime>();

            //日报表
            if (reportType == EnumLibrary.ReportType.Day.ToString())
                //取得26个交易日日期
                queryDates = CommonHelper.GetWorkdaysBeforeCurrentDay(endDate, 26).OrderBy(x => x).ToList();
            else
                return result;

            foreach (var investor in investors)
            {
                var statisticalInvestorCodes = new string[] { investor.Code };

                //交易记录
                var tradeRecords = _dailyRecordService.GetDailyRecords(tradeType: (int)tradeType, beneficiaries: statisticalInvestorCodes, tradeDateFrom: _initDate, tradeDateTo: endDate).ToList();

                //交易记录中的所有股票代码
                var stockFullCodes = tradeRecords.Select(x => x.StockCode).Distinct().ToArray();
                //所有交易日期
                var tradeDates = CommonHelper.GetAllWorkDays(tradeRecords.Min(x => x.TradeDate).AddDays(-1), endDate);

                //各交易日所有股票收盘价
                var stockClosePrices = this._tKLineService.GetStockClosePrices(tradeDates, stockFullCodes);

                var investIncome = this._statisticsReportService.CalculateUserInvestIncome(investor, statisticalInvestorCodes, tradeRecords, queryDates, stockClosePrices);

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
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnExport2Excel_Click(object sender, EventArgs e)
        {
            try
            {
                btnExport2Excel.Enabled = false;

                //查询截至交易日
                var endDate = CommonHelper.StringToDateTime(this.deEnd.EditValue.ToString());

                var deptId = int.Parse(this.cbDepartment.SelectedValue());

                var reportType = this.cbReportType.SelectedValue();

                CreateReport(endDate, deptId, reportType);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                btnExport2Excel.Enabled = true;
            }
        }

        #endregion Events
    }
}