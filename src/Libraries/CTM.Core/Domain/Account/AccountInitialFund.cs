namespace CTM.Core.Domain.Account
{
    public class AccountInitialFund : BaseEntity
    {
        public int AccountId { get; set; }

        public string AccountCode { get; set; }

        public int Month { get; set; }

        public decimal Amount { get; set; }

        public bool IsInitial { get; set; }
    }
}