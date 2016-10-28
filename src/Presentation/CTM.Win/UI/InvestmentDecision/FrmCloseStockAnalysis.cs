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
    public partial class FrmCloseStockAnalysis : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private bool _isSearch = false;

        #endregion Fields

        #region Constructors

        public FrmCloseStockAnalysis(ICommonService commonService, IInvestmentDecisionService IDService)
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

            this.gridView1.SetLayout(showCheckBoxRowSelect: false, editable: true, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.gridView1.Columns)
            {
                if (column.Name == this.colOperate.Name)
                    column.OptionsColumn.AllowEdit = true;
                else
                    column.OptionsColumn.AllowEdit = false;
            }
        }

        private void BindCSAInfo()
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
            commandText = $@"SELECT TOP 20 * FROM [dbo].[v_CSAInfo] ORDER BY SerialNo DESC";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
        }

        private void DisplayCSADetail(DataRow dr)
        {
            var dialog = EngineContext.Current.Resolve<_dialogCloseStockAnalysis>();
            dialog.Owner = this.ParentForm;
            dialog.Text = "收盘个股分析详情";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.SerialNo = dr[colSerialNo.FieldName].ToString ();
            dialog.JudgmentDate = dr[colJudgmentDate.FieldName].ToString();
            dialog.InvestorName = dr[colInvestorName.FieldName].ToString();
            dialog.Show();
        }

        #endregion Utilities

        #region Events

        private void FrmCloseStockAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindCSAInfo();
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

                var judgmentDate = CommonHelper.StringToDateTime(this.deTradeDate.EditValue.ToString());

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
                var commandText = $@"EXEC [dbo].[sp_GenerateCloseStockAnalysisInfo] @InvestorCode = '{LoginInfo.CurrentUser.UserCode }', @JudgmentDate = '{judgmentDate}'";
                SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);

                this._isSearch = true;
                BindCSAInfo();
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

                BindCSAInfo();
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
                buttonVI.RightButtons[0].Button.Enabled = true;
                buttonVI.RightButtons[0].State = ObjectState.Normal;
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
                    if (DXMessage.ShowYesNoAndWarning("确定删除该分析信息吗？") == DialogResult.Yes)
                    {
                        this._IDService.DeleteCSAInfo(serialNo);

                        BindCSAInfo();
                    }
                }
                else if (buttonTag == "Edit")
                {
                    DisplayCSADetail(dr);
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