using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Department;
using CTM.Core.Domain.Dictionary;
using CTM.Core.Domain.User;

namespace CTM.Services.User
{
    public partial class UserService : IUserService
    {
        #region Fields

        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly IRepository<DictionaryInfo> _dictionaryInfoRepository;
        private readonly IRepository<DepartmentInfo> _departmentRepository;

        #endregion Fields

        #region Constructors

        public UserService(
            IRepository<UserInfo> userInfoRepository,
            IRepository<DictionaryInfo> dictionaryInfoRepository,
            IRepository<DepartmentInfo> departmentRepository
           )
        {
            this._userInfoRepository = userInfoRepository;
            this._dictionaryInfoRepository = dictionaryInfoRepository;
            this._departmentRepository = departmentRepository;
        }

        #endregion Constructors

        #region Method

        public IList<UserInfo> GetAllAdmins(bool showDeleted = false)
        {
            var result = new List<UserInfo>();

            var query = _userInfoRepository.Table.Where(x => x.IsAdmin);

            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);

            result = query.ToList();

            return result;
        }

        public virtual IList<UserInfo> GetAllOperators(bool showDeleted = false)
        {
            var result = new List<UserInfo>();

            var tradeDeptIds = new int[] { 2, 3, 4, 5 };

            var query = _userInfoRepository.Table.Where(x => tradeDeptIds.Contains(x.DepartmentId));

            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);

            query = query.OrderBy(x => x.Name);

            result = query.ToList();

            return result;
        }

        public virtual IList<UserInfo> GetAllUsers(bool showDeleted = false)
        {
            var query = _userInfoRepository.Table;

            if (!showDeleted)
            {
                query = query.Where(x => !x.IsDeleted);
            }

            return query.ToList();
        }

        public virtual IList<UserInfo> GetUserInfos(int[] departmentIds = null, int[] userIds = null, string[] userCodes = null)
        {
            var query = _userInfoRepository.TableNoTracking;

            if (departmentIds != null)
                query = query.Where(x => departmentIds.Contains(x.DepartmentId));

            if (userIds != null)
                query = query.Where(x => userIds.Contains(x.Id));

            if (userCodes != null)
                query = query.Where(x => userCodes.Contains(x.Code));

            return query.ToList();
        }

        public virtual IList<UserInfo> GetUserDetails(int departmentId = 0, int[] userIds = null, string[] userCodes = null)
        {
            var query = _userInfoRepository.TableNoTracking;

            if (departmentId > 0)
                query = query.Where(x => x.DepartmentId == departmentId);

            if (userIds != null)
                query = query.Where(x => userIds.Contains(x.Id));

            if (userCodes != null)
                query = query.Where(x => userCodes.Contains(x.Code));

            var infos = from u in query
                        join position in _dictionaryInfoRepository.Table
                        on new { f1 = u.PositionCode, f2 = (int)EnumLibrary.DictionaryType.PositionInfo } equals new { f1 = position.Code, f2 = position.TypeId } into temp1
                        from positionDic in temp1.DefaultIfEmpty()
                        join department in _departmentRepository.Table
                        on u.DepartmentId equals department.Id into temp2
                        from departmentInfo in temp2.DefaultIfEmpty()
                        join c in _userInfoRepository.Table
                        on u.CooperatorCode equals c.Code into temp3
                        from cooperators in temp3.DefaultIfEmpty()
                        join s in _userInfoRepository.Table
                        on u.Superior equals s.Code into temp4
                        from superiors in temp4.DefaultIfEmpty()
                        select new { UserInfo = u, PositionName = positionDic.Name, DepartmentName = departmentInfo.Name, CooperatorName = cooperators.Name, SuperiorName = superiors.Name };

            var result = infos.ToList().Select(x => new UserInfo
            {
                Id = x.UserInfo.Id,
                AllotFund = x.UserInfo.AllotFund,
                Code = x.UserInfo.Code,
                CooperatorCode = x.UserInfo.CooperatorCode,
                CooperatorName = x.CooperatorName,
                DepartmentId = x.UserInfo.DepartmentId,
                DepartmentName = x.DepartmentName,
                IsAdmin = x.UserInfo.IsAdmin,
                IsDealer = x.UserInfo.IsDealer,
                IsDeleted = x.UserInfo.IsDeleted,
                IsManager = x.UserInfo.IsManager,
                Name = x.UserInfo.Name,
                Password = x.UserInfo.Password,
                PositionCode = x.UserInfo.PositionCode,
                PositionName = x.PositionName,
                RandomKey = x.UserInfo.RandomKey,
                Remarks = x.UserInfo.Remarks,
                Superior = x.UserInfo.Superior,
                SuperiorName = x.SuperiorName,
                TypeCode = x.UserInfo.TypeCode,
            }
            ).ToList();

            return result;
        }

        public virtual UserInfo GetUserInfoByCode(string code)
        {
            var info = _userInfoRepository.Table.Where(x => x.Code == code).FirstOrDefault();

            return info;
        }

        public virtual IList<UserInfo> GetUserInfoByCode(string[] codes)
        {
            if (codes == null)
                throw new ArgumentNullException(nameof(codes));

            var query = _userInfoRepository.Table.Where(x => codes.Contains(x.Code));

            return query.ToList();
        }

        public virtual UserInfo GetUserInfoByName(string name)
        {
            var info = _userInfoRepository.Table.Where(x => x.Name == name).FirstOrDefault();

            return info;
        }

        public virtual UserInfo GetUserInfoById(int userId)
        {
            return _userInfoRepository.GetById(userId);
        }

        public virtual bool IsExistedUser(string code, int userId = 0)
        {
            var query = _userInfoRepository.Table;

            if (userId > 0)
                query = query.Where(x => x.Id != userId);

            query = query.Where(x => x.Code == code);

            var info = query.SingleOrDefault();

            return info == null ? false : true;
        }

        public virtual void AddUserInfo(UserInfo user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userInfoRepository.Insert(user);
        }

        public virtual void UpdateUserInfo(UserInfo user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userInfoRepository.Update(user);
        }

        public virtual void DisableUser(int[] userIds)
        {
            if (userIds == null)
                throw new ArgumentNullException(nameof(userIds));

            var query = _userInfoRepository.Table;
            query = query.Where(x => userIds.Contains(x.Id));

            query.ToList().ForEach(x =>
            {
                x.IsDeleted = true;
            });

            _userInfoRepository.Update(query);
        }

        public virtual void EnableUser(int[] userIds)
        {
            if (userIds == null)
                throw new ArgumentNullException(nameof(userIds));

            var query = _userInfoRepository.Table;
            query = query.Where(x => userIds.Contains(x.Id));

            query.ToList().ForEach(x =>
            {
                x.IsDeleted = false;
            });

            _userInfoRepository.Update(query);
        }

        public virtual void ResetPwd(int[] userIds)
        {
            if (userIds == null)
                throw new ArgumentNullException(nameof(userIds));

            var query = _userInfoRepository.Table;
            query = query.Where(x => userIds.Contains(x.Id));

            query.ToList().ForEach(x =>
            {
                x.Password = x.Code;
            });
            _userInfoRepository.Update(query);
        }

        #endregion Method
    }
}