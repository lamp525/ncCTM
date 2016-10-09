using System.Collections.Generic;
using CTM.Core.Domain.User;

namespace CTM.Services.User
{
    public partial interface IUserService : IBaseService
    {
        IList<UserInfo> GetAllDealer(bool showDeleted = false);

        IList<UserInfo> GetAllManager(bool showDeleted = false);

        IList<UserInfo> GetAllOperators(bool isOnWorking);

        IList<UserInfo> GetAllAdmins(bool showDeleted = false);

        UserInfo GetUserInfoByCode(string code);

        IList<UserInfo> GetUserInfoByCode(string[] codes);

        UserInfo GetUserInfoByName(string name);

        IList<UserInfo> GetAllUsers(bool showDeleted = false);

        IList<UserInfo> GetUserInfos(int[] departmentIds = null, int[] userIds = null, string[] userCodes = null);

        IList<UserInfo> GetUserDetails(int departmentId = 0, int[] userIds = null, string[] userCodes = null);

        UserInfo GetUserInfoById(int userId);

        bool IsExistedUser(string code, int userId = 0);

        void AddUserInfo(UserInfo user);

        void UpdateUserInfo(UserInfo user);

        void DisableUser(int[] userIds);
    }
}