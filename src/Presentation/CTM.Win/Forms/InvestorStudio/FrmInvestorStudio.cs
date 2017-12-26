using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmInvestorStudio : BaseForm
    {
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

        private void DisplayProfitContrastChart()
        {
            //亏损
            Series seLoss = chartLoss.Series[0];
            //盈利
            Series seGain = chartGain.Series[0];

            seLoss.Points.Clear();
            seGain.Points.Clear();

            string sqlText = $@"EXEC	[dbo].[sp_IS_InvestorStockProfitContrast]	@InvestorCode = 'nctz026'	,@TradeDate = '2017/12/01'";
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

                    IList<StockProfit> lossList = contrastList.Where(x => x.Profit < 0).OrderBy(x=>x.Profit).ToList();
                    IList<StockProfit> gainList = contrastList.Where(x => x.Profit > 0).OrderByDescending(x=>x.Profit).ToList();

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

                    foreach (var item in gainList)
                    {
                        argument = item.StockName;
                        fund = item.Fund;
                        profit = item.Profit;
                        rate = item.Rate;
                        seGain.Points.Add(new SeriesPoint(argument, profit));
                    }
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

            string sqlText = $@"EXEC	[dbo].[sp_IS_Investor25PeriodProfit]		@InvestorCode = 'nctz026'	,@TradeDate = '2017/12/01'";
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

        private void FrmInvestorStudio_Load(object sender, EventArgs e)
        {
            try
            {
                DisplayProfitContrastChart();
                DisplayProfitTrendChart();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}