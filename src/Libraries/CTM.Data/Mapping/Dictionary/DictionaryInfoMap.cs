using System.Data.Entity.ModelConfiguration;
using CTM.Core.Domain.Dictionary;

namespace CTM.Data.Mapping.Dictionary
{
    public partial class DictionaryInfoMap : EntityTypeConfiguration<DictionaryInfo>
    {
        public DictionaryInfoMap()
        {
            this.ToTable("DictionaryInfo");
            this.HasKey(p => p.Id);

            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.Remarks).HasMaxLength(200);

            this.HasRequired(i => i.DictionaryType)
                .WithMany(t => t.DictionaryInfos)
                .HasForeignKey(i => i.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}