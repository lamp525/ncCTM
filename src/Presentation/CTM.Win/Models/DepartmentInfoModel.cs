namespace CTM.Win.Models
{
    public class DepartmentInfoModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string PrincipalCode { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remarks { get; set; }

        public bool IsDeleted { get; set; }

        public int ParentId { get; set; }

        public string ParentCode { get; set; }

        public string ParentName { get; set; }

        public int ParentLevel { get; set; }
    }
}