using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using CTM.Core.Domain.Industry;

namespace CTM.Data.Mapping.Industry
{
    public partial class IndustryInfoMap : EntityTypeConfiguration<IndustryInfo>
    {
        public IndustryInfoMap()
        {
            this.ToTable("IndustryInfo");
            this.HasKey(p => p.Id);
        }
    }
}