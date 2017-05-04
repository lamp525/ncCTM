using System;
using System.Data;
using System.Drawing;
using System.Linq;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmTimeSharingTradeIdentifier : BaseForm
    {
        #region Fields

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _tradeRecords;
        private DataTable _timeSharingData;
        private double _preClose;
        private Series _sePrice;
        private Series _seVolume;
        private Series _seAvgPrice;

        private string[] _visibleAxisXLableText = new string[] { "09:15", "09:30", "10:00", "10:30", "11:00", "11:30", "13:30", "14:00", "14:30", "15:00" };

        private bool _chartGenerated = false;

        #endregion Fields

        #region Constructors

        public FrmTimeSharingTradeIdentifier()
        {
            InitializeComponent();

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

            foreach (DataRow row in _timeSharingData.Rows)
            {
                //var tradeTime = Regex.Replace(row["TradeTime"].ToString().Trim(), "[:]", "");
                var tradeTime = row["TradeTime"].ToString().Trim().Substring(0, 5);

                var closePrice = CommonHelper.StringToDouble(row["Close"].ToString().Trim());
                var avgPrice = CommonHelper.StringToDouble(row["AvgPrice"].ToString().Trim());
                var volume = CommonHelper.StringToDouble(row["Volume"].ToString().Trim());

                SeriesPoint spPrice = new SeriesPoint(tradeTime, closePrice);
                _sePrice.Points.Add(spPrice);
                SeriesPoint spAvgPrice = new SeriesPoint(tradeTime, avgPrice);
                _seAvgPrice.Points.Add(spAvgPrice);
                SeriesPoint spVolume = new SeriesPoint(tradeTime, volume);
                _seVolume.Points.Add(spVolume);
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
                var commandText = $@"SELECT * FROM DailyRecord WHERE  TradeDate = '2017/05/03' AND StockCode = '000839.SZ' ";
                var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);
                _tradeRecords = ds.Tables[0];

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
                if (!_chartGenerated || _tradeRecords == null || _tradeRecords.Rows.Count == 0) return;

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

                foreach (DataRow row in _tradeRecords.Rows)
                {
                    bool dealFlag = bool.Parse(row["DealFlag"].ToString().Trim());

                    string identifierText = (dealFlag ? "B" : "S") + ":" + row["DealPrice"] + " " + row["DealVolume"];
                    string tradeTime = row["TradeTime"].ToString().Trim().Substring(0, 5);
                    double dealPrice = CommonHelper.SetDoubelDigits( CommonHelper.StringToDouble(row["DealPrice"].ToString().Trim()));
                    Point dealPoint = (chartControl1.Diagram as XYDiagram).DiagramToPoint(tradeTime, dealPrice).Point;
                    targetPoint.X = dealPoint.X;
                    targetPoint.Y = dealPoint.Y;

                    SizeF size = g.MeasureString(identifierText, font);

                    if (dealFlag)
                    {
                        foldDY += upDeviant ? deviant : -deviant;
                        pArrow.Color = Color.AliceBlue;
                        g.DrawCustomFlodLineWithArrow(pArrow, targetPoint, -foldDX, foldDY, -straightDX);
                        textStartPoint.X = targetPoint.X - foldDX - straightDX - size.Width;
                        textStartPoint.Y = targetPoint.Y + foldDY - size.Height / 2;
                        g.DrawString(identifierText, font, Brushes.AliceBlue, textStartPoint);

                        upDeviant = !upDeviant;
                    }
                    else
                    {
                        foldDY -= downDeviant ? deviant : -deviant;
                        pArrow.Color = Color.Orange;
                        g.DrawCustomFlodLineWithArrow(pArrow, targetPoint, foldDX, -foldDY, straightDX);
                        textStartPoint.X = targetPoint.X + foldDX + straightDX;
                        textStartPoint.Y = targetPoint.Y - foldDY - size.Height / 2;
                        g.DrawString(identifierText, font, Brushes.Orange, textStartPoint);

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