using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.Stock;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Forms.Common;
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

        private readonly string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

        //private bool _isExpanded = false;
        private bool _isGridDataSourceChanged = false;
        private int _defaultMasterFocusedRowHandle = 0;

        private string _stopReasonContent = string.Empty;

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
            var applyUsers = new List<UserInfo>();

            var allUser = new UserInfo
            {
                Code = string.Empty,
                Name = "所有申请人",
            };
            applyUsers.Add(allUser);

            applyUsers.AddRange(_userService.GetAllOperators(true).OrderBy(x => x.Name));
            this.luApplyUser.Initialize(applyUsers, "Code", "Name", true);

            //股票
            var allStock = new StockInfoModel
            {
                FullCode = string.Empty,
                Name = "所有股票",
                DisplayMember = "所有股票",
            };

            var stocks = _stockService.GetAllStocks(showDeleted: true)
                .Select(x => new StockInfoModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    FullCode = x.FullCode,
                    Name = x.Name,
                    DisplayMember = x.FullCode + " - " + x.Name,
                }
           ).ToList();

            stocks.Add(allStock);

            stocks = stocks.OrderBy(x => x.FullCode).ToList();

            this.luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true);

            this.viewDetail.LoadLayout(_layoutXmlName_Detail);
            this.viewDetail.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: -1, columnAutoWidth: false);

            foreach (GridColumn column in this.viewDetail.Columns)
            {
                column.OptionsColumn.AllowEdit = column == this.colOperate_D ? true : false;
            }

            this.viewMaster.LoadLayout(_layoutXmlName_Master);

            if (CurrentQueryMode == QueryMode.Done)
            {
                this.viewMaster.SetLayout(showCheckBoxRowSelect: false, editable: false, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: true, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 45, columnAutoWidth: true);
                this.colOperate.Visible = false;

                this.colOperate_D.Width = 90;
                this.colOperate_D.MaxWidth = 90;
                this.colOperate_D.ColumnEdit = this.ribtnView_D;
            }
            else
            {
                this.viewMaster.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 45, columnAutoWidth: true);

                foreach (GridColumn column in this.viewMaster.Columns)
                {
                    column.OptionsColumn.AllowEdit = column == this.colOperate ? true : false;
                }

                this.colOperate_D.Width = 420;
                this.colOperate_D.MaxWidth = 420;
                this.colOperate_D.ColumnEdit = this.ribtnOperate_D;
            }
        }

        private void TimerInit()
        {
            this.timer1.Interval = 60000;
            this.timer1.Start();
        }

        #region Master

        private void SetApplicationIdentify(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e, string applyNo)
        {
            e.DisplayText = @"决: 0   准: 0";

            var commandText = $@"EXEC [dbo].[sp_GetIDIdentify] @InvestorCode = '{LoginInfo.CurrentUser.UserCode}' ,@ApplyNo = '{applyNo}'";

            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return;

            var needVote = ds.Tables[0].Rows[0]["NeedVote"].ToString();
            var needAccuracy = ds.Tables[0].Rows[0]["NeedAccuracy"].ToString();

            if (!string.IsNullOrEmpty(needVote) && !string.IsNullOrEmpty(needAccuracy))
                e.DisplayText = $@"决: {int.Parse(needVote)}   准: {int.Parse(needAccuracy)}";
            else
                e.DisplayText = @"决: 0   准: 0";
        }

        private void OperateButtonStatusSetting(DataRow dr, ButtonEditViewInfo buttonVI)
        {
            var investorCode = dr[this.colApplyUser.FieldName]?.ToString();
            var applyType = int.Parse(dr[this.colApplyType.FieldName]?.ToString());
            var accuracyEvaluateFlag = string.IsNullOrEmpty(dr[this.colAccuracyEvaluateOperateNo.FieldName]?.ToString()) ? false : true;
            var finishConfirmFlag = bool.Parse(dr[this.colFinishConfirmFlag.FieldName]?.ToString());
            var status = int.Parse(dr[this.colStatus.FieldName]?.ToString());

            ///按钮0：交易申请
            ///按钮1：准确度评价
            ///按钮2：完结确认

            //var a = 1;
            //if (dr[colApplyNo.FieldName].ToString() == "SQ000120")
            //    a++;

            if (status == (int)EnumLibrary.IDApplicationStatus.Done)
            {
                buttonVI.RightButtons[0].Button.Enabled = false;
                buttonVI.RightButtons[0].State = ObjectState.Disabled;

                buttonVI.RightButtons[1].Button.Enabled = false;
                buttonVI.RightButtons[1].State = ObjectState.Disabled;

                buttonVI.RightButtons[2].Button.Enabled = false;
                buttonVI.RightButtons[2].State = ObjectState.Disabled;
            }
            else
            {
                if (LoginInfo.CurrentUser.IsAdmin || investorCode == LoginInfo.CurrentUser.UserCode)
                {
                    if (applyType == (int)EnumLibrary.IDOperationApplyType.None)
                    {
                        buttonVI.RightButtons[0].Button.Enabled = false;
                        buttonVI.RightButtons[0].State = ObjectState.Disabled;
                        buttonVI.RightButtons[0].Button.Caption = "买卖申请";
                    }
                    else
                    {
                        buttonVI.RightButtons[0].Button.Enabled = true;
                        buttonVI.RightButtons[0].State = ObjectState.Normal;

                        if (applyType == (int)EnumLibrary.IDOperationApplyType.Buy)
                            buttonVI.RightButtons[0].Button.Caption = "买入申请";
                        else if (applyType == (int)EnumLibrary.IDOperationApplyType.Sell)
                            buttonVI.RightButtons[0].Button.Caption = "卖出申请";
                        else if (applyType == (int)EnumLibrary.IDOperationApplyType.Both)
                            buttonVI.RightButtons[0].Button.Caption = "买卖申请";
                    }

                    if (accuracyEvaluateFlag)
                    {
                        buttonVI.RightButtons[1].Button.Enabled = true;
                        buttonVI.RightButtons[1].State = ObjectState.Normal;
                    }
                    else
                    {
                        buttonVI.RightButtons[1].Button.Enabled = false;
                        buttonVI.RightButtons[1].State = ObjectState.Disabled;
                    }

                    if (finishConfirmFlag)
                    {
                        buttonVI.RightButtons[2].Button.Enabled = true;
                        buttonVI.RightButtons[2].State = ObjectState.Normal;
                    }
                    else
                    {
                        buttonVI.RightButtons[2].Button.Enabled = false;
                        buttonVI.RightButtons[2].State = ObjectState.Disabled;
                    }
                }
                else
                {
                    buttonVI.RightButtons[0].Button.Enabled = false;
                    buttonVI.RightButtons[0].State = ObjectState.Disabled;

                    buttonVI.RightButtons[1].Button.Enabled = false;
                    buttonVI.RightButtons[1].State = ObjectState.Disabled;

                    buttonVI.RightButtons[2].Button.Enabled = false;
                    buttonVI.RightButtons[2].State = ObjectState.Disabled;
                }
            }
        }

        #endregion Master

        #region Detail

        private void DetailOperateButtonStatusSetting(DataRow drMaster, DataRow drDetail, ButtonEditViewInfo btnVI)
        {
            var currentStep = int.Parse(drMaster[colCurrentStep.FieldName].ToString());

            var voteStatus = int.Parse(drDetail[colVoteStatus_D.FieldName]?.ToString());
            var executeFlag = int.Parse(drDetail[colExecuteFlag_D.FieldName]?.ToString());
            var tradeRecordRelateFlag = bool.Parse(drDetail[colTradeRecordRelateFlag_D.FieldName].ToString());
            var accuracyStatus = int.Parse(drDetail[colAccuracyStatus_D.FieldName]?.ToString());
            var operateUser = drDetail[colOperateUser_D.FieldName].ToString();
            var stopFlag = bool.Parse(drDetail[colIsStopped_D.FieldName].ToString());
            var step = int.Parse(drDetail[colStep_D.FieldName].ToString());

            //var a = 1;
            //if (drDetail[colOperateNo_D.FieldName].ToString() == "CZ000139")
            //    a++;

            if (!stopFlag)
            {
                /// 按钮0：决策投票
                if (voteStatus == (int)EnumLibrary.IDOperationVoteStatus.None || voteStatus == (int)EnumLibrary.IDOperationVoteStatus.Proceed)
                {
                    btnVI.RightButtons[0].Button.Enabled = true;
                    btnVI.RightButtons[0].State = ObjectState.Normal;
                }
                else
                {
                    btnVI.RightButtons[0].Button.Enabled = false;
                    btnVI.RightButtons[0].State = ObjectState.Disabled;
                }

                ///// 按钮3：查看详情
                //btnVI.RightButtons[3].Button.Enabled = true;
                //btnVI.RightButtons[3].State = ObjectState.Normal;

                if (LoginInfo.CurrentUser.UserCode == operateUser || LoginInfo.CurrentUser.IsAdmin)
                {
                    /// 按钮1：执行\关联
                    if (voteStatus == (int)EnumLibrary.IDOperationVoteStatus.Passed && accuracyStatus == (int)EnumLibrary.IDOperationAccuracyStatus.None)
                    {
                        btnVI.RightButtons[1].Button.Enabled = true;
                        btnVI.RightButtons[1].State = ObjectState.Normal;
                    }
                    else
                    {
                        btnVI.RightButtons[1].Button.Enabled = false;
                        btnVI.RightButtons[1].State = ObjectState.Disabled;
                    }

                    /// 按钮2：准确度设定
                    if (accuracyStatus == (int)EnumLibrary.IDOperationAccuracyStatus.Proceed
                        || (accuracyStatus == (int)EnumLibrary.IDOperationAccuracyStatus.None && executeFlag == (int)EnumLibrary.IDOperationExecuteStatus.Unexecuted))
                    {
                        btnVI.RightButtons[2].Button.Enabled = true;
                        btnVI.RightButtons[2].State = ObjectState.Normal;
                    }
                    else
                    {
                        btnVI.RightButtons[2].Button.Enabled = false;
                        btnVI.RightButtons[2].State = ObjectState.Disabled;
                    }

                    /// 按钮4：强制中止
                    if (currentStep > step)
                    {
                        btnVI.RightButtons[4].Button.Enabled = false;
                        btnVI.RightButtons[4].State = ObjectState.Disabled;
                    }
                    else
                    {
                        btnVI.RightButtons[4].Button.Enabled = true;
                        btnVI.RightButtons[4].State = ObjectState.Normal;
                    }

                    /// 按钮5：删除
                    if (voteStatus == (int)EnumLibrary.IDOperationVoteStatus.None)
                    {
                        btnVI.RightButtons[5].Button.Enabled = true;
                        btnVI.RightButtons[5].State = ObjectState.Normal;
                    }
                    else
                    {
                        btnVI.RightButtons[5].Button.Enabled = false;
                        btnVI.RightButtons[5].State = ObjectState.Disabled;
                    }
                }
                else
                {
                    btnVI.RightButtons[1].Button.Enabled = false;
                    btnVI.RightButtons[1].State = ObjectState.Disabled;

                    btnVI.RightButtons[2].Button.Enabled = false;
                    btnVI.RightButtons[2].State = ObjectState.Disabled;

                    btnVI.RightButtons[4].Button.Enabled = false;
                    btnVI.RightButtons[4].State = ObjectState.Disabled;

                    btnVI.RightButtons[5].Button.Enabled = false;
                    btnVI.RightButtons[5].State = ObjectState.Disabled;
                }
            }
            else
            {
                btnVI.RightButtons[0].Button.Enabled = false;
                btnVI.RightButtons[0].State = ObjectState.Disabled;

                btnVI.RightButtons[1].Button.Enabled = false;
                btnVI.RightButtons[1].State = ObjectState.Disabled;

                btnVI.RightButtons[2].Button.Enabled = false;
                btnVI.RightButtons[2].State = ObjectState.Disabled;

                btnVI.RightButtons[4].Button.Enabled = false;
                btnVI.RightButtons[4].State = ObjectState.Disabled;

                btnVI.RightButtons[5].Button.Enabled = false;
                btnVI.RightButtons[5].State = ObjectState.Disabled;
            }
        }

        private void DisplayOperationDetail(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ViewDetail;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = $@"操作记录详情 - {operateNo}";
            dialog.ShowDialog();
        }

        private void OperationAccuracyVoteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.AccuracyDetermination;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = $@"操作记录准确度评定 - {operateNo}";
            dialog.ShowDialog();
        }

        private void OperationExecuteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ExecutionConfirm;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = $@"操作记录执行确认 - {operateNo}";
            dialog.ShowDialog();
        }

        private void OperationRelateProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ExecutionConfirm;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = $@"操作记录关联交易记录 - {operateNo}";
            dialog.ShowDialog();
        }

        private void OperationIDVoteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.RefreshEvent += new _dialogIDApplication.RefreshParentForm(BindApplicationInfo);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.OperationVote;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = $@"操作记录决策投票 - {operateNo}";
            dialog.ShowDialog();
        }

        private void OperationDeleteProcess(string applyNo, string operateNo)
        {
            if (DXMessage.ShowYesNoAndWarning($@"确定删除操作记录【{operateNo}】吗？") == DialogResult.Yes)
            {
                this._IDService.DeleteInvestmentDecisionOperation(applyNo, operateNo);

                BindApplicationInfo();
            }
        }

        private void OperationStopProcess(string operateNo)
        {
            if (DXMessage.ShowYesNoAndWarning($@"确定中止操作记录【{operateNo}】吗？") == DialogResult.Yes)
            {
                var dialog = this.CreateDialog<_dialogInputContent>();
                dialog.ReturnEvent += new _dialogInputContent.ReturnContentToParentForm(GetStopReason);
                dialog.ContentTitle = "中止理由";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this._IDService.StopInvestmentDecisionOperation(operateNo, this._stopReasonContent);

                    BindApplicationInfo();
                }
            }
        }

        private void GetStopReason(string content)
        {
            this._stopReasonContent = content?.Replace("'", "''");
        }

        #endregion Detail

        #endregion Utilities

        #region Methods

        public void BindApplicationInfo()
        {
            this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            string commandText = @" EXEC [dbo].[sp_GetIDApplicationAndIDOperation] ";

            string whereCondition = $@" @ApplyDateFrom = '{CommonHelper.StringToDateTime(this.deFrom.EditValue.ToString())}'
                                                            ,@ApplyDateTo= '{CommonHelper.StringToDateTime(this.deTo.EditValue.ToString())}'
                                                            ,@Applyuser = '{this.luApplyUser.SelectedValue()}'
                                                            ,@StockCode = '{this.luStock.SelectedValue()}' ";

            switch (CurrentQueryMode)
            {
                case QueryMode.Proceed:
                    whereCondition += $@" ,@Status = {(int)EnumLibrary.IDApplicationStatus.Proceed}  ";
                    break;

                case QueryMode.Done:
                    whereCondition += $@" ,@Status = {(int)EnumLibrary.IDApplicationStatus.Done}  ";
                    break;

                case QueryMode.All:
                    break;

                default:
                    break;
            }

            commandText = commandText + whereCondition;

            var ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);

            this._isGridDataSourceChanged = false;

            if (ds == null)
                this.gridApplication.DataSource = null;
            else if (ds != null && ds.Tables.Count == 2)
            {
                ds.Relations.Add("MD", ds.Tables[0]?.Columns["ApplyNo"], ds.Tables[1]?.Columns["ApplyNo"]);

                this.gridApplication.DataSource = ds.Tables[0];

                this.lciExpand.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                this.btnExpandOrCollapse.Text = "全部收起";
                //this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
                //this.viewMaster.SetAllRowsExpanded(_isExpanded);

                this._isGridDataSourceChanged = true;
                this.viewMaster.FocusedRowHandle = _defaultMasterFocusedRowHandle;
            }
        }

        #endregion Methods

        #region Events

        private void FrmStockInvestmentDecision_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindApplicationInfo();

                TimerInit();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BindApplicationInfo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;

                BindApplicationInfo();
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

                this.viewMaster.SetAllRowsExpanded(false);

                // this.viewMaster.SetAllRowsExpanded(!_isExpanded);

                //this._isExpanded = !_isExpanded;
                //this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
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

        private void viewMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (_isGridDataSourceChanged)
            {
                this.viewMaster.SetMasterRowExpanded(_defaultMasterFocusedRowHandle, false);
                this.viewMaster.SetMasterRowExpanded(e.FocusedRowHandle, true);
                _defaultMasterFocusedRowHandle = e.FocusedRowHandle;
            }
        }

        private void viewMaster_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void viewMaster_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            
            if (e.ListSourceRowIndex > -1)
            {
                if (e.Column == this.colIdentify)
                {
                    var applyNo = this.viewMaster.GetRowCellValue(e.ListSourceRowIndex, this.colApplyNo).ToString();
                    
                    if (!string.IsNullOrEmpty(applyNo))
                        SetApplicationIdentify(e, applyNo);
                }
            }
        }

        private void viewMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == colOperate.Name)
            {
                var masterView = sender as GridView;
                DataRow dr = masterView.GetDataRow(e.RowHandle);
                
                if (dr != null)
                {
                    ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;

                    OperateButtonStatusSetting(dr, buttonVI);
                }
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
                    dialog.ApplyType = (EnumLibrary.IDOperationApplyType)int.Parse(dr?[colApplyType.FieldName].ToString());
                    dialog.ApplyNo = applyNo;
                    dialog.OperateNo = string.Empty;
                    dialog.OperateStep = int.Parse(dr?[colCurrentStep.FieldName].ToString());
                    dialog.Text = $@"投资决策交易操作申请 - {applyNo}";
                    dialog.ShowDialog();
                }
                else if (buttonTag == "Accuracy")
                {
                    var operateNo = dr?[colAccuracyEvaluateOperateNo.FieldName]?.ToString();

                    if (!string.IsNullOrEmpty(operateNo))
                        OperationAccuracyVoteProcess(applyNo, operateNo);
                }
                else if (buttonTag == "Finish")
                {
                    if (DXMessage.ShowYesNoAndTips("该决策申请是否已经完结？确认后该申请将不可继续操作！") == System.Windows.Forms.DialogResult.Yes)
                    {
                        _IDService.UpdateIDApplicationStatus(applyNo, (int)EnumLibrary.IDApplicationStatus.Done);
                        BindApplicationInfo();
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

        #endregion MasterView

        #region DetailView

        private void viewDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (CurrentQueryMode == QueryMode.Done) return;

            if (e.Column.Name == colOperate_D.Name)
            {
                var currentDetailView = sender as GridView;
                DataRow drDetail = currentDetailView.GetDataRow(e.RowHandle);

                DataRow drMaster = (currentDetailView.SourceRow as DataRowView).Row;

                if (drDetail != null && drMaster != null)
                {
                    ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;
                    DetailOperateButtonStatusSetting(drMaster, drDetail, buttonVI);
                }
            }
        }

        private void viewDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void viewDetail_GotFocus(object sender, EventArgs e)
        {
            var currentDetailView = gridApplication.FocusedView as GridView;
            this.viewMaster.FocusedRowHandle = currentDetailView.SourceRowHandle;
        }

        private void ribtnOperate_D_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var masterRowHandle = this.viewMaster.FocusedRowHandle;
                var relationIndex = this.viewMaster.GetRelationIndex(masterRowHandle, "MD");
                var detailView = this.viewMaster.GetDetailView(masterRowHandle, relationIndex) as DevExpress.XtraGrid.Views.Grid.GridView;

                DataRow dr = detailView?.GetDataRow(detailView.FocusedRowHandle);

                var applyNo = dr?["ApplyNo"]?.ToString();

                var operateNo = dr?["OperateNo"]?.ToString();

                if (string.IsNullOrEmpty(applyNo) || string.IsNullOrEmpty(operateNo)) return;

                var buttonTag = e.Button.Tag?.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                switch (buttonTag)
                {
                    case "IDVote":
                        OperationIDVoteProcess(applyNo, operateNo);
                        break;

                    case "Execute":
                        OperationExecuteProcess(applyNo, operateNo);
                        break;

                    case "Relate":
                        OperationRelateProcess(applyNo, operateNo);
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

                    case "Stop":
                        OperationStopProcess(operateNo);
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

        private void ribtnView_D_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var masterRowHandle = this.viewMaster.FocusedRowHandle;
                var relationIndex = this.viewMaster.GetRelationIndex(masterRowHandle, "MD");
                var detailView = this.viewMaster.GetDetailView(masterRowHandle, relationIndex) as DevExpress.XtraGrid.Views.Grid.GridView;

                DataRow dr = detailView?.GetDataRow(detailView.FocusedRowHandle);

                var applyNo = dr?["ApplyNo"]?.ToString();

                var operateNo = dr?["OperateNo"]?.ToString();

                if (string.IsNullOrEmpty(applyNo) || string.IsNullOrEmpty(operateNo)) return;

                var buttonTag = e.Button.Tag?.ToString().Trim();

                if (!string.IsNullOrEmpty(buttonTag) && buttonTag == "View")

                    DisplayOperationDetail(applyNo, operateNo);
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