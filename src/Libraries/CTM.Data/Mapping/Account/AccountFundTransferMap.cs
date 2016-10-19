using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Account;

namespace CTM.Data.Mapping.Account
{
    public partial class AccountFundTransferMap : EntityTypeConfiguration<AccountFundTransfer>
    {
        public AccountFundTransferMap()
        {
            this.ToTable(nameof(AccountFundTransfer));
            this.HasKey(p => p.Id);

            this.Property(p => p.TransferAmount).HasPrecision(24, 6);
        }
    }
}