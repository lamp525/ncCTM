using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmUserTradeIdentifier : BaseForm
    {
        #region Fields

        private Series _seriesDayKLine;
        private CrosshairFreePosition _crosshairFreePosition1 = new CrosshairFreePosition();
        private Color _redColor = Color.FromArgb(204, 51, 0);

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

        private IDailyRecordService _dailyRecordService;
        private IList<DailyRecord> _tradeRecords = null;
        private IList<DealAvgInfo> _dealAvg = null;
        private DataTable _KLineData = null;
        private DataTable _positionProfit = null;

        private DateTime _startDate, _endDate;
        private bool _chartGenerated = false;
        private DateTime _currentDate;
        private TradeInfo _currentTradeInfo = null;

        #endregion Fields

        #region NestedClass

        private class TradeInfo
        {
            public string InvestorCode { get; set; }

            public string InvestorName { get; set; }

            public string StockCode { get; set; }

            public string StockName { get; set; }

            public string TradeCode { get; set; }

            public string DisplayText { get; set; }
        }

        private class DealAvgInfo
        {
            public DateTime TradeDate { get; set; }

            public bool DealFlag { get; set; }

            public decimal DealPrice { get; set; }

            public decimal DealVolume { get; set; }
        }

        #endregion NestedClass

        #region Constructors

        public FrmUserTradeIdentifier(IDailyRecordService dailyRecordService)
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.deStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;

            var now = DateTime.Now.Date;
            this.deStart.EditValue = now.AddMonths(-6);
            this.deEnd.EditValue = now;
            this.btnView.Enabled = false;
            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, rowIndicatorWidth: 35, columnPanelRowHeight: 22);
            this.colDealFlag.SetDisplayFormatToBoolean("买", "卖");
            this.esiProfitTitle.Text = string.Empty;
        }

        private void ChartInit()
        {
            #region Chart

            ChartTitle chartTitle = new ChartTitle();
            //标题内容
            chartTitle.Text = "股票名称";
            //字体颜色
            chartTitle.TextColor = Color.White;
            //字体类型字号
            chartTitle.Font = new Font("新宋体", 11, FontStyle.Bold);
            //标题对齐方式
            chartTitle.Dock = ChartTitleDockStyle.Top;
            chartTitle.Alignment = StringAlignment.Near;

            chartControl1.Titles.Clear();
            chartControl1.Titles.Add(chartTitle);

            chartControl1.BackColor = Color.Black;

            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //RuntimeHitTesting设为True时，才可从ChartHitInfo中取得SeriesPoint
            chartControl1.RuntimeHitTesting = true;

            //_crosshairFreePosition1.DockTargetName = "Default Pane";
            //_crosshairFreePosition1.DockCorner = DockCorner.RightTop;
            //_crosshairFreePosition1.OffsetX = 50;
            //_crosshairFreePosition1.OffsetY = 30;

            //chartControl1.CrosshairOptions.CommonLabelPosition = _crosshairFreePosition1;
            chartControl1.CrosshairOptions.ShowValueLine = true;
            chartControl1.CrosshairOptions.ShowValueLabels = true;
            chartControl1.CrosshairOptions.ShowArgumentLine = true;
            chartControl1.CrosshairOptions.ShowArgumentLabels = true;

            #endregion Chart

            #region Series

            _seriesDayKLine = new Series("日行情", ViewType.CandleStick);

            _seriesDayKLine.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            CandleStickSeriesView myView = (CandleStickSeriesView)_seriesDayKLine.View;
            myView.Color = _redColor;
            myView.LineThickness = 1;
            myView.LevelLineLength = 0.15;
            myView.ReductionOptions.Level = StockLevel.Close;
            myView.ReductionOptions.Color = Color.FromArgb(102, 255, 255);
            myView.ReductionOptions.Visible = true;

            this.chartControl1.Series.Add(_seriesDayKLine);

            #endregion Series

            #region XYDiagram

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;
            myDiagram.DefaultPane.BackColor = Color.Black;
            myDiagram.DefaultPane.BorderColor = _redColor;
            myDiagram.EnableAxisXScrolling = true;
            myDiagram.EnableAxisXZooming = true;

            #endregion XYDiagram

            #region AxisX

            AxisX myAxisX = myDiagram.AxisX;
            myAxisX.Color = _redColor;
            myAxisX.Label.TextColor = _redColor;
            myAxisX.Label.Staggered = false;
            myAxisX.Label.Angle = -45;
            myAxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            myAxisX.Tickmarks.MinorVisible = false;
            myAxisX.DateTimeScaleOptions.WorkdaysOnly = true;
            myAxisX.DateTimeScaleOptions.ProcessMissingPoints = ProcessMissingPointsMode.Skip;
            myAxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
            myAxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
            myAxisX.WholeRange.Auto = false;

            #endregion AxisX

            #region AxisY

            AxisY myAxisY = myDiagram.AxisY;
            myAxisY.Alignment = AxisAlignment.Far;
            myAxisY.Color = _redColor;
            myAxisY.Label.TextColor = _redColor;
            myAxisY.Label.TextPattern = "{ V:F2}";
            myAxisY.GridLines.Color = Color.FromArgb(165, 42, 42);
            myAxisY.GridLines.LineStyle.Thickness = 1;
            myAxisY.GridLines.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            myAxisY.Tickmarks.MinorVisible = false;
            myAxisY.WholeRange.Auto = false;

            #endregion AxisY
        }

        private void BindTradeInfo()
        {
            if (this.deStart.EditValue == null || this.deEnd.EditValue == null) return;

            luTradeInfo.Properties.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_TITradeInfo] @StartDate ='{_startDate}', @EndDate='{_endDate}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
            {
                var source = ds.Tables[0].AsEnumerable()
                                    .Select(x => new TradeInfo
                                    {
                                        DisplayText = x.Field<string>("DisplayText").Trim(),
                                        InvestorCode = x.Field<string>("InvestorCode").Trim(),
                                        InvestorName = x.Field<string>("InvestorName").Trim(),
                                        StockCode = x.Field<string>("StockCode").Trim(),
                                        StockName = x.Field<string>("StockName").Trim(),
                                        TradeCode = x.Field<string>("TradeCode").Trim(),
                                    }).ToList();

                luTradeInfo.Initialize(source, "TradeCode", "DisplayText", enableSearch: true);
            }
        }

        private void DisplayChart()
        {
            _seriesDayKLine.Points.Clear();
            chartControl1.Titles[0].Text = luTradeInfo.Text;

            double low, high, open, close;
            foreach (DataRow row in _KLineData.Rows)
            {
                DateTime tradeDate = CommonHelper.StringToDateTime(row["TradeDate"].ToString().Trim()).Date;

                high = CommonHelper.StringToDouble(row["High"].ToString().Trim());
                low = CommonHelper.StringToDouble(row["Low"].ToString().Trim());
                open = CommonHelper.StringToDouble(row["Open"].ToString().Trim());
                close = CommonHelper.StringToDouble(row["Close"].ToString().Trim());
                SeriesPoint spDayMD = new SeriesPoint(tradeDate, new double[] { low, high, open, close });

                _seriesDayKLine.Points.Add(spDayMD);
            }

            //this.chartControl1.Series.Add(_seriesDayMarketData);

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;

            AxisX myAxisX = myDiagram.AxisX;
            myAxisX.WholeRange.AutoSideMargins = false;
            myAxisX.WholeRange.SideMarginsValue = 3D;
            myAxisX.WholeRange.SetMinMaxValues(_startDate, _endDate);
            myAxisX.VisualRange.Auto = false;
            myAxisX.VisualRange.SetMinMaxValues(_endDate.AddMonths(-3), _endDate);

            AxisY myAxisY = myDiagram.AxisY;
            var currentKLineData = _KLineData.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate") >= _endDate.AddMonths(-2) && x.Field<DateTime>("TradeDate") <= _endDate);
            decimal minValueY = currentKLineData.Select(x => x.Field<decimal>("Low")).Min();
            decimal maxValueY = currentKLineData.Select(x => x.Field<decimal>("High")).Max();
            myAxisY.WholeRange.SetMinMaxValues(minValueY, maxValueY);
        }

        private void ReDrawAxisY(ChartControl chart, RangeInfo newXRange)
        {
            var minValueX = CommonHelper.StringToDateTime(newXRange.MinValue.ToString());
            var maxValueX = CommonHelper.StringToDateTime(newXRange.MaxValue.ToString());
            var currentKLineData = _KLineData.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate") >= minValueX && x.Field<DateTime>("TradeDate") <= maxValueX);
            decimal minValueY = currentKLineData.Select(x => x.Field<decimal>("Low")).Min();
            decimal maxValueY = currentKLineData.Select(x => x.Field<decimal>("High")).Max();

            AxisY myAxisY = (chart.Diagram as XYDiagram).AxisY;
            myAxisY.WholeRange.SetMinMaxValues(minValueY, maxValueY);
        }

        #endregion Utilities

        #region Events

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                ChartInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void deStart_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _startDate = CommonHelper.StringToDateTime(deStart.EditValue.ToString());
                BindTradeInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void deEnd_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _endDate = CommonHelper.StringToDateTime(deEnd.EditValue.ToString());
                BindTradeInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luTradeInfo_EditValueChanged(object sender, EventArgs e)
        {
            this.btnView.Enabled = !string.IsNullOrEmpty(luTradeInfo.SelectedValue());
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                this._chartGenerated = false;
                this.btnView.Enabled = false;

                this.esiProfitTitle.Text = string.Empty;

                _currentTradeInfo = luTradeInfo.GetSelectedDataRow() as TradeInfo;
                if (_currentTradeInfo == null) return;

                var sqlText1 = $@"EXEC [dbo].[sp_TIKLineData] @InvestorCode = '{_currentTradeInfo.InvestorCode}', @StockCode = '{_currentTradeInfo.StockCode}',	@StartDate = '{_startDate}' ,@EndDate = '{_endDate}'";
                var dsKLine = SqlHelper.ExecuteDataset(_connString, CommandType.Text, sqlText1);

                if (dsKLine != null && dsKLine.Tables.Count == 1)
                    _KLineData = dsKLine.Tables[0];

                _tradeRecords = _dailyRecordService.GetDailyRecordsDetail(stockCode: _currentTradeInfo.StockCode, beneficiary: _currentTradeInfo.InvestorCode, tradeDateFrom: _startDate, tradeDateTo: _endDate)
                            .Where(x => x.DealVolume != 0)
                            .ToList();

                _dealAvg = _tradeRecords.GroupBy(x => new { TradeDate = x.TradeDate, DealFlag = x.DealFlag })
                    .Select(x => new DealAvgInfo
                    {
                        DealFlag = x.Key.DealFlag,
                        DealPrice = CommonHelper.SetDecimalDigits(x.Sum(y => y.DealAmount) / x.Sum(y => Math.Abs(y.DealVolume)), 3),
                        DealVolume = x.Sum(y => Math.Abs(y.DealVolume)),
                        TradeDate = x.Key.TradeDate,
                    }).ToList();

                var sqlText2 = $@"EXEC [dbo].[sp_TIPositionProfit] @InvestorCode = '{_currentTradeInfo.InvestorCode}', @StockCode = '{_currentTradeInfo.StockCode}',	@StartDate = '{_startDate}' ,@EndDate = '{_endDate}'";
                var dsProfit = SqlHelper.ExecuteDataset(_connString, CommandType.Text, sqlText2);

                if (dsProfit != null && dsProfit.Tables.Count == 1)
                    _positionProfit = dsProfit.Tables[0];

                DisplayChart();

                this._chartGenerated = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnView.Enabled = true;
            }
        }

        private void chartControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!_chartGenerated || _tradeRecords == null || !_tradeRecords.Any()) return;

                ChartControl currentChart = sender as ChartControl;
                ChartHitInfo hitInfo = currentChart.CalcHitInfo(e.Location);

                if (hitInfo.InDiagram)
                {
                    DiagramCoordinates dc = (currentChart.Diagram as XYDiagram).PointToDiagram(e.Location);

                    if (_currentDate != dc.DateTimeArgument.Date)
                    {
                        _currentDate = dc.DateTimeArgument.Date;
                        var currentDateRecords = _tradeRecords.Where(x => x.TradeDate == _currentDate).OrderBy(x => x.TradeTime).ToList();
                        this.gridControl1.DataSource = currentDateRecords;

                        esiProfitTitle.Text = $@"{_currentDate.ToShortDateString()} - {_currentTradeInfo.DisplayText}";

                        DataRow currentProfit = _positionProfit.AsEnumerable().SingleOrDefault(x => x.Field<DateTime>("TradeDate") == _currentDate);
                        if (currentProfit != null)
                        {
                            txtVolume.Text = CommonHelper.StringToDecimal(currentProfit["PositionVolume"].ToString()).ToString("N0");
                            txtValue.Text = CommonHelper.StringToDecimal(currentProfit["PositionValue"].ToString()).ToString("N4");
                            txtProfit.Text = CommonHelper.StringToDecimal(currentProfit["DayProfit"].ToString()).ToString("N4");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chartControl1_Scroll(object sender, ChartScrollEventArgs e)
        {
            ChartControl chart = sender as ChartControl;

            ReDrawAxisY(chart, e.NewXRange);
        }

        private void chartControl1_Zoom(object sender, ChartZoomEventArgs e)
        {
            ChartControl chart = sender as ChartControl;

            ReDrawAxisY(chart, e.NewXRange);
        }

        private void chartControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!_chartGenerated || _dealAvg == null || !_dealAvg.Any()) return;

                Graphics g = e.Graphics;

                Pen pArrow = new Pen(Color.White, 0.5f);
                pArrow.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pArrow.EndCap = LineCap.ArrowAnchor;

                float lineLength = 30;
                Font font = new Font("新宋体", 9, FontStyle.Regular);

                foreach (var item in _dealAvg)
                {
                    var identifierText = (item.DealFlag ? "B" : "S") + ":" + item.DealPrice + " " + item.DealVolume + "股";
                    var dealPoint = (chartControl1.Diagram as XYDiagram).DiagramToPoint(item.TradeDate, (double)item.DealPrice).Point;
                    var dealPointX = dealPoint.X;
                    var dealPointY = dealPoint.Y;

                    SizeF size = g.MeasureString(identifierText, font);
                    float posX;
                    float posY;
                    if (item.DealFlag)
                    {
                        pArrow.Color = Color.AliceBlue;
                        g.DrawLine(pArrow, dealPointX - lineLength, dealPointY, dealPointX, dealPointY);

                        posX = dealPointX - lineLength - size.Width;
                        posY = dealPointY - size.Height / 2;
                        g.DrawString(identifierText, font, Brushes.AliceBlue, posX, posY);
                    }
                    else
                    {
                        pArrow.Color = Color.Orange;
                        g.DrawLine(pArrow, dealPointX + lineLength, dealPointY, dealPointX, dealPointY);

                        posX = dealPointX + lineLength;
                        posY = dealPointY - size.Height / 2;
                        g.DrawString(identifierText, font, Brushes.Orange, posX, posY);
                    }
                }
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