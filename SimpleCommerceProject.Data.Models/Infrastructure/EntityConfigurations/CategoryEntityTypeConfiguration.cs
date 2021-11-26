using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerceProject.Data.Models.Core.Domain;

namespace SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id).UseHiLo("category_hilo").IsRequired();

            builder.Property(cb => cb.Name).IsRequired().HasMaxLength(100);
        }
    }
}
