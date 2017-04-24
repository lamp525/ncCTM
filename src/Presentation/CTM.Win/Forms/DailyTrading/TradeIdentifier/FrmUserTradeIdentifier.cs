using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmUserTradeIdentifier : BaseForm
    {
        #region Fields

        private Series _seriesDayMarketData;
        private CrosshairFreePosition _crosshairFreePosition1 = new CrosshairFreePosition();

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _kLineDate;
        private DataTable _tradeAvg;
        private DataTable _profitDetail;
        private DataTable _tradeRecords;

        private DataTable _investorRecords;
        private DataTable _investorTradeAvg;
        private DataTable _investorProfitDetail;

        private DataTable _stockRecords;
        private DataTable _stockKLineDate;
        private DataTable _stockTradeAvg;
        private DataTable _stockProfitDetail;

        private bool _chartGenerated = false;

        private DateTime _startDate, _endDate;
        private decimal _lowestPrice, _highestPrice;
        private Color _redColor = Color.FromArgb(204, 51, 0);

        #endregion Fields

        #region Constructors

        public FrmUserTradeIdentifier()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.deStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;

            var now = DateTime.Now.Date;
            this.deStart.EditValue = now.AddMonths(-3);
            this.deEnd.EditValue = now;
        }

        private void GetIdentifierData()
        {
            if (this.deStart.EditValue == null || this.deEnd.EditValue == null) return;

            _kLineDate = null;
            _tradeAvg = null;
            _profitDetail = null;
            _tradeRecords = null;

            var commandText = $@"EXEC [dbo].[sp_TradeIdentifierDay] @StartDate ='{_startDate}', @EndDate='{_endDate}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count == 4)
            {
                _kLineDate = ds.Tables[0];
                _tradeAvg = ds.Tables[1];
                _profitDetail = ds.Tables[2];
                _tradeRecords = ds.Tables[3];
            }
        }

        private void BindInvestor()
        {
            if (_tradeRecords == null || this._tradeRecords.Rows.Count == 0) return;

            var investorInfo = _tradeRecords.AsEnumerable()
                .GroupBy(x => new { Code = x.Field<string>("InvestorCode").Trim(), Name = x.Field<string>("InvestorName").Trim() })
                .Select(x => new UserInfo
                {
                    Code = x.Key.Code,
                    Name = x.Key.Name
                }).ToList();

            var allSelect = new UserInfo
            {
                Code = string.Empty,
                Name = " 全部 "
            };

            investorInfo.Add(allSelect);
            investorInfo = investorInfo.OrderBy(x => x.Name).ToList();

            luInvestor.Initialize(investorInfo, "Code", "Name", enableSearch: true);

            if (investorInfo.Exists(x => x.Code == LoginInfo.CurrentUser.UserCode))
                this.luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
            else
                this.luInvestor.EditValue = string.Empty;
        }

        private void BindStockInfo()
        {
            if (_investorRecords == null || this._investorRecords.Rows.Count == 0) return;

            var stockInfo = _investorRecords.AsEnumerable()
                .GroupBy(x => new { Code = x.Field<string>("StockCode").Trim(), Name = x.Field<string>("StockName").Trim() })
                .Select(x => new StockInfoModel
                {
                    FullCode = x.Key.Code,
                    Name = x.Key.Name,
                    DisplayMember = x.Key.Code + " - " + x.Key.Name
                }).OrderBy(x => x.FullCode).ToList();
            luStock.Initialize(stockInfo, "FullCode", "DisplayMember", enableSearch: true);
        }

        private void GenerateChart()
        {
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

            foreach (DataRow row in _kLineDate.Rows)
            {
                DateTime tradeDate = CommonHelper.StringToDateTime(row["TradeDate"].ToString().Trim()).Date;

                high = CommonHelper.StringToDouble(row["High"].ToString().Trim());
                low = CommonHelper.StringToDouble(row["Low"].ToString().Trim());
                open = CommonHelper.StringToDouble(row["Open"].ToString().Trim());
                close = CommonHelper.StringToDouble(row["Close"].ToString().Trim());
                SeriesPoint spDayMD = new SeriesPoint(tradeDate, new double[] { low, high, open, close });
                spDayMD.Color = close - open > 0 ? System.Drawing.Color.Red : System.Drawing.Color.Green;

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
            myAxisX.VisualRange.SetMinMaxValues(_endDate.AddMonths(-2), _endDate);

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
            _lowestPrice = _stockRecords.AsEnumerable().Select(x => x.Field<decimal>("Low")).Min();
            _highestPrice = _stockRecords.AsEnumerable().Select(x => x.Field<decimal>("High")).Max();
            myAxisY.WholeRange.SetMinMaxValues(_lowestPrice, _highestPrice);
        }

        #endregion Utilities

        #region Events

        private void Form2_Load(object sender, EventArgs e)
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

        private void deStart_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _startDate = CommonHelper.StringToDateTime(deStart.EditValue.ToString());
                GetIdentifierData();
                BindInvestor();
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
                GetIdentifierData();
                BindInvestor();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luInvestor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var investorCode = luInvestor.SelectedValue();

                if (!string.IsNullOrEmpty(investorCode))
                {
                    _investorRecords = _tradeRecords.AsEnumerable().Where(x => x.Field<string>("InvestorCode").Trim() == investorCode).CopyToDataTable();
                    _investorProfitDetail = _profitDetail.AsEnumerable().Where(x => x.Field<string>("InvestorCode").Trim() == investorCode).CopyToDataTable();
                    _investorTradeAvg = _tradeAvg.AsEnumerable().Where(x => x.Field<string>("InvestorCode").Trim() == investorCode).CopyToDataTable();
                }
                else
                {
                    _investorRecords = _tradeRecords.Copy();
                    _investorProfitDetail = _profitDetail.Copy();
                    _investorTradeAvg = _tradeAvg.Copy();
                }

                BindStockInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luStock_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(luStock.SelectedValue()))
                {
                    this.btnView.Enabled = false;
                }
                else
                {
                    this.btnView.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                this._chartGenerated = false;
                this.btnView.Enabled = false;

                var stockCode = luStock.SelectedValue();
                _stockRecords = _investorRecords.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockCode).CopyToDataTable();
                _stockKLineDate = _kLineDate.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockCode).CopyToDataTable();
                _stockProfitDetail = _investorProfitDetail.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockCode).CopyToDataTable();
                _stockTradeAvg = _investorTradeAvg.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockCode).CopyToDataTable();

                GenerateChart();

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
                if (!_chartGenerated || _stockTradeAvg == null || _stockTradeAvg.Rows.Count == 0) return;

                Graphics g = e.Graphics;
                Pen pBuy = new Pen(Color.Green, 0.5f);
                pBuy.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pBuy.EndCap = LineCap.ArrowAnchor;

                Pen pSell = new Pen(Color.Red, 0.5f);
                pSell.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pSell.EndCap = LineCap.ArrowAnchor;

                foreach (DataRow row in _stockTradeAvg.Rows)
                {
                    var tradeDate = row["TradeDate"].ToString().Trim();
                    var dealFlag = bool.Parse(row["DealFlag"].ToString().Trim());
                    var dealPrice = row["AvgPrice"].ToString().Trim();
                    var dealVolume = row["TotalVolume"].ToString().Trim();

                    var identifierText = (dealFlag ? "B" : "S") + ":" + dealPrice + dealVolume + "股";

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
                    //g.DrawString(identifierText, font, foreBrush, posX, posY);
                    g.DrawString(identifierText, font, backBrush, posX, posY);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Events
    }
}