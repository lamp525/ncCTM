using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Dictionary;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmInvestorStudio : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;

        private string _currentDate = null;
        private DataRow _drInvestorProfit = null;
        private DataTable _dtPositionData = null;
        private DataTable _dtProfitData = null;

        #endregion Fields

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

        public FrmInvestorStudio(IDictionaryService dictionaryService)
        {
            InitializeComponent();

            _dictionaryService = dictionaryService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            esiInvestor.Text = LoginInfo.CurrentUser.UserName;

            dePosition.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            dePosition.Enabled = false;
            deProfit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            deProfit.Enabled = false;
            dePosition.EditValue = _currentDate;
            deProfit.EditValue = _currentDate;

            //交易类别
            var tradeTypes = this._dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.TradeType)
                .Select(x => new ComboBoxItemModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString(),
                }).ToList();
            this.cbTradeTypePosition.Initialize(tradeTypes, displayAdditionalItem: true);
            this.cbTradeTypeProfit.Initialize(tradeTypes, displayAdditionalItem: true);
        }

        private void GetInvestorProfit()
        {
            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorLatestProfit]	@InvestorCode = '{LoginInfo.CurrentUser.UserCode}',@TradeDate = '{_currentDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    _drInvestorProfit = ds.Tables[0].Rows[0];
                }
            }
        }

        private void BindInvestorProfit()
        {
            if (_drInvestorProfit == null) return;

            DataRow dr = _drInvestorProfit;

            lblCurValue.Text = dr.Field<decimal>("CurValue").ToString("N2");

            lblDayP.Text = dr.Field<decimal>("DayProfit").ToString("N2");
            if (dr.Field<decimal>("DayProfit") > 0)
                lblDayP.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("DayProfit") < 0)
                lblDayP.ForeColor = System.Drawing.Color.Green;

            lblDayR.Text = dr.Field<decimal>("DayRate").ToString("P2");
            if (dr.Field<decimal>("DayRate") > 0)
                lblDayR.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("DayRate") < 0)
                lblDayR.ForeColor = System.Drawing.Color.Green;

            lblWeekP.Text = dr.Field<decimal>("WeekProfit").ToString("N2");
            if (dr.Field<decimal>("WeekProfit") > 0)
                lblWeekP.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("WeekProfit") < 0)
                lblWeekP.ForeColor = System.Drawing.Color.Green;

            lblWeekR.Text = dr.Field<decimal>("WeekRate").ToString("P2");
            if (dr.Field<decimal>("WeekRate") > 0)
                lblWeekR.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("WeekRate") < 0)
                lblWeekR.ForeColor = System.Drawing.Color.Green;

            lblMonthP.Text = dr.Field<decimal>("MonthProfit").ToString("N2");
            if (dr.Field<decimal>("MonthProfit") > 0)
                lblMonthP.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("MonthProfit") < 0)
                lblMonthP.ForeColor = System.Drawing.Color.Green;

            lblMonthR.Text = dr.Field<decimal>("MonthRate").ToString("P2");
            if (dr.Field<decimal>("DayProfit") > 0)
                lblMonthR.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("DayProfit") < 0)
                lblMonthR.ForeColor = System.Drawing.Color.Green;

            lblYearP.Text = dr.Field<decimal>("YearProfit").ToString("N2");
            if (dr.Field<decimal>("MonthRate") > 0)
                lblYearP.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("MonthRate") < 0)
                lblYearP.ForeColor = System.Drawing.Color.Green;

            lblYearR.Text = dr.Field<decimal>("YearRate").ToString("P2");
            if (dr.Field<decimal>("YearRate") > 0)
                lblYearR.ForeColor = System.Drawing.Color.Red;
            else if (dr.Field<decimal>("YearRate") < 0)
                lblYearR.ForeColor = System.Drawing.Color.Green;
        }

        private void GetPositionRelateData()
        {
            string date = dePosition.Text.Trim();
            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorStockPosition]	@InvestorCode = '{LoginInfo.CurrentUser.UserCode}', @TradeDate = '{date}'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                _dtPositionData = ds.Tables[0];
            }
        }

        private void BindPositionData()
        {
            Series sePosition = chartPosition.Series[0];
            sePosition.Points.Clear();

            gcPosition.DataSource = null;

            int tradeType = int.Parse(cbTradeTypePosition.SelectedValue());

            IList<DataRow> data = _dtPositionData.AsEnumerable().Where(x => x.Field<int>("TradeType") == tradeType).OrderByDescending(x => x.Field<decimal>("CurValue")).ToArray();

            if (data.Count == 0) return;

            //持仓变动Grid
            gcPosition.DataSource = data.CopyToDataTable();

            //持仓分布图
            string argument = string.Empty;
            decimal positionValue;
            decimal otherValue = 0;
            for (int i = 0; i < data.Count; i++)
            {
                DataRow row = data[i];

                if (i < 7)
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

        private void DisplayProfitContrastChart()
        {
            //亏损
            Series seLoss = chartLoss.Series[0];
            //盈利
            Series seGain = chartGain.Series[0];

            seLoss.Points.Clear();
            seGain.Points.Clear();

            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorStockProfitContrast]	@InvestorCode = 'nctz046'	,@TradeDate = '2017/12/27'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);

            if (ds != null && ds.Tables.Count == 1)
            {
                string reportType = "D";

                IList<DataRow> profitList = ds.Tables[0].AsEnumerable().Where(x => x.Field<int>("TradeType") == (int)EnumLibrary.TradeType.All).ToList();
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

                    foreach (var item in lossList)
                    {
                        argument = item.StockName;
                        fund = item.Fund;
                        profit = item.Profit;
                        rate = item.Rate;
                        seLoss.Points.Add(new SeriesPoint(argument, profit));
                    }

                    //for (int i = 0; i < 10 - seLoss.Points.Count ; i++)
                    //{
                    //    seLoss.Points.Add(new SeriesPoint("11 ", 0));
                    //}

                    foreach (var item in gainList)
                    {
                        argument = item.StockName;
                        fund = item.Fund;
                        profit = item.Profit;
                        rate = item.Rate;
                        seGain.Points.Add(new SeriesPoint(argument, profit));
                    }

                    //for (int i = 0; i < 10 - seGain.Points.Count; i++)
                    //{
                    //    seGain.Points.Add(new SeriesPoint("11 ", 0));
                    //}
                }
            }
        }

        private void DisplayProfitTrendChart()
        {
            //使用资金
            Series seFund = chartProfitTrend.Series[0];
            //收益额
            Series seProfit = chartProfitTrend.Series[1];
            //收益率
            Series seRate = chartProfitTrend.Series[2];
            //年收益额
            Series seYearProfit = chartProfitTrend.Series[3];
            //年收益率
            Series seYearRate = chartProfitTrend.Series[4];
            //日均投入资金
            Series seAvgFund = chartProfitTrend.Series[5];

            seFund.Points.Clear();
            seProfit.Points.Clear();
            seRate.Points.Clear();
            seYearProfit.Points.Clear();
            seYearRate.Points.Clear();
            seAvgFund.Points.Clear();

            string sqlText = $@"EXEC	[dbo].[sp_IS_Investor25PeriodProfit]	@InvestorCode = 'nctz046'	,@TradeDate = '2017/12/27'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);

            if (ds != null && ds.Tables.Count == 1)
            {
                var profitList = ds.Tables[0].AsEnumerable()
                                        .Where(x => x.Field<int>("TradeType") == (int)EnumLibrary.TradeType.All && x.Field<string>("ReportType") == "D")
                                        .OrderBy(x => x.Field<string>("ReportDate"))
                                        .ToList();

                string argument = string.Empty;
                decimal fund;
                decimal profit;
                decimal rate;
                decimal yearProfit;
                decimal yearRate;
                decimal avgFund;

                foreach (DataRow row in profitList)
                {
                    argument = row["ReportDate"].ToString().Trim();
                    fund = CommonHelper.StringToDecimal(row["Fund"].ToString().Trim());
                    profit = CommonHelper.StringToDecimal(row["Profit"].ToString().Trim());
                    rate = CommonHelper.StringToDecimal(row["Rate"].ToString().Trim());
                    yearProfit = CommonHelper.StringToDecimal(row["YearProfit"].ToString().Trim());
                    yearRate = CommonHelper.StringToDecimal(row["YearRate"].ToString().Trim());
                    avgFund = CommonHelper.StringToDecimal(row["YearAvgFund"].ToString().Trim());

                    seFund.Points.Add(new SeriesPoint(argument, fund));
                    seProfit.Points.Add(new SeriesPoint(argument, profit));
                    seRate.Points.Add(new SeriesPoint(argument, rate));
                    seYearProfit.Points.Add(new SeriesPoint(argument, yearProfit));
                    seYearRate.Points.Add(new SeriesPoint(argument, yearRate));
                    seAvgFund.Points.Add(new SeriesPoint(argument, avgFund));
                }
            }
        }

        #endregion Utilities

        #region Events

        private void FrmInvestorStudio_Load(object sender, EventArgs e)
        {
            try
            {
                string sqlText = $@"SELECT  MAX(TradeDate) FROM DSTradeTypeProfit";
                var ret = SqlHelper.ExecuteScalar(AppConfig._ConnString, CommandType.Text, sqlText);
                _currentDate = ret == null ? DateTime.MinValue.ToShortDateString() : ret.ToString().Split(' ')[0];

                var bwInvestorProfit = new BackgroundWorker();
                bwInvestorProfit.WorkerSupportsCancellation = true;
                bwInvestorProfit.DoWork += BwInvestorProfit_DoWork;
                bwInvestorProfit.RunWorkerCompleted += BwInvestorProfit_RunWorkerCompleted;
                bwInvestorProfit.RunWorkerAsync();

                FormInit();
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

        private void dePosition_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dePosition.Enabled = false;

                var bwPosition = new BackgroundWorker();
                bwPosition.DoWork += BwPosition_DoWork;
                bwPosition.RunWorkerCompleted += BwPosition_RunWorkerCompleted;
                bwPosition.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void BwPosition_DoWork(object sender, DoWorkEventArgs e)
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

        private void BwPosition_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null && e.Error == null)
                {
                    dePosition.Enabled = true;
                    cbTradeTypePosition.DefaultSelected("0");
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
                BindPositionData();
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

        private void deProfit_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void cbTradeTypeProfit_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbTradeTypePosition_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void chbDay_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chbWeek_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chbMonth_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chbYear_CheckedChanged(object sender, EventArgs e)
        {
        }

        #endregion Events
    }
}