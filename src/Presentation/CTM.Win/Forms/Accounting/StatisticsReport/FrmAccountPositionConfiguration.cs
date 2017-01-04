using System;
using System.Data;
using CTM.Data;
using CTM.Services.Common;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Accounting.StatisticsReport
{
    public partial class FrmAccountPositionConfiguration : BaseForm
    {
        private readonly ICommonService _commonSercice;

        public FrmAccountPositionConfiguration(ICommonService commonSercice)
        {
            InitializeComponent();

            this._commonSercice = commonSercice;
        }

        private void FormInit()
        {
            var now = _commonSercice.GetCurrentServerTime();
            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = new DateTime(now.Year, 1, 1);
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = now.Date;

            this.gridView1.SetLayout(allowCellMerge: true, showAutoFilterRow: false, multiSelect: false, showCheckBoxRowSelect: false,columnAutoWidth:true);
        }

        private void DisplayResult()
        {
            var fromDate = this.deFrom.EditValue.ToString();
            var toDate = this.deTo.EditValue.ToString();

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"EXEC [dbo].[sp_AccountPositionInfo] @FromDate = '{fromDate }', @ToDate = '{toDate}' ";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            this.gridControl1.DataSource = ds?.Tables?[0];
        }

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

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == this.colOwnerName)
            {
                e.Appearance.BackColor = System.Drawing.Color.GreenYellow;
            }
            else if (e.Column == this.colCurrentPrice)
            {
                e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            else if (e.Column == this.colChangePercentage || e.Column == this.colStockProfitRate)
            {
                var cellValue = e.CellValue.ToString();

                if (cellValue.IndexOf('-') == 0)
                    e.Appearance.BackColor = System.Drawing.Color.MediumAquamarine;
                else if (cellValue != "0.00%")
                    e.Appearance.BackColor = System.Drawing.Color.MistyRose;
            }
            else if (e.Column == this.colStockProfit)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.BackColor = System.Drawing.Color.MistyRose;
                else if (cellValue < 0)
                    e.Appearance.BackColor = System.Drawing.Color.MediumAquamarine;
            }
        }
    }
}