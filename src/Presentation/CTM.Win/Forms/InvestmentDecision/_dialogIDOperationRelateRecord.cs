using System;
using System.Data;
using CTM.Data;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogIDOperationRelateRecord : BaseForm
    {
        #region Properties

        public string OperateNo { get; set; }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogIDOperationRelateRecord()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void BindRecord()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var relateRecordCommandText = $@"EXEC [dbo].[sp_GetIDOperationRelateRecord] @OperateNo = '{OperateNo}'";

            var dsRecords = SqlHelper.ExecuteDataset(connString, CommandType.Text, relateRecordCommandText);

            this.gridControl1.DataSource = dsRecords?.Tables?[0];
            this.gridView1.PopulateColumns();
        }

        #endregion Utilities

        #region Events

        private void _dialogIDOperationRelateRecord_Load(object sender, EventArgs e)
        {
            try
            {
                BindRecord();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #endregion Events

    }
}