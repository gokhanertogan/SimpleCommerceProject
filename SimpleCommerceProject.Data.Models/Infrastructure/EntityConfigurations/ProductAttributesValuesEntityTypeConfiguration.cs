using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class ProductAttributesValuesEntityTypeConfiguration: IEntityTypeConfiguration<ProductsAttributesValues>
    {
        public void Configure(EntityTypeBuilder<ProductsAttributesValues> builder)
        {
            builder.ToTable("ProductAttributesValues");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("products_attributes_values_hilo").IsRequired();

            builder.HasOne(ci => ci.Attributes)
                .WithMany()
                .HasForeignKey(ci => ci.AttributeId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("products_attributes_values_attribute_id_fk");

            builder.HasOne(ci => ci.Values)
                .WithMany()
                .HasForeignKey(ci => ci.ValueId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("products_attributes_values_values_id_fk");

            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("products_attributes_values_products_id_fk");
        }
    }
}
