using Microsoft.EntityFrameworkCore;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Data.Models.Infrastructure.EntityConfigurations;

namespace SimpleCommerceProject.Data.Models.Infrastructure.Context
{
    public class CommerceContext : DbContext
    {
        //public const string DEFAULT_SCHEME = "commerce";

        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Values> Values { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<AttributesValues> AttributesValues { get; set; }
        public DbSet<ProductsAttributes> ProductsAttributes { get; set; }
        public DbSet<ProductsAttributesValues> ProductsAttributesValues { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new ValueEntityTypeConfiguration());
            builder.ApplyConfiguration(new AttributesEntityTypeConfiguration());
            builder.ApplyConfiguration(new AttributesValuesEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductsAttributesEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductAttributesValuesEntityTypeConfiguration());
        }
    }
}
