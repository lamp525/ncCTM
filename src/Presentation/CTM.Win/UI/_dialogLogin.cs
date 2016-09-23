using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Domain.User;
using CTM.Core.Util;
using CTM.Services.Common;
using CTM.Services.Department;
using CTM.Services.User;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI
{
    public partial class _dialogLogin : BaseForm
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IDepartmentService _deptService;
        private readonly ICommonService _commonService;

        private bool _reLogin = false;

        #endregion Fields

        #region Properties

        public bool ReLogin
        {
            get { return this._reLogin; }
            set { this._reLogin = value; }
        }

        #endregion Properties

        #region Delegates

        public delegate void RefreshParentForm();

        public event RefreshParentForm RefreshEvent;

        #endregion Delegates

        #region Constructors

        public _dialogLogin(IUserService userService, IDepartmentService deptService, ICommonService commonService)
        {
            InitializeComponent();

            this._userService = userService;
            this._deptService = deptService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Utilities

        private void SetBackGroundImage()
        {
            //this.BackgroundImage =Image.FromFile()
        }

        private void SaveLoginLog(UserInfo info)
        {
        }

        private void SaveLoginInfo(UserInfo info)
        {
            //保存登陆用户信息
            LoginInfo.CurrentUser.UserId = info.Id;
            LoginInfo.CurrentUser.DepartmentId = info.DepartmentId;
            LoginInfo.CurrentUser.UserCode = info.Code;
            LoginInfo.CurrentUser.UserName = info.Name;
            LoginInfo.CurrentUser.LoginTime = _commonService.GetCurrentServerTime();
            LoginInfo.CurrentUser.IsAdmin = info.IsAdmin;
        }

        private void SaveLoginInfoToFile()
        {
            var userCode = txtAccount.Text;
            var pwd = txtPassword.Text;

            var loginModel = new LoginInfo();

            loginModel.UserCode = userCode;
            if (chkRememberPwd.Checked)
            {
                loginModel.IsRememberPwd = true;
                loginModel.Password = pwd;
            }

            AppSettingHelper.Save(loginModel);
        }

        private void GetSavedLoginInfo()
        {
            var loginInfo = AppSettingHelper.Load();

            if (loginInfo == null)
                loginInfo = AppSettingHelper.LoadDefault();

            this.txtAccount.Text = loginInfo.UserCode;

            if (loginInfo.IsRememberPwd)
            {
                this.txtPassword.Text = loginInfo.Password;
                this.chkRememberPwd.Checked = true;
            }
        }

        #endregion Utilities

        #region Events

        private void _dialogLogin_Load(object sender, EventArgs e)
        {
            this.defaultLookAndFeel1.LookAndFeel.SkinName = AppConfigHelper.DefaultSkinName;

            SetBackGroundImage();

            this.AcceptButton = this.btnLogin;

            this.txtAccount.Properties.MaxLength = 20;
            this.txtPassword.Properties.MaxLength = 20;
            this.txtPassword.Properties.PasswordChar = '*';

            GetSavedLoginInfo();

            if (this.txtAccount.Text.Trim().Length == 0)
            {
                this.ActiveControl = this.txtAccount;
            }
            else
            {
                if (this.txtPassword.Text.Length == 0)
                {
                    this.ActiveControl = this.txtPassword;
                }
                else
                {
                    this.ActiveControl = this.btnLogin;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnLogin.Enabled = false;
                this.btnCancel.Enabled = false;

                if (txtAccount.Text.Length == 0)
                {
                    DXMessage.ShowTips("请输入账号！");

                    txtAccount.Focus();

                    return;
                }

                if (txtPassword.Text.Length == 0)
                {
                    DXMessage.ShowTips("请输入密码！");

                    txtPassword.Focus();

                    return;
                }

                var userCode = txtAccount.Text;
                var pwd = txtPassword.Text;
                var info = _userService.GetUserInfoByCode(userCode);

                if (info == null)
                {
                    DXMessage.ShowTips("账号不存在，请重新输入！");
                    txtAccount.Focus();

                    return;
                }

                if (info.IsDeleted)
                {
                    DXMessage.ShowTips("该账号已被禁用，请联系管理员！");
                    txtAccount.Focus();

                    return;
                }

                if (info.Password != pwd)
                {
                    DXMessage.ShowTips("密码不正确，请重新输入!");
                    txtPassword.Text = string.Empty;
                    txtPassword.Focus();

                    return;
                }

                SaveLoginInfo(info);

                SaveLoginLog(info);

                SaveLoginInfoToFile();

                this.DialogResult = DialogResult.OK;

                if (this.ReLogin)
                {
                    RefreshEvent?.Invoke();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnLogin.Enabled = true;
                this.btnCancel.Enabled = true;
            }
        }

        #endregion Events
    }
}