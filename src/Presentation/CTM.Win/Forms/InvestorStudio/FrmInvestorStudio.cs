using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.InvestorStudio
{
    public partial class FrmInvestorStudio : BaseForm
    {
        public FrmInvestorStudio()
        {
            InitializeComponent();
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

            if(ds != null && ds.Tables.Count == 1 )
            {
                var profitList = ds.Tables[0].AsEnumerable()
                                        .Where(x=>x.Field<int>("TradeType") == (int)EnumLibrary.TradeType.All && x.Field<string>("ReportType") == "D")
                                        .OrderBy(x => x.Field<string>("ReportDate"))
                                        .ToList ();

                string argument = string.Empty;
                decimal fund;
                decimal profit;
                decimal rate;

                foreach (DataRow row in profitList)
                {
                    argument = row["ReportDate"].ToString().Trim();
                    fund =CommonHelper.StringToDecimal( row["Fund"].ToString().Trim());
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
