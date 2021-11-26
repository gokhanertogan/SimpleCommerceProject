using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Data.Models.Infrastructure.Context;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class AttributesEntityTypeConfiguration : IEntityTypeConfiguration<Attributes>
    {
        public void Configure(EntityTypeBuilder<Attributes> builder)
        {
            builder.ToTable("Attributes");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("attributes_hilo").IsRequired();

            builder.Property(ci => ci.Name).IsRequired(true).HasMaxLength(100);

            builder.HasOne(ci => ci.Category)
                .WithMany()
                .HasForeignKey(ci => ci.CategoryId).HasConstraintName("attributes_category_id_fk");
        }
    }
}
