using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.MonthlyProcess;

namespace CTM.Data.Mapping.MonthlyProcess
{
    public partial class AccountMonthlyPositionMap : EntityTypeConfiguration<AccountMonthlyPosition>
    {
        public AccountMonthlyPositionMap()
        {
            this.ToTable(nameof(AccountMonthlyPosition));
            this.HasKey(p => p.Id);

            this.Property(p => p.PositionVolume).HasPrecision(24, 0);
        }
    }
}