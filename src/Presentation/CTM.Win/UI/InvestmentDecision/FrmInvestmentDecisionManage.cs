using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.UI.Common;
using CTM.Win.Util;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class FrmInvestmentDecisionManage : BaseForm
    {
        private readonly IInvestmentDecisionService _IDService;

        private IList<InvestmentDecisionVote> _currentUserVotes = null;
        private string _voteReason = null;

        private const string _layoutXmlName = "FrmInvestmentDecisionManage";

        public FrmInvestmentDecisionManage(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        private void SetGridViewLayout()
        {
            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showCheckBoxRowSelect: true, editable: true, readOnly: false, showFilterPanel: true, showAutoFilterRow: false, rowIndicatorWidth: 50);

            foreach (GridColumn column in this.gridView1.Columns)
            {
                if (column.Name == this.colOperate.Name) continue;

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
        }

        private void GetCurrentUserVoteInfo()
        {
            _currentUserVotes = _IDService.GetInvestmentDecisionVotes(LoginInfo.CurrentUser.UserCode);
        }

        private void GetVoteReason(string content)
        {
            this._voteReason = content;
        }

        private void FrmInvestmentDecisionMange_Load(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                SetGridViewLayout();

                BindApplicationInfo();

                GetCurrentUserVoteInfo();
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

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的信息吗？") == DialogResult.Yes)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
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

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void repositoryItemBtnApproval_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                e.Button.Enabled = false;

                var myView = this.gridView1;
                DataRow dr = myView.GetDataRow(myView.FocusedRowHandle);

                var formSerialNo = dr?["SerialNo"]?.ToString();

                if (string.IsNullOrEmpty(formSerialNo)) return;

                var buttonTag = e.Button.Tag.ToString().Trim();

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

                if (voteFlag == EnumLibrary.IDVoteFlag.Approval || voteFlag == EnumLibrary.IDVoteFlag.Oppose)
                {
                    var dialog = this.CreateDialog<_dialogInputContent>();
                    dialog.ReturnEvent += new _dialogInputContent.ReturnContentToParentForm(GetVoteReason);
                    dialog.ContentTitle = CTMHelper.GetIDVoteFlagName((int)voteFlag) + "理由";
                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;
                }

                _IDService.InvestmentDecisionVoteProcess(LoginInfo.CurrentUser.UserCode, formSerialNo, voteFlag, _voteReason);
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

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            //GridView view = sender as GridView;
            //ButtonEdit editor = (view.ActiveEditor as ButtonEdit);
            //if (editor == null) return;
            //ButtonEditViewInfo info = editor.GetViewInfo() as ButtonEditViewInfo;
            //Point mousePoint = gridControl1.PointToClient(MousePosition);
            //Point clickPoint = new Point(mousePoint.X - editor.Location.X, 1);
            //for (int i = 0; i < info.LeftButtons.Count; i++)
            //{
            //    EditorButtonObjectInfoArgs btn = info.LeftButtons[i];
            //    bool inBounds = info.LeftButtons[0].Bounds.Contains(clickPoint);
            //    if (inBounds)
            //        editor.PerformClick(btn.Button);
            //}
            //for (int i = 0; i < info.RightButtons.Count; i++)
            //{
            //    EditorButtonObjectInfoArgs btn = info.RightButtons[i];
            //    bool inBounds = info.RightButtons[0].Bounds.Contains(clickPoint);
            //    if (inBounds)
            //        editor.PerformClick(btn.Button);
            //}
        }
    }
}