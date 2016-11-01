using System;
using System.Data;
using System.Windows.Forms;
using CTM.Data;
using DevExpress.XtraEditors.Controls;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class _dialogPSAResult : Form
    {
        #region Properties

        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        #endregion Properties

        #region Constructors

        public _dialogPSAResult()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void BindStockList()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@" SELECT DISTINCT StockCode, StockName  FROM [dbo].[v_PSADetail] WHERE SerialNo = '{SerialNo}'";
            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);
            if (ds == null || ds.Tables.Count == 0) return;

            var dtStocks = ds.Tables[0];

            if (dtStocks == null || dtStocks.Rows.Count == 0) return;

            var items = new CheckedListBoxItem[dtStocks.Rows.Count];
            for (int i = 0; i < dtStocks.Rows.Count; i++)
            {
                var row = dtStocks.Rows[i];
                items[i] = new CheckedListBoxItem(value: row["StockCode"], description: $@"{row["StockCode"].ToString()} - {row["StockName"].ToString()}");
            }

            this.clbStock.Items.AddRange(items);
        }

        private void FormInit()
        {
            throw new NotImplementedException();
        }

        #endregion Utilities

        #region Events

        private void _dialogPSAResult_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindStockList();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Events
    }
}