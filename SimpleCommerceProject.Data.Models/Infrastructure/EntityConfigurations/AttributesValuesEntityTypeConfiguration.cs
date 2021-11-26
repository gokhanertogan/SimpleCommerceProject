using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class AttributesValuesEntityTypeConfiguration : IEntityTypeConfiguration<AttributesValues>
    {
        public void Configure(EntityTypeBuilder<AttributesValues> builder)
        {
            builder.ToTable("AttributesValues");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("attributes_values_hilo").IsRequired();

            builder.HasOne(ci => ci.Attributes)
                .WithMany()
                .HasForeignKey(ci => ci.AttributeId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("attribute_value_attribute_id_fk");

            builder.HasOne(ci => ci.Values)
                .WithMany()
                .HasForeignKey(ci => ci.ValueId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("attribute_value_values_id_fk");
        }
    }
}
