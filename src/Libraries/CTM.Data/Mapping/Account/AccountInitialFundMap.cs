using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Account;

namespace CTM.Data.Mapping.Account
{
    public partial class AccountInitialFundMap:EntityTypeConfiguration <AccountInitialFund >
    {
        public AccountInitialFundMap()
        {
            this.ToTable(nameof(AccountInitialFund));
            this.HasKey(p => p.Id);

            this.Property(p => p.Amount).HasPrecision(24, 4);
        }
    }
}
