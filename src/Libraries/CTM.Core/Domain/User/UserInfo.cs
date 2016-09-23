using System.Collections.Generic;
using CTM.Core.Domain.Account;

namespace CTM.Core.Domain.User
{
    public class UserInfo : BaseEntity
    {
        private ICollection<AccountOperator> _accountOperators;

        public int TypeCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string RandomKey { get; set; }

        public string Password { get; set; }

        public int PositionCode { get; set; }

        public int DepartmentId { get; set; }

        public string Superior { get; set; }

        public string CooperatorCode { get; set; }

        public decimal AllotFund { get; set; }

        public decimal RiskControlLine { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsManager { get; set; }

        public bool IsDealer { get; set; }

        public bool IsDeleted { get; set; }

        public string Remarks { get; set; }

        public virtual string PositionName { get; set; }

        public virtual string DepartmentName { get; set; }

        public virtual string SuperiorName { get; set; }

        public virtual string CooperatorName { get; set; }

        public virtual ICollection<AccountOperator> AccountOperators
        {
            get { return _accountOperators ?? (_accountOperators = new List<AccountOperator>()); }
            protected set { _accountOperators = value; }
        }
    }
}