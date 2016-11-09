using System;
using System.Data;
using CTM.Core.Util;
using CTM.Data;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.DailyTrading.StatisticsReport
{
    public partial class FrmStockPositionQuery : BaseForm
    {
        private bool _isExpanded = true;

        private const string _layoutXmlName = "FrmStockPositionQuery";

        public FrmStockPositionQuery()
        {
            InitializeComponent();
        }

        #region Utilities

        private void FormInit()
        {
            this.deEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deEnd.EditValue = DateTime.Now.Date;

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showGroupPanel: true, showCheckBoxRowSelect: false, showAutoFilterRow: true);
        }

        private void BindStockPosition()
        {
            var endDate = CommonHelper.StringToDateTime(this.deEnd.EditValue.ToString());
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"EXEC [dbo].[sp_StockPositionQuery] @EndDate ='{endDate}'";

            var positionInfos = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText)?.Tables?[0];
            this.gridControl1.DataSource = positionInfos;

            _isExpanded = true;
            this.btnExpand.Enabled = true;
            ExpandOrCollapse();
        }

        private void ExpandOrCollapse()
        {
            if (_isExpanded)
                this.gridView1.CollapseAllGroups();
            else
                this.gridView1.ExpandAllGroups();

            _isExpanded = !_isExpanded;
            this.btnExpand.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
        }

        #endregion Utilities

        #region Events

        private void FrmStockPositionQuery_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindStockPosition();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSearch.Enabled = false;
                BindStockPosition();
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

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExpand.Enabled = false;

                ExpandOrCollapse();
            }
            finally
            {
                this.btnExpand.Enabled = true;
            }
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        #endregion Events
    }
}