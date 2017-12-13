using System;
using System.Data;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraEditors;

namespace CTM.Win.Forms.Accounting.DataManage
{
    public partial class _dialogAmountSplit : BaseForm
    {
        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Properties

        public int DeliveryId { get; set; }

        public int AccountId { get; set; }

        public string AccountInfo { get; set; }

        public string TradeDate { get; set; }

        public string StockCode { get; set; }

        public string StockName { get; set; }

        public decimal ActualAmount { get; set; }

        public decimal DealVolume { get; set; }

        #endregion Properties

        public _dialogAmountSplit()
        {
            InitializeComponent();
        }

        private void DisplayYestodayPosition()
        {
            string sqlText = $@"EXEC [dbo].[sp_GetAccountPositionRate] @AccountId = {AccountId}, @StockCode = '{StockCode}', @TradeDate = '{TradeDate}'";
            var ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
            if (ds != null && ds.Tables.Count == 1)
            {
                this.gridControl1.DataSource = ds.Tables[0];
            }
        }

        private void FormInit()
        {
            this.esiTitle.Text = $@"{TradeDate.Split(' ')[0]}  [{AccountInfo}] - [{StockCode} - {StockName}] （总金额：{ActualAmount}）";
            this.gridView1.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false, editable: false , readOnly: true, rowIndicatorWidth: 30);
        }

        private void _dialogAmountSplit_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                DisplayYestodayPosition();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            var dr = myView.GetRow(e.RowHandle) as DataRowView;
            if (dr == null) return;

            if (e.Column.Name == colSplitAmount.Name)
            {            
                e.DisplayText = CommonHelper.SetDecimalDigits(decimal.Parse(dr[colRate.FieldName].ToString()) * ActualAmount).ToString();
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void repositoryItemTextEdit1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void gridView1_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;

                if (DXMessage.ShowYesNoAndTips("是否确定导入？") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.RefreshEvent?.Invoke();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}