using System;
using System.Data;
using System.Windows.Forms;
using CTM.Core.Infrastructure;
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
    public partial class FrmIDPositionStockAnalysis : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private bool _isSearch = false;

        #endregion Fields

        #region Constructors

        public FrmIDPositionStockAnalysis(ICommonService commonService, IInvestmentDecisionService IDService)
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

            this.deTradeDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTradeDate.EditValue = now;

            this.gridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.gridView1.Columns)
            {
                if (column.Name == this.colOperate.Name)
                    column.OptionsColumn.AllowEdit = true;
                else
                    column.OptionsColumn.AllowEdit = false;
            }
        }

        private void BindPSAInfo()
        {
            this.gridControl1.DataSource = null;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = string.Empty;

            //if (_isSearch)
            //{
            //    var fromDate = CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString());
            //    var toDate = CommonHelper.StringToDateTime(this.deTo.EditValue.ToString());
            //    infoCommandText = $@"SELECT * FROM [dbo].[v_MTFInfo] WHERE ApplyDate BETWEEN '{fromDate}' AND '{toDate}' ORDER BY SerialNo DESC";
            //}
            //else
            commandText = $@"SELECT TOP 20 * FROM [dbo].[PositionStockAnalysisInfo] ORDER BY SerialNo DESC";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
        }

        private void DisplayCSAEdit( DataRow dr)
        {
            var dialog = EngineContext.Current.Resolve<_dialogPSAEdit>();
            dialog.Owner = this.ParentForm;
            dialog.Text = "股票池操作建议";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.SerialNo = dr[colSerialNo.FieldName].ToString();
            dialog.AnalysisDate = CommonHelper.StringToDateTime(dr[colAnalysisDate.FieldName].ToString());
            dialog.Show();
        }

        private void DisplayCSAResult(DataRow dr)
        {
            var dialog = EngineContext.Current.Resolve<_dialogPSAResult>();
            dialog.Owner = this.ParentForm;
            dialog.Text = "股票池操作建议一览";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.SerialNo = dr[colSerialNo.FieldName].ToString();
            dialog.Show();
        }

        private void OperateButtonStatusSetting(DataRow dr, ButtonEditViewInfo buttonVI)
        {
            buttonVI.RightButtons[0].Button.Enabled = true;
            buttonVI.RightButtons[0].State = ObjectState.Normal;

            buttonVI.RightButtons[1].Button.Enabled = LoginInfo.CurrentUser.IsAdmin;
            buttonVI.RightButtons[1].State = ObjectState.Normal;

            buttonVI.RightButtons[2].Button.Enabled = true;
            buttonVI.RightButtons[2].State = ObjectState.Normal;
        }

        #endregion Utilities

        #region Events

        private void FrmCloseStockAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindPSAInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                var analysisDate = CommonHelper.StringToDateTime(this.deTradeDate.EditValue.ToString());

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
                var commandText = $@"EXEC [dbo].[sp_GeneratePSAInfo]  @AnalysisDate = '{analysisDate}'";
                SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);

                this._isSearch = true;
                BindPSAInfo();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRefresh.Enabled = false;

                BindPSAInfo();
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

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as GridView;

            DataRow dr = myView.GetDataRow(e.RowHandle);

            if (dr == null) return;

            //操作
            if (e.Column.Name == this.colOperate.Name)
            {
                ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;
                OperateButtonStatusSetting(dr, buttonVI);
            }
        }

        private void riButtonEditOperate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.gridView1;

                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var serialNo = dr?[colSerialNo.FieldName]?.ToString();

                if (string.IsNullOrEmpty(serialNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                if (buttonTag == "Delete")
                {
                    if (DXMessage.ShowYesNoAndWarning("确定删除该记录所有投资人员的分析信息吗？") == DialogResult.Yes)
                    {
                        this._IDService.DeletePSAInfo(serialNo);

                        BindPSAInfo();
                    }
                }
                else if (buttonTag == "Edit")
                {
                    DisplayCSAEdit( dr);
                }
                else if (buttonTag == "View")
                {
                    DisplayCSAResult( dr);
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

        #endregion Events
    }
}