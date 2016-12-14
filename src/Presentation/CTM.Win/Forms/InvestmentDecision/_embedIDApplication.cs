using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDApplication : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;
        private readonly ICommonService _commonService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;

        private bool _isExpanded = true;

        private const string _layoutXmlName_Master = "_embedIDApplication_Master";
        private const string _layoutXmlName_Detail = "_embedIDApplication_Detail";

        #endregion Fields

        #region Enums

        public enum QueryMode
        {
            /// <summary>
            /// 进行中
            /// </summary>
            Proceed,

            /// <summary>
            /// 完成
            /// </summary>
            Done,

            /// <summary>
            /// 全部
            /// </summary>
            All
        }

        #endregion Enums

        #region Properties

        public QueryMode CurrentQueryMode { get; set; }

        #endregion Properties

        #region Constructors

        public _embedIDApplication(
            IInvestmentDecisionService IDService,
            ICommonService commonService,
            IUserService userService,
            IStockService stockService)
        {
            InitializeComponent();

            this._IDService = IDService;
            this._commonService = commonService;
            this._userService = userService;
            this._stockService = stockService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            var now = _commonService.GetCurrentServerTime().Date;

            this.deFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFrom.EditValue = now.AddMonths(-1);
            this.deTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTo.EditValue = now;

            //申请人
            var applyUsers = _userService.GetAllOperators(true);
            this.luApplyUser.Initialize(applyUsers, "Id", "Name", true);

            //股票
            var stocks = _stockService.GetAllStocks(showDeleted: true)
                .Select(x => new StockInfoModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    FullCode = x.FullCode,
                    Name = x.Name,
                    DisplayMember = x.FullCode + " - " + x.Name,
                }
           ).OrderBy(x => x.FullCode).ToList();
            this.luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

            this.viewMaster.LoadLayout(_layoutXmlName_Master);
            this.viewMaster.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 30, columnAutoWidth: false);

            foreach (GridColumn column in this.viewMaster.Columns)
            {
                column.OptionsColumn.AllowEdit = column == this.colOperate ? true : false;
            }

            this.viewDetail.LoadLayout(_layoutXmlName_Detail);
            this.viewDetail.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 25, columnAutoWidth: false);

            foreach (GridColumn column in this.viewDetail.Columns)
            {
                column.OptionsColumn.AllowEdit = column == this.colOperate_D ? true : false;
            }
        }

        #region Master

        private void OperateButtonStatusSetting(DataRow dr, ButtonEditViewInfo buttonVI)
        {
            var investorCode = dr[this.colApplyUser.FieldName]?.ToString();

            if (LoginInfo.CurrentUser.IsAdmin || investorCode == LoginInfo.CurrentUser.UserCode)
            {
                buttonVI.RightButtons[0].Button.Enabled = true;
                buttonVI.RightButtons[0].State = ObjectState.Normal;
            }
            else
            {
                buttonVI.RightButtons[0].Button.Enabled = false;
                buttonVI.RightButtons[0].State = ObjectState.Disabled;
            }
        }

        #endregion Master

        #region Detail

        private void DisplayOperationDetail(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ViewDetail;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录详情";
            dialog.ShowDialog();
        }

        private void OperationAccuracyVoteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.AccuracyDetermination;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录准确度评定";
            dialog.ShowDialog();
        }

        private void OperationExecuteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ExecutionConfirm;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录执行确认";
            dialog.ShowDialog();
        }

        private void OperationIDVoteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.OperationVote;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录决策投票";
            dialog.ShowDialog();
        }

        private void OperationDeleteProcess(string applyNo, string operateNo)
        {
            if (DXMessage.ShowYesNoAndWarning("确定删除该申请单吗？") == DialogResult.Yes)
            {
                //  this._IDService.DeleteInvestmentDecisionForm(serialNo);

                BindApplicationInfo();
            }
        }

        #endregion Detail

        #endregion Utilities

        #region Methods

        public void BindApplicationInfo()
        {
            this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
   
            string commandText = string.Empty;

            switch (CurrentQueryMode)
            {
                case QueryMode.Proceed:
                    break;

                case QueryMode.Done:
                    break;

                case QueryMode.All:
                    break;

                default:
                    break;
            }

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            commandText = $@"EXEC [dbo].[sp_GetIDApplicationAndIDOperation] @Status = {(int)EnumLibrary.IDApplicationStatus.Proceed} ";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            ds.Relations.Add("MD", ds.Tables[0]?.Columns["ApplyNo"], ds.Tables[1]?.Columns["ApplyNo"]);

            this.gridApplication.DataSource = ds.Tables[0];

            this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
            this.viewMaster.SetAllRowsExpanded(_isExpanded);
        }

        #endregion Methods

        #region Events

        private void FrmStockInvestmentDecision_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindApplicationInfo();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRefresh.Enabled = false;

                BindApplicationInfo();
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

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.viewMaster.SaveLayout(_layoutXmlName_Master, false);
            this.viewDetail.SaveLayout(_layoutXmlName_Detail, true);
        }

        #region MasterView

        private void viewMaster_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void viewMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as GridView;

            DataRow dr = myView.GetDataRow(e.RowHandle);

            if (dr == null) return;
            if (e.Column.Name == colOperate.Name)
            {
                ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;

                OperateButtonStatusSetting(dr, buttonVI);
            }
        }

        private void riBtnOperate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.viewMaster;

                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var applyNo = dr?[colApplyNo.FieldName]?.ToString();

                if (string.IsNullOrEmpty(applyNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                if (buttonTag == "Apply")
                {
                    var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
                    dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
                    dialog.CurrentPageMode = _dialogIDApplication.PageMode.NewOperation;
                    dialog.ApplyNo = applyNo;
                    dialog.OperateNo = string.Empty;
                    dialog.Text = "投资决策交易操作申请";
                    dialog.ShowDialog();
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

        #endregion MasterView

        #region DetailView

        private void viewDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void ribtnOperate_D_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                switch (buttonTag)
                {
                    case "IDVote":
                        OperationIDVoteProcess(applyNo, operateNo);
                        break;

                    case "Execute":
                        OperationExecuteProcess(applyNo, operateNo);
                        break;

                    case "AccuracyVote":
                        OperationAccuracyVoteProcess(applyNo, operateNo);
                        break;

                    case "View":
                        DisplayOperationDetail(applyNo, operateNo);
                        break;

                    case "Delete":
                        OperationDeleteProcess(applyNo, operateNo);
                        break;

                    default:
                        break;
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

        #endregion DetailView

        #endregion Events


    }
}