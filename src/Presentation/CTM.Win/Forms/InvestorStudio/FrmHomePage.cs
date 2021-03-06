﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Dictionary;
using CTM.Win.Extensions;
using CTM.Win.Forms.DailyTrading.TradeIdentifier;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmHomePage : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;

        private string _tradeDate;
        private string _investorCode;
        private string _investorName;
        private DataRow _drInvestorProfit = null;
        private DataTable _dtPositionData = null;
        private DataTable _dtProfitContrastData = null;
        private DataTable _dtProfitTrendData = null;
        private DataTable _dtStockProfitData = null;
        private DataTable _dtIndexRate = null;

        #endregion Fields

        #region Properties

        public string TradeDate
        {
            get { return _tradeDate; }
            set { _tradeDate = value; }
        }

        public string InvestorCode
        {
            get { return _investorCode; }
            set { _investorCode = value; }
        }

        public string InvestorName
        {
            get { return _investorName; }
            set { _investorName = value; }
        }

        #endregion Properties

        #region Models

        private class StockProfit
        {
            public string StockCode { get; set; }
            public string StockName { get; set; }
            public decimal Profit { get; set; }
            public decimal Rate { get; set; }
            public decimal Fund { get; set; }
        }

        #endregion Models

        #region Constructors

        public FrmHomePage(IDictionaryService dictionaryService)
        {
            InitializeComponent();

            _dictionaryService = dictionaryService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            if (string.IsNullOrEmpty(_investorCode))
                _investorCode = LoginInfo.CurrentUser.UserCode;

            if (string.IsNullOrEmpty(_investorName))
                _investorName = LoginInfo.CurrentUser.UserName;

            if (string.IsNullOrEmpty(_tradeDate))
            {
                string sqlText = $@"SELECT  MAX(TradeDate) FROM DSTradeTypeProfit  WHERE DataType = 1 AND TradeType = 0 AND InvestorCode = '{_investorCode}' AND DayFund > 0";
                var ret = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText);
                _tradeDate = ret == null ? DateTime.MinValue.ToShortDateString() : ret.ToString().Split(' ')[0];
            }

            lblInvestor.Text = "   " + _investorName;

            deInvestor.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            deInvestor.Enabled = false;
            deInvestor.EditValue = _tradeDate;

            deProfit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            dePosition.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            dePosition.Enabled = false;
            dePosition.EditValue = _tradeDate;

            deProfit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            deProfit.Enabled = false;
            deProfit.EditValue = _tradeDate;

            //交易类别
            var tradeTypes = this._dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            this.cbTradeTypePosition.Initialize(tradeTypes, displayAdditionalItem: true);
            this.cbTradeTypeProfit.Initialize(tradeTypes, displayAdditionalItem: true);

            ttcPosition.SetToolTip(gcPosition, "双击有买卖操作的数据行，" + Environment.NewLine + "可查看分时交易标识！");

            string sqlText1 = $@"SELECT DISTINCT Code,Name FROM [FinancialCenter].[dbo].[IndexInfo] ORDER BY Code";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
            {
                List<ComboBoxItemModel> indexInfo = ds1.Tables[0].AsEnumerable().Select(x => new ComboBoxItemModel
                {
                    Text = x.Field<string>("Name"),
                    Value = x.Field<string>("Code")
                }).ToList();

                cbIndex.Initialize(indexInfo);
            }
        }

        private void GetInvestorProfit()
        {
            string date = deInvestor.EditValue.ToString();
            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorLatestProfit]	@InvestorCode = '{_investorCode}',@TradeDate = '{date}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
            {
                _drInvestorProfit = ds.Tables[0].Rows[0];
            }
            else
                _drInvestorProfit = null;
        }

        private void BindInvestorProfit()
        {
            if (_drInvestorProfit == null)
            {
                string nulValue = "-";
                lblCurValue.Text = nulValue;
                lblDayP.Text = nulValue;
                lblDayR.Text = nulValue;
                lblWeekP.Text = nulValue;
                lblWeekR.Text = nulValue;
                lblMonthP.Text = nulValue;
                lblMonthR.Text = nulValue;
                lblYearP.Text = nulValue;
                lblYearR.Text = nulValue;
                lblAccProfit.Text = nulValue;
                lblAccRate.Text = nulValue;
            }
            else
            {
                DataRow dr = _drInvestorProfit;

                string unit = " 万元";

                lblCurValue.Text = dr.Field<decimal>("CurValue").ToString("N2") + unit;

                lblDayP.Text = dr.Field<decimal>("DayProfit").ToString("N2") + unit;
                if (dr.Field<decimal>("DayProfit") > 0)
                    lblDayP.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("DayProfit") < 0)
                    lblDayP.ForeColor = System.Drawing.Color.Green;

                lblDayR.Text = dr.Field<decimal>("DayRate").ToString("P2");
                if (dr.Field<decimal>("DayRate") > 0)
                    lblDayR.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("DayRate") < 0)
                    lblDayR.ForeColor = System.Drawing.Color.Green;

                lblWeekP.Text = dr.Field<decimal>("WeekProfit").ToString("N2") + unit;
                if (dr.Field<decimal>("WeekProfit") > 0)
                    lblWeekP.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("WeekProfit") < 0)
                    lblWeekP.ForeColor = System.Drawing.Color.Green;

                lblWeekR.Text = dr.Field<decimal>("WeekRate").ToString("P2");
                if (dr.Field<decimal>("WeekRate") > 0)
                    lblWeekR.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("WeekRate") < 0)
                    lblWeekR.ForeColor = System.Drawing.Color.Green;

                lblMonthP.Text = dr.Field<decimal>("MonthProfit").ToString("N2") + unit;
                if (dr.Field<decimal>("MonthProfit") > 0)
                    lblMonthP.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("MonthProfit") < 0)
                    lblMonthP.ForeColor = System.Drawing.Color.Green;

                lblMonthR.Text = dr.Field<decimal>("MonthRate").ToString("P2");
                if (dr.Field<decimal>("MonthRate") > 0)
                    lblMonthR.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("MonthRate") < 0)
                    lblMonthR.ForeColor = System.Drawing.Color.Green;

                lblYearP.Text = dr.Field<decimal>("YearProfit").ToString("N2") + unit;
                if (dr.Field<decimal>("YearProfit") > 0)
                    lblYearP.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("YearProfit") < 0)
                    lblYearP.ForeColor = System.Drawing.Color.Green;

                lblYearR.Text = dr.Field<decimal>("YearRate").ToString("P2");
                if (dr.Field<decimal>("YearRate") > 0)
                    lblYearR.ForeColor = System.Drawing.Color.Red;
                else if (dr.Field<decimal>("YearRate") < 0)
                    lblYearR.ForeColor = System.Drawing.Color.Green;

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

        private void GetPositionRelateData()
        {
            _dtPositionData = null;
            string date = dePosition.EditValue.ToString();
            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorStockPosition]	@InvestorCode = '{_investorCode}', @TradeDate = '{date}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                _dtPositionData = ds.Tables[0];
            }
        }

        private void BindPositionRelateData()
        {
            Series sePosition = chartPosition.Series[0];
            sePosition.Points.Clear();

            gcPosition.DataSource = null;

            int tradeType = int.Parse(cbTradeTypePosition.SelectedValue());

            if (_dtPositionData == null) return;

            IList<DataRow> data = _dtPositionData.AsEnumerable().Where(x => x.Field<int>("TradeType") == tradeType).OrderByDescending(x => Math.Abs(x.Field<decimal>("CurValue"))).ToArray();

            if (!data.Any()) return;

            //持仓变动Grid
            gcPosition.DataSource = data.CopyToDataTable();

            //持仓分布图
            string argument = string.Empty;
            decimal positionValue;
            decimal otherValue = 0;
            for (int i = 0; i < data.Count; i++)
            {
                DataRow row = data[i];

                if (i < 12)
                {
                    argument = row["StockName"].ToString().Trim();
                    positionValue = Math.Abs(CommonHelper.StringToDecimal(row["CurValue"].ToString().Trim()));
                    sePosition.Points.Add(new SeriesPoint(argument, positionValue));
                }
                else
                {
                    otherValue += Math.Abs(CommonHelper.StringToDecimal(row["CurValue"].ToString().Trim()));

                    if (i == data.Count - 1)
                    {
                        argument = "其它";
                        sePosition.Points.Add(new SeriesPoint(argument, otherValue));
                    }
                }
            }

            //sePosition.Points.OrderBy(x => x.QualitativeArgument);
        }

        private void GetProfitRelateData()
        {
            _dtProfitContrastData = null;
            _dtProfitTrendData = null;
            _dtStockProfitData = null;

            string date = deProfit.EditValue.ToString();

            string sqlText1 = $@"EXEC	[dbo].[sp_IS_InvestorStockProfitContrast]	@InvestorCode = '{_investorCode}', @TradeDate = '{date}'";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
                _dtProfitContrastData = ds1.Tables[0];

            string sqlText2 = $@"EXEC	[dbo].[sp_IS_Investor25PeriodProfit]	@InvestorCode = '{_investorCode}', @TradeDate = '{date}'";
            DataSet ds2 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText2);
            if (ds2 != null && ds2.Tables.Count == 1 && ds2.Tables[0].Rows.Count > 0)
            {
                _dtProfitTrendData = ds2.Tables[0].AsEnumerable().Where(x => x.Field<int>("DataType") == 1).CopyToDataTable();
                _dtStockProfitData = ds2.Tables[0].AsEnumerable().Where(x => x.Field<int>("DataType") == 99).CopyToDataTable();
            }
        }

        private void BindProfitRelateData()
        {
            int tradeType = int.Parse(cbTradeTypeProfit.SelectedValue());
            string reportType = string.Empty;

            switch (rgReportType.SelectedIndex)
            {
                case 0:
                    reportType = "D";
                    break;

                case 1:
                    reportType = "W";
                    break;

                case 2:
                    reportType = "M";
                    break;

                case 3:
                    reportType = "Y";
                    break;

                default:
                    reportType = "D";
                    break;
            }

            if (tabProfit.SelectedTabPage == tpProfitChart)
            {
                DisplayProfitContrastChart(tradeType, reportType);
                DisplayProfitTrendChart(tradeType, reportType);
            }
            else
            {
                BindInvestorProfitList(tradeType, reportType);
            }
        }

        private void DisplayProfitContrastChart(int tradeType, string reportType)
        {
            //亏损
            Series seLoss = chartLoss.Series[0];
            //盈利
            Series seGain = chartGain.Series[0];

            seLoss.Points.Clear();
            seGain.Points.Clear();

            if (_dtProfitContrastData == null) return;

            IList<DataRow> profitList = _dtProfitContrastData.AsEnumerable().Where(x => x.Field<int>("TradeType") == tradeType).ToList();

            if (!profitList.Any()) return;

            IList<StockProfit> contrastList = null;
            if (profitList.Count > 0)
            {
                switch (reportType)
                {
                    case "D":
                        contrastList = profitList
                            .Select(x => new StockProfit
                            {
                                Fund = x.Field<decimal>("DayFund"),
                                Profit = x.Field<decimal>("DayProfit"),
                                Rate = x.Field<decimal>("DayRate"),
                                StockCode = x.Field<string>("StockCode"),
                                StockName = x.Field<string>("StockName"),
                            }
                            ).ToList();
                        break;

                    case "W":
                        contrastList = profitList
                            .Select(x => new StockProfit
                            {
                                Fund = x.Field<decimal>("WeekAvgFund"),
                                Profit = x.Field<decimal>("WeekProfit"),
                                Rate = x.Field<decimal>("WeekRate"),
                                StockCode = x.Field<string>("StockCode"),
                                StockName = x.Field<string>("StockName"),
                            }
                            ).ToList();
                        break;

                    case "M":
                        contrastList = profitList
                            .Select(x => new StockProfit
                            {
                                Fund = x.Field<decimal>("MonthAvgFund"),
                                Profit = x.Field<decimal>("MonthProfit"),
                                Rate = x.Field<decimal>("MonthRate"),
                                StockCode = x.Field<string>("StockCode"),
                                StockName = x.Field<string>("StockName"),
                            }
                            ).ToList();
                        break;

                    case "Y":
                        contrastList = profitList
                            .Select(x => new StockProfit
                            {
                                Fund = x.Field<decimal>("YearAvgFund"),
                                Profit = x.Field<decimal>("YearProfit"),
                                Rate = x.Field<decimal>("YearRate"),
                                StockCode = x.Field<string>("StockCode"),
                                StockName = x.Field<string>("StockName"),
                            }
                            ).ToList();
                        break;

                    default:
                        break;
                }

                IList<StockProfit> lossList = contrastList.Where(x => x.Profit < 0).OrderBy(x => x.Profit).Take(10).ToList();
                IList<StockProfit> gainList = contrastList.Where(x => x.Profit > 0).OrderByDescending(x => x.Profit).Take(10).ToList();

                string argument = string.Empty;
                decimal fund;
                decimal profit;
                decimal rate;

                StockProfit item = null;
                for (int i = 0; i < lossList.Count; i++)
                {
                    item = lossList[i];
                    argument = item.StockName;
                    fund = item.Fund;
                    profit = item.Profit;
                    rate = item.Rate;
                    seLoss.Points.Add(new SeriesPoint(argument, profit, i));
                }

                int lossPointCount = seLoss.Points.Count;
                for (int i = 10; i > lossPointCount; i--)
                {
                    seLoss.Points.Add(new SeriesPoint("NA" + i.ToString(), 0, i));
                }

                for (int i = 0; i < gainList.Count; i++)
                {
                    item = gainList[i];
                    argument = item.StockName;
                    fund = item.Fund;
                    profit = item.Profit;
                    rate = item.Rate;
                    seGain.Points.Add(new SeriesPoint(argument, profit, i));
                }

                int gainPointCount = seGain.Points.Count;
                for (int i = 10; i > gainPointCount; i--)
                {
                    seGain.Points.Add(new SeriesPoint("NA" + i.ToString(), 0, i));
                }
            }
        }

        private void DisplayProfitTrendChart(int tradeType, string reportType)
        {
            string reportName = string.Empty;
            switch (reportType)
            {
                case "D":
                    reportName = "日";
                    break;

                case "W":
                    reportName = "周";
                    break;

                case "M":
                    reportName = "月";
                    break;

                case "Y":
                    reportName = "年";
                    break;
            }

            //日均投入资金
            Series seAvgFund = chartProfitTrend.Series[1];

            //使用资金
            Series seFund = chartProfitTrend.Series[2];

            //收益额
            Series seProfit = chartProfitTrend.Series[3];
            seProfit.Name = reportName + "收益额";

            //收益率
            Series seRate = chartProfitTrend.Series[4];
            seRate.Name = reportName + "收益率";

            //年收益额
            Series seYearProfit = chartProfitTrend.Series[5];
            //年收益率
            Series seYearRate = chartProfitTrend.Series[6];

            seFund.Points.Clear();
            seProfit.Points.Clear();
            seRate.Points.Clear();
            seYearProfit.Points.Clear();
            seYearRate.Points.Clear();
            seAvgFund.Points.Clear();

            if (_dtProfitTrendData == null) return;

            var profitList = _dtProfitTrendData.AsEnumerable()
                                    .Where(x => x.Field<int>("TradeType") == tradeType && x.Field<string>("ReportType") == reportType)
                                    .OrderBy(x => x.Field<string>("TradeDate"))
                                    .ToList();

            if (!profitList.Any()) return;

            string argument = string.Empty;
            decimal fund;
            decimal profit;
            decimal rate;
            decimal accProfit;
            decimal accRate;
            decimal avgFund;

            foreach (DataRow row in profitList)
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

        private void BindInvestorProfitList(int tradeType, string reportType)
        {
            gcInvestorProfit.DataSource = null;

            var data = _dtProfitTrendData.AsEnumerable()
                        .Where(x => x.Field<int>("TradeType") == tradeType && x.Field<string>("ReportType") == reportType)
                        .OrderByDescending(x => x.Field<string>("TradeDate"));

            if (data.Any())
                gcInvestorProfit.DataSource = data.CopyToDataTable();
        }

        private void BindStockProfitList(int tradeType, string reportType, string date)
        {
            gcStockProfit.DataSource = null;

            if (_dtStockProfitData == null) return;

            var data = _dtStockProfitData.AsEnumerable()
                        .Where(x => x.Field<string>("TradeDate") == date && x.Field<int>("TradeType") == tradeType && x.Field<string>("ReportType") == reportType && x.Field<decimal>("Fund") != 0)
                        .OrderBy(x => x.Field<string>("StockCode"));

            if (data.Any())
                gcStockProfit.DataSource = data.CopyToDataTable();
        }

        private void GetIndexRateData()
        {
            string date = deProfit.EditValue.ToString();
            string sqlText1 = $@"EXEC	[dbo].[sp_IS_Index_Rate] @CurDate = '{date}', @DayNumber = 25";
            DataSet ds1 = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);
            if (ds1 != null && ds1.Tables.Count == 1)
                _dtIndexRate = ds1.Tables[0];
        }

        private void DisplayIndexTrendChart()
        {
            DateTime date = CommonHelper.StringToDateTime(deProfit.EditValue.ToString());

            string indexCode = (cbIndex.SelectedItem as ComboBoxItemModel).Value;
            string indexName = cbIndex.Text.Trim();
            Series seIndex = chartProfitTrend.Series[0];
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

        private void FrmInvestorStudio_Load(object sender, EventArgs e)
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

        private void deInvestor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                deInvestor.Enabled = false;

                var bwInvestorProfit = new BackgroundWorker();
                bwInvestorProfit.WorkerSupportsCancellation = true;
                bwInvestorProfit.DoWork += BwInvestorProfit_DoWork;
                bwInvestorProfit.RunWorkerCompleted += BwInvestorProfit_RunWorkerCompleted; bwInvestorProfit.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void BwInvestorProfit_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                GetInvestorProfit();
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void BwInvestorProfit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null && e.Error == null)
                {
                    BindInvestorProfit();
                    deInvestor.Enabled = true;
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

        #region Position

        private void dePosition_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dePosition.Enabled = false;
                cbTradeTypePosition.Enabled = false;

                var bwPosition = new BackgroundWorker();
                bwPosition.DoWork += bwPosition_DoWork;
                bwPosition.RunWorkerCompleted += bwPosition_RunWorkerCompleted;
                bwPosition.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void bwPosition_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                GetPositionRelateData();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void bwPosition_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null && e.Error == null)
                {
                    if (cbTradeTypePosition.EditValue == null)
                        cbTradeTypePosition.DefaultSelected("0");
                    else
                        BindPositionRelateData();

                    dePosition.Enabled = true;
                    cbTradeTypePosition.Enabled = true;
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

        private void cbTradeTypePosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbTradeTypePosition.Enabled = false;
                BindPositionRelateData();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                cbTradeTypePosition.Enabled = true;
            }
        }

        private void gvPosition_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvPosition.DrawRowIndicator(e);
        }

        private void gvPosition_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
                else
                {
                    if (e.Column == colDiffVol)
                    {
                        e.DisplayText = (cellValue > 0 ? "+ " : "- ") + Math.Abs(cellValue).ToString("N0");
                    }
                    else if (e.Column == colDiffValue)
                    {
                        e.DisplayText = (cellValue > 0 ? "+ " : "- ") + Math.Abs(cellValue).ToString("N2");
                    }
                }
            }
        }

        private void gvPosition_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == colDiffVol || e.Column == colDiffValue)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Column == this.colBuyVolume)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            else if (e.Column == this.colSellVolume)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void gvPosition_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gvPosition.GridControl.PointToClient(Control.MousePosition);
            var hi = gvPosition.CalcHitInfo(pt);
            if (hi.InRow)
            {
                var row = gvPosition.GetDataRow(hi.RowHandle);
                if (row != null)
                {
                    decimal buyVol = decimal.Parse(row["BuyVolume"].ToString());
                    decimal sellVol = decimal.Parse(row["SellVolume"].ToString());
                    if (buyVol != 0 || sellVol != 0)
                    {
                        var curDate = CommonHelper.StringToDateTime(dePosition.EditValue.ToString());

                        TradeInfoModel tradeInfo = new TradeInfoModel
                        {
                            DisplayText = row["StockCode"].ToString() + '-' + row["StockName"].ToString() + '-' + _investorName,
                            InvestorCode = _investorCode,
                            InvestorName = _investorName,
                            StockCode = row["StockCode"].ToString(),
                            StockName = row["StockName"].ToString(),
                            TradeCode = row["StockCode"].ToString() + '-' + _investorCode,
                        };
                        var dialog = this.CreateDialog<FrmTimeSharingTradeIdentifier>(borderStyle: FormBorderStyle.Sizable, windowState: FormWindowState.Normal);
                        dialog.Text = "分时交易标识";
                        dialog.TradeDate = curDate;
                        dialog.TradeInfo = tradeInfo;
                        dialog.Show();
                    }
                }
            }
        }

        #endregion Position

        #region Profit

        private void deProfit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                deProfit.Enabled = false;
                cbTradeTypeProfit.Enabled = false;
                rgReportType.Enabled = false;
                tabProfit.Enabled = false;

                var bwProfit = new BackgroundWorker();
                bwProfit.DoWork += bwProfit_DoWork;
                bwProfit.RunWorkerCompleted += BwProfit_RunWorkerCompleted;
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
                GetProfitRelateData();

                GetIndexRateData();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void BwProfit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null && e.Error == null)
                {
                    if (cbTradeTypeProfit.EditValue == null)
                        cbTradeTypeProfit.DefaultSelected("0");
                    else
                        BindProfitRelateData();

                    if (cbIndex.EditValue == null)
                        cbIndex.DefaultSelected("000001");
                    else
                        DisplayIndexTrendChart();

                    deProfit.Enabled = true;
                    cbTradeTypeProfit.Enabled = true;
                    rgReportType.Enabled = true;
                    tabProfit.Enabled = true;
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

        private void cbTradeTypeProfit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbTradeTypeProfit.Enabled = false;
                BindProfitRelateData();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                cbTradeTypeProfit.Enabled = true;
            }
        }

        private void rgReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rgReportType.Enabled = false;
                BindProfitRelateData();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                rgReportType.Enabled = true;
            }
        }

        private void tabProfit_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                tabProfit.Enabled = false;

                BindProfitRelateData();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                tabProfit.Enabled = true;
            }
        }

        private void gvInvestorProfit_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRow row = gv.GetDataRow(gv.FocusedRowHandle);

                if (row == null) return;

                string tradeDate = row["TradeDate"].ToString();
                int tradeType = int.Parse(row["TradeType"].ToString());
                string reportType = row["ReportType"].ToString();
                BindStockProfitList(tradeType, reportType, tradeDate);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
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

        private void gvInvestorProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvInvestorProfit.DrawRowIndicator(e);
        }

        private void gvStockProfit_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            decimal cellValue;

            if (decimal.TryParse(e.CellValue.ToString(), out cellValue))
            {
                if (cellValue == 0)
                    e.DisplayText = "-";
            }
        }

        private void gvStockProfit_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        private void gvStockProfit_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            gvStockProfit.DrawRowIndicator(e);
        }

        private void cbIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayIndexTrendChart();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Profit

        #region Export

        private void btnExportFlow_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExportFlow.Enabled = false;
                if (this.gcInvestorProfit.DataSource != null)
                {
                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = "交易员收益流水_" + DateTime.Now.ToString("yyyyMMdd") + "_" + this._investorName + ".xlsx";
                    string destinyFilePath = Path.Combine(filePath, fileName);
                    if (File.Exists(destinyFilePath))
                        File.Delete(destinyFilePath);
                    this.gvInvestorProfit.ExportToXlsx(destinyFilePath);
                    DXMessage.ShowTips($@"报表[{fileName}]已成功导出至桌面！");
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnExportFlow.Enabled = true;
            }
        }

        private void btnExportDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExportDetail.Enabled = false;

                if (this.gcStockProfit.DataSource != null)
                {
                    string curDate = this.gvInvestorProfit.GetFocusedDataRow()?["TradeDate"].ToString().Replace("-", "");
                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = "股票收益明细_" + curDate + "_" + this._investorName + ".xlsx";
                    string destinyFilePath = Path.Combine(filePath, fileName);
                    if (File.Exists(destinyFilePath))
                        File.Delete(destinyFilePath);
                    this.gvStockProfit.ExportToXlsx(destinyFilePath);
                    DXMessage.ShowTips($@"报表[{fileName}]已成功导出至桌面！");
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnExportDetail.Enabled = true;
            }
        }

        #endregion Export

        #endregion Events
    }
}