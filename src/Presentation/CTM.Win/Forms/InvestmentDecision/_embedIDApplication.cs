using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Infrastructure;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Forms.Common;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
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

        private IList<InvestmentDecisionVote> _myVotes = null;
        private string _voteReason = null;

        private const string _layoutXmlName = "FrmInvestmentDecisionManage";

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
            IUserService userService)
        {
            InitializeComponent();

            this._IDService = IDService;
            this._commonService = commonService;
            this._userService = userService;
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

            var operators = _userService.GetAllOperators(true);

            this.viewMaster.LoadLayout(_layoutXmlName);
            this.viewMaster.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: true, showFilterPanel: true, showAutoFilterRow: true, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.viewMaster.Columns)
            {
                column.OptionsColumn.AllowEdit = column == this.colOperate ? true : false;
            }

            this.viewDetail.SetLayout(showCheckBoxRowSelect: false, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 25);

            foreach (GridColumn column in this.viewDetail.Columns)
            {
                column.OptionsColumn.AllowEdit = column == this.colOperate_D ? true : false;
            }
        }

        private void GetVoteReason(string content)
        {
            this._voteReason = content;
        }

        private bool VoteProcess(string formSerialNo, string buttonTag)
        {
            EnumLibrary.IDVoteFlag voteFlag = EnumLibrary.IDVoteFlag.None;

            switch (buttonTag)
            {
                case "Approval":
                    voteFlag = EnumLibrary.IDVoteFlag.Approval;
                    break;

                case "Oppose":
                    voteFlag = EnumLibrary.IDVoteFlag.Oppose;
                    break;

                case "Abstain":
                    voteFlag = EnumLibrary.IDVoteFlag.Abstain;
                    break;

                case "Revoke":
                    voteFlag = EnumLibrary.IDVoteFlag.None;
                    break;

                default:
                    voteFlag = EnumLibrary.IDVoteFlag.None;
                    break;
            }

            if (voteFlag == EnumLibrary.IDVoteFlag.Approval || voteFlag == EnumLibrary.IDVoteFlag.Oppose || voteFlag == EnumLibrary.IDVoteFlag.Abstain)
            {
                var dialog = this.CreateDialog<_dialogInputContent>();
                dialog.ReturnEvent += new _dialogInputContent.ReturnContentToParentForm(GetVoteReason);
                dialog.ContentTitle = CTMHelper.GetIDVoteFlagName((int)voteFlag) + "理由";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return false;
            }
            else
            {
                if (DXMessage.ShowYesNoAndTips("确定撤销上次投票结果么？") == DialogResult.No)
                    return false;
            }

            _IDService.InvestmentDecisionVoteProcess(LoginInfo.CurrentUser.UserCode, formSerialNo, voteFlag, _voteReason);

            return true;
        }

        private void DisplayVoteResult(string formSerialNo)
        {
            var dialog = EngineContext.Current.Resolve<_dialogIDVoteResult>();
            dialog.Owner = this.ParentForm;
            dialog.Text = "交易申请单投票结果";
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.SerialNo = formSerialNo;
            dialog.Show();
        }

        private void VoteButtonStatusSetting(DataRow dr, ButtonEditViewInfo buttonVI)
        {
            var status = int.Parse(dr[colStatus.FieldName]?.ToString());
            var serialNo = dr[colApplyNo.FieldName]?.ToString();
            var applyUser = dr[colApplyUser.FieldName]?.ToString();

            //申请单已提交或投票进行中
            if (status == (int)EnumLibrary.IDFormStatus.Submited || status == (int)EnumLibrary.IDFormStatus.Proceed)
            {
                //当前用户为申请者
                if (applyUser == LoginInfo.CurrentUser.UserCode)
                {
                    foreach (EditorButtonObjectInfoArgs args in buttonVI.RightButtons)
                    {
                        if (args.Button.Tag.ToString() == "View")
                        {
                            args.Button.Enabled = true;
                            args.State = ObjectState.Normal;
                        }
                        else
                        {
                            args.Button.Enabled = false;
                            args.State = ObjectState.Disabled;
                        }
                    }
                }
                //当前用户不为申请者
                else
                {
                    //查看按钮
                    buttonVI.RightButtons[4].Button.Enabled = true;
                    buttonVI.RightButtons[4].State = ObjectState.Normal;

                    var myVoteInfo = _myVotes.FirstOrDefault(x => x.FormSerialNo == serialNo && x.UserCode == LoginInfo.CurrentUser.UserCode);

                    //未投票
                    if (myVoteInfo == null || myVoteInfo.Flag == (int)EnumLibrary.IDVoteFlag.None)
                    {
                        buttonVI.RightButtons[0].Button.Enabled = true;
                        buttonVI.RightButtons[0].State = ObjectState.Normal;

                        buttonVI.RightButtons[1].Button.Enabled = true;
                        buttonVI.RightButtons[1].State = ObjectState.Normal;

                        buttonVI.RightButtons[2].Button.Enabled = true;
                        buttonVI.RightButtons[2].State = ObjectState.Normal;

                        buttonVI.RightButtons[3].Button.Enabled = false;
                        buttonVI.RightButtons[3].State = ObjectState.Disabled;

                        ////查看按钮
                        //buttonVI.RightButtons[4].Button.Enabled = false;
                        //buttonVI.RightButtons[4].State = ObjectState.Disabled;
                    }
                    //已投票
                    else
                    {
                        buttonVI.RightButtons[0].Button.Enabled = false;
                        buttonVI.RightButtons[0].State = ObjectState.Disabled;

                        buttonVI.RightButtons[1].Button.Enabled = false;
                        buttonVI.RightButtons[1].State = ObjectState.Disabled;

                        buttonVI.RightButtons[2].Button.Enabled = false;
                        buttonVI.RightButtons[2].State = ObjectState.Disabled;

                        //撤销按钮
                        buttonVI.RightButtons[3].Button.Enabled = true;
                        buttonVI.RightButtons[3].State = ObjectState.Normal;

                        ////查看按钮
                        //buttonVI.RightButtons[4].Button.Enabled = true;
                        //buttonVI.RightButtons[4].State = ObjectState.Normal;

                        //bool canRevoke = true;
                        //if (_commonService.GetCurrentServerTime() - myVoteinfo.VoteTime > new TimeSpan(0, 5, 0))
                        //    canRevoke = false;

                        //if (canRevoke)
                        //{
                        //    //撤销按钮
                        //    buttonVI.RightButtons[3].Button.Enabled = true;
                        //    buttonVI.RightButtons[3].State = ObjectState.Normal;

                        //    //查看按钮
                        //    buttonVI.RightButtons[4].Button.Enabled = false;
                        //    buttonVI.RightButtons[4].State = ObjectState.Disabled;
                        //}
                        //else
                        //{
                        //    //撤销按钮
                        //    buttonVI.RightButtons[3].Button.Enabled = false;
                        //    buttonVI.RightButtons[3].State = ObjectState.Disabled;

                        //    //查看按钮
                        //    buttonVI.RightButtons[4].Button.Enabled = true;
                        //    buttonVI.RightButtons[4].State = ObjectState.Normal;
                        //}
                    }
                }
            }
            //申请单投票完成
            else if (status == (int)EnumLibrary.IDFormStatus.Denied || status == (int)EnumLibrary.IDFormStatus.Passed)
            {
                buttonVI.RightButtons[0].Button.Enabled = false;
                buttonVI.RightButtons[0].State = ObjectState.Disabled;

                buttonVI.RightButtons[1].Button.Enabled = false;
                buttonVI.RightButtons[1].State = ObjectState.Disabled;

                buttonVI.RightButtons[2].Button.Enabled = false;
                buttonVI.RightButtons[2].State = ObjectState.Disabled;

                buttonVI.RightButtons[3].Button.Enabled = false;
                buttonVI.RightButtons[3].State = ObjectState.Disabled;

                buttonVI.RightButtons[4].Button.Enabled = true;
                buttonVI.RightButtons[4].State = ObjectState.Normal;
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
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.AccuracyDetermination;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录准确度评定";
            dialog.ShowDialog();
        }

        private void OperationExecuteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
            dialog.CurrentPageMode = _dialogIDApplication.PageMode.ExecutionConfirm;
            dialog.ApplyNo = applyNo;
            dialog.OperateNo = operateNo;
            dialog.Text = "操作记录执行确认";
            dialog.ShowDialog();
        }

        private void OperationIDVoteProcess(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
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

            _myVotes = _IDService.GetInvestmentDecisionVotes(LoginInfo.CurrentUser.UserCode);
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

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.viewMaster.SaveLayout(_layoutXmlName);
        }

        #region MasterView

        private void viewMaster_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
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

                if (buttonTag == "Delete")
                {
                    if (DXMessage.ShowYesNoAndWarning("确定删除该申请单吗？") == DialogResult.Yes)
                    {
                        this._IDService.DeleteInvestmentDecisionForm(applyNo);

                        BindApplicationInfo();
                    }
                }
                else if (buttonTag == "Apply")
                {
                    var dialog = this.CreateDialog<_dialogIDApplication>(borderStyle: FormBorderStyle.Sizable);
                    dialog.CurrentPageMode = _dialogIDApplication.PageMode.NewOperation;
                    dialog.ApplyNo = applyNo;
                    dialog.OperateNo = string.Empty;
                    dialog.Text = "投资决策操作申请";
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