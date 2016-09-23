using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Services.User;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Setting
{
    public partial class _dialogChangePwd : BaseForm
    {
        #region Fields

        private IUserService _userSerivice;

        #endregion Fields

        #region Constructors

        public _dialogChangePwd(IUserService userSerivice)
        {
            InitializeComponent();

            this._userSerivice = userSerivice;
        }

        #endregion Constructors

        #region Events

        private void _dialogChangePwd_Load(object sender, EventArgs e)
        {
            this.txtOld.Focus();

            this.txtOld.Properties.MaxLength = 20;
            this.txtNew.Properties.MaxLength = 20;
            this.txtConfirm.Properties.MaxLength = 20;

            this.txtOld.Properties.PasswordChar = '*';
            this.txtNew.Properties.PasswordChar = '*';
            this.txtConfirm.Properties.PasswordChar = '*';

            this.AcceptButton = this.btnOk;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOk.Enabled = false;
                this.btnCancel.Enabled = false;

                if (txtOld.Text.Length == 0)
                {
                    DXMessage.ShowTips("请输入当前密码！");
                    txtOld.Focus();
                    return;
                }

                if (txtNew.Text.Length == 0)
                {
                    DXMessage.ShowTips("请输入新密码！");
                    txtNew.Focus();
                    return;
                }

                if (txtConfirm.Text.Length == 0)
                {
                    DXMessage.ShowTips("请输入确认密码！");
                    txtConfirm.Focus();
                    return;
                }

                if (txtConfirm.Text != txtNew.Text)
                {
                    DXMessage.ShowTips("确认密码和新密码不一致，请重新输入！");
                    txtNew.Text = string.Empty;
                    txtConfirm.Text = string.Empty;
                    txtNew.Focus();

                    return;
                }

                var info = _userSerivice.GetUserInfoById(LoginInfo.CurrentUser.UserId);

                if (info.Password != txtOld.Text)
                {
                    DXMessage.ShowTips("当前密码错误，请重新输入！");
                    txtOld.Text = string.Empty;
                    txtOld.Focus();

                    return;
                }

                info.Password = txtNew.Text;

                _userSerivice.UpdateUserInfo(info);

                if (DXMessage.ShowTips("密码修改成功！") == DialogResult.OK)
                    this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnOk.Enabled = true;
                this.btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events
    }
}