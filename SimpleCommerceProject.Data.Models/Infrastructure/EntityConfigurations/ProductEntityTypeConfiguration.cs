using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Data.Models.Infrastructure.Context;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("product_hilo").IsRequired();

            builder.Property(ci => ci.Name).IsRequired(true).HasMaxLength(50);

            builder.Property(ci => ci.Price).IsRequired(true);

            builder.HasOne(ci => ci.Category)
                .WithMany()
                .HasForeignKey(ci => ci.ProductCategoryId).HasConstraintName("product_category_id_fk");
        }
    }
}
