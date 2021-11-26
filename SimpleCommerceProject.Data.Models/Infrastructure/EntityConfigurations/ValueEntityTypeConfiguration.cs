using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class ValueEntityTypeConfiguration : IEntityTypeConfiguration<Values>
    {
        public void Configure(EntityTypeBuilder<Values> builder)
        {
            builder.ToTable("Values");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("values_hilo").IsRequired();

            builder.Property(ci => ci.Value).IsRequired(true).HasMaxLength(150);

            builder.HasOne(ci => ci.Attributes)
                .WithMany()
                .HasForeignKey(ci => ci.AttributeId).HasConstraintName("values_category_id_fk");
        }
    }
}
