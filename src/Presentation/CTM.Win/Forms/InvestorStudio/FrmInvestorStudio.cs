using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmInvestorStudio : BaseForm       
    {

        private DataTable _dtPositionData = null;
        private IList<DataRow> _filteredPosition = null;

        private class StockProfit
        {
            public string StockCode { get; set; }
            public string StockName { get; set; }
            public decimal Profit { get; set; }
            public decimal Rate { get; set; }
            public decimal Fund { get; set; }
        }

        public FrmInvestorStudio()
        {
            InitializeComponent();
        }


        private void FormInit()
        {
            esiInvestor.Text = LoginInfo.CurrentUser.UserName;


        }

        private void DisplayLatestProfit()
        {     

            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorLatestProfit]	@InvestorCode = 'nctz026',@TradeDate = '2017/12/27'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                gcCurrent.DataSource = ds.Tables[0];
                cvCurrent.PopulateColumns();
                cvCurrent.SaveLayoutToXml ("111.xml");
                //if (ds.Tables[0].Rows.Count == 1)
                //{
                //    DataRow dr = ds.Tables[0].Rows[0];

                //    lblCurValue.Text = dr.Field<decimal>("CurValue").ToString("N2");
                //    lblDayP.Text = dr.Field<decimal>("DayProfit").ToString("N2");
                //    lblDayR.Text = dr.Field<decimal>("DayRate").ToString("P2");
                //    lblWeekP.Text = dr.Field<decimal>("WeekProfit").ToString("N2");
                //    lblWeekR.Text = dr.Field<decimal>("WeekRate").ToString("P2");
                //    lblMonthP.Text = dr.Field<decimal>("MonthProfit").ToString("N2");
                //    lblMonthR.Text = dr.Field<decimal>("MonthRate").ToString("P2");
                //    lblYearP.Text = dr.Field<decimal>("YearProfit").ToString("N2");
                //    lblYearR.Text = dr.Field<decimal>("YearRate").ToString("P2");
                //}
            }
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

            seFund.Points.Clear();
            seProfit.Points.Clear();
            seRate.Points.Clear();

            string sqlText = $@"EXEC	[dbo].[sp_IS_Investor25PeriodProfit]	@InvestorCode = 'nctz026'	,@TradeDate = '2017/12/27'";
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

                foreach (DataRow row in profitList)
                {
                    argument = row["ReportDate"].ToString().Trim();
                    fund = CommonHelper.StringToDecimal(row["Fund"].ToString().Trim());
                    profit = CommonHelper.StringToDecimal(row["Profit"].ToString().Trim());
                    rate = CommonHelper.StringToDecimal(row["Rate"].ToString().Trim());

                    seFund.Points.Add(new SeriesPoint(argument, fund));
                    seProfit.Points.Add(new SeriesPoint(argument, profit));
                    seRate.Points.Add(new SeriesPoint(argument, rate));
                }
            }
        }

        private void DisplayPositionChart()
        {
            Series sePosition = chartPosition.Series[0];
            sePosition.Points.Clear();

            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorStockPosition]	@InvestorCode = 'nctz001', @TradeDate = '2017/12/27'";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                _dtPositionData = ds.Tables[0];

                _filteredPosition  = _dtPositionData.AsEnumerable()
                                            .Where(x => x.Field<int>("TradeType") == (int)EnumLibrary.TradeType.All)
                                            .OrderByDescending (x => x.Field<decimal>("CurValue")).ToArray();

                string argument = string.Empty;
                decimal positionValue;
                decimal otherValue = 0;
                for (int i = 0; i < _filteredPosition.Count; i++)
                {
                    DataRow row = _filteredPosition[i];

                    if (i < 7)
                    {
                        argument = row["StockName"].ToString().Trim();
                        positionValue = Math.Abs(CommonHelper.StringToDecimal(row["CurValue"].ToString().Trim()));
                        sePosition.Points.Add(new SeriesPoint(argument, positionValue));
                    }
                    else
                    {
                        otherValue += Math.Abs(CommonHelper.StringToDecimal(row["CurValue"].ToString().Trim()));

                        if (i == _filteredPosition.Count - 1)
                        {
                            argument = "其它";
                            sePosition.Points.Add(new SeriesPoint(argument, otherValue));
                        }
                    }
                }

                //sePosition.Points.OrderBy(x => x.QualitativeArgument);

            }
        }

        private void BindPositionGrid()
        {
            gcPosition.DataSource = _filteredPosition.CopyToDataTable ();

        }

        private void FrmInvestorStudio_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                DisplayLatestProfit();
                DisplayProfitContrastChart();
                DisplayProfitTrendChart();
                DisplayPositionChart();
                BindPositionGrid();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

    }
}