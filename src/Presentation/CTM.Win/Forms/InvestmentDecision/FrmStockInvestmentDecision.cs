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
    public partial class FrmStockInvestmentDecision : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;
        private readonly ICommonService _commonService;

        private IList<InvestmentDecisionVote> _myVotes = null;
        private string _voteReason = null;

        private const string _layoutXmlName = "FrmInvestmentDecisionManage";

        #endregion Fields

        #region Constructors

        public FrmStockInvestmentDecision(IInvestmentDecisionService IDService, ICommonService commonService)
        {
            InitializeComponent();

            this._IDService = IDService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.lblHint.Text = string.Empty;

            //if (LoginInfo.CurrentUser.IsAdmin)
            //    this.lciDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //else
                this.lciDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            this.btnDelete.Enabled = false;

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showCheckBoxRowSelect: LoginInfo.CurrentUser.IsAdmin, editable: true, editorShowMode: DevExpress.Utils.EditorShowMode.MouseDown, readOnly: false, showGroupPanel: true, showFilterPanel: false, showAutoFilterRow: true, rowIndicatorWidth: 40);

            foreach (GridColumn column in this.gridView1.Columns)
            {
                if (column.Name == this.colVote.Name || column.Name == this.colOperate.Name)
                    column.OptionsColumn.AllowEdit = true;
                else
                    column.OptionsColumn.AllowEdit = false;
            }
        }

        private void BindApplicationInfo()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "[sp_GetInvestmentDecisionForm]");

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;

            _myVotes = _IDService.GetInvestmentDecisionVotes(LoginInfo.CurrentUser.UserCode);
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
            var serialNo = dr[colSerialNo.FieldName]?.ToString();
            var applyUser = dr[colApplyUser.FieldName]?.ToString();

            var myVoteinfo = _myVotes.FirstOrDefault(x => x.FormSerialNo == serialNo && x.UserCode == LoginInfo.CurrentUser.UserCode);

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

                    //未投票
                    if (myVoteinfo == null || myVoteinfo.Flag == (int)EnumLibrary.IDVoteFlag.None)
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

        #endregion Utilities

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;
                var dialog = this.CreateDialog<_dialogTradeApplication>();
                dialog.RefreshEvent += new _dialogTradeApplication.RefreshParentForm(BindApplicationInfo);
                dialog.Text = "股票投资交易申请";

                dialog.ShowDialog();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的申请单吗？") == DialogResult.Yes)
                {
                    var serialNos = new List<string>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        serialNos.Add(myView.GetRowCellValue(selectedHandles[rowhandle], colSerialNo).ToString());
                    }

                    this._IDService.DeleteInvestmentDecisionForm(serialNos.ToArray());

                    BindApplicationInfo();
                }
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

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length > 0)
                this.btnDelete.Enabled = true;
            else
                this.btnDelete.Enabled = false;
        }

          private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void riButtonEditVote_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.gridView1;
                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var formSerialNo = dr?[colSerialNo.FieldName]?.ToString();

                if (string.IsNullOrEmpty(formSerialNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

                if (string.IsNullOrEmpty(buttonTag)) return;

                if (buttonTag == "View")
                {
                    DisplayVoteResult(formSerialNo);
                }
                else
                {
                    if (VoteProcess(formSerialNo, buttonTag))
                        BindApplicationInfo();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                e.Button.Enabled = true;
                _voteReason = null;
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
                    if (DXMessage.ShowYesNoAndWarning("确定删除该申请单吗？") == DialogResult.Yes)
                    {
                        this._IDService.DeleteInvestmentDecisionForm(serialNo);

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

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var myView = sender as GridView;

            DataRow dr = myView.GetDataRow(e.RowHandle);

            if (dr == null) return;

            //投票
            if (e.Column.Name == colVote.Name)
            {
                ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;

                VoteButtonStatusSetting(dr, buttonVI);
            }
            //操作
            else if (e.Column.Name == colOperate.Name)
            {
                ButtonEditViewInfo buttonVI = (ButtonEditViewInfo)((GridCellInfo)e.Cell).ViewInfo;

                OperateButtonStatusSetting(dr, buttonVI);
            }
        }

        #endregion Events
    }
}