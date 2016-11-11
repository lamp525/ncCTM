using System;
using System.Data;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.DataManage
{
    public partial class _dialogTradeDataContrast : BaseForm
    {
        #region Properties

        public int AccountId { get; set; }

        public string AccountInfo { get; set; }

        public DateTime TradeDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        #endregion Properties

        #region Constructors

        public _dialogTradeDataContrast()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.esiTitle.Text = $@"{TradeDate.ToShortDateString()} - [{AccountInfo}] - [{StockCode} - {StockName}] ";

            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
            this.gridView2.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
        }

        private void BindTradeDate()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"EXEC [dbo].[sp_GetDeliveryAndDailyContrastData] @AccountId = {AccountId} , @StockCode = '{StockCode}' , @TradeDate = '{TradeDate}'";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            this.gridControl1.DataSource = ds.Tables[0];

            this.gridControl2.DataSource = ds.Tables[1];
        }

        #endregion Utilities

        #region Events

        private void _dialogTradeDataContrast_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindTradeDate();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}