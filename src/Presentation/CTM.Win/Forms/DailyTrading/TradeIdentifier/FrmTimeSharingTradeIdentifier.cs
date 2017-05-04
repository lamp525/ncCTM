using System;
using System.Data;
using System.Drawing;
using System.Linq;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Util;
using DevExpress.XtraCharts;

namespace CTM.Win.Forms.DailyTrading.TradeIdentifier
{
    public partial class FrmTimeSharingTradeIdentifier : BaseForm
    {
        #region Fields

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
        private DataTable _timeSharingData;
        private decimal _preClose;
        private Series _sePrice;
        private Series _seVolume;
        private Series _seAvgPrice;

        private string[] _visibleAxisXLableText = new string[] { "09:25", "09:30", "10:00", "10:30", "11:00", "11:30", "13:30", "14:00", "14:30", "15:00" };

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
            decimal minClose = _timeSharingData.AsEnumerable().Select(x => x.Field<decimal>("Close")).Min();
            decimal maxClose = _timeSharingData.AsEnumerable().Select(x => x.Field<decimal>("Close")).Max();
            decimal maxDiff = Math.Abs(minClose - _preClose) > Math.Abs(maxClose - _preClose) ? Math.Abs(minClose - _preClose) : Math.Abs(maxClose - _preClose);

            decimal minValueY = _preClose - maxDiff;
            decimal maxValueY = _preClose + maxDiff;

            myAxisY.WholeRange.SetMinMaxValues(minValueY, maxValueY);
        }

        #endregion Utilities

        #region Events

        private void FrmTimeSharingTradeIdentifier_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                ChartInit();

                var commandText1 = $@"SELECT * FROM TKLineToday WHERE  TradeDate = '2017/05/02' AND StockCode = '000417.SZ' ";
                var ds1 = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText1);
                _preClose = CommonHelper.StringToDecimal(ds1.Tables[0].Rows[0]["Close"].ToString().Trim());

                var commandText2 = $@"SELECT *	FROM [FinancialCenter].[dbo].[TKLine_1Min] WHERE TradeDate = '{"2017/05/03"}' AND StockCode = '{"000417.SZ"}' ORDER BY TradeTime";
                var ds2 = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText2);

                if (ds2 == null || ds2.Tables.Count == 0) return;

                _timeSharingData = ds2.Tables[0];

                DisplayChart();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chartControl1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            AxisBase axis = e.Item.Axis;

            var tag = axis.Tag.ToString();

            switch (tag )
            {
                case "x":
                    string valueX = e.Item.AxisValue.ToString().Trim();
                    if (!_visibleAxisXLableText.Contains(valueX))
                    {                 
                        e.Item.Text = string.Empty;
                    }
                    break;
                case "y":
                    decimal valueY =CommonHelper .StringToDecimal( e.Item.AxisValue.ToString ());
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

        #endregion Events
    }
}