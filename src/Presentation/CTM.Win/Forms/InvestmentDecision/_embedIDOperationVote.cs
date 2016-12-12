using System;
using System.Data;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDOperationVote : BaseForm
    {
        #region Properties

        public string SerialNo { get; set; }

        #endregion Properties

        #region Constructors

        public _embedIDOperationVote()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void DisplayResult()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"EXEC [dbo].[sp_GetIDVoteResult] @FormSerialNo = 'SQ161024002'";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            var source = ds.Tables[0];
            this.gridControl1.DataSource = source;
        }

        #endregion Utilities

        #region Events

        private void _dialogIDVoteResult_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridView1.SetLayout(showCheckBoxRowSelect: false, showFilterPanel: false, showAutoFilterRow: false, rowIndicatorWidth: 50);
                this.gridView1.OptionsView.RowAutoHeight = true;
                this.lciResult.Text = $@"交易单 [{SerialNo}] 投票结果";

                DisplayResult();
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

        #endregion Events
    }
}