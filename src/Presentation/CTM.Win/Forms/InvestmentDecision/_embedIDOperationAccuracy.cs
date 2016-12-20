using System;
using System.Data;
using CTM.Core;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Forms.Common;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDOperationAccuracy : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;

        private bool _voteSucceedFlag = false;
        private string _reasonContent = null;

        #endregion Fields

        #region Properties

        public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        public bool VoteSucceedFlag
        {
            get { return _voteSucceedFlag; }
            set { _voteSucceedFlag = value; }
        }

        #endregion Properties

        #region Constructors

        public _embedIDOperationAccuracy(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.lcgResult.Text = $@"操作记录[{OperateNo}] - 准确度投票结果";

            this.gridView1.SetLayout(showCheckBoxRowSelect: false, showFilterPanel: false, showAutoFilterRow: false, columnAutoWidth: false, rowIndicatorWidth: 35);
            this.gridView1.OptionsView.RowAutoHeight = true;

            if (LoginInfo.CurrentUser.IsAdmin)
            {
                this.lciAdminVeto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.lciAdminVeto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void SetVoteButtonStatus()
        {
            if (this.chkAdminVeto.Checked && this.lciAdminVeto.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                var adminVetoInfo = _IDService.GetIDOperationAccuracyAdminVetoInfo(OperateNo);

                if (adminVetoInfo == null || adminVetoInfo.Flag == (int)EnumLibrary.IDVoteFlag.None)
                {
                    this.btnApproval.Enabled = true;
                    this.btnOppose.Enabled = true;
                    this.btnRevoke.Enabled = false;
                }
                else
                {
                    this.btnApproval.Enabled = false;
                    this.btnOppose.Enabled = false;
                    this.btnRevoke.Enabled = true;
                }
            }
            else
            {
                var myVoteInfo = _IDService.GetIDOperationAccuracyInfo(LoginInfo.CurrentUser.UserCode, OperateNo);

                //未投票
                if (myVoteInfo == null || myVoteInfo.Flag == (int)EnumLibrary.IDVoteFlag.None)
                {
                    this.btnApproval.Enabled = true;
                    this.btnOppose.Enabled = true;
                    this.btnRevoke.Enabled = false;
                }
                //已投票
                else
                {
                    this.btnApproval.Enabled = false;
                    this.btnOppose.Enabled = false;
                    this.btnRevoke.Enabled = true;
                }
            }
        }

        private void DisplayResult()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var voteStatusCommandText = $@"SELECT * FROM [dbo].[v_IDOperation] WHERE OperateNo = '{OperateNo}'";

            var dsStatus = SqlHelper.ExecuteDataset(connString, CommandType.Text, voteStatusCommandText);

            var drVoteStatusInfo = dsStatus?.Tables?[0].Rows?[0];

            if (drVoteStatusInfo != null)
            {
                this.esiVoteStatusInfo.Text = $@"投票状态：{drVoteStatusInfo["AccuracyStatusName"]}    投票分数：{drVoteStatusInfo["AccuracyPoint"]}";
            }

            var resultCommandText = $@"SELECT * FROM [dbo].[v_IDOperationAccuracy] WHERE OperateNo = '{OperateNo}' ORDER BY IsAdminVeto DESC, InvestorName";

            var dsResult = SqlHelper.ExecuteDataset(connString, CommandType.Text, resultCommandText);

            if (dsResult == null || dsResult.Tables.Count == 0) return;

            var source = dsResult.Tables[0];
            this.gridControl1.DataSource = source;
        }

        private void RefreshForm()
        {
            SetVoteButtonStatus();
            DisplayResult();
        }

        private void GetVoteReason(string content)
        {
            this._reasonContent = content?.Replace("'", "''");
        }

        private bool VoteProcess(EnumLibrary.IDVoteFlag voteFlag)
        {
            if (voteFlag == EnumLibrary.IDVoteFlag.None)
            {
                if (DXMessage.ShowYesNoAndTips("确定撤销上次投票结果么？") == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
            }
            else
            {
                var dialog = this.CreateDialog<_dialogInputContent>();
                dialog.ReturnEvent += new _dialogInputContent.ReturnContentToParentForm(GetVoteReason);
                dialog.ContentTitle = "判定理由";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return false;
                }
            }

            _IDService.IDOperationAccuracyProcess(LoginInfo.CurrentUser.UserCode, ApplyNo, OperateNo, voteFlag, _reasonContent, this.chkAdminVeto.Checked);

            this._reasonContent = null;

            RefreshForm();

            return true;
        }

        #endregion Utilities

        #region Events

        private void _dialogIDVoteResult_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                DisplayResult();

                SetVoteButtonStatus();
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

        /// <summary>
        /// 赞同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApproval_Click(object sender, EventArgs e)
        {
            var currentButton = sender as DevExpress.XtraEditors.SimpleButton;
            try
            {
                currentButton.Enabled = false;
                this._voteSucceedFlag = VoteProcess(EnumLibrary.IDVoteFlag.Approval);
                if (!this._voteSucceedFlag)
                    currentButton.Enabled = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 反对
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOppose_Click(object sender, EventArgs e)
        {
            var currentButton = sender as DevExpress.XtraEditors.SimpleButton;
            try
            {
                currentButton.Enabled = false;
                this._voteSucceedFlag = VoteProcess(EnumLibrary.IDVoteFlag.Oppose);
                if (!this._voteSucceedFlag)
                    currentButton.Enabled = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevoke_Click(object sender, EventArgs e)
        {
            var currentButton = sender as DevExpress.XtraEditors.SimpleButton;
            try
            {
                currentButton.Enabled = false;
                this._voteSucceedFlag = VoteProcess(EnumLibrary.IDVoteFlag.None);

                if (!this._voteSucceedFlag)
                    currentButton.Enabled = true;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void chkAdminVeto_CheckedChanged(object sender, EventArgs e)
        {
            SetVoteButtonStatus();
        }

        #endregion Events
    }
}