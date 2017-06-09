using System;
using System.Data;
using System.Drawing;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmFundTeamStrategyProfit : BaseForm
    {
        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _profitData = null;
        private ConstantLine _clZero;
        private Series _seAccumulateProfit;
        private Series _seDayProfit;

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

            _clZero = new ConstantLine(string.Empty, 0);
            _clZero.Color = Color.Black;
            _clZero.ShowInLegend = false;
            _clZero.ShowBehind = true;

            #region Series

            _seAccumulateProfit = new Series("累计收益额（万元）", ViewType.Line);
            _seAccumulateProfit.ArgumentScaleType = ScaleType.Qualitative;
            _seAccumulateProfit.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            _seAccumulateProfit.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            LineSeriesView myView1 = (LineSeriesView)_seAccumulateProfit.View;
            myView1.Color = Color.DeepSkyBlue;

            _seDayProfit = new Series("日收益额（万元）", ViewType.Line);
            _seDayProfit.ArgumentScaleType = ScaleType.Qualitative;
            _seDayProfit.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            _seDayProfit.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            LineSeriesView myView2 = (LineSeriesView)_seDayProfit.View;
            myView2.Color = Color.OrangeRed;

            this.chartControl1.Series.Add(_seAccumulateProfit);
            this.chartControl1.Series.Add(_seDayProfit);

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

            myAxisY.ConstantLines.Add(_clZero);

            #endregion AxisY
        }

        private void GetProfitData()
        {
            var commandText = $@"EXEC [dbo].[sp_FundTeamStrategyProfit] ";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count > 0)
            {
                _profitData = ds.Tables[1];
            }
        }

        private void DisplayChart()
        {
            _seAccumulateProfit.Points.Clear();
            _seDayProfit.Points.Clear();

            if (_profitData == null || _profitData.Rows.Count == 0) return;

            var argument = string.Empty;
            double accumulateProfit, dayProfit;

            foreach (DataRow row in _profitData.Rows)
            {
                argument = row["TradeDate"].ToString().Trim();
                accumulateProfit = CommonHelper.StringToDouble(row["AccumulateProfit"].ToString().ToString());
                _seAccumulateProfit.Points.Add(new SeriesPoint(argument, accumulateProfit));
                dayProfit = CommonHelper.StringToDouble(row["DayProfit"].ToString().ToString());
                _seDayProfit.Points.Add(new SeriesPoint(argument, dayProfit));
            }
        }

        #endregion Utilities

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
                DXMessage.ShowError(ex.Message);
            }
        }
    }
}