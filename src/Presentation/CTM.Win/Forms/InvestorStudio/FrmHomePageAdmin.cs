﻿using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Dictionary;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmHomePageAdmin : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;

        private DataTable _dtIndexRate = null;
        private DataTable _dtSummaryProfit = null;
        private DataTable _dtInvestorProfit = null;
        private int _tradeType = 0;

        #endregion Fields

        #region Constructors

        public FrmHomePageAdmin(IDictionaryService dictionaryService)
        {
            InitializeComponent();

            _dictionaryService = dictionaryService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            deProfit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            string sqlText = $@"SELECT  MAX(TradeDate) FROM DSTradeTypeProfit  WHERE DataType = 3 AND TradeType = 0  AND IsTradeDay = 1 AND DayFund > 0";
            var ret = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText);
            string currentDate = ret == null ? DateTime.MinValue.ToShortDateString() : ret.ToString().Split(' ')[0];
            deProfit.Enabled = false;
            deProfit.EditValue = currentDate;

            //交易类别
            var tradeTypes = this._dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            this.cbTradeType.Initialize(tradeTypes, displayAdditionalItem: true);

            string sqlText1 = $@"SELECT DISTINCT Code,Name FROM [FinancialCenter].[dbo].[IndexInfo] ORDER BY Code";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
            {
                List<ComboBoxItemModel> indexInfo = ds1.Tables[0].AsEnumerable().Select(x => new ComboBoxItemModel
                {
                    Text = x.Field<string>("Name"),
                    Value = x.Field<string>("Code")
                }).ToList();

                cbIndexUp.Initialize(indexInfo);
                cbIndexDown.Initialize(indexInfo);
            }

            ttcInvestorDetail.SetToolTip(gcInvestorProfit, "双击明细行可查看该投资人员统计页！");
        }

        private void GetIndexRateData(string date)
        {
            string sqlText1 = $@"EXEC	[dbo].[sp_IS_Index_Rate] @CurDate = '{date}', @DayNumber = 50";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
                _dtIndexRate = ds1.Tables[0];
        }

        private void GetSummaryProfitData(string date)
        {
            string sqlText1 = $@"EXEC	[dbo].[sp_IS_InvestorProfit_Admin] @TradeDate = '{date}', @DataType  = 3";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
                _dtSummaryProfit = ds1.Tables[0];
            else
                _dtSummaryProfit = null;
        }

        private void BindSummaryInfo()
        {
            DataRow dr = _dtSummaryProfit.AsEnumerable().Where(x => x.Field<int>("TradeType") == _tradeType).FirstOrDefault();

            if (dr == null)
            {
                string nulValue = "-";
                lblCurValue.Text = nulValue;
                lblDayProfit.Text = nulValue;
                lblDayRate.Text = nulValue;
                lblAccProfit.Text = nulValue;
                lblAccRate.Text = nulValue;
            }
            else
            {
                deProfit.EditValue = dr.Field<string>("TradeDate");

                string unit = " 万元";

                lblCurValue.Text = dr.Field<decimal>("CurValue").ToString("N2") + unit;

                lblDayProfit.Text = dr.Field<decimal>("Profit").ToString("N2") + unit;
                if (dr.Field<decimal>("Profit") > 0)
                    lblDayProfit.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("Profit") < 0)
                    lblDayProfit.ForeColor = System.Drawing.Color.Green;

                lblDayRate.Text = dr.Field<decimal>("Rate").ToString("P2");
                if (dr.Field<decimal>("Rate") > 0)
                    lblDayRate.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("Rate") < 0)
                    lblDayRate.ForeColor = System.Drawing.Color.Green;

                lblAccProfit.Text = dr.Field<decimal>("AccProfit").ToString("N2") + unit;
                if (dr.Field<decimal>("AccProfit") > 0)
                    lblAccProfit.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("AccProfit") < 0)
                    lblAccProfit.ForeColor = System.Drawing.Color.Green;

                lblAccRate.Text = dr.Field<decimal>("AccRate").ToString("P2");
                if (dr.Field<decimal>("AccRate") > 0)
                    lblAccRate.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("AccRate") < 0)
                    lblAccRate.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void BindSummaryProfit()
        {
            gcSummaryProfit.DataSource = null;

            DataTable dtProfit = _dtSummaryProfit.AsEnumerable().Where(x => x.Field<int>("TradeType") == _tradeType).CopyToDataTable();

            gcSummaryProfit.DataSource = dtProfit;

            string reportName = "日";
            //日均投入资金
            Series seAvgFund = chartSummaryProfit.Series[1];

            //使用资金
            Series seFund = chartSummaryProfit.Series[2];

            //收益额
            Series seProfit = chartSummaryProfit.Series[3];
            seProfit.Name = reportName + "收益额";

            //收益率
            Series seRate = chartSummaryProfit.Series[4];
            seRate.Name = reportName + "收益率";

            //年收益额
            Series seYearProfit = chartSummaryProfit.Series[5];
            //年收益率
            Series seYearRate = chartSummaryProfit.Series[6];

            seFund.Points.Clear();
            seProfit.Points.Clear();
            seRate.Points.Clear();
            seYearProfit.Points.Clear();
            seYearRate.Points.Clear();
            seAvgFund.Points.Clear();

            if (dtProfit == null || dtProfit.Rows.Count == 0) return;

            string argument = string.Empty;
            decimal fund;
            decimal profit;
            decimal rate;
            decimal accProfit;
            decimal accRate;
            decimal avgFund;

            foreach (DataRow row in dtProfit.AsEnumerable().OrderBy(x => x.Field<string>("TradeDate")))
            {
                argument = CommonHelper.StringToDateTime(row["TradeDate"].ToString()).ToString("yy/MM/dd");
                fund = CommonHelper.StringToDecimal(row["Fund"].ToString().Trim());
                profit = CommonHelper.StringToDecimal(row["Profit"].ToString().Trim());
                rate = CommonHelper.StringToDecimal(row["Rate"].ToString().Trim());
                accProfit = CommonHelper.StringToDecimal(row["AccProfit"].ToString().Trim());
                accRate = CommonHelper.StringToDecimal(row["AccRate"].ToString().Trim());
                avgFund = CommonHelper.StringToDecimal(row["AccAvgFund"].ToString().Trim());

                seFund.Points.Add(new SeriesPoint(argument, fund));
                seProfit.Points.Add(new SeriesPoint(argument, profit));
                seRate.Points.Add(new SeriesPoint(argument, rate));
                seYearProfit.Points.Add(new SeriesPoint(argument, accProfit));
                seYearRate.Points.Add(new SeriesPoint(argument, accRate));
                seAvgFund.Points.Add(new SeriesPoint(argument, avgFund));
            }
        }

        private void BindInvestorProfit(string date)
        {
            gcInvestorProfit.DataSource = null;

            string sqlText1 = $@"EXEC	[dbo].[sp_IS_InvestorProfit_Admin] @TradeDate = '{date}', @DataType  = 1";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
                _dtInvestorProfit = ds1.Tables[0];
            else
                _dtInvestorProfit = null;

            gcInvestorProfit.DataSource = _dtInvestorProfit.AsEnumerable()
                                                                .Where(x => x.Field<string>("TradeDate") == date && x.Field<int>("TradeType") == _tradeType)
                                                                .CopyToDataTable();
        }

        private void DisplayInvestorProfitTrendChart(string investorCode, string investorName)
        {
            lcgChartInvestor.Text = investorName + " - 收益趋势图";

            DataTable dtProfit = _dtInvestorProfit.AsEnumerable()
                                                .Where(x => x.Field<string>("InvestorCode") == investorCode && x.Field<int>("TradeType") == _tradeType).OrderBy(x => x.Field<string>("TradeDate"))
                                                .CopyToDataTable();

            string reportName = "日";
            //日均投入资金
            Series seAvgFund = chartInvestorProfit.Series[1];

            //使用资金
            Series seFund = chartInvestorProfit.Series[2];

            //收益额
            Series seProfit = chartInvestorProfit.Series[3];
            seProfit.Name = reportName + "收益额";

            //收益率
            Series seRate = chartInvestorProfit.Series[4];
            seRate.Name = reportName + "收益率";

            //年收益额
            Series seYearProfit = chartInvestorProfit.Series[5];
            //年收益率
            Series seYearRate = chartInvestorProfit.Series[6];

            seFund.Points.Clear();
            seProfit.Points.Clear();
            seRate.Points.Clear();
            seYearProfit.Points.Clear();
            seYearRate.Points.Clear();
            seAvgFund.Points.Clear();

            if (dtProfit == null || dtProfit.Rows.Count == 0) return;

            string argument = string.Empty;
            decimal fund;
            decimal profit;
            decimal rate;
            decimal accProfit;
            decimal accRate;
            decimal avgFund;

            foreach (DataRow row in dtProfit.Rows)
            {
                argument = CommonHelper.StringToDateTime(row["TradeDate"].ToString()).ToString("yy/MM/dd");
                fund = CommonHelper.StringToDecimal(row["Fund"].ToString().Trim());
                profit = CommonHelper.StringToDecimal(row["Profit"].ToString().Trim());
                rate = CommonHelper.StringToDecimal(row["Rate"].ToString().Trim());
                accProfit = CommonHelper.StringToDecimal(row["AccProfit"].ToString().Trim());
                accRate = CommonHelper.StringToDecimal(row["AccRate"].ToString().Trim());
                avgFund = CommonHelper.StringToDecimal(row["AccAvgFund"].ToString().Trim());

                seFund.Points.Add(new SeriesPoint(argument, fund));
                seProfit.Points.Add(new SeriesPoint(argument, profit));
                seRate.Points.Add(new SeriesPoint(argument, rate));
                seYearProfit.Points.Add(new SeriesPoint(argument, accProfit));
                seYearRate.Points.Add(new SeriesPoint(argument, accRate));
                seAvgFund.Points.Add(new SeriesPoint(argument, avgFund));
            }
        }

        private void DisplayIndexTrendChart(DateTime date, string indexCode, string indexName, ChartControl chart)
        {
            Series seIndex = chart.Series[0];
            seIndex.Name = indexName;

            seIndex.Points.Clear();

            DataRow[] indexData = _dtIndexRate.AsEnumerable()
                                                    .Where(x => x.Field<string>("Code") == indexCode && x.Field<DateTime>("TradeDate") <= date).Take(25)
                                                     .OrderBy(x => x.Field<DateTime>("TradeDate")).ToArray();
            string argument = string.Empty;
            decimal rate;

            foreach (DataRow row in indexData)
            {
                argument = CommonHelper.StringToDateTime(row["TradeDate"].ToString()).ToString("yy/MM/dd");
                rate = CommonHelper.StringToDecimal(row["Rate"].ToString().Trim());

                seIndex.Points.Add(new SeriesPoint(argument, rate));
            }
        }

        #endregion Utilities

        #region Events

        protected override Point ScrollToControl(Control activeControl)
        {
            return this.AutoScrollPosition;
        }

        private void FrmHomePageAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void deProfit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                deProfit.Enabled = false;

                var bwProfit = new BackgroundWorker();
                bwProfit.DoWork += bwProfit_DoWork;
                bwProfit.RunWorkerCompleted += bwProfit_RunWorkerCompleted;
                bwProfit.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void bwProfit_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string date = deProfit.EditValue.ToString();
                GetIndexRateData(date);
                GetSummaryProfitData(date);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void bwProfit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null && e.Error == null)
                {
                    if (_dtSummaryProfit != null)
                    {
                        BindSummaryInfo();
                        BindSummaryProfit();
                    }

                    if (cbIndexUp.EditValue == null)
                        cbIndexUp.DefaultSelected("000001");
                    else
                    {
                        DateTime date = CommonHelper.StringToDateTime(deProfit.EditValue.ToString());
                        string indexCode = (cbIndexUp.SelectedItem as ComboBoxItemModel).Value;
                        string indexName = cbIndexUp.Text.Trim();
                        DisplayIndexTrendChart(date, indexCode, indexName, chartSummaryProfit);
                    }

                    if (cbIndexDown.EditValue == null)
                        cbIndexDown.DefaultSelected("000001");

                    deProfit.Enabled = true;
                }
                else
                {
                    if (e.Result != null)
                        throw new Exception(e.Result.ToString());
                    else if (e.Error != null)
                        throw e.Error;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbTradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbTradeType.EditValue != null)
                    _tradeType = int.Parse(cbTradeType.SelectedValue());

                BindSummaryInfo();
                BindSummaryProfit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvSummaryProfit_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataRow dr = gvSummaryProfit.GetFocusedDataRow();

                if (dr != null)
                {
                    string tradeDate = dr.Field<string>("TradeDate");

                    BindInvestorProfit(tradeDate);

                    //更新下趋势图的对标指数
                    var item = cbIndexDown.SelectedItem as ComboBoxItemModel;
                    if (item != null)
                        DisplayIndexTrendChart(CommonHelper.StringToDateTime(tradeDate), item.Value, item.Text, chartInvestorProfit);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvSummaryProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvSummaryProfit.DrawRowIndicator(e);
        }

        private void gvSummaryProfit_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void gvSummaryProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.FieldName.IndexOf("Profit") >= 0 || e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void gvInvestorProfit_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gvInvestorProfit.GridControl.PointToClient(Control.MousePosition);
            var hi = gvInvestorProfit.CalcHitInfo(pt);
            if (hi.InRow)
            {
                var row = gvInvestorProfit.GetDataRow(hi.RowHandle);
                if (row != null)
                {
                    string investorCode = row["InvestorCode"].ToString();
                    string investorName = row["investorName"].ToString();
                    string tradeDate = row["TradeDate"].ToString();
                    decimal fund = row.Field<decimal>("Fund");

                    var dialog = this.CreateDialog<FrmHomePage>(borderStyle: FormBorderStyle.Sizable, windowState: FormWindowState.Normal);
                    dialog.Text = "个人首页";
                    dialog.TradeDate = fund > 0 ? tradeDate : null;
                    dialog.InvestorCode = investorCode;
                    dialog.InvestorName = investorName;
                    dialog.Show();
                }
            }
        }

        private void gvInvestorProfit_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataRow dr = gvInvestorProfit.GetFocusedDataRow();
                if (dr != null)
                {
                    string investorCode = dr.Field<string>("InvestorCode");
                    string investorName = dr.Field<string>("InvestorName");
                    DisplayInvestorProfitTrendChart(investorCode, investorName);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvInvestorProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvInvestorProfit.DrawRowIndicator(e);
        }

        private void gvInvestorProfit_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void gvInvestorProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.FieldName.IndexOf("Profit") >= 0 || e.Column.FieldName.IndexOf("Rate") >= 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void cbIndexUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date = CommonHelper.StringToDateTime(deProfit.EditValue.ToString());
                string indexCode = (cbIndexUp.SelectedItem as ComboBoxItemModel).Value;
                string indexName = cbIndexUp.Text.Trim();
                DisplayIndexTrendChart(date, indexCode, indexName, chartSummaryProfit);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void cbIndexDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date;
                DataRow dr = gvSummaryProfit.GetFocusedDataRow();

                if (dr != null)
                {
                    date = CommonHelper.StringToDateTime(dr.Field<string>("TradeDate"));
                }
                else
                {
                    date = CommonHelper.StringToDateTime(deProfit.EditValue.ToString());
                }
                string indexCode = (cbIndexDown.SelectedItem as ComboBoxItemModel).Value;
                string indexName = cbIndexDown.Text.Trim();
                DisplayIndexTrendChart(date, indexCode, indexName, chartInvestorProfit);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Events
    }
}