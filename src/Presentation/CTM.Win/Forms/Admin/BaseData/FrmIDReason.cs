using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Data;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Util;
using DevExpress.XtraBars;
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

        private void BindCategory()
        {
            var categories = _IDService.GetIDReasonCategories();

            var root = new DecisionReasonCategory()
            {
                Id = 0,
                Name = "全部",
                ParentId = -1,
                Remarks = null,
            };

            categories.Add(root);

            this.tlCategory.Initialize(categories, "Id", "ParentId", editable: true, autoWidth: true, showColumns: false, showVertLines: false, showHorzLines: false);
            this.tlCategory.AllowDrop = false;
            this.tlCategory.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;

            categories = null;
        }

        private void BindContent(int categoryId)
        {
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();
            var commandText = $@"SELECT * FROM DecisionReasonContent WHERE CategoryId = {categoryId} ORDER BY Title";

            DataTable dtContent = SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText)?.Tables[0];

            this.gridControl1.DataSource = dtContent;
        }

        private void DisplayEditDialog(int categoryId, int contentId)
        {
            var dialog = this.CreateDialog<_dialogIDReasonContentEdit>();
            dialog.RefreshEvent += new _dialogIDReasonContentEdit.RefreshParentForm(RefreshForm);
            dialog.ContentId = contentId;
            dialog.CategoryId = categoryId;

            if (contentId > 0)
                dialog.Text = "编辑理由详情";
            else
                dialog.Text = "添加理由详情";

            dialog.ShowDialog();
        }

        private void RefreshForm(int categoryId)
        {
            BindContent(categoryId);
        }

        #endregion Utilities

        #region Events

        private void FrmIDReason_Load(object sender, EventArgs e)
        {
            try
            {
                this.gvContent.SetLayout();
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;

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
            if (e.Node == null) return;

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

        private void barBtnAddPeer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode currentNode = this.tlCategory.FocusedNode;
                if (currentNode == null) return;
                var peer = new DecisionReasonCategory
                {
                    Name = "新建分类",
                    ParentId = Convert.ToInt32(currentNode.GetValue(tcParentId)),
                    Remarks = null,
                };
                peer.Id = _IDService.AddIDReasonCategory(peer);

                var source = this.tlCategory.DataSource as List<DecisionReasonCategory>;
                source.Add(peer);
                this.tlCategory.RefreshDataSource();
                this.tlCategory.SetFocusedNode(this.tlCategory.FocusedNode.ParentNode.LastNode);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void barBtnAddChild_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode currentNode = this.tlCategory.FocusedNode;
                if (currentNode == null) return;
                var peer = new DecisionReasonCategory
                {
                    Name = "新建分类",
                    ParentId = Convert.ToInt32(currentNode.GetValue(tcId)),
                    Remarks = null,
                };
                peer.Id = _IDService.AddIDReasonCategory(peer);

                var source = this.tlCategory.DataSource as List<DecisionReasonCategory>;
                source.Add(peer);
                this.tlCategory.RefreshDataSource();
                this.tlCategory.SetFocusedNode(this.tlCategory.FocusedNode.LastNode);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void barBtnDeleteCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode currentNode = this.tlCategory.FocusedNode;
                if (currentNode == null) return;

                if (DXMessage.ShowYesNoAndTips("确定删除当前分类么？") == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(currentNode.GetValue(tcId));
                    _IDService.DeleteIDReasonCategory(id);
                    this.tlCategory.Nodes.Remove(currentNode);
                    this.tlCategory.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void tlCategory_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && tl.State == TreeListState.Regular)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo hitInfo = tl.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tl.SetFocusedNode(hitInfo.Node);
                }

                if (tl.FocusedNode != null)
                {
                    if (Convert.ToInt32(tl.FocusedNode.GetValue(tcId)) == 0)
                    {
                        this.barBtnAddChild.Visibility = BarItemVisibility.Always;
                        this.barBtnDeleteCurrent.Visibility = BarItemVisibility.Never;
                        this.barBtnAddPeer.Visibility = BarItemVisibility.Never;
                    }
                    else
                    {
                        foreach (BarButtonItemLink item in this.popupMenu1.ItemLinks)
                        {
                            item.Item.Visibility = BarItemVisibility.Always;
                        }
                    }
                    popupMenu1.ShowPopup(p);
                }
            }
        }

        #endregion Category

        #region Content

        private void gvContent_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvContent_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gvContent;
            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else if (selectedHandles.Length > 0)
            {
                btnDelete.Enabled = true;

                if (selectedHandles.Length == 1)
                {
                    this.btnEdit.Enabled = true;
                }
                else
                {
                    this.btnEdit.Enabled = false;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;
                var focusedNode = this.tlCategory.FocusedNode;

                var categoryId = Convert.ToInt32(focusedNode.GetValue(tcId));

                DisplayEditDialog(categoryId, 0);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnEdit.Enabled = false;

                var myView = this.gvContent;

                var selectedHandles = myView.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                if (selectedHandles.Length != 1)
                {
                    DXMessage.ShowTips("请选择一个要编辑的行!");
                    return;
                }

                var categoryId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colCategoryId).ToString());
                var contentId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colId).ToString());

                DisplayEditDialog(categoryId, contentId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnEdit.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete .Enabled = false;

                var myView = this.gvContent ;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0)
                {
                    DXMessage.ShowTips("请选择要删除的行！");
                    return;
                }

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的行吗？") == DialogResult.Yes)
                {
                    var contentIds = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        contentIds.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    this._IDService.DeleteIDReasonContent(contentIds.ToArray());

                    var focusedNode = this.tlCategory.FocusedNode;

                    var categoryId = Convert.ToInt32(focusedNode.GetValue(tcId));
                    RefreshForm(categoryId);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnDelete.Enabled = true;
            }
        }

        #endregion Content

        #endregion Events
    }
}