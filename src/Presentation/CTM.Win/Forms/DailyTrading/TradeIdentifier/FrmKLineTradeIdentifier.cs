using CTM.Core;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmKLineTradeIdentifier : BaseForm
    {
        #region Fields

        private Series _seriesDayKLine;
        private CrosshairFreePosition _crosshairFreePosition1 = new CrosshairFreePosition();
        private Color _redColor = Color.FromArgb(204, 51, 0);

        private IDailyRecordService _dailyRecordService;
        private IList<DailyRecord> _tradeRecords = null;
        private IList<DealAvgInfo> _dealAvg = null;
        private DataTable _KLineData = null;
        private DataTable _positionProfit = null;

        private DateTime _startDate, _endDate, _currentDate;
        private bool _chartGenerated = false;    
        private IList<TradeInfoModel> _tradeInfoList = null;
        private TradeInfoModel _tradeInfo = null;

        #endregion Fields

        #region NestedClass

        private class DealAvgInfo
        {
            public DateTime TradeDate { get; set; }

            public bool DealFlag { get; set; }

            public decimal DealPrice { get; set; }

            public decimal DealVolume { get; set; }
        }

        #endregion NestedClass

        #region Constructors

        public FrmKLineTradeIdentifier(IDailyRecordService dailyRecordService)
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            ChartInit();

            this.deStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;

            var now = DateTime.Now.Date;
            this.deStart.EditValue = new DateTime(now.Year -1, 1, 1);
            this.deEnd.EditValue = now;
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
            chartControl1.Cursor = Cursors.Default;

            #endregion Chart

            #region Series

            _seriesDayKLine = new Series("日行情", ViewType.CandleStick);
            _seriesDayKLine.ArgumentScaleType = ScaleType.Qualitative;

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
            myDiagram.DefaultPane.ScrollBarOptions.BackColor = Color.Black;
            myDiagram.DefaultPane.ScrollBarOptions.BorderColor = _redColor;
            myDiagram.DefaultPane.ScrollBarOptions.BarColor = _redColor;
            myDiagram.EnableAxisXScrolling = true;
            myDiagram.EnableAxisXZooming = true;

            #endregion XYDiagram

            #region AxisX

            AxisX myAxisX = myDiagram.AxisX;
            myAxisX.Color = _redColor;
            myAxisX.Label.TextColor = _redColor;
            myAxisX.Label.Staggered = false;
            //myAxisX.Label.TextPattern = "{A:MM/dd}";
            myAxisX.Label.Angle = -60;
            myAxisX.Label.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            myAxisX.Tickmarks.MinorVisible = false;
            //myAxisX.DateTimeScaleOptions.WorkdaysOnly = true;
            //myAxisX.DateTimeScaleOptions.ProcessMissingPoints = ProcessMissingPointsMode.Skip;
            //myAxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
            //myAxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
            myAxisX.WholeRange.Auto = false;

            #endregion AxisX

            #region AxisY

            AxisY myAxisY = myDiagram.AxisY;
            myAxisY.Alignment = AxisAlignment.Far;
            myAxisY.Color = _redColor;
            myAxisY.Label.TextColor = _redColor;
            myAxisY.Label.TextPattern = "{V:F2}";
            myAxisY.GridLines.Color = Color.FromArgb(165, 42, 42);
            myAxisY.GridLines.LineStyle.Thickness = 1;
            myAxisY.GridLines.LineStyle.DashStyle = DashStyle.Dot;
            myAxisY.Tickmarks.MinorVisible = false;
            myAxisY.WholeRange.Auto = false;

            #endregion AxisY
        }

        private void BindTradeInfo()
        {
            if (this.deStart.EditValue == null || this.deEnd.EditValue == null) return;

            luInvestor.Properties.DataSource = null;
            luStock.Properties.DataSource = null;

            var commandText = $@"EXEC [dbo].[sp_TITradeInfo] @StartDate ='{_startDate}', @EndDate='{_endDate}'";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, commandText);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                _tradeInfoList = ds.Tables[0].AsEnumerable()
                                    .Select(x => new TradeInfoModel
                                    {
                                        DisplayText = x.Field<string>("DisplayText").Trim(),
                                        InvestorCode = x.Field<string>("InvestorCode").Trim(),
                                        InvestorName = x.Field<string>("InvestorName").Trim(),
                                        StockCode = x.Field<string>("StockCode").Trim(),
                                        StockName = x.Field<string>("StockName").Trim(),
                                        TradeCode = x.Field<string>("TradeCode").Trim(),
                                    }).ToList();

                var investorInfo = ds.Tables[1].AsEnumerable()
                                                .Select(x => new UserInfo
                                                {
                                                    Code = x.Field<string>("InvestorCode").Trim(),
                                                    Name = x.Field<string>("InvestorName").Trim(),
                                                }).ToList();

                var stockInfo = ds.Tables[2].AsEnumerable()
                                .Select(x => new StockInfoModel
                                {
                                    FullCode = x.Field<string>("StockCode").Trim(),
                                    Name = x.Field<string>("StockName").Trim(),
                                    DisplayMember = x.Field<string>("StockCode").Trim() + " - " + x.Field<string>("StockName").Trim(),
                                }).ToList();

                if (!LoginInfo.CurrentUser.IsAdmin)
                {
                    luInvestor.Properties.ReadOnly = true;
                    luStock.Properties.ReadOnly = true;
                    _tradeInfoList = _tradeInfoList.Where(x => x.InvestorCode == LoginInfo.CurrentUser.UserCode).ToList();
                    investorInfo = investorInfo.Where(x => x.Code == LoginInfo.CurrentUser.UserCode).ToList();
                }

                luInvestor.Initialize(investorInfo, "Code", "Name", enableSearch: true);
                luInvestor.EditValue = LoginInfo.CurrentUser.UserCode;
                luStock.Initialize(stockInfo, "FullCode", "DisplayMember", enableSearch: true);
                //luTradeInfo.Initialize(_tradeInfoList, "TradeCode", "DisplayText", enableSearch: true);
            }
        }

        private void TradeInfoFilter()
        {
            try
            {
                if (this.luInvestor.EditValue == null && this.luStock.EditValue == null) return;

                var investorCode = luInvestor.SelectedValue();
                var stockCode = luStock.SelectedValue();

                luTradeInfo.Properties.DataSource = null;

                var source = _tradeInfoList;
                if (!string.IsNullOrEmpty(investorCode))
                    source = _tradeInfoList.Where(x => x.InvestorCode == investorCode).ToList();

                if (!string.IsNullOrEmpty(stockCode))
                    source = source.Where(x => x.StockCode == stockCode).ToList();

                luTradeInfo.Initialize(source, "TradeCode", "DisplayText", enableSearch: true);

                if (source.Any())
                    luTradeInfo.EditValue = source.First().TradeCode;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void DisplayChart(DateTime startDate, DateTime endDate)
        {
            chartControl1.Cursor = Cursors.Default;

            _seriesDayKLine.Points.Clear();
            chartControl1.Titles[0].Text = "";

            string argument = string.Empty;
            double low, high, open, close;
            foreach (DataRow row in _KLineData.Rows)
            {
                argument = CommonHelper.StringToDateTime(row["TradeDate"].ToString()).ToShortDateString();

                high = CommonHelper.StringToDouble(row["High"].ToString().Trim());
                low = CommonHelper.StringToDouble(row["Low"].ToString().Trim());
                open = CommonHelper.StringToDouble(row["Open"].ToString().Trim());
                close = CommonHelper.StringToDouble(row["Close"].ToString().Trim());
                SeriesPoint spDayMD = new SeriesPoint(argument, new double[] { low, high, open, close });

                _seriesDayKLine.Points.Add(spDayMD);
            }

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;

            AxisX myAxisX = myDiagram.AxisX;
            myAxisX.WholeRange.AutoSideMargins = false;
            myAxisX.WholeRange.SideMarginsValue = 0.8D;
            myAxisX.WholeRange.SetMinMaxValues(startDate.ToShortDateString(), endDate.ToShortDateString());
            myAxisX.VisualRange.Auto = false;
            myAxisX.VisualRange.SetMinMaxValues(endDate.AddMonths(-3).ToShortDateString(), endDate.ToShortDateString());

            AxisY myAxisY = myDiagram.AxisY;                        
            decimal minValueY = _KLineData.AsEnumerable().Select(x => x.Field<decimal>("Low")).Min();
            decimal maxValueY = _KLineData.AsEnumerable().Select(x => x.Field<decimal>("High")).Max();
            myAxisY.WholeRange.SetMinMaxValues(minValueY - (maxValueY - minValueY) / 10, maxValueY);
            myAxisY.VisualRange.Auto = false;
            var currentKLineData = _KLineData.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate") >= endDate.AddMonths(-3) && x.Field<DateTime>("TradeDate") <= endDate);
            if (currentKLineData.Any())
            {
                decimal curMinValueY = currentKLineData.Select(x => x.Field<decimal>("Low")).Min();
                decimal curMaxValueY = currentKLineData.Select(x => x.Field<decimal>("High")).Max();
                myAxisY.VisualRange.SetMinMaxValues(curMinValueY - (curMaxValueY - curMinValueY) / 10, curMaxValueY);
            }

        }

        private void ReDrawAxisY(ChartControl chart, RangeInfo newXRange)
        {
            try
            {
                if (string.IsNullOrEmpty(newXRange.MinValue.ToString()) || string.IsNullOrEmpty(newXRange.MaxValue.ToString())) return;

                var minValueX = CommonHelper.StringToDateTime(newXRange.MinValue.ToString());
                var maxValueX = CommonHelper.StringToDateTime(newXRange.MaxValue.ToString());
                var currentKLineData = _KLineData.AsEnumerable().Where(x => x.Field<DateTime>("TradeDate") >= minValueX && x.Field<DateTime>("TradeDate") <= maxValueX);

                if (currentKLineData.Any())
                {
                    decimal minValueY = currentKLineData.Select(x => x.Field<decimal>("Low")).Min();
                    decimal maxValueY = currentKLineData.Select(x => x.Field<decimal>("High")).Max();

                    AxisY myAxisY = (chart.Diagram as XYDiagram).AxisY;
                    myAxisY.VisualRange.SetMinMaxValues(minValueY - (maxValueY - minValueY) / 10, maxValueY);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void ShowTimeSharingForm(DateTime tradeDate, TradeInfoModel tradeInfo)
        {
            var dialog = this.CreateDialog<FrmTimeSharingTradeIdentifier>(borderStyle: FormBorderStyle.Sizable, windowState: FormWindowState.Normal);
            dialog.Text = "分时交易标识";
            dialog.TradeDate = tradeDate.Date;
            dialog.TradeInfo = tradeInfo;
            dialog.Show();
        }

        #endregion Utilities

        #region Events

        private void FrmKLineTradeIdentifier_Load(object sender, EventArgs e)
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

        private void luInvestor_EditValueChanged(object sender, EventArgs e)
        {
            TradeInfoFilter();
        }

        private void luStock_EditValueChanged(object sender, EventArgs e)
        {
            TradeInfoFilter();
        }

        private void luTradeInfo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this._chartGenerated = false;

                chartControl1.Titles[0].Text = luTradeInfo.Text;

                this.esiProfitTitle.Text = string.Empty;

                _tradeInfo = luTradeInfo.GetSelectedDataRow() as TradeInfoModel;
                if (_tradeInfo == null) return;

                var sqlText1 = $@"EXEC [dbo].[sp_TIKLineData] @InvestorCode = '{_tradeInfo.InvestorCode}', @StockCode = '{_tradeInfo.StockCode}',	@StartDate = '{_startDate}' ,@EndDate = '{_endDate}'";
                var dsKLine = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText1);

                if (dsKLine == null || dsKLine.Tables.Count == 0 || dsKLine.Tables[0].Rows.Count ==0) return;

                _KLineData = dsKLine.Tables[0];
                var startDate = _KLineData.AsEnumerable().Select(x => x.Field<DateTime>("TradeDate")).Min();
                var endDate = _KLineData.AsEnumerable().Select(x => x.Field<DateTime>("TradeDate")).Max();

                _tradeRecords = _dailyRecordService.GetDailyRecordsDetail(stockCode: _tradeInfo.StockCode, beneficiary: _tradeInfo.InvestorCode, tradeDateFrom: startDate, tradeDateTo: endDate)
                            .Where(x => x.DealVolume != 0)
                            .OrderBy(x => x.BeneficiaryName).ThenBy(x => x.TradeDate).ThenBy(x => x.TradeTime).ToList();

                _dealAvg = _tradeRecords.GroupBy(x => new { TradeDate = x.TradeDate, DealFlag = x.DealFlag })
                    .Select(x => new DealAvgInfo
                    {
                        DealFlag = x.Key.DealFlag,
                        DealPrice = CommonHelper.SetDecimalDigits(x.Sum(y => y.DealAmount) / x.Sum(y => Math.Abs(y.DealVolume)), 3),
                        DealVolume = x.Sum(y => Math.Abs(y.DealVolume)),
                        TradeDate = x.Key.TradeDate,
                    }).ToList();

                var sqlText2 = $@"EXEC [dbo].[sp_TIPositionProfit] @InvestorCode = '{_tradeInfo.InvestorCode}', @StockCode = '{_tradeInfo.StockCode}',	@StartDate = '{startDate}' ,@EndDate = '{endDate}'";
                var dsProfit = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText2);

                if (dsProfit != null && dsProfit.Tables.Count == 1)
                    _positionProfit = dsProfit.Tables[0];

                DisplayChart(startDate,endDate);

                this._chartGenerated = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chartControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                chartControl1.Cursor = Cursors.Default;

                if (!_chartGenerated || _tradeRecords == null || !_tradeRecords.Any()) return;

                ChartHitInfo hitInfo = chartControl1.CalcHitInfo(e.Location);

                if (hitInfo.InDiagram)
                {
                    DiagramCoordinates dc = (chartControl1.Diagram as XYDiagram).PointToDiagram(e.Location);

                    if (!dc.IsEmpty && _currentDate.ToShortDateString() !=   dc.QualitativeArgument)
                    {
                        _currentDate = CommonHelper.StringToDateTime( dc.QualitativeArgument);
                        var currentDateRecords = _tradeRecords.Where(x => x.TradeDate == _currentDate).ToList();
                        this.gridControl1.DataSource = currentDateRecords;

                        esiProfitTitle.Text = $@"{_currentDate.ToShortDateString()} - {_tradeInfo.DisplayText}";

                        DataRow currentProfit = _positionProfit.AsEnumerable().SingleOrDefault(x => x.Field<DateTime>("TradeDate") == _currentDate);
                        if (currentProfit != null)
                        {
                            txtVolume.Text = CommonHelper.StringToDecimal(currentProfit["PositionVolume"].ToString()).ToString("N0");
                            txtValue.Text = CommonHelper.StringToDecimal(currentProfit["PositionValue"].ToString()).ToString("N4");
                            txtProfit.Text = CommonHelper.StringToDecimal(currentProfit["DayProfit"].ToString()).ToString("N4");
                        }
                        else
                        {
                            txtVolume.Text = string.Empty;
                            txtValue.Text = string.Empty;
                            txtProfit.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chartControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ChartHitInfo hitInfo = chartControl1.CalcHitInfo(e.Location);

                    if (hitInfo.InDiagram)
                    {
                        DiagramCoordinates dc = (chartControl1.Diagram as XYDiagram).PointToDiagram(e.Location);
                        if (!dc.IsEmpty)
                        {
                            ShowTimeSharingForm(dc.DateTimeArgument, _tradeInfo);
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

                Font font = new Font("新宋体", 9, FontStyle.Regular);

                float foldDX = 15f;
                float foldDY = 30f;
                float straightDX = 15;
                PointF targetPoint = new PointF();
                PointF textStartPoint = new PointF();

                bool upDeviant = false;
                bool downDeviant = false;
                float deviant = 20f;

                foreach (var item in _dealAvg)
                {
                    var identifierText = (item.DealFlag ? "B" : "S") + ":" + item.DealPrice.ToString("F2") + " " + item.DealVolume.ToString("N0");
                    var dealPoint = (chartControl1.Diagram as XYDiagram).DiagramToPoint(item.TradeDate.ToShortDateString(), (double)item.DealPrice).Point;
                    targetPoint.X = dealPoint.X;
                    targetPoint.Y = dealPoint.Y;

                    SizeF size = g.MeasureString(identifierText, font);

                    if (item.DealFlag)
                    {
                        foldDY += upDeviant ? deviant : -deviant;
                        pArrow.Color = Color.OrangeRed;
                        g.DrawCustomFlodLineWithArrow(pArrow, targetPoint, -foldDX, foldDY, -straightDX);
                        textStartPoint.X = targetPoint.X - foldDX - straightDX - size.Width;
                        textStartPoint.Y = targetPoint.Y + foldDY - size.Height / 2;
                        g.DrawString(identifierText, font, Brushes.OrangeRed, textStartPoint);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              

                        upDeviant = !upDeviant;
                    }
                    else
                    {
                        foldDY -= downDeviant ? deviant : -deviant;
                        pArrow.Color = Color.Green;
                        g.DrawCustomFlodLineWithArrow(pArrow, targetPoint, foldDX, -foldDY, straightDX);
                        textStartPoint.X = targetPoint.X + foldDX + straightDX;
                        textStartPoint.Y = targetPoint.Y - foldDY - size.Height / 2;
                        g.DrawString(identifierText, font, Brushes.Green, textStartPoint);

                        downDeviant = !downDeviant;
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