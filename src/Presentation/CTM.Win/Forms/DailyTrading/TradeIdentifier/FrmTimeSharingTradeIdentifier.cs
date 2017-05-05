using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CTM.Core.Domain.TradeRecord;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.TradeRecord;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmTimeSharingTradeIdentifier : BaseForm
    {
        #region Fields

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private IDailyRecordService _dailyRecordService;
        private IList<DailyRecord> _tradeRecords = null;
        private DataTable _timeSharingData;
        private double _preClose;
        private Series _sePrice;
        private Series _seVolume;
        private Series _seAvgPrice;

        private string[] _visibleAxisXLableText = new string[] { "09:15", "09:30", "10:00", "10:30", "11:00", "11:30", "13:30", "14:00", "14:30", "15:00" };

        private bool _chartGenerated = false;

        #endregion Fields

        #region Constructors

        public FrmTimeSharingTradeIdentifier(IDailyRecordService dailyRecordService)
        {
            InitializeComponent();

            this._dailyRecordService = dailyRecordService;

            _sePrice = chartControl1.Series[0];
            _seVolume = chartControl1.Series[1];
            _seAvgPrice = chartControl1.Series[2];
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
        }

        private void ChartInit()
        {
            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;
            AxisX myAxisX = myDiagram.AxisX;

            foreach (ConstantLine cLine in myAxisX.ConstantLines)
            {
                cLine.Name = string.Empty;
            }
        }

        private void DisplayChart()
        {
            _sePrice.Points.Clear();
            _seVolume.Points.Clear();
            _seAvgPrice.Points.Clear();

            foreach (DataRow row in _timeSharingData.Rows)
            {
                //var tradeTime = Regex.Replace(row["TradeTime"].ToString().Trim(), "[:]", "");
                var tradeTime = row["TradeTime"].ToString().Trim().Substring(0, 5);

                var closePrice = CommonHelper.StringToDouble(row["Close"].ToString().Trim());
                var avgPrice = CommonHelper.StringToDouble(row["AvgPrice"].ToString().Trim());
                var volume = CommonHelper.StringToDouble(row["Volume"].ToString().Trim());

                SeriesPoint spPrice = new SeriesPoint(tradeTime, closePrice);
                _sePrice.Points.Add(spPrice);
                SeriesPoint spVolume = new SeriesPoint(tradeTime, volume);
                _seVolume.Points.Add(spVolume);

                if (string.Compare(tradeTime, "09:30") >= 0)
                {
                    SeriesPoint spAvgPrice = new SeriesPoint(tradeTime, avgPrice);
                    _seAvgPrice.Points.Add(spAvgPrice);
                }
            }

            XYDiagram myDiagram = chartControl1.Diagram as XYDiagram;
            AxisX myAxisX = myDiagram.AxisX;

            AxisY myAxisY = myDiagram.AxisY;
            double minClose = (double)_timeSharingData.AsEnumerable().Select(x => x.Field<decimal>("Close")).Min();
            double maxClose = (double)_timeSharingData.AsEnumerable().Select(x => x.Field<decimal>("Close")).Max();
            double maxDiff = Math.Abs(minClose - _preClose) > Math.Abs(maxClose - _preClose) ? Math.Abs(minClose - _preClose) : Math.Abs(maxClose - _preClose);

            double minValueY = _preClose - maxDiff;
            double maxValueY = _preClose + maxDiff;
            myAxisY.WholeRange.Auto = false;
            myAxisY.WholeRange.SetMinMaxValues(minValueY, maxValueY);
        }

        #endregion Utilities

        #region Events

        private void FrmTimeSharingTradeIdentifier_Load(object sender, EventArgs e)
        {
            try
            {
                _chartGenerated = false;

                FormInit();

                ChartInit();

                _tradeRecords = _dailyRecordService.GetDailyRecordsDetail(stockCode: "000839.SZ", tradeDateFrom: CommonHelper.StringToDateTime("2017/05/03"), tradeDateTo: CommonHelper.StringToDateTime("2017/05/03"))
                        .Where(x => x.DealVolume != 0)
                        .OrderBy(x => x.TradeTime).ToList();

                var commandText1 = $@"SELECT * FROM TKLineToday WHERE  TradeDate = '2017/05/02' AND StockCode = '000839.SZ' ";
                var ds1 = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText1);
                _preClose = CommonHelper.StringToDouble(ds1.Tables[0].Rows[0]["Close"].ToString().Trim());

                var commandText2 = $@"SELECT *	FROM [FinancialCenter].[dbo].[TKLine_1Min] WHERE TradeDate = '{"2017/05/03"}' AND StockCode = '{"000839.SZ"}' ORDER BY TradeTime";
                var ds2 = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText2);

                if (ds2 == null || ds2.Tables.Count == 0) return;

                _timeSharingData = ds2.Tables[0];

                DisplayChart();

                _chartGenerated = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chartControl1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            AxisBase axis = e.Item.Axis;

            if (axis.Tag == null) return;

            var tag = axis.Tag.ToString();

            switch (tag)
            {
                case "x":
                    string valueX = e.Item.AxisValue.ToString().Trim();
                    if (!_visibleAxisXLableText.Contains(valueX))
                    {
                        e.Item.Text = string.Empty;
                    }
                    break;

                case "y":
                    double valueY = CommonHelper.StringToDouble(e.Item.AxisValue.ToString());
                    if (valueY == _preClose)
                        e.Item.TextColor = Color.White;
                    else if (valueY < _preClose)
                        e.Item.TextColor = Color.Green;
                    else if (valueY > _preClose)
                        e.Item.TextColor = Color.FromArgb(204, 51, 0);
                    break;

                case "y1":
                    break;

                case "y2":
                default:
                    break;
            }
        }

        private void chartControl1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (!_chartGenerated || _tradeRecords == null || !_tradeRecords.Any()) return;

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

                var recordGroupByTimeAndFlag = _tradeRecords.GroupBy(x => new { TradeTime = x.TradeTime.Trim().Substring(0, 5), DealFlag = x.DealFlag });
                foreach (var group in recordGroupByTimeAndFlag)
                {
                    bool dealFlag = group.Key.DealFlag;

                    StringBuilder identifierText = new StringBuilder();
                    identifierText.Append((dealFlag ? "B" : "S") + ":");
                    foreach (var item in group)
                    {
                        identifierText.Append(item.DealPrice.ToString ("F2") );
                        identifierText.Append(" ");
                        identifierText.Append(Math.Abs(item.DealVolume).ToString("N0"));
                        identifierText.AppendLine();
                        identifierText.Append("  ");
                    }

                    identifierText.Remove(identifierText.Length - 2, 2);

                    string tradeTime = group.Key.TradeTime;
                    double dealPrice = (double)group.First().DealPrice;
                    Point dealPoint = (chartControl1.Diagram as XYDiagram).DiagramToPoint(tradeTime, dealPrice).Point;
                    targetPoint.X = dealPoint.X;
                    targetPoint.Y = dealPoint.Y;

                    SizeF size = g.MeasureString(identifierText.ToString(), font);

                    if (dealFlag)
                    {
                        foldDY += upDeviant ? deviant : -deviant;
                        pArrow.Color = Color.OrangeRed;
                        g.DrawCustomFlodLineWithArrow(pArrow, targetPoint, -foldDX, foldDY, -straightDX);
                        textStartPoint.X = targetPoint.X - foldDX - straightDX - size.Width;
                        textStartPoint.Y = targetPoint.Y + foldDY - size.Height / 2;
                        g.DrawString(identifierText.ToString(), font, Brushes.OrangeRed, textStartPoint);

                        upDeviant = !upDeviant;
                    }
                    else
                    {
                        foldDY -= downDeviant ? deviant : -deviant;
                        pArrow.Color = Color.Green;
                        g.DrawCustomFlodLineWithArrow(pArrow, targetPoint, foldDX, -foldDY, straightDX);
                        textStartPoint.X = targetPoint.X + foldDX + straightDX;
                        textStartPoint.Y = targetPoint.Y - foldDY - size.Height / 2;
                        g.DrawString(identifierText.ToString(), font, Brushes.Green, textStartPoint);

                        downDeviant = !downDeviant;
                    }
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