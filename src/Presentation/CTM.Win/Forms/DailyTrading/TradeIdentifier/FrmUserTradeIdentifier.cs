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

        private Series _seriesDayKLine;
        private CrosshairFreePosition _crosshairFreePosition1 = new CrosshairFreePosition();

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _kLineData;
        private DataTable _tradeAvg;
        private DataTable _tradeRecords;

        private DataTable _investorRecords;
        private DataTable _investorTradeAvg;

        private DataTable _stockRecords;
        private DataTable _stockKLineData;
        private DataTable _stockTradeAvg;

        private bool _chartGenerated = false;

        private DateTime _startDate, _endDate;
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

            this.btnView.Enabled = false;
        }

        private void ChartInit()
        {
            #region Chart

            //ChartTitle chartTitle = new ChartTitle();
            ////标题内容
            //chartTitle.Text = "股票名称（股票代码） - 投资人员";
            ////字体颜色
            //chartTitle.TextColor = Color.OrangeRed;
            ////字体类型字号
            //chartTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
            ////标题对齐方式
            //chartTitle.Dock = ChartTitleDockStyle.Top;
            //chartTitle.Alignment = StringAlignment.Center;

            //chartControl1.Titles.Clear();
            //chartControl1.Titles.Add(chartTitle);

            chartControl1.BackColor = Color.Black;

            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //RuntimeHitTesting设为True时，才可从ChartHitInfo中取得SeriesPoint
            chartControl1.RuntimeHitTesting = true;

            _crosshairFreePosition1.DockTargetName = "Default Pane";
            _crosshairFreePosition1.DockCorner = DockCorner.LeftTop;
            _crosshairFreePosition1.OffsetX = 45;
            _crosshairFreePosition1.OffsetY = 12;

            chartControl1.CrosshairOptions.CommonLabelPosition = _crosshairFreePosition1;
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

        private void GirdInit()
        {
            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, rowIndicatorWidth: 35, columnPanelRowHeight: 22);
        }

        private void GetIdentifierData()
        {
            if (this.deStart.EditValue == null || this.deEnd.EditValue == null) return;

            _kLineData = null;
            _tradeAvg = null;
            _tradeRecords = null;

            var commandText = $@"EXEC [dbo].[sp_TradeIdentifierDay] @StartDate ='{_startDate}', @EndDate='{_endDate}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count == 3)
            {
                _kLineData = ds.Tables[0];
                _tradeAvg = ds.Tables[1];
                _tradeRecords = ds.Tables[2];
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

        private void DisplayChart()
        {
            _seriesDayKLine.Points.Clear();

            double low, high, open, close;
            foreach (DataRow row in _stockKLineData.Rows)
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
            myAxisX.WholeRange.SideMarginsValue = 4D;
            myAxisX.WholeRange.SetMinMaxValues(_startDate, _endDate);
            myAxisX.VisualRange.Auto = false;
            myAxisX.VisualRange.SetMinMaxValues(_endDate.AddMonths(-2), _endDate);

            AxisY myAxisY = myDiagram.AxisY;
            var currentKLineData = _stockKLineData.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate") >= _endDate.AddMonths(-2) && x.Field<DateTime>("TradeDate") <= _endDate);
            decimal minValueY = currentKLineData.Select(x => x.Field<decimal>("Low")).Min();
            decimal maxValueY = currentKLineData.Select(x => x.Field<decimal>("High")).Max();
            myAxisY.WholeRange.SetMinMaxValues(minValueY, maxValueY);
        }

        private void BindDayPositionProfit(DateTime tradeDate)
        {
            esiProfitTitle.Text = $@"{tradeDate.ToShortDateString()} - {luStock.Text} - {luInvestor.Text}";

            var investorCode = luInvestor.SelectedValue();
            var stockCode = luStock.SelectedValue();

            var commandText = $@"EXEC [dbo].[sp_InvestorStockProfitDaily] @InvestorCode = '{investorCode}', @StockCode = '{stockCode}',	@TradeDate = '{tradeDate}'";
            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count == 1)
            {
                DataRow profit = ds.Tables[0].Rows[0];

                txtVolume.Text = CommonHelper.StringToDecimal(profit["PositionVolume"].ToString()).ToString("N0");
                txtValue.Text = CommonHelper.StringToDecimal(profit["PositionValue"].ToString()).ToString("N4");
                txtProfit.Text = CommonHelper.StringToDecimal(profit["DayProfit"].ToString()).ToString("N4");
            }
        }

        private void ReDrawAxisY(ChartControl chart, RangeInfo newXRange)
        {
            var minValueX = CommonHelper.StringToDateTime(newXRange.MinValue.ToString());
            var maxValueX = CommonHelper.StringToDateTime(newXRange.MaxValue.ToString());
            var currentKLineData = _stockKLineData.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate") >= minValueX && x.Field<DateTime>("TradeDate") <= maxValueX);
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

                GirdInit();
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
                luStock.EditValue = null;
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

                _investorRecords = null;
                _investorTradeAvg = null;

                if (!string.IsNullOrEmpty(investorCode))
                {
                    luStock.EditValue = null;

                    _investorRecords = _tradeRecords.AsEnumerable().Where(x => x.Field<string>("InvestorCode").Trim() == investorCode).CopyToDataTable();
                    _investorTradeAvg = _tradeAvg.AsEnumerable().Where(x => x.Field<string>("InvestorCode").Trim() == investorCode).CopyToDataTable();
                }
                else
                {
                    _investorRecords = _tradeRecords.Copy();
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
                _stockKLineData = _kLineData.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockCode).CopyToDataTable();
                _stockTradeAvg = _investorTradeAvg.AsEnumerable().Where(x => x.Field<string>("StockCode").Trim() == stockCode).CopyToDataTable();

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
            ChartControl currentChart = sender as ChartControl;
            ChartHitInfo hitInfo = currentChart.CalcHitInfo(e.Location);

            if (hitInfo.SeriesPoint != null)
            {
                this.gridControl1.DataSource = null;
                var currentDate = CommonHelper.StringToDateTime(hitInfo.SeriesPoint.Argument).Date;
                var currentRecords = _stockRecords.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate").Date == currentDate).OrderBy(x => x.Field<string>("TradeTime"));

                this.gridControl1.DataSource = currentRecords.Count() > 0 ? currentRecords.CopyToDataTable() : null;

                BindDayPositionProfit(currentDate);
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
                if (!_chartGenerated || _stockTradeAvg == null || _stockTradeAvg.Rows.Count == 0) return;

                Graphics g = e.Graphics;
                
                Pen pArrow = new Pen(Color.White, 0.5f);
                pArrow.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pArrow.EndCap = LineCap.ArrowAnchor;

                float lineLength = 20;
                Font font = new Font("新宋体", 8, FontStyle.Regular);

                foreach (DataRow row in _stockTradeAvg.Rows)
                {
                    var tradeDate = row["TradeDate"].ToString().Trim();
                    var dealFlag = bool.Parse(row["DealFlag"].ToString().Trim());
                    var dealPrice = CommonHelper.StringToDecimal(row["AvgPrice"].ToString().Trim()).ToString("f3");
                    var dealVolume = Math.Abs(CommonHelper.StringToDecimal(row["TotalVolume"].ToString().Trim()));

                    var identifierText = (dealFlag ? "B" : "S") + ":" + dealPrice + " " + dealVolume + "股";
                    var dealPoint = (chartControl1.Diagram as XYDiagram).DiagramToPoint(tradeDate, CommonHelper.StringToDouble(dealPrice)).Point;
                    var dealPointX = dealPoint.X;
                    var dealPointY = dealPoint.Y;

                    SizeF size = g.MeasureString(identifierText, font);
                    float posX;
                    float posY;
                    if (dealFlag)
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

        private void chartControl1_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElementGroup elementGroup in e.CrosshairElementGroups)
            {
                foreach (CrosshairElement element in elementGroup.CrosshairElements)
                {
                    element.LabelElement.TextColor = Color.OrangeRed;                   
                }
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