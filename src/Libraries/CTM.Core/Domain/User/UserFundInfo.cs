namespace CTM.Core.Domain.User
{
    public class UserFundInfo : BaseEntity
    {
        public string UserCode { get; set; }

        public decimal AllotAmount { get; set; }

        public string Remarks { get; set; }
    }
}