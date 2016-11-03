using System;
using System.Data;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogMTFEdit : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Properties

        public string SerialNo { get; set; }

        public DateTime ForecastDate { get; set; }

        #endregion Properties

        #region Constructors
        public _dialogMTFEdit(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        } 
        #endregion

        #region Utilities

        private void FormInit()
        {
            this.gridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.gridView1.Columns)
            {
                if (column.Name == this.colInvestorCode.Name || column.Name == this.colSerialNo.Name || column.Name == this.colWeight.Name)
                    column.OptionsColumn.AllowEdit = false;
                else
                    column.OptionsColumn.AllowEdit = true;
            }

            this.lciMTF.Text = $@"大盘趋势预测（{ SerialNo}） - { ForecastDate.ToShortDateString()}";
        }

        private void BindTrendDailyInfo()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"EXEC [dbo].[sp_GenerateMTFDetail] @InvestorCode = '{LoginInfo.CurrentUser.UserCode }', @ForecastDate = '{ForecastDate}'";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
        }

        #endregion Utilities

        #region Events

        private void _dialogMTFEdit_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindTrendDailyInfo();
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

        private void gridView1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataRow row = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (row == null) return;

            var investorCode = row[colInvestorCode.FieldName].ToString();

            if (investorCode != LoginInfo.CurrentUser.UserCode)
                e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row;
            DataRow row = drv.Row;
            if (row.RowState == DataRowState.Modified)
            {
                var investorCode = row[colInvestorCode.FieldName].ToString();
                var serialNo = row[colSerialNo.FieldName].ToString();

                var detail = _IDService.GetMTFDetail(investorCode, serialNo);

                if (detail == null) return;

                detail.Accuracy = row[colAccuracy.FieldName].ToString().Trim();
                var acquaintanceGraphDate = row[colAcquaintanceGraphDate.FieldName].ToString().Trim();
                if (!string.IsNullOrEmpty(acquaintanceGraphDate))
                    detail.AcquaintanceGraphDate = CommonHelper.StringToDateTime(acquaintanceGraphDate);
                detail.Afternoon = row[colAfternoon.FieldName].ToString().Trim();
                detail.Close = row[colClose.FieldName].ToString().Trim();
                detail.CreateTime = _commonService.GetCurrentServerTime();
                detail.Forenoon = row[colForenoon.FieldName].ToString().Trim();
                detail.Reason = row[colReason.FieldName].ToString().Trim();
                detail.Trend = row[colTrend.FieldName].ToString().Trim();

                _IDService.UpdateMTFDetail(detail);
            }
        }

        #endregion Events
    }
}