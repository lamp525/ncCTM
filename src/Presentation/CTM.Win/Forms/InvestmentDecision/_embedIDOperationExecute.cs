using System;
using System.Data;
using CTM.Core;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraEditors;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedIDOperationExecute : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;
        private bool _succeedFlag = false;

        #endregion Fields

        #region Properties

        public string ApplyNo { get; set; }

        public string OperateNo { get; set; }

        public bool ProcessSucceedFlag
        {
            get { return _succeedFlag; }
            set { _succeedFlag = value; }
        }

        #endregion Properties

        #region Constructors

        public _embedIDOperationExecute(IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.gridView1.SetLayout(showCheckBoxRowSelect: false, showGroupPanel: true, columnAutoWidth: true, rowIndicatorWidth: 40);

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"SELECT * FROM [dbo].[v_IDOperation] WHERE OperateNo = '{OperateNo}'";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            var drOperation = ds?.Tables?[0].Rows?[0];

            if (drOperation == null)
            {
                this.chkNo.Checked = true;
            }
            else
            {
                var executeFlag = int.Parse(drOperation["ExecuteFlag"].ToString());
                if (executeFlag == (int)EnumLibrary.IDOperationExecuteStatus.Executed)
                {
                    this.chkYes.Checked = true;

                    BindRelatedRecord();
                }
                else
                    this.chkNo.Checked = true;
            }
        }

        private void BindRelatedRecord()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var relateRecordCommandText = $@"EXEC [dbo].[sp_GetIDOperationTradeRecord] @OperateNo = '{OperateNo}'";

            var dsRecords = SqlHelper.ExecuteDataset(connString, CommandType.Text, relateRecordCommandText);

            this.gridControl1.DataSource = dsRecords?.Tables?[0];
        }

        private void DisplayRecordRelatePanel()
        {
            if (this.chkYes.Checked)
                lcgRecord.Enabled = true;
            else
                lcgRecord.Enabled = false;
        }

        private void DisplayRecordSelectForm()
        {
            var dialog = this.CreateDialog<_dialogIDOperationRelateRecord>();
            dialog.RefreshEvent += new _dialogIDOperationRelateRecord.RefreshParentForm(BindRelatedRecord);
            dialog.ApplyNo = ApplyNo;
            dialog.OperateNo = OperateNo;
            dialog.Text = $@"实际交易记录关联 - {OperateNo}";
            dialog.ShowDialog();
        }

        private void ExecuteComfirmProcess()
        {
            if (this.chkNo.Checked)
            {
                DataTable dtRecords = this.gridControl1.DataSource as DataTable;
                if (dtRecords != null && dtRecords.Rows.Count > 0)
                    if (DXMessage.ShowYesNoAndWarning("该决策操作已经关联交易记录，继续处理将取消关联！") == System.Windows.Forms.DialogResult.No)
                        return;
            }

            var executeFlag = this.chkNo.Checked ? (int)EnumLibrary.IDOperationExecuteStatus.Unexecuted : (int)EnumLibrary.IDOperationExecuteStatus.Executed;

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"EXEC [dbo].[sp_IDOperationExecuteProcess] @OperateNo = '{OperateNo}' , @ExecuteFlag = {executeFlag}";

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, commandText);

            DXMessage.ShowTips("处理成功！");
        }

        #endregion Utilities

        #region Events

        private void _embedIDOperationExecute_Load(object sender, EventArgs e)
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

        private void chkNo_CheckedChanged(object sender, EventArgs e)
        {
            this.chkYes.Checked = !this.chkNo.Checked;

            DisplayRecordRelatePanel();

            if (this.chkNo.Checked)
                ExecuteComfirmProcess();
        }

        private void chkYes_CheckedChanged(object sender, EventArgs e)
        {
            this.chkNo.Checked = !this.chkYes.Checked;

            DisplayRecordRelatePanel();

            if (this.chkYes.Checked)
                ExecuteComfirmProcess();
        }

        private void btnRelate_Click(object sender, EventArgs e)
        {
            var currentBtn = sender as SimpleButton;
            try
            {
                currentBtn.Enabled = false;
                DisplayRecordSelectForm();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                currentBtn.Enabled = true;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}