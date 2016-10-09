using CTM.Core.Domain.User;

namespace CTM.Core.Domain.Account
{
    public class AccountOperator : BaseEntity
    {
        public int AccountId { get; set; }

        public int OperatorId { get; set; }

        public virtual AccountInfo AccountInfo { get; set; }

        public virtual UserInfo OperatorInfo { get; set; }
    }
}