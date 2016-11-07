using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CTM.Data;
using CTM.Win.Util;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class FrmIDReason : BaseForm
    {
        public FrmIDReason()
        {
            InitializeComponent();
        }

        #region Utilities

        private void FormInit()
        {
            this.tlCategory.OptionsBehavior.Editable = true;
            this.tlCategory.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.tlCategory.OptionsSelection.MultiSelect = false;
            this.tlCategory.OptionsSelection.UseIndicatorForSelection = true;
        }

        private void BindCategory()
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"SELECT * FROM DecisionReasonCategory ORDER BY Name";

            DataTable dtCategory = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText)?.Tables[0];

            this.tlCategory.DataSource = dtCategory;
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

            TreeListHitInfo info = myTree.CalcHitInfo(myTree.PointToClient(new Point(e.X, e.Y)));

        
            if (info.Node == null) return;

            var targetNode = info.Node;


            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"UPDATE DecisionReasonCategory SET ParentId = {Convert.ToInt32(targetNode.GetValue(tcParentId))} WHERE Id = {Convert.ToInt32(dragNode.GetValue(tcId))}";

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, commandText);

            BindCategory();
        }

        #endregion Events
    }
}