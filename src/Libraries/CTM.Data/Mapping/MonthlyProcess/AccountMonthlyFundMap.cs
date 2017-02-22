using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyProcess;

namespace CTM.Data.Mapping.MonthlyProcess
{
   public partial  class AccountMonthlyFundMap: EntityTypeConfiguration<AccountMonthlyFund >
    {
        public AccountMonthlyFundMap()
        {
            this.ToTable(nameof(AccountMonthlyFund));
            this.HasKey(p => p.Id);

            this.Property(p => p.TotalAsset).HasPrecision(24, 4);
            this.Property(p => p.AvailableFund).HasPrecision(24, 4);
            this.Property(p => p.PositionValue).HasPrecision(24, 4);
            this.Property(p => p.FinancedAmount).HasPrecision(24, 4);
            this.Property(p => p.FinancingLimit).HasPrecision(24, 4);
        }
    }
}
