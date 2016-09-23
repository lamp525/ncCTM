using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Services.Department;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.BaseData
{
    public partial class FrmDepartment : BaseForm
    {
        #region Fields

        private readonly IDepartmentService _departmentService;

        #endregion Fields

        #region Constructors

        public FrmDepartment(IDepartmentService departmentService)
        {
            InitializeComponent();

            this._departmentService = departmentService;
        }

        #endregion Constructors

        #region Utilities

        private void BindDepartment()
        {
            List<DepartmentInfoModel> source = new List<DepartmentInfoModel>();

            var departments = _departmentService.GetAllDepartmentInfo().ToList();

            foreach (var item in departments)
            {
                var department = new DepartmentInfoModel()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    Level = item.Level,
                    Description = item.Description,
                    Remarks = item.Remarks,
                    IsDeleted = item.IsDeleted,
                    ParentId = item.ParentId
                };
                var parentInfo = departments.Where(x => x.Id == department.ParentId).SingleOrDefault();
                if (parentInfo != null)
                {
                    department.ParentCode = parentInfo.Code;
                    department.ParentName = parentInfo.Name;
                    department.ParentLevel = parentInfo.Level;
                }
                else
                {
                    department.ParentName = " / ";
                }
                source.Add(department);
            }

            this.treeList1.Initialize(source, "Id", "ParentId");
        }

        private void DisplayEditDialog(int deptId)
        {
            var editDialog = this.CreateDialog<_dialogDepartmentEdit>();
            editDialog.RefreshEvent += new _dialogDepartmentEdit.RefreshParentForm(RefreshForm);
            editDialog.DepartmenId = deptId;

            if (deptId == 0)
                editDialog.Text = "添加部门";
            else
                editDialog.Text = "编辑部门";

            editDialog.ShowDialog();
        }

        private void RefreshForm()
        {
            BindDepartment();
        }

        #endregion Utilities

        #region Events

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            BindDepartment();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DisplayEditDialog(0);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeList1.FocusedNode;
            if (selectedNode == null)
            {
                DXMessage.ShowTips("请选择要编辑的部门。");
                return;
            }

            var deptId = int.Parse(selectedNode.GetValue(colId).ToString());

            if (deptId > 0)
            {
                DisplayEditDialog(deptId);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeList1.FocusedNode;
            if (selectedNode == null)
            {
                DXMessage.ShowTips("请选择要删除的部门。");
                return;
            }

            var department = _departmentService.GetDepartmentInfoById(int.Parse(selectedNode.GetValue(colId).ToString()));

            if (department != null)
            {
                // department.IsDeleted = true;

                var childDepts = _departmentService.GetChildDepartmentsById(department.Id);

                if (childDepts != null && childDepts.Count > 0)
                {
                    DXMessage.ShowWarning(string.Format("部门【{0}】存在下级部门，无法删除！", department.Name));
                    return;
                }

                var result = DXMessage.ShowYesNoAndTips(string.Format("确定删除部门【{0}】么？", department.Name));

                if (result == DialogResult.Yes)
                {
                    _departmentService.DeleteDepartmentInfo(department);

                    BindDepartment();
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.treeList1.ShowFindPanel();
        }

        #endregion Events
    }
}