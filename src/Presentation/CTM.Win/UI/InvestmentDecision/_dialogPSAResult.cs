using System;
using System.Data;
using System.Windows.Forms;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;
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

            var items = new ListBoxItem[dtStocks.Rows.Count];
            for (int i = 0; i < dtStocks.Rows.Count; i++)
            {
                var row = dtStocks.Rows[i];
                items[i].Value = $@"{row["StockCode"].ToString()} - {row["StockName"].ToString()}";
                items[i].Tag = row["StockCode"].ToString();
            }

            this.lbStock.Items.AddRange(items);
        }

        private void BindAnalysisDetail(string stockCode)
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@" SELECT DISTINCT *  FROM [dbo].[v_PSADetail] WHERE SerialNo = '{SerialNo}' AND StockCode = '{stockCode}'";
            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            this.gridControl1.DataSource = ds?.Tables[0];         
        }

        private void FormInit()
        {          
            this.lbStock.SelectionMode = SelectionMode.One;
           
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

        private void clbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkedListBox = sender as CheckedListBox;

            var stockCode = string.Empty;

            if (checkedListBox.SelectedItems.Count > 0)
                stockCode = (checkedListBox.SelectedItem as CheckedListBoxItem).Value.ToString().Trim();

            BindAnalysisDetail(stockCode);
        }

        #endregion Events

        private void lbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lb = sender as ListBox;

            var stockCode = string.Empty;

            if (lb.SelectedItems.Count > 0)
                stockCode = (lb.SelectedItem as ListBoxItem).Tag.ToString();

            BindAnalysisDetail(stockCode);
        }
    }
}