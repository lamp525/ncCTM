using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmUserTradeIdentifier : BaseForm
    {
  
        private Series _seriesDayMarketData;
        private CrosshairFreePosition _crosshairFreePosition1 = new CrosshairFreePosition();

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _tradeRecords = new DataTable();
        private DateTime _startDate, _endDate;
        private decimal _lowestPrice, _highestPrice;
        private Color _redColor = Color.FromArgb(204, 51, 0);


        public FrmUserTradeIdentifier()
        {
            InitializeComponent();            
        }

        private DataTable GetStockDayMarketData(string stockCode)
        {
            var commandText = $@"SELECT * FROM [FinancialCenter].[dbo].[TKLine_Today] WHERE TradeDate BETWEEN '{_startDate}' AND '{_endDate}'  AND StockCode = '{stockCode}' ORDER BY TradeDate";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return null;

            return ds.Tables[0];
        }

        private DataTable GetStockTradeRecord(string investorCode, string stockCode)
        {
            var commandText = $@"SELECT * FROM [dbo].[DailyRecord] WHERE TradeDate BETWEEN '{_startDate}' AND '{_endDate}'  AND StockCode = '{stockCode}'  AND Beneficiary = '{investorCode}'  AND DealVolume !=0 ORDER BY TradeDate, TradeTime";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return null;

            return ds.Tables[0];
        }

        private void GenerateChart()
        {
            var stockCode = "600511.SH";
            var stockDMD = GetStockDayMarketData(stockCode);

            if (stockDMD == null || stockDMD.Rows.Count == 0) return;
       
            _crosshairFreePosition1.DockTargetName = "Default Pane";
            _crosshairFreePosition1.DockCorner = DockCorner.LeftTop;
            _crosshairFreePosition1.OffsetX = 45;
            _crosshairFreePosition1.OffsetY = 12;

            chartControl1.CrosshairOptions.CommonLabelPosition = _crosshairFreePosition1;     
            chartControl1.CrosshairOptions.ShowValueLine = true;
            chartControl1.CrosshairOptions.ShowValueLabels = true;
            chartControl1.CrosshairOptions.ShowArgumentLine = true;
            chartControl1.CrosshairOptions.ShowArgumentLabels = true;

            chartControl1.BackColor = Color.Black;

            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //RuntimeHitTesting设为True时，才可从ChartHitInfo中取得SeriesPoint
            chartControl1.RuntimeHitTesting = true;

            _seriesDayMarketData = new Series("日行情", ViewType.CandleStick);
            
            _seriesDayMarketData.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.False;
            CandleStickSeriesView myView = (CandleStickSeriesView)_seriesDayMarketData.View;
            myView.Color = _redColor;
            myView.LineThickness = 1;           
            myView.LevelLineLength = 0.15;     
            myView.ReductionOptions.Level = StockLevel.Close;
            myView.ReductionOptions.Color = Color.FromArgb(102, 255, 255);
            myView.ReductionOptions.Visible = true;
          

            double low, high, open, close;

            foreach (DataRow row in stockDMD.Rows)
            {
                DateTime tradeDate = CommonHelper.StringToDateTime(row["TradeDate"].ToString().Trim()).Date;

                high = CommonHelper.StringToDouble(row["High"].ToString().Trim());
                low = CommonHelper.StringToDouble(row["Low"].ToString().Trim());
                open = CommonHelper.StringToDouble(row["Open"].ToString().Trim());
                close = CommonHelper.StringToDouble(row["Close"].ToString().Trim());
                SeriesPoint spDayMD = new SeriesPoint(tradeDate, new double[] { low, high, open, close });
                spDayMD.Color = close - open > 0 ? System.Drawing.Color.Red : System.Drawing.Color.Green;

                var currentRecords = _tradeRecords.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate").Date == tradeDate).OrderBy(x => x.Field<string>("TradeTime"));
                if (currentRecords.Count() > 0)
                {
                    var toolTipHintText = $@"{tradeDate},{tradeDate.DayOfWeek }{Environment.NewLine}";

                    foreach (DataRow record in currentRecords)
                    {
                        var dealFlag = Convert.ToBoolean(record["DealFlag"]);
                        var dealFlagText = dealFlag ? "买入" : "卖出";
                        var dealInfo = $@"{record["TradeTime"]}  {dealFlag} 数量：{record["DealVolume"]}股 价格：{record["DealPrice"]}";

                        toolTipHintText += Environment.NewLine + dealInfo;
                    }

                    spDayMD.ToolTipHint = toolTipHintText;
                }

                _seriesDayMarketData.Points.Add(spDayMD);
            }

            this.chartControl1.Series.Add(_seriesDayMarketData);


            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;
            myDiagram.DefaultPane.BackColor = Color.Black;
            myDiagram.DefaultPane.BorderColor = _redColor;
            myDiagram.EnableAxisXScrolling = true;
            myDiagram.EnableAxisXZooming = true;
            myDiagram.ZoomingOptions.AxisXMaxZoomPercent = 300; 

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
            //myAxisX.WholeRange.AutoSideMargins = false;
            //myAxisX.WholeRange.SideMarginsValue = 1D;
            myAxisX.WholeRange.SetMinMaxValues(_startDate, _endDate);
            myAxisX.VisualRange.Auto = false;
            myAxisX.VisualRange.SetMinMaxValues(_endDate.AddMonths (-2),_endDate); 

            AxisY myAxisY = myDiagram.AxisY;
            myAxisY.Alignment = AxisAlignment.Far;
            myAxisY.Color = _redColor ;
            myAxisY.Label.TextColor = _redColor;
            myAxisY.Label.TextPattern ="{ V:F2}";
            myAxisY.GridLines.Color = Color.FromArgb(165, 42, 42);
            myAxisY.GridLines.LineStyle.Thickness = 1;
            myAxisY.GridLines.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dot;
            myAxisY.Tickmarks.MinorVisible = false;
            myAxisY.WholeRange.Auto = false;
            _lowestPrice = stockDMD.AsEnumerable().Select(x => x.Field<decimal>("Low")).Min();
            _highestPrice = stockDMD.AsEnumerable().Select(x => x.Field<decimal>("High")).Max();
            myAxisY.WholeRange.SetMinMaxValues(_lowestPrice, _highestPrice );
        }

        private void DrawCustomTradeIdentifier()
        {
            var investorCode = "nctz039";
            var stockCode = "600511.SH";
            var records = GetStockTradeRecord(investorCode, stockCode);

            foreach (DataRow row in records.Rows)
            {
                var dealPrice = row["DealPrice"].ToString().Trim();
                var dealFlag = bool.Parse(row["DealFlag"].ToString().Trim());
                var dealVolume = row["DealVolume"].ToString().Trim();

                var identifierText = dealPrice + (dealFlag ? "买入" : "卖出") + dealVolume + "股";
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
     
                _endDate = DateTime.Now.Date;
                _startDate = _endDate.AddMonths(-2);

                var investorCode = "nctz039";
                var stockCode = "600511.SH";
                _tradeRecords = GetStockTradeRecord(investorCode, stockCode);

                GenerateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void chartControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //this.gridControl1.DataSource = null;

            ChartControl currentChart = sender as ChartControl;
            ChartHitInfo hitInfo = currentChart.CalcHitInfo(e.Location);

            if (hitInfo.SeriesPoint != null)
            {
                var currentDate = CommonHelper.StringToDateTime(hitInfo.SeriesPoint.Argument).Date;
                var currentRecords = _tradeRecords.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate").Date == currentDate).OrderBy(x => x.Field<string>("TradeTime"));

                this.gridControl1.DataSource = currentRecords.Count() > 0 ? currentRecords.CopyToDataTable() : null;
         
            }
        }

        private void chartControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {                
                if (_tradeRecords == null || _tradeRecords.Rows.Count == 0) return;

                Graphics g = e.Graphics;
                Pen pBuy = new Pen(Color.Green, 0.5f);
                pBuy.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pBuy.EndCap = LineCap.ArrowAnchor;

                Pen pSell = new Pen(Color.Red, 0.5f);
                pSell.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pSell.EndCap = LineCap.ArrowAnchor;
  

                foreach (DataRow row in _tradeRecords.Rows)
                {
                    var tradeDate = row["TradeDate"].ToString().Trim();
                    var dealPrice = row["DealPrice"].ToString().Trim();
                    var dealFlag = bool.Parse(row["DealFlag"].ToString().Trim());
                    var dealVolume = row["DealVolume"].ToString().Trim();

                    var identifierText = dealPrice + (dealFlag ? "买入" : "卖出") + dealVolume + "股";

                    var dealPoint = (chartControl1.Diagram as XYDiagram).DiagramToPoint(tradeDate, CommonHelper.StringToDouble(dealPrice)).Point;

                    var dealPointX = dealPoint.X;
                    var dealPointY = dealPoint.Y;
                    float lineLength = 50;

                    g.DrawLine(pBuy, dealPointX - lineLength, dealPointY, dealPointX, dealPointY);

                    Brush backBrush = Brushes.Green;
                    Brush foreBrush = Brushes.Violet;
                    Font font = new Font("新宋体", 9, FontStyle.Regular);

                    SizeF size = g.MeasureString(identifierText, font);
                    float posX = dealPointX - 50 - size.Width;
                    float posY = dealPointY - size.Height / 2;
                    g.DrawString(identifierText, font, backBrush, posX, posY);
                    g.DrawString(identifierText, font, backBrush, posX, posY);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}