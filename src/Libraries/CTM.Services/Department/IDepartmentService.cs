using System.Collections.Generic;
using CTM.Core.Domain.Department;

namespace CTM.Services.Department
{
    public partial interface IDepartmentService : IBaseService
    {
        void AddDepartmentInfo(DepartmentInfo departmentInfoEntity);

        void UpdateDepartmentInfo(DepartmentInfo departmentInfoEntity);

        void DeleteDepartmentInfo(DepartmentInfo departmentInfoEntity);

        int GetDepartmentPeerCount(int parentId);

        bool IsDepartmentExisted(string departmentName);

        IList<int> GetAllAccountingDepartmentId();

        DepartmentInfo GetDepartmentInfoById(int departmentId);

        IList<DepartmentInfo> GetDepartmentInfoByIds(int[] deptIds);

        IList<DepartmentInfo> GetAllDepartmentInfo(bool showDeleted = false);

        IList<DepartmentInfo> GetChildDepartmentsById(int deptId);

        IList<DepartmentInfo> GetAllAccountingDepartmentInfo();
    }
}