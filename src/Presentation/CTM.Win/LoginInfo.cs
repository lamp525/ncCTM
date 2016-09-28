using System;

namespace CTM.Win.Models
{
    /// <summary>
    /// 当前用户登录信息
    /// </summary>
    [Serializable]
    public class LoginInfo
    {
        private static LoginInfo _currentUser = null;

        public int DepartmentId { get; set; }

        public int UserId { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime LoginTime { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsRememberPwd { get; set; }

        /// <summary>
        /// 单例模式，保存用户登录状态
        /// </summary>
        public static LoginInfo CurrentUser
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = new LoginInfo();
                return _currentUser;
            }
        }
    }
}