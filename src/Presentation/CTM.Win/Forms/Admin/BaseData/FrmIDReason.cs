using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class FrmIDReason : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;

        #endregion Fields

        #region Constructors

        public FrmIDReason(IInvestmentDecisionService IDService)
        {
            InitializeComponent();
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.tlCategory.AllowDrop = false;
            this.tlCategory.OptionsBehavior.Editable = true;
            this.tlCategory.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.tlCategory.OptionsSelection.MultiSelect = false;
            this.tlCategory.OptionsSelection.UseIndicatorForSelection = true;
            this.tlCategory.OptionsView.ShowColumns = false;
            this.tlCategory.OptionsView.ShowVertLines = false;
            this.tlCategory.OptionsView.ShowHorzLines = false;
        }

        private void BindCategory()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"SELECT * FROM DecisionReasonCategory ORDER BY Name";

            DataTable dtCategory = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText)?.Tables[0];

            this.tlCategory.DataSource = dtCategory;
            this.tlCategory.ExpandAll();
        }

        private void BindContent(int categoryId)
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"SELECT * FROM DecisionReasonContent WHERE CategoryId = {categoryId} ORDER BY Title";

            DataTable dtContent = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText)?.Tables[0];

            this.gridControl1.DataSource = dtContent;
        }

        #endregion Utilities

        #region Events

        private void FrmIDReason_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindCategory();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        #region Category

        private void tlCategory_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            e.Node.ExpandAll();
            var categoryId = Convert.ToInt32(e.Node.GetValue(tcId));

            BindContent(categoryId);
        }

        private void tlCategory_DragDrop(object sender, DragEventArgs e)
        {
            TreeListNode dragNode = (TreeListNode)e.Data.GetData(typeof(TreeListNode));
            if (dragNode == null) return;

            TreeList myTree = (TreeList)sender;

            TreeListHitInfo hitInfo = myTree.CalcHitInfo(myTree.PointToClient(new Point(e.X, e.Y)));

            if (hitInfo.Node == null) return;

            var targetNode = hitInfo.Node;

            bool? isPeer = null;

            DragInsertPosition dip = this.tlCategory.GetDragInsertPosition();
            switch (dip)
            {
                case DragInsertPosition.AsChild:
                    isPeer = false;
                    break;

                case DragInsertPosition.Before:
                case DragInsertPosition.After:
                    isPeer = true;
                    break;

                case DragInsertPosition.None:
                default:
                    break;
            }

            if (!isPeer.HasValue) return;

            var newParentId = 0;
            if (isPeer.Value)
                newParentId = Convert.ToInt32(targetNode.GetValue(tcParentId));
            else
                newParentId = Convert.ToInt32(targetNode.GetValue(tcId));

            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"UPDATE DecisionReasonCategory SET ParentId = {newParentId} WHERE Id = {Convert.ToInt32(dragNode.GetValue(tcId))}";

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);

            this.tlCategory.ExpandAll();

            //BindCategory();
        }

        private void tlCategory_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == this.tcName)
            {
                if (string.IsNullOrEmpty(e.Value.ToString().Trim()))
                {
                    DXMessage.ShowTips("类别名称不能为空！");
                    return;
                }
                var id = Convert.ToInt32(e.Node.GetValue(tcId));
                var name = e.Value.ToString().Trim();
                _IDService.UpdateIDReasonCategory(id, name);
            }
        }

        #endregion Category

        #region Content

        private void gvContent_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion Content

        #endregion Events
    }
}