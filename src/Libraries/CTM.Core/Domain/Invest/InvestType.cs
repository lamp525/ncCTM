namespace CTM.Core.Domain.Invest
{
    public class InvestType : BaseEntity
    {
        public string ParentCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remarks { get; set; }
    }
}