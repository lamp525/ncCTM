using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Account;

namespace CTM.Data.Mapping.Account
{
    public partial class AccountOperatorMap : EntityTypeConfiguration<AccountOperator>
    {
        public AccountOperatorMap()
        {
            this.ToTable("AccountOperator");
            this.HasKey(p => p.Id);

            this.HasRequired(ao => ao.AccountInfo)
                .WithMany(a => a.AccountOperators)
                .HasForeignKey(ao => ao.AccountId)
                .WillCascadeOnDelete(false);

            this.HasRequired(ao => ao.OperatorInfo)
                .WithMany(a => a.AccountOperators)
                .HasForeignKey(ao => ao.OperatorId)
                .WillCascadeOnDelete(false);
        }
    }
}