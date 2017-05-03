using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private Series _sePrice;
        private Series _seVolume;
        private Series _seAvgPrice;
        #endregion

        #region Constructors
        public FrmTimeSharingTradeIdentifier()
        {
            InitializeComponent();

            _sePrice = chartControl1.Series[0];
            _seVolume = chartControl1.Series[1];
            _seAvgPrice = chartControl1.Series[2];
        }
        #endregion

        #region Utilities
        private void FormInit()
        {
            
        }

        private void ChartInit()
        {
            
        }

        private void DisplayChart()
        {
            _sePrice.Points.Clear();
            _seVolume.Points.Clear();

            foreach (DataRow row in _timeSharingData.Rows  )
            {
                var tradeTime = row["TradeTime"].ToString().Trim();

                var closePrice = CommonHelper.StringToDouble( row["Close"].ToString().Trim());
                var avgPrice = CommonHelper.StringToDouble( row["AvgPrice"].ToString().Trim());
                var volume = CommonHelper.StringToDouble ( row["Volume"].ToString().Trim());

                SeriesPoint spPrice = new SeriesPoint(tradeTime, closePrice);
                _sePrice.Points.Add(spPrice);
                SeriesPoint spAvgPrice = new SeriesPoint(tradeTime, avgPrice);
                _seAvgPrice.Points.Add(spAvgPrice);
                SeriesPoint spVolume = new SeriesPoint(tradeTime, volume);
                _seVolume.Points.Add(spVolume);
            }
        }

        #endregion

        #region Events
        private void FrmTimeSharingTradeIdentifier_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                ChartInit();

                var commandText = $@"SELECT *	FROM [FinancialCenter].[dbo].[TKLine_1Min] WHERE TradeDate = '{"2017/05/03"}' AND StockCode = '{"000417.SZ"}' ORDER BY TradeTime";
                var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

                if (ds == null || ds.Tables.Count == 0) return;

                    _timeSharingData = ds.Tables[0];


                DisplayChart();
            }
            catch (Exception ex)
            {

                DXMessage.ShowError(ex.Message );
            }
        }




        #endregion
    }
}
