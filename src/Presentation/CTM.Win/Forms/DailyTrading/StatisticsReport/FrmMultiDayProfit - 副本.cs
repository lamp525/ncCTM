using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using Excel = Microsoft.Office.Interop.Excel;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmMultiDayProfit : BaseForm
    {
        private readonly ExcelHelper _excelEdit = new ExcelHelper();

        public FrmMultiDayProfit()
        {
            InitializeComponent();
        }

        private void BindSearchInfo()
        {
            this.deStart.Properties.AllowNullInput = DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DefaultBoolean.False;

            deStart.EditValue = "2017/08/04";
            deEnd.EditValue = DateTime.Now;

            string sqlText1 = $@"SELECT TeamId,TeamName FROM TeamInfo WHERE IsDeleted =0 ORDER BY TeamName ";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);

            if (ds == null || ds.Tables.Count == 0) return;

            List<ComboBoxItemModel> teamInfos = new List<ComboBoxItemModel>();
            var allTeam = new ComboBoxItemModel { Text = "全部小组", Value = "-1" };
            teamInfos.Add(allTeam);

            teamInfos.AddRange(ds.Tables[0].AsEnumerable()
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Field<string>("TeamName"),
                    Value = x.Field<int>("TeamId").ToString(),
                }));

            this.cbTeam.Initialize(teamInfos, displayAdditionalItem: false);

            string defaultTeam = string.Empty;

            if (!LoginInfo.CurrentUser.IsAdmin)
            {
                string sqlText2 = $@"SELECT TeamId FROM TeamMember WHERE InvestorCode = '{LoginInfo.CurrentUser.UserCode}'";

                var obj = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText2);

                if (obj != null)
                    defaultTeam = obj.ToString();
                else
                {
                    btnSearch.Enabled = false;
                    return;
                }
                cbTeam.ReadOnly = true;
            }
            else
                defaultTeam = "-1";

            this.cbTeam.DefaultSelected(defaultTeam);

            this.bandedGridView1.SetLayout(showAutoFilterRow: true, showCheckBoxRowSelect: false, setAlternateRowColor: false);
            this.bandedGridView1.SetColumnHeaderAppearance();
            this.bandedGridView1.OptionsView.EnableAppearanceEvenRow = false;
            this.bandedGridView1.OptionsView.EnableAppearanceOddRow = false;

            this.ActiveControl = this.btnSearch;
        }

        private void Export2ExcelProcess(object sender, DoWorkEventArgs e)
        {
            try
            {
                string savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string directoryName = "ReportTemplate";
                string templateFileName = Path.Combine(Application.StartupPath, directoryName, AppConfig._ReportTemplateMultiDayProfit);

                if (!File.Exists(templateFileName))
                    throw new FileNotFoundException("报表模板Excel文件不存在！");

                string destinyFileName = $@"隔日短差收益表({DateTime.Now.ToString("yyMMddhhmm")}).xlsx";
                destinyFileName = Path.Combine(savePath, destinyFileName);
                if (File.Exists(destinyFileName))
                    File.Delete(destinyFileName);

                WriteDataToExcel(templateFileName, destinyFileName);
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void WriteDataToExcel(string templateFileName, string destinyFileName)
        {
            try
            {
                DataTable dtProfit = (this.bandedGridView1.DataSource as DataView).ToTable();

                _excelEdit.Open(templateFileName);

                //收益明细模板Sheet
                Excel.Worksheet detailSheet = _excelEdit.GetSheet("Detail");
                if (detailSheet != null)
                    GenerateDetailSheet(dtProfit, detailSheet);

                //汇总打印模板Sheet
                Excel.Worksheet printSheet = _excelEdit.GetSheet("Print");
                if (printSheet != null)
                    GeneratePrintSheet(dtProfit, printSheet);

                //默认选中汇总Sheet
                printSheet.Select();
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

        private void GenerateDetailSheet(DataTable dtProfit, Excel.Worksheet detailSheet)
        {
            detailSheet.Name = "隔日短差收益";

            int startRow = 3;
            int dataRowCount = dtProfit.Rows.Count;
            int dataColumnCount = 33;

            //数据输入区
            Excel.Range dataRange = detailSheet.Range[detailSheet.Cells[startRow, 1], detailSheet.Cells[startRow + dataRowCount - 1, dataColumnCount]];

            //设置数据区边框线(实线)
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            object[,] ss = new object[dataRowCount, dataColumnCount];
            for (int i = 0; i < dataRowCount; i++)
            {
                DataRow data = dtProfit.Rows[i];

                //序号
                ss[i, 0] = data[this.colRowNumber.Name];

                /*基础信息*/
                //日期
                ss[i, 1] = data[this.colTradeDate.Name];
                //投资小组
                ss[i, 2] = data[this.colTeamName.Name];
                //投资人员
                ss[i, 3] = data[this.colInvestorName.Name];
                //占用资金
                ss[i, 4] = data[this.colAllocateFund.Name];
                //股票代码
                ss[i, 5] = data[this.colStockCode.Name];
                //股票名称
                ss[i, 6] = data[this.colStockName.Name];

                /*昨日持仓 */
                //数量
                ss[i, 7] = data[this.colPreVolume.Name];
                //市值
                ss[i, 8] = data[this.colPreValue.Name];

                /*当日持仓 */
                //数量
                ss[i, 9] = data[this.colCurVolume.Name];
                //市值
                ss[i, 10] = data[this.colCurValue.Name];

                /*当日成交 */
                //买入数量
                ss[i, 11] = data[this.colBuyVolume.Name];
                //买入金额
                ss[i, 12] = data[this.colBuyAmount.Name];
                //卖出数量
                ss[i, 13] = data[this.colSellVolume.Name];
                //卖出金额
                ss[i, 14] = data[this.colSellAmount.Name];

                /*当日收益 */
                //投入资金
                ss[i, 15] = data[this.colDayFund.Name];
                //日收益额
                ss[i, 16] = data[this.colDayProfit.Name];
                //日收益额排行
                ss[i, 17] = data[this.colRankDP.Name];
                //日收益率
                ss[i, 18] = data[this.colDayRate.Name];
                //日收益率排行
                ss[i, 19] = data[this.colRankDR.Name];
                //占用资金日收益率
                ss[i, 20] = data[this.colDayAllocateRate.Name];
                //占用资金日收益率排行
                ss[i, 21] = data[this.colRankDAR.Name];
                //综合指数
                ss[i, 22] = data[this.colIndexDay.Name];

                /*本周收益 */
                //日均投入资金
                ss[i, 23] = data[this.colWeekAvgFund.Name];
                //周收益额
                ss[i, 24] = data[this.colWeekProfit.Name];
                //周收益额排行
                ss[i, 25] = data[this.colRankWP.Name];
                //周收益率
                ss[i, 26] = data[this.colWeekRate.Name];
                //周收益率排行
                ss[i, 27] = data[this.colRankWR.Name];
                //占用资金周收益率
                ss[i, 28] = data[this.colWeekAllocateRate.Name];
                //占用资金周收益率排行
                ss[i, 29] = data[this.colRankWAR.Name];
                //综合指数
                ss[i, 30] = data[this.colIndexWeek.Name];

                //累计收益额
                ss[i, 31] = data[this.colAccProfit.Name];
                //累计奖金限额
                ss[i, 32] = data[this.colBonusLimit.Name];
            }

            //填充数据
            dataRange.Value = ss;
        }

        private void GeneratePrintSheet(DataTable dtProfit, Excel.Worksheet printSheet)
        {
            int teamId = int.Parse(this.cbTeam.SelectedValue());
            string InvestorCode = this.luInvestor.SelectedValue();

            if (teamId == -1 && string.IsNullOrEmpty(InvestorCode))
            {
                //最近交易日
                var tradeDate = dtProfit.AsEnumerable().Max(x => x.Field<string>(colTradeDate.Name).Trim());

                //该日部门汇总数据
                DataRow[] deptData = dtProfit.AsEnumerable().Where(x => x.Field<string>(colTradeDate.Name).Trim() == tradeDate && x.Field<int>(colDataType.Name) == 88).ToArray();
                PrintSheetDataFill(printSheet, deptData, 5);

                //该日投资小组汇总数据
                DataRow[] teamData = dtProfit.AsEnumerable().Where(x => x.Field<string>(colTradeDate.Name).Trim() == tradeDate && x.Field<int>(colDataType.Name) == 2).ToArray();
                PrintSheetDataFill(printSheet, teamData, 11);

                //该日投资人员汇总数据
                DataRow[] investorData = dtProfit.AsEnumerable().Where(x => x.Field<string>(colTradeDate.Name).Trim() == tradeDate && x.Field<int>(colDataType.Name) == 1).ToArray();
                PrintSheetDataFill(printSheet, investorData, 19);
            }
        }

        private void PrintSheetDataFill(Excel.Worksheet printSheet, DataRow[] dataList, int startRow)
        {
            int dataRowCount = dataList.Count();
            int dataColumnCount = 27;

            //数据输入区
            Excel.Range dataRange = printSheet.Range[printSheet.Cells[startRow, 1], printSheet.Cells[startRow + dataRowCount - 1, dataColumnCount]];

            //设置数据区边框线(实线)
            dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            object[,] ss = new object[dataRowCount, dataColumnCount];
            for (int i = 0; i < dataRowCount; i++)
            {
                DataRow data = dataList[i];

                //序号
                ss[i, 0] = i + 1;

                //日期
                ss[i, 1] = data[this.colTradeDate.Name];
                //投资小组
                ss[i, 2] = data[this.colTeamName.Name];
                //投资人员
                ss[i, 3] = data[this.colInvestorName.Name];
                //占用资金
                ss[i, 4] = data[this.colAllocateFund.Name];

                //昨日持仓市值
                ss[i, 5] = data[this.colPreValue.Name];
                //当日持仓市值
                ss[i, 6] = data[this.colCurValue.Name];

                //买入金额
                ss[i, 7] = data[this.colBuyAmount.Name];
                //卖出金额
                ss[i, 8] = data[this.colSellAmount.Name];

                /*当日收益 */
                //投入资金
                ss[i, 9] = data[this.colDayFund.Name];
                //日收益额
                ss[i, 10] = data[this.colDayProfit.Name];
                //日收益额排行
                ss[i, 11] = data[this.colRankDP.Name];
                //日收益率
                ss[i, 12] = data[this.colDayRate.Name];
                //日收益率排行
                ss[i, 13] = data[this.colRankDR.Name];
                //占用资金日收益率
                ss[i, 14] = data[this.colDayAllocateRate.Name];
                //占用资金日收益率排行
                ss[i, 15] = data[this.colRankDAR.Name];
                //综合指数
                ss[i, 16] = data[this.colIndexDay.Name];

                /*本周收益 */
                //日均投入资金
                ss[i, 17] = data[this.colWeekAvgFund.Name];
                //周收益额
                ss[i, 18] = data[this.colWeekProfit.Name];
                //周收益额排行
                ss[i, 19] = data[this.colRankWP.Name];
                //周收益率
                ss[i, 20] = data[this.colWeekRate.Name];
                //周收益率排行
                ss[i, 21] = data[this.colRankWR.Name];
                //占用资金周收益率
                ss[i, 22] = data[this.colWeekAllocateRate.Name];
                //占用资金周收益率排行
                ss[i, 23] = data[this.colRankWAR.Name];
                //综合指数
                ss[i, 24] = data[this.colIndexWeek.Name];

                //累计收益额
                ss[i, 25] = data[this.colAccProfit.Name];
                //累计奖金限额
                ss[i, 26] = data[this.colBonusLimit.Name];
            }

            //填充数据
            dataRange.Value = ss;
        }

        private void Export2ExcelCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.mpbExport.Properties.Stopped = true;
            this.mpbExport.Enabled = false;

            var msg = string.Empty;
            if (e.Error == null && e.Result == null)
                msg = "报表导出成功！已保存到桌面！";
            else
                msg = e.Error == null ? e.Result?.ToString() : e.Error.Message;

            DXMessage.ShowTips(msg);

            this.btnExport.Enabled = true;
        }

        private void FrmMultiDayProfit_Load(object sender, EventArgs e)
        {
            try
            {
                BindSearchInfo();

                this.mpbExport.Enabled = false;
                this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlText1 = string.Empty;
                int teamId = int.Parse(this.cbTeam.SelectedValue());

                if (teamId > 0)
                    sqlText1 = $@"SELECT InvestorCode,InvestorName FROM TeamMember  WHERE IsDeleted =0 AND TeamId = {teamId} ORDER BY InvestorName ";
                else
                    sqlText1 = $@"SELECT InvestorCode,InvestorName FROM TeamMember  WHERE IsDeleted =0 ORDER BY InvestorName ";

                var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);

                if (ds == null || ds.Tables.Count == 0) return;

                List<ComboBoxItemModel> investorInfos = new List<ComboBoxItemModel>();
                var allInvestor = new ComboBoxItemModel { Text = "全部人员", Value = "All" };
                investorInfos.Add(allInvestor);

                investorInfos.AddRange(ds.Tables[0].AsEnumerable()
                    .Select(x => new ComboBoxItemModel
                    {
                        Text = x.Field<string>("InvestorName"),
                        Value = x.Field<string>("InvestorCode"),
                    }));
                luInvestor.Initialize(investorInfos, "Value", "Text");

                if (LoginInfo.CurrentUser.IsAdmin)
                {
                    luInvestor.EditValue = "All";
                }
                else
                {
                    luInvestor.ReadOnly = true;
                    luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;

                DateTime dtStart = CommonHelper.StringToDateTime(deStart.EditValue.ToString());
                DateTime dtEnd = CommonHelper.StringToDateTime(deEnd.EditValue.ToString());
                int teamId = int.Parse(this.cbTeam.SelectedValue());
                string InvestorCode = this.luInvestor.SelectedValue();
                string sqlText = $@"EXEC [dbo].[sp_GetMultiDayProfit] @StartDate = '{dtStart}',@EndDate = '{dtEnd}',@TeamId = {teamId},@InvestorCode = N'{InvestorCode}'";

                var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
                if (ds == null || ds.Tables.Count == 0) return;

                this.gridControl1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void bandedGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.Name.IndexOf("Profit") > 0 || e.Column.Name.IndexOf("Rate") > 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void bandedGridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            int dataType = int.Parse(this.bandedGridView1.GetRowCellValue(e.RowHandle, this.colDataType).ToString());

            if (dataType == 1)
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(225, 244, 255);
            else if (dataType == 2)
                e.Appearance.BackColor = System.Drawing.Color.SkyBlue;
            else if (dataType == 88)
                e.Appearance.BackColor = System.Drawing.Color.SteelBlue;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.bandedGridView1.DataSource == null) return;

                btnExport.Enabled = false;

                this.lciProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.mpbExport.Enabled = true;
                this.mpbExport.Properties.Stopped = false;
                this.mpbExport.Text = "报表生成中...请稍后...";
                this.mpbExport.Properties.ShowTitle = true;

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
            finally
            {
                GC.Collect();
            }
        }
    }
}