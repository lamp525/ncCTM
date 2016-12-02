using System;
using System.Linq;
using CTM.Core;
using CTM.Core.Domain.User;
using CTM.Services.Department;
using CTM.Services.Dictionary;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.Forms.Admin.BaseData
{
    public partial class _dialogUserEdit : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;

        private int _userId;

        private bool _isEdit;

        #endregion Fields

        #region Properties

        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm(int departmentId);

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogUserEdit(
            IDictionaryService dictionaryService,
            IUserService userService,
            IDepartmentService departmentService
            )
        {
            InitializeComponent();

            this._departmentService = departmentService;
            this._userService = userService;
            this._dictionaryService = dictionaryService;
        }

        #endregion Constructors

        #region Utilities

        private void SetControlProperties()
        {
            this.chkAdmin.Checked = false;
            this.chkDealer.Checked = true;
            this.chkManager.Checked = false;

            this.txtCode.ReadOnly = this._isEdit;
            this.txtCode.Properties.MaxLength = 20;
            this.txtName.Properties.MaxLength = 20;
            this.memoRemarks.Properties.MaxLength = 200;

            this.txtAllotFund.SetNumericMask();
            this.txtAllotFund.Text = "0";
        }

        private void BindUserInfo()
        {
            //职位
            var positions = _dictionaryService.GetDictionaryInfoByTypeId((int)EnumLibrary.DictionaryType.PositionInfo)
                  .Select(x => new ComboBoxItemModel
                  {
                      Value = x.Code.ToString(),
                      Text = x.Name,
                  }
                  ).ToList();

            this.cbPosition.Initialize(positions, displayAdditionalItem: true, additionalItemText: "无");

            //部门
            var departments = _departmentService.GetAllDepartmentInfo().ToList();
            var departmentDetails = departments.Select(x => new DepartmentInfoModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ParentName = departments.SingleOrDefault(p => p.Id == x.ParentId) == null ? "/" : departments.SingleOrDefault(p => p.Id == x.ParentId).Name,
            }
            ).ToList();

            this.luDepartment.Initialize(departmentDetails, "Id", "Name", showHeader: true, enableSearch: true);

            var users = _userService.GetAllUsers();

            var noneModel = new UserInfo
            {
                Code = string.Empty,
                Name = "无",
            };
            users.Add(noneModel);

            users = users.OrderBy(x => x.Code).ToList();

            //直接上级
            this.luSuperior.Initialize(users, "Code", "Name", showHeader: true, enableSearch: true);

            //配合人员
            this.luCooperator.Initialize(users, "Code", "Name", showHeader: true, enableSearch: true);

            if (this._isEdit)
            {
                var userInfo = _userService.GetUserInfoById(_userId);

                if (userInfo == null) return;

                this.cbPosition.DefaultSelected(userInfo.PositionCode.ToString());

                this.luCooperator.EditValue = userInfo.CooperatorCode;

                this.luDepartment.EditValue = userInfo.DepartmentId;

                this.luSuperior.EditValue = userInfo.Superior;

                this.txtAllotFund.Text = userInfo.AllotFund.ToString();

                this.txtName.Text = userInfo.Name;

                this.txtCode.Text = userInfo.Code;

                this.chkAdmin.Checked = userInfo.IsAdmin ? true : false;

                this.chkDealer.Checked = userInfo.IsDealer ? true : false;

                this.chkManager.Checked = userInfo.IsManager ? true : false;

                this.memoRemarks.Text = userInfo.Remarks;
            }
        }

        private bool InputCheck()
        {
            if (string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                DXMessage.ShowTips("用户编码不能为空！");
                this.txtCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                DXMessage.ShowTips("用户名称不能为空！");
                this.txtName.Focus();
                return false;
            }

            if (this.cbPosition.SelectedIndex == -1)
            {
                DXMessage.ShowTips("请选择职位！");
                return false;
            }

            if (this.luDepartment.EditValue == null || this.luDepartment.EditValue.ToString() == "nulltext")
            {
                DXMessage.ShowTips("请选择部门！");
                return false;
            }

            return true;
        }

        #endregion Utilities

        #region Events

        private void _dialogUserEdit_Load(object sender, EventArgs e)
        {
            this._isEdit = this._userId > 0 ? true : false;

            SetControlProperties();
            BindUserInfo();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;

                if (!InputCheck()) return;

                //添加用户
                if (!this._isEdit)
                {
                    var newUser = new UserInfo();

                    newUser.AllotFund = string.IsNullOrEmpty(txtAllotFund.Text.Trim()) ? 0 : decimal.Parse(txtAllotFund.Text.Trim());
                    newUser.Code = txtCode.Text.Trim();
                    newUser.CooperatorCode = luCooperator.SelectedValue();
                    newUser.DepartmentId = int.Parse(luDepartment.SelectedValue());
                    newUser.IsAdmin = chkAdmin.Checked ? true : false;
                    newUser.IsDealer = chkDealer.Checked ? true : false;
                    newUser.IsDeleted = false;
                    newUser.IsManager = chkManager.Checked ? true : false;
                    newUser.Name = txtName.Text.Trim();
                    newUser.Password = txtCode.Text.Trim();
                    newUser.PositionCode = int.Parse(cbPosition.SelectedValue());
                    newUser.Remarks = memoRemarks.Text.Trim();
                    newUser.RandomKey = null;
                    newUser.Superior = luSuperior.SelectedValue();
                    newUser.TypeCode = 1;

                    var isExisted = _userService.IsExistedUser(newUser.Code);

                    if (!isExisted)
                    {
                        _userService.AddUserInfo(newUser);

                        this._userId = newUser.Id;
                    }
                    else
                    {
                        DXMessage.ShowTips("系统已经存在该用户编号，无法添加！");
                        this.txtCode.Focus();
                        return;
                    }
                }
                //修改用户
                else
                {
                    var user = _userService.GetUserInfoById(_userId);

                    user.AllotFund = string.IsNullOrEmpty(txtAllotFund.Text.Trim()) ? 0 : decimal.Parse(txtAllotFund.Text.Trim());
                    user.Code = txtCode.Text.Trim();
                    user.CooperatorCode = luCooperator.SelectedValue();
                    user.DepartmentId = int.Parse(luDepartment.SelectedValue());
                    user.IsAdmin = chkAdmin.Checked ? true : false;
                    user.IsDealer = chkDealer.Checked ? true : false;
                    user.IsManager = chkManager.Checked ? true : false;
                    user.Name = txtName.Text.Trim();
                    user.PositionCode = int.Parse(cbPosition.SelectedValue());
                    user.Remarks = memoRemarks.Text.Trim();
                    user.Superior = luSuperior.SelectedValue();

                    var isExisted = _userService.IsExistedUser(user.Code, user.Id);

                    if (!isExisted)
                    {
                        _userService.UpdateUserInfo(user);
                    }
                    else
                    {
                        DXMessage.ShowTips("系统已经存在该用户编号，无法修改！");
                        this.txtCode.Focus();
                        return;
                    }
                }

                var departmentId = int.Parse(luDepartment.SelectedValue());

                RefreshEvent?.Invoke(departmentId);

                this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnOk.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events
    }
}