﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using CTM.Core.Domain.TradeRecord;

namespace CTM.Data.Mapping.TradeRecord
{
    public partial class DeliveryRecordMap : EntityTypeConfiguration<DeliveryRecord>
    {
        public DeliveryRecordMap()
        {
            this.ToTable("DeliveryRecord");
            this.HasKey(p => p.Id);

            this.Property(p => p.TradeTime).HasMaxLength(30);
            this.Property(p => p.StockCode).HasMaxLength(20);
            this.Property(p => p.StockName).HasMaxLength(20);
            this.Property(p => p.StockHolderCode).HasMaxLength(30);
            this.Property(p => p.DealNo).HasMaxLength(30);
            this.Property(p => p.ContractNo).HasMaxLength(30);
            this.Property(p => p.ImportUser).HasMaxLength(20);
            this.Property(p => p.UpdateUser).HasMaxLength(20);
            this.Property(p => p.Remarks).HasMaxLength(200);

            this.Property(p => p.DealPrice).HasPrecision(18, 4);
            this.Property(p => p.DealAmount).HasPrecision(24, 4);
            this.Property(p => p.ActualAmount).HasPrecision(24, 4);
            this.Property(p => p.StampDuty).HasPrecision(18, 4);
            this.Property(p => p.Commission).HasPrecision(18, 4);
            this.Property(p => p.Incidentals).HasPrecision(18, 4);

            this.Ignore(p => p.AccountName);
            this.Ignore(p => p.ImportUserName);
            this.Ignore(p => p.UpdateUserName);
        }
    }
}