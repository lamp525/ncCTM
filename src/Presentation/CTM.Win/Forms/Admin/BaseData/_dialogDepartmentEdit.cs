using System;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.Department;
using CTM.Core.Util;
using CTM.Services.Department;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class _dialogDepartmentEdit : BaseForm
    {
        #region Fields

        private readonly IDepartmentService _departmentService;

        private DepartmentInfo _currentDeptInfo = null;

        private int _currentDeptId;

        #endregion Fields

        #region Properties

        public int DepartmenId
        {
            get
            {
                return this._currentDeptId;
            }
            set
            {
                this._currentDeptId = value;
            }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogDepartmentEdit(IDepartmentService departmentService)
        {
            InitializeComponent();
            this._departmentService = departmentService;
        }

        #endregion Constructors

        #region Utilities

        private void BindDepartment()
        {
            var departments = _departmentService.GetAllDepartmentInfo().Where(x => x.Id != _currentDeptId).ToList();

            var noSelect = new DepartmentInfo()
            {
                Id = 0,
                Name = "无上级部门",
            };
            departments.Add(noSelect);

            departments = departments.OrderBy(x => x.Id).ToList();

            this.luParentDept.Initialize(departments, "Id", "Name");

            this.luParentDept.EditValue = this._currentDeptInfo.ParentId;

            txtName.Text = this._currentDeptInfo.Name;
            memoDescription.Text = this._currentDeptInfo.Description;
            memoRemarks.Text = this._currentDeptInfo.Remarks;
        }

        private bool InputCheck()
        {
            if (txtName.Text.Trim().Length == 0)
            {
                DXMessage.ShowTips("部门名称不能为空!");
                this.txtName.Focus();
                return false;
            }
            if (this._currentDeptInfo == null || this._currentDeptId == 0)
            {
                if (_departmentService.IsDepartmentExisted(txtName.Text.Trim()))
                {
                    DXMessage.ShowTips("该部门名称已经存在！");
                    this.txtName.Focus();
                    return false;
                }
            }

            return true;
        }

        private void UpdateDepartmentInfo()
        {
            var parentDeptInfo = (this.luParentDept.GetSelectedDataRow() as DepartmentInfo) ?? new DepartmentInfo();

            this._currentDeptInfo.Name = txtName.Text.Trim();
            this._currentDeptInfo.ParentId = parentDeptInfo.Id;
            this._currentDeptInfo.Level = parentDeptInfo.Level + 1;
            this._currentDeptInfo.Description = memoDescription.Text.Trim();
            this._currentDeptInfo.Remarks = memoRemarks.Text.Trim();

            _departmentService.UpdateDepartmentInfo(this._currentDeptInfo);
        }

        private void AddDepartmentInfo()
        {
            var parentDeptInfo = (this.luParentDept.GetSelectedDataRow() as DepartmentInfo) ?? new DepartmentInfo();

            var department = new DepartmentInfo()
            {
                Name = txtName.Text.Trim(),
                ParentId = parentDeptInfo.Id,
                Description = memoDescription.Text.Trim(),
                Remarks = memoRemarks.Text.Trim(),
                Level = parentDeptInfo.Level + 1,
            };

            var peerNumber = _departmentService.GetDepartmentPeerCount(department.ParentId);
            var parentCode = parentDeptInfo.Code;
            department.Code = CommonHelper.GenerateCode(peerNumber, parentCode);

            _departmentService.AddDepartmentInfo(department);
        }

        #endregion Utilities

        #region Events

        private void _dialogDepartmentEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this._currentDeptInfo = this._departmentService.GetDepartmentInfoById(this._currentDeptId)?? new DepartmentInfo();

                BindDepartment();

                this.AcceptButton = this.btnSubmit;
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSubmit.Enabled = false;

                if (InputCheck())
                {
                    if (this._currentDeptId == 0)
                        AddDepartmentInfo();
                    else
                        UpdateDepartmentInfo();

                    RefreshEvent?.Invoke();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnSubmit.Enabled = true;
            }
        }

        #endregion Events
    }
}