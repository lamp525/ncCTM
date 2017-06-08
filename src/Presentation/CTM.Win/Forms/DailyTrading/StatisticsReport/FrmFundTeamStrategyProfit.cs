using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmFundTeamStrategyProfit : BaseForm
    {
        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _profitData = null;
        private Series _seProfit;

        public FrmFundTeamStrategyProfit()
        {
            InitializeComponent();
        }
        #region Utilities
        private void ChartInit()
        {
            //#region Title

            //ChartTitle chartTitle = new ChartTitle();
            ////标题内容
            //chartTitle.Text = "";
            ////字体颜色
            //chartTitle.TextColor = Color.White;
            ////字体类型字号
            //chartTitle.Font = new Font("新宋体", 11, FontStyle.Bold);
            ////标题对齐方式
            //chartTitle.Dock = ChartTitleDockStyle.Top;
            //chartTitle.Alignment = StringAlignment.Near;

            //chartControl1.Titles.Clear();
            //chartControl1.Titles.Add(chartTitle);
            //#endregion

            #region Series

            _seProfit = new Series("累计收益额（万元）", ViewType.Line);
            _seProfit.ArgumentScaleType = ScaleType.Qualitative;
            _seProfit.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            LineSeriesView myView1 = (LineSeriesView)_seProfit.View;
            myView1.Color = Color.DeepSkyBlue;
            

            this.chartControl1.Series.Add(_seProfit);
     

            #endregion Series

            #region XYDiagram

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;

            #endregion XYDiagram

            #region AxisX

            AxisX myAxisX = myDiagram.AxisX;

            myAxisX.Label.Font = new Font("Tahoma ", 8, FontStyle.Bold);
            myAxisX.Label.Staggered = false;
            myAxisX.Label.Angle = -45;
            myAxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            myAxisX.Tickmarks.MinorVisible = false;
            myAxisX.WholeRange.Auto = true;
            myAxisX.WholeRange.AutoSideMargins = true;

            #endregion AxisX

            #region AxisY

            AxisY myAxisY = myDiagram.AxisY;
            myAxisY.Label.Font = new Font("Tahoma ", 8, FontStyle.Bold);
            myAxisY.Label.TextPattern = "{ V:F2}";
            //myAxisY.GridLines.Color = Color.FromArgb(165, 42, 42);
            //myAxisY.GridLines.LineStyle.Thickness = 1;
            //myAxisY.GridLines.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            myAxisY.Tickmarks.MinorVisible = false;
            myAxisY.WholeRange.Auto = true;

            #endregion AxisY
        }

        private void GetProfitData()
        {
            var commandText = $@"EXEC [dbo].[sp_FundTeamStrategyProfit] ";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count > 0)
            {
                _profitData = ds.Tables[0];
            }
        }

        private void DisplayChart()
        {
            _seProfit.Points.Clear();

            if (_profitData == null || _profitData.Rows.Count == 0) return;

            var argument = string.Empty;
            double profit;

            foreach  (DataRow row in _profitData.Rows)
            {
                argument = row["TradeDate"].ToString().Trim();
                profit = CommonHelper.StringToDouble(row["AccumulateProfit"].ToString().ToString ());

                _seProfit.Points.Add(new SeriesPoint(argument,profit));
            }
        }

        #endregion

        private void FrmFundTeamStrategyProfit_Load(object sender, EventArgs e)
        {
            try
            {
                ChartInit();

                GetProfitData();

                DisplayChart();
            }
            catch (Exception ex)
            {

                DXMessage.ShowError (ex.Message);
            }
        }


    }
}
