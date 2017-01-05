using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Account;

namespace CTM.Data.Mapping.Account
{
    public partial class AccountInfoMap : EntityTypeConfiguration<AccountInfo>
    {
        public AccountInfoMap()
        {
            this.ToTable(nameof (AccountInfo));
            this.HasKey(p => p.Id);

            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.IncomeRate).HasPrecision(18, 5);
            this.Property(p => p.StampDutyRate).HasPrecision(18, 5);
            this.Property(p => p.CommissionRate).HasPrecision(18, 5);
            this.Property(p => p.IncidentalsRate).HasPrecision(18, 5);
            this.Property(p => p.Remarks).HasMaxLength(200);
            this.Property(p => p.TotalFund).HasPrecision(24, 4);
            this.Property(p => p.InvestFund).HasPrecision(24, 4);
            this.Property(p => p.FinancingAmount).HasPrecision(24, 4);
            this.Property(p => p.Balance).HasPrecision(24, 4);
        }
    }
}