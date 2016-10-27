using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class FrmMarketTrendForecastManage : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private bool _isSearch = false;

        #endregion Fields

        #region Constructors

        public FrmMarketTrendForecastManage(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            var now = _commonService.GetCurrentServerTime().Date;

            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = now.AddDays(-7);

            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = now.AddDays(1);

            this.deForecast.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deForecast.EditValue = now.AddDays(1);

            this.gridViewInfo.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 40);
            this.gridViewInfo.ViewCaption = "点 + 号展开详情，点击 - 号收起";
            foreach (GridColumn column in this.gridViewInfo.Columns)
            {
                if (column.Name == this.colOperate.Name)
                    column.OptionsColumn.AllowEdit = true;
                else
                    column.OptionsColumn.AllowEdit = false;
            }

            this.gridViewDetail.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 40);
            this.gridViewDetail.ViewCaption = "预测详情（点击相应单元格进行输入）";
        }

        private void BindMTFInfo()
        {
            this.gridControl1.DataSource = null;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var infoCommandText = string.Empty;

            if (_isSearch)
            {
                var fromDate = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
                var toDate = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());
                infoCommandText = $@"SELECT * FROM [dbo].[v_MTFInfo] WHERE ApplyDate BETWEEN '{fromDate}' AND '{toDate}' ORDER BY SerialNo DESC";
            }
            else
                infoCommandText = $@"SELECT TOP 20 * FROM [dbo].[v_MTFInfo] ORDER BY SerialNo DESC";

            var infoAdapter = new SqlDataAdapter(infoCommandText, connString);
            var infoTable = new DataTable("Info");
            infoAdapter.Fill(infoTable);

            if ((infoTable == null) || infoTable.Rows.Count == 0) return;

            var serialNoList = infoTable.AsEnumerable().Select(x => x.Field<string>("SerialNo")).Distinct().ToList();

            var serialNos = CommonHelper.ArrayListToSqlConditionString(serialNoList);

            var detailCommandText = $@"SELECT * FROM [dbo].[v_MTFDetail] WHERE SerialNo in ({serialNos })";
            var detailAdapter = new SqlDataAdapter(detailCommandText, connString);

            var detailTable = new DataTable("Detail");
            detailAdapter.Fill(detailTable);

            var source = new DataSet();
            source.Tables.Add(infoTable);
            source.Tables.Add(detailTable);

            DataColumn keyColumn = infoTable.Columns["SerialNo"];
            DataColumn foreignKeyColumn = detailTable.Columns["SerialNo"];

            source.Relations.Add("InfoDetail", keyColumn, foreignKeyColumn);

            this.gridControl1.DataSource = source.Tables["Info"];
        }

        #endregion Utilities

        #region Events

        private void FrmMarketTrendForecast_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindMTFInfo();
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
                this._isSearch = true;
                BindMTFInfo();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRefresh.Enabled = false;
                BindMTFInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally

            {
                this.btnRefresh.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                var applyDate = CommonHelper.StringToDateTime(this.deForecast.EditValue.ToString());

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
                var commandText = $@"EXEC [dbo].[sp_GenerateMarketTrendInfo] @ApplyUser = '{LoginInfo.CurrentUser.UserCode }', @ApplyDate = '{applyDate}'";
                SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);

                this._isSearch = true;
                BindMTFInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAdd.Enabled = true;
            }
        }

        #region Master View

        private void gridViewInfo_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewInfo_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            var currentDetailView = this.gridViewInfo.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;

            foreach (GridColumn column in currentDetailView.Columns)
            {
                if (column.Name == this.colInvestorName.Name || column.Name == this.colSerialNoDetail.Name || column.Name == this.colWeightPercentage.Name)
                    column.OptionsColumn.AllowEdit = false;
                else
                    column.OptionsColumn.AllowEdit = true;
            }
        }

        private void repositoryItembtnOperate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.gridViewInfo;

                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var serialNo = dr?[colSerialNo.FieldName]?.ToString();

                if (string.IsNullOrEmpty(serialNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                if (buttonTag == "Delete")
                {
                    if (DXMessage.ShowYesNoAndWarning("确定删除该预测信息吗？") == DialogResult.Yes)
                    {
                        this._IDService.DeleteInvestmentDecisionForm(serialNo);

                        BindMTFInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                e.Button.Enabled = true;
            }
        }

        private void gridViewInfo_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as GridView;

            DataRow dr = myView.GetDataRow(e.RowHandle);

            if (dr == null) return;

            //操作
            if (e.Column.Name == this.colOperate.Name)
            {
                ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;
                buttonVI.RightButtons[0].Button.Enabled = true;
                buttonVI.RightButtons[0].State = ObjectState.Normal;
            }
        }

        #endregion Master View

        #region Detail View

        private void gridViewDetail_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewDetail_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var currentDetailView = sender as GridView;

            DataRow row = currentDetailView.GetFocusedDataRow();
            if (row == null) return;

            var investorCode = row[colInvestorCode.FieldName].ToString();

            if (investorCode != LoginInfo.CurrentUser.UserCode)
                e.Cancel = true;
        }

        private void gridViewDetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
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
                detail.ForecastTime = _commonService.GetCurrentServerTime();
                detail.Forenoon = row[colForenoon.FieldName].ToString().Trim();
                detail.Reason = row[colReason.FieldName].ToString().Trim();
                detail.Trend = row[colTrend.FieldName].ToString().Trim();

                _IDService.UpdateMTFDetail(detail);
            }
        }

        #endregion Detail View

        #endregion Events
    }
}