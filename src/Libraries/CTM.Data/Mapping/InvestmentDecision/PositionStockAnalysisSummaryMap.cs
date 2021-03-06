﻿using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.InvestmentDecision;

namespace CTM.Data.Mapping.InvestmentDecision
{
    public partial class PositionStockAnalysisSummaryMap : EntityTypeConfiguration<PositionStockAnalysisSummary>
    {
        public PositionStockAnalysisSummaryMap()
        {
            this.ToTable(nameof(PositionStockAnalysisSummary));
            this.HasKey(p => p.Id);

            this.Property(p => p.DealAmount).HasPrecision(24, 4);
            this.Property(p => p.DealRange).HasPrecision(18, 4);
        }
    }
}