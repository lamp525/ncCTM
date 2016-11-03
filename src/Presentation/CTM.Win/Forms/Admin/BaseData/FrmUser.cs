using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Domain.Department;
using CTM.Services.Department;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class FrmUser : BaseForm
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;

        private const string _layoutXmlName = "FrmUser";
        private bool _firstFocused = true;
        private int _departmentId;

        #endregion Fields

        #region Constructors

        public FrmUser(
            IUserService userService,
            IDepartmentService deptService
            )
        {
            InitializeComponent();

            this._departmentService = deptService;
            this._userService = userService;
        }

        #endregion Constructors

        #region Utilities

        private void SetOperateButtonProperties()
        {
            this.btnEdit.Enabled = false;
            this.btnDisable.Enabled = false;
        }

        private void BindDepartmentTree()
        {
            var departments = _departmentService.GetAllDepartmentInfo();

            var all = new DepartmentInfo()
            {
                Id = 0,
                ParentId = -1,
                Name = "全部",
                Code = "00",
                Level = 0,
            };
            departments.Add(all);

            this.treeList1.Initialize(departments, "Id", "ParentId", expandAll: true);
            this.treeList1.SetDefaultFocusedNode(0);
        }

        private void BindUserInfo(int departmentId)
        {
            this.gridControl1.DataSource = _userService.GetUserDetails(departmentId).OrderBy(x => x.Code);
        }

        private void RefreshForm(int departmentId)
        {
            SetOperateButtonProperties();

            BindUserInfo(departmentId);
        }

        private void DisplayEditDialog(int userId)
        {
            var dialog = this.CreateDialog<_dialogUserEdit>();
            dialog.RefreshEvent += new _dialogUserEdit.RefreshParentForm(RefreshForm);
            dialog.UserId = userId;

            if (userId > 0)
                dialog.Text = "编辑用户";
            else
                dialog.Text = "添加用户";

            dialog.ShowDialog();
        }

        #endregion Utilities

        #region Events

        private void FrmUser_Load(object sender, EventArgs e)
        {
            SetOperateButtonProperties();

            this.gridView1.LoadLayout(_layoutXmlName);
            this.gridView1.SetLayout(showGroupPanel: true);

            BindDepartmentTree();

            this.ActiveControl = this.btnAdd;
        }

        /// <summary>
        /// 保存样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            this.gridView1.SaveLayout(_layoutXmlName);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var myView = this.gridView1;

            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length != 1)
            {
                DXMessage.ShowTips("请选择一个要编辑的用户！");
                return;
            }

            //用户ID
            var userId = int.Parse(myView.GetRowCellValue(selectedHandles[0], colId).ToString());

            DisplayEditDialog(userId);
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            var myView = this.gridView1;

            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Length == 0)
            {
                DXMessage.ShowTips("请选择要禁用的账户！");
                return;
            }

            if (DXMessage.ShowYesNoAndWarning("确定禁用选择的账户吗？") == DialogResult.Yes)
            {
                var userIds = new List<int>();

                for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                {
                    userIds.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                }

                this._userService.DisableUser(userIds.ToArray());

                RefreshForm(this._departmentId);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DisplayEditDialog(0);
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                var selectedNode = this.treeList1.FocusedNode;

                if (selectedNode == null) return;

                if (!this._firstFocused)
                {
                    _departmentId = int.Parse(selectedNode.GetValue(treeColId).ToString());
                    BindUserInfo(_departmentId);
                }
                this._firstFocused = false;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();

            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length == 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDisable.Enabled = false;
            }
            else if (selectedHandles.Length > 0)
            {
                btnDisable.Enabled = true;

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

        /// <summary>
        /// 显示数据行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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