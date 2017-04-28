using System;
using System.Data;
using System.Drawing;
using System.Linq;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.Market
{
    public partial class FrmIndexTrend5M : BaseForm
    {
        #region Fields

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private Series _seCurrent;
        private Series _seCorrected;

        private DataTable _trendData = null;

        #endregion Fields

        #region Constructors

        public FrmIndexTrend5M()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.gridView1.SetLayout(showCheckBoxRowSelect: false, showAutoFilterRow: false, showFooter: true, rowIndicatorWidth: 30);
            this.gridView1.SetColumnHeaderAppearance();
        }

        private void ChartInit()
        {
            //#region Title

            //ChartTitle chartTitle = new ChartTitle();
            ////标题内容
            //chartTitle.Text = "大盘5分钟走势预测";
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

            _seCurrent = new Series(DateTime.Now.ToShortDateString(), ViewType.Line);
            _seCurrent.ArgumentScaleType = ScaleType.Qualitative;
            _seCurrent.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            LineSeriesView myView1 = (LineSeriesView)_seCurrent.View;
            myView1.Color = Color.DeepSkyBlue;

            _seCorrected = new Series("当日修正值", ViewType.Spline);
            _seCorrected.ArgumentScaleType = ScaleType.Qualitative;
            _seCorrected.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            SplineSeriesView myView2 = (SplineSeriesView)_seCorrected.View;
            myView2.Color = Color.OrangeRed;

            this.chartControl1.Series.Add(_seCurrent);
            this.chartControl1.Series.Add(_seCorrected);

            #endregion Series

            #region XYDiagram

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;

            #endregion XYDiagram

            #region AxisX

            AxisX myAxisX = myDiagram.AxisX;

            myAxisX.Label.Font = new Font("Tahoma ", 8, FontStyle.Bold);
            myAxisX.Label.Staggered = false;
            myAxisX.Label.Angle = -90;
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

        private void TrendProcess()
        {
            LoadTrendData();

            DisplayChart();
        }

        private void LoadTrendData()
        {
            var marketName = string.Empty;

            switch (radioGroup1.SelectedIndex)
            {
                case 0:
                    marketName = "sz";
                    break;

                case 1:
                    marketName = "zx";
                    break;

                case 2:
                    marketName = "cy";
                    break;

                default:
                    break;
            }

            if (string.IsNullOrEmpty(marketName)) return;

            _trendData = null;

            var date = CommonHelper.StringToDateTime(deTrade.EditValue.ToString());

            var commandText = $@"EXEC [dbo].[sp_MTIndex5M_{marketName}] @TradeDate ='{date}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count > 0)
            {
                _trendData = ds.Tables[0];
            }

            this.gridControl1.DataSource = _trendData;
        }

        private void DisplayChart()
        {

            _seCurrent.Name = deTrade.Text;

            _seCurrent.Points.Clear();
            _seCorrected.Points.Clear();

            if (_trendData == null || _trendData.Rows.Count == 0) return;

            var argument = string.Empty;
            double currentAmount;
            double correctedAmount;
            foreach (DataRow row in _trendData.Rows)
            {
                argument = row["Ttime"].ToString().Trim();
                currentAmount = CommonHelper.StringToDouble(row["Amount"].ToString().Trim());
                correctedAmount = CommonHelper.StringToDouble(row["Amount_B"].ToString().Trim());

                _seCurrent.Points.Add(new SeriesPoint(argument, currentAmount));
                _seCorrected.Points.Add(new SeriesPoint(argument, correctedAmount));
            }

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;
            AxisX myAxisX = myDiagram.AxisX;

            AxisY myAxisY = myDiagram.AxisY;
            decimal minCurrent = _trendData.AsEnumerable().Select(x => x.Field<decimal>("Amount")).Min();
            decimal maxCurrent = _trendData.AsEnumerable().Select(x => x.Field<decimal>("Amount")).Max();
            decimal minCorrected = _trendData.AsEnumerable().Select(x => x.Field<decimal>("Amount_B")).Min();
            decimal maxCorrected = _trendData.AsEnumerable().Select(x => x.Field<decimal>("Amount_B")).Max();
            myAxisY.WholeRange.SetMinMaxValues(minCurrent > minCorrected ? minCorrected : minCurrent, maxCurrent > maxCorrected ? maxCurrent : maxCorrected);
        }

        #endregion Utilities

        #region Events

        private void FrmIndexTrend5M_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                ChartInit();

                deTrade.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
                var now = DateTime.Now.Date;
                if (now.Hour > 15 && now.Minute > 12)
                    deTrade.EditValue = now;
                else
                    deTrade.EditValue = now.AddDays(-1);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void deTrade_EditValueChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == -1)
                this.radioGroup1.SelectedIndex = 0;

            TrendProcess();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TrendProcess();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}