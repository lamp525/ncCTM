using System;
using System.Data;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _embedAppointedStockApplication : BaseForm
    {
        #region Properties

        public string StockCode { get; set; }

        #endregion Properties

        public _embedAppointedStockApplication()
        {
            InitializeComponent();
        }

        #region Utilities

        private void FormInit()
        {
        }



        private void DisplayOperationDetail(string applyNo, string operateNo)
        {
            var dialog = this.CreateDialog<_embedIDOperationDetail>(borderStyle: System.Windows.Forms.FormBorderStyle.Sizable);
            dialog.Text = "决策操作记录详情";

            dialog.ShowDialog();
        }

        #endregion Utilities

        #region Methods
        public void BindStockApplication( string stockCode)
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

            var commandText = $@"EXEC [dbo].[sp_GetIDApplicationAndIDOperation] @ApplyUser = '{LoginInfo.CurrentUser.UserCode}' ";

            var ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText);

            if (ds == null || ds.Tables.Count == 0) return;

            ds.Relations.Add("MD", ds.Tables[0]?.Columns["ApplyNo"], ds.Tables[1]?.Columns["ApplyNo"]);

            this.gridApplication.DataSource = ds.Tables[0];
        }
        #endregion

        #region Events

        private void _embedAppointedStockApplication_Load(object sender, EventArgs e)
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

        private void riBtnOperate_D_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                if (buttonTag == "View")
                {
                    DisplayOperationDetail(applyNo, operateNo);
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

        #endregion Events
    }
}