using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core;
using CTM.Core.Data;
using CTM.Core.Domain.Department;

namespace CTM.Services.Department
{
    public partial class DepartmentService : IDepartmentService
    {
        #region Fields

        private readonly IRepository<DepartmentInfo> _departmentInfoRepository;

        #endregion Fields

        #region Constructors

        public DepartmentService(IRepository<DepartmentInfo> departmentRepository)
        {
            this._departmentInfoRepository = departmentRepository;
        }

        #endregion Constructors

        #region Methods

        public virtual IList<DepartmentInfo> GetDepartmentInfoByIds(int[] deptIds)
        {
            if (deptIds == null)
                throw new ArgumentException(nameof(deptIds));

            var query = _departmentInfoRepository.Table;

            query = query.Where(x => deptIds.Contains(x.Id));

            return query.ToList();
        }

        public virtual void AddDepartmentInfo(DepartmentInfo departmentInfoEntity)
        {
            if (departmentInfoEntity == null)
                throw new ArgumentException(nameof(departmentInfoEntity));

            _departmentInfoRepository.Insert(departmentInfoEntity);
        }

        public virtual void UpdateDepartmentInfo(DepartmentInfo departmentInfoEntity)
        {
            if (departmentInfoEntity == null)
                throw new ArgumentException(nameof(departmentInfoEntity));

            _departmentInfoRepository.Update(departmentInfoEntity);
        }

        public virtual void DeleteDepartmentInfo(DepartmentInfo departmentInfoEntity)
        {
            if (departmentInfoEntity == null)
                throw new ArgumentException(nameof(departmentInfoEntity));

            departmentInfoEntity.IsDeleted = true;
            UpdateDepartmentInfo(departmentInfoEntity);
        }

        public virtual IList<DepartmentInfo> GetAllDepartmentInfo(bool showDeleted = false)
        {
            var result = new List<DepartmentInfo>();

            var query = _departmentInfoRepository.TableNoTracking;

            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);

            result = query.ToList();

            return result;
        }

        public virtual bool IsDepartmentExisted(string departmentName)
        {
            var result = true;

            var info = _departmentInfoRepository.Table.Where(x => x.Name == departmentName).FirstOrDefault();
            if (info == null)
                result = false;

            return result;
        }

        public virtual DepartmentInfo GetDepartmentInfoById(int departmentId)
        {
            if (departmentId == 0)
                return null;

            return _departmentInfoRepository.GetById(departmentId);
        }

        public virtual int GetDepartmentPeerCount(int parentId)
        {
            int count = _departmentInfoRepository.Table.Where(x => x.ParentId == parentId).Count();

            return count;
        }

        public virtual IList<DepartmentInfo> GetChildDepartmentsById(int deptId)
        {
            var childDepartments = _departmentInfoRepository.Table.Where(x => x.ParentId == deptId && !x.IsDeleted).ToList();

            return childDepartments;
        }

        public virtual IList<int> GetAllAccountingDepartmentId()
        {
            var type = typeof(EnumLibrary.AccountingDepartment);

            var values = new List<int>();
            foreach (var field in type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                values.Add(int.Parse(field.GetRawConstantValue().ToString()));
            }

            return values;
        }

        public virtual IList<DepartmentInfo> GetAllAccountingDepartmentInfo()
        {
            var deptIds = GetAllAccountingDepartmentId();

            var deptInfo = GetDepartmentInfoByIds(deptIds.ToArray());

            return deptInfo;
        }

        #endregion Methods
    }
}