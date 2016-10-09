using System.Collections.Generic;
using CTM.Core.Domain.Account;

namespace CTM.Core.Domain.Industry
{
    public class IndustryInfo : BaseEntity
    {
        private ICollection<AccountInfo> _accountInfos { get; set; }

        public string Code { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public bool IsDeleted { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<AccountInfo> AccountInfos
        {
            get { return _accountInfos ?? (_accountInfos = new List<AccountInfo>()); }
            protected set { _accountInfos = value; }
        }
    }
}