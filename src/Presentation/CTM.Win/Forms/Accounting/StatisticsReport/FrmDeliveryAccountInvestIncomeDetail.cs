﻿using System;
using CTM.Core.Util;
using CTM.Services.StatisticsReport;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.Utils;

namespace CTM.Win.Forms.Accounting.StatisticsReport
{
    public partial class FrmDeliveryAccountInvestIncomeDetail : BaseForm
    {
        #region Fields

        private readonly IDeliveryStatisticsReportService _deliveryReportService;

        private readonly DateTime _initDate = AppConfigHelper.StatisticsInitDate;

        private const string _layoutXmlName = "FrmDeliveryAccountInvestIncomeDetail";

        #endregion Fields

        #region Constructors

        public FrmDeliveryAccountInvestIncomeDetail(IDeliveryStatisticsReportService deliveryReportService)
        {
            InitializeComponent();

            this._deliveryReportService = deliveryReportService;
        }

        #endregion Constructors

        #region Utilities

        private void DisplaySearchResult()
        {
            var dateFrom = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            var dateTo = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());

            var source = _deliveryReportService.GetDeliveryAccountInvestIncomeDetail(dateFrom, dateTo);

            this.gridControl1.DataSource = source;
        }

        #endregion Utilities

        #region Events

        private void FrmUserInvestIncomeAccount_Load(object sender, EventArgs e)
        {
            this.deFrom.Properties.AllowNullInput = DefaultBoolean.False;
            this.deFrom.EditValue = CommonHelper.GetFirstDayOfMonth(DateTime.Now.Date);

            this.deTo.Properties.AllowNullInput = DefaultBoolean.False;
            var now = DateTime.Now;
            if (now.Hour < 15)
                this.deTo.EditValue = now.Date.AddDays(-1);
            else
                this.deTo.EditValue = now.Date;

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showGroupPanel: true, showFilterPanel: true, showCheckBoxRowSelect: false, rowIndicatorWidth: 60);
            this.gridView1.SetColumnHeaderAppearance();
            gridView1.OptionsBehavior.AllowPartialGroups = DefaultBoolean.True;

            this.ActiveControl = this.btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                this.gridControl1.DataSource = null;

                DisplaySearchResult();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.gridControl1.DataSource = null;
            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.CellValue == null) return;

            if (e.Column == this.colProfit || e.Column == this.colAccumulatedProfit)
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