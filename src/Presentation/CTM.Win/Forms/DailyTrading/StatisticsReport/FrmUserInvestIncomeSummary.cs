using CTM.Core;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using System;
using System.Data;
using System.Linq;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmUserInvestIncomeSummary : BaseForm
    {
        #region Fields

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;
        private const string _layoutXmlName = "FrmUserInvestIncomeSummary";
        private DataTable _profitData = null;

        #endregion Fields

        #region Constructors

        public FrmUserInvestIncomeSummary()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void DisplaySearchResult(bool isSearch)
        {
            this.gridControl1.DataSource = null;

            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            if (isSearch)
            {
                string sqlText = $@"EXEC	[dbo].[sp_RPT_InvestorProfitSummary]	@StartDate = '{dateFrom}',		@EndDate = '{dateTo}'";
                DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, sqlText);
                _profitData = ds == null || ds.Tables.Count == 0 ? null : ds.Tables[0];
            }

            if (_profitData == null || _profitData.Rows.Count == 0) return;

            DataTable result = !this.chkOnWorking.Checked ? _profitData : _profitData.AsEnumerable().Where(x => x.Field<int>("IsOnWorking") == 1).CopyToDataTable();

            this.gridControl1.DataSource = result;    

        }

        private void FilterSearchResult()
        {
            DisplaySearchResult(false);
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeSummary_Load(object sender, EventArgs e)
        {
            this.deFrom.Properties.AllowNullInput = DefaultBoolean.False;
            this.deFrom.EditValue = _initDate;

            this.deTo.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTo.EditValue = now.Date.AddDays(-1);
            else
                this.deTo.EditValue = now.Date;

            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.lciCheckAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lciCheckOnWorking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciCheckAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciCheckOnWorking.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            this.bandedGridView1.LoadLayout(_layoutXmlName);
            this.bandedGridView1.SetLayout(showCheckBoxRowSelect: false, showFilterPanel: true, showGroupPanel: true, setAlternateRowColor: false);
            this.bandedGridView1.SetColumnHeaderAppearance();

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                DisplaySearchResult(true);
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

        private void chkOnWorking_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAll.Checked = !this.chkOnWorking.Checked;

            if (this.chkOnWorking.Checked)
                FilterSearchResult();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            this.chkOnWorking.Checked = !this.chkAll.Checked;

            if (this.chkAll.Checked)
                FilterSearchResult();
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.bandedGridView1.SaveLayout(_layoutXmlName);
        }

        private void bandedGridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            int dataType = int.Parse(this.bandedGridView1.GetRowCellValue(e.RowHandle, this.colDataType).ToString());

            if (dataType == 99)
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(225, 244, 255);
            else if (dataType == 1)
                e.Appearance.BackColor = System.Drawing.Color.SkyBlue;
            else if (dataType == 3)
             e.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 211, 155);
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            bandedGridView1.DrawRowIndicator(e);
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            bandedGridView1.ReplaceCellValueZero(e);
        }

        private void bandedGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column.FieldName.IndexOf("Profit") > 0 || e.Column.FieldName.IndexOf("Rate") > 0)
            {
                var cellValue = decimal.Parse(e.CellValue.ToString());
                if (cellValue > 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                else if (cellValue < 0)
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
            }
        }

        #endregion Events
    }
}