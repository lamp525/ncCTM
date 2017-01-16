using System;
using System.Data;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.StatisticsReport
{
    public partial class FrmAccountPositionConfiguration : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonSercice;

        #endregion Fields

        #region Constructors

        public FrmAccountPositionConfiguration(ICommonService commonSercice)
        {
            InitializeComponent();

            this._commonSercice = commonSercice;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            var now = _commonSercice.GetCurrentServerTime();
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = new DateTime(now.Year, 1, 1);
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = now.Date;

            this.gridView1.SetLayout(allowCellMerge: true, showAutoFilterRow: false, showCheckBoxRowSelect: false, columnAutoWidth: false);
        }

        private void DisplayResult()
        {
            var fromDate = this.deFrom.EditValue.ToString();
            var toDate = this.deTo.EditValue.ToString();

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"EXEC [dbo].[sp_GetAccountPositionConfiguration] @FromDate = '{fromDate}', @ToDate = '{toDate}' ";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            this.gridControl1.DataSource = ds?.Tables?[0];
        }

        #endregion Utilities

        #region Events

        private void FrmAccountPositionConfiguration_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                DisplayResult();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                DisplayResult();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var currentUniqueSerialNo = int.Parse(this.gridView1.GetRowCellValue(e.RowHandle, this.colUniqueSerialNo).ToString());

            if (currentUniqueSerialNo % 2 == 0)
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(225, 244, 255);
            //else
            //    e.Appearance.BackColor = System.Drawing.Color.SkyBlue;
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colCurrentPrice)
            {
                var changePercentage = this.gridView1.GetRowCellValue(e.RowHandle, this.colChangePercentage).ToString();
                if (changePercentage.IndexOf('-') == 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
                else if (changePercentage != "0.00%")
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            else if (e.Column == this.colSubjectNetProfitRate || e.Column == this.colChangePercentage || e.Column == this.colStockProfitRate)
            {
                var cellValueString = e.CellValue.ToString();

                var cellValue = decimal.Parse(cellValueString.Substring(0, cellValueString.Length - 1));

                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
            else if (e.Column == this.colSubjectNetProfit || e.Column == this.colStockProfit)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void deFrom_EditValueChanged(object sender, EventArgs e)
        {
            var fromDate = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString()).Date;
            var now = _commonSercice.GetCurrentServerTime().Date;

            if (fromDate > now)
                this.deFrom.EditValue = now;

            if (this.deTo.EditValue == null) return;
            var toDate = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString()).Date;
            if (fromDate > toDate)
                this.deFrom.EditValue = toDate;
        }

        private void deTo_EditValueChanged(object sender, EventArgs e)
        {
            var toDate = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString()).Date;
            var now = _commonSercice.GetCurrentServerTime().Date;

            if (toDate > now)
                this.deTo.EditValue = now;

            if (this.deFrom.EditValue == null) return;
            var fromDate = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString()).Date;
            if (toDate < fromDate)
                this.deTo.EditValue = fromDate;
        }

        #endregion Events
    }
}