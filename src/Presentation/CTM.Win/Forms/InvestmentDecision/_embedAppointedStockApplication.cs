using System;
using System.Data;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedAppointedStockApplication : BaseForm
    {
        #region Fields

        private bool _isExpanded = true;

        #endregion Fields

        #region Properties

        public string StockCode { get; set; }

        #endregion Properties

        public _embedAppointedStockApplication()
        {
            InitializeComponent();
        }

        #region Utilities

        private void FormInit()
        {
            if (string.IsNullOrEmpty(StockCode))
            {
                this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lciIDApplicationList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            this.viewMaster.SetLayout(showCheckBoxRowSelect: false, showAutoFilterRow: false, editable: false, readOnly: true, showGroupPanel: true, rowIndicatorWidth: 35, columnAutoWidth: true);

            this.viewDetail.SetLayout(showCheckBoxRowSelect: false, showAutoFilterRow: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: false, rowIndicatorWidth: -1, columnAutoWidth: true);

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.viewDetail.Columns)
            {
                column.OptionsColumn.AllowEdit = column == this.colOperate_D ? true : false;
            }
        }

        private void DisplayOperationDetail(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: System.Windows.Forms.FormBorderStyle.Sizable);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ViewDetail;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录详情";
            dialog.ShowDialog();
        }

        #endregion Utilities

        #region Methods

        public void BindStockApplication(string stockCode, string stockName)
        {
            try
            {
                this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (string.IsNullOrEmpty(stockCode) || string.IsNullOrEmpty(stockName)) return;

                this.lciIDApplicationList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.lcgApplicationList.Text = $@"股票[{stockCode} - {stockName}] 决策申请单一览";

                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

                var commandText = $@"EXEC [dbo].[sp_GetIDApplicationAndIDOperation] @StockCode = '{stockCode}' ";

                var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

                if (ds == null || ds.Tables.Count == 0) return;

                ds.Relations.Add("MD", ds.Tables[0]?.Columns["ApplyNo"], ds.Tables[1]?.Columns["ApplyNo"]);

                this.gridApplication.DataSource = ds.Tables[0];

                this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
                this.viewMaster.SetAllRowsExpanded(_isExpanded);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Methods

        #region Events

        private void _embedAppointedStockApplication_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnExpandOrCollapse_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExpandOrCollapse.Enabled = false;

                this.viewMaster.SetAllRowsExpanded(!_isExpanded);

                this._isExpanded = !_isExpanded;
                this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
            }
            finally
            {
                this.btnExpandOrCollapse.Enabled = true;
            }
        }

        private void riBtnOperate_D_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var masterRowHandle = this.viewMaster.FocusedRowHandle;
                var relationIndex = this.viewMaster.GetRelationIndex(masterRowHandle, "MD");
                var myView = this.viewMaster.GetDetailView(masterRowHandle, relationIndex) as DevExpress.XtraGrid.Views.Grid.GridView;

                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var applyNo = dr?["ApplyNo"]?.ToString();

                var operateNo = dr?["OperateNo"]?.ToString();

                if (string.IsNullOrEmpty(applyNo) || string.IsNullOrEmpty(operateNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                if (buttonTag == "View")
                {
                    DisplayOperationDetail(applyNo, operateNo);
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

        private void viewMaster_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}