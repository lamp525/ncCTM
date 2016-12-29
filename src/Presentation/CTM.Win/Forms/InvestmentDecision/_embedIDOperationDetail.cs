using System;
using System.Data;
using CTM.Core;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDOperationDetail : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Properties

        public string OperateNo { get; set; }

        #endregion Properties

        #region Constructors

        public _embedIDOperationDetail(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.gvIDVote.SetLayout(showCheckBoxRowSelect: false, showAutoFilterRow: false, columnAutoWidth: true, rowIndicatorWidth: 35);
            this.gvRecord.SetLayout(showCheckBoxRowSelect: false, showAutoFilterRow: false, columnAutoWidth: true, rowIndicatorWidth: 35);
            this.gvAccuracy.SetLayout(showCheckBoxRowSelect: false, showAutoFilterRow: false, columnAutoWidth: true, rowIndicatorWidth: 35);
        }

        private void BindDetail()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"EXEC [dbo].[sp_GetIDOperationDetail] @OperateNo ='{OperateNo}'";

            var dsDetail = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (dsDetail == null || dsDetail.Tables.Count != 4) return;

            DataRow drOperation = dsDetail.Tables[0].Rows?[0];

            if (drOperation != null)
            {
                this.esiIDVote.Text = $@"投票状态：{drOperation["VoteStatusName"]}     投票分数：{drOperation["VotePoint"]}";
                this.gcIDVote.DataSource = dsDetail.Tables[1];

                if (int.Parse(drOperation["VoteStatus"].ToString()) == (int)EnumLibrary.IDOperationVoteStatus.None
                    || int.Parse(drOperation["VoteStatus"].ToString()) == (int)EnumLibrary.IDOperationVoteStatus.Proceed)
                {
                    lcgRecord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    lcgRecord.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    this.esiRecord.Text = $@"执行状态：{drOperation["ExecuteFlagName"]}     交易关联标志：{drOperation["RelateFlagName"]}";

                    this.gcRecord.DataSource = dsDetail.Tables[2];
                }

                if (int.Parse(drOperation["ExecuteFlag"].ToString()) == (int)EnumLibrary.IDOperationExecuteStatus.Unexecuted)
                {
                    this.lcgAccuracy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.esiAccuracy.Text = $@"评定状态：{drOperation["AccuracyStatusName"]}     评定分数：{drOperation["AccuracyPoint"]}";
                    this.gcAccuracy.DataSource = dsDetail.Tables[3];
                }
                else
                {
                    this.lcgAccuracy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
        }

        #endregion Utilities

        #region Events

        private void _embedIDOperationDetail_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindDetail();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gvIDVote_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvRecord_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvAccuracy_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}