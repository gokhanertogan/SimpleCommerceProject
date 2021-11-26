using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class ProductsAttributesEntityTypeConfiguration : IEntityTypeConfiguration<ProductsAttributes>
    {
        public void Configure(EntityTypeBuilder<ProductsAttributes> builder)
        {
            builder.ToTable("ProductsAttributes");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("products_attributes_hilo").IsRequired();

            builder.HasOne(ci => ci.Attributes)
                .WithMany()
                .HasForeignKey(ci => ci.AttributeId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("products_attributes_attribute_id_fk");

            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("products_attributes_products_id_fk");
        }
    }
}
