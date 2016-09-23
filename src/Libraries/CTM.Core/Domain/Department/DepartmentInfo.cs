using System.Collections.Generic;

namespace CTM.Core.Domain.Department
{
    public class DepartmentInfo : BaseEntity
    {
        public string Code { get; set; }

        public int ParentId { get; set; }

        public string PrincipalCode { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remarks { get; set; }

        // public bool IsAccounting { get; set; }

        public bool IsDeleted { get; set; }
    }
}