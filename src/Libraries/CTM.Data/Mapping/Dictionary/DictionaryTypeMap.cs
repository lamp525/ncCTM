using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Dictionary;

namespace CTM.Data.Mapping.Dictionary
{
    public partial class DictionaryTypeMap : EntityTypeConfiguration<DictionaryType>
    {
        public DictionaryTypeMap()
        {
            this.ToTable("DictionaryType");
            this.HasKey(p => p.Id);

            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.Remarks).HasMaxLength(200);
        }
    }
}