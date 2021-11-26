using Microsoft.Extensions.Logging;
using Polly;
using SimpleCommerceProject.Data.Models.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Data.Models.Infrastructure.Context
{
    public class CommerceContextSeed
    {
        public async Task SeedAsync(CommerceContext context, ILogger<CommerceContextSeed> logger)
        {
            var policy = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timespan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attemp {retry} of {}");
                    }
                );

            await policy.ExecuteAsync(() => ProcessSeeding(context, logger));
        }
        private async Task ProcessSeeding(CommerceContext context, ILogger<CommerceContextSeed> logger)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(GetCategories());
                await context.SaveChangesAsync();
            }

            if (!context.Attributes.Any())
            {
                context.Attributes.AddRange(GetAttributes());
                await context.SaveChangesAsync();
            }

            if (!context.Values.Any())
            {
                context.Values.AddRange(GetValues());
                await context.SaveChangesAsync();
            }

            if (!context.AttributesValues.Any())
            {
                context.AttributesValues.AddRange(GetAttributesValues());
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(GetProductList());
                await context.SaveChangesAsync();
            }

            if (!context.ProductsAttributes.Any())
            {
                context.ProductsAttributes.AddRange(GetProductsAttributes());
                await context.SaveChangesAsync();
            }

            if (!context.ProductsAttributesValues.Any())
            {
                context.ProductsAttributesValues.AddRange(GetProductsAttributesValues());
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<Values> GetValues()
        {
            return new List<Values>
            {
                new Values {Id=1, AttributeId = 1,Value="XL" },
                new Values {Id=2, AttributeId = 2,Value="Black" },
                new Values {Id=3, AttributeId = 3,Value="Unisex" },
                new Values {Id=4, AttributeId = 4,Value="SampleBrand" },
                new Values {Id=5, AttributeId = 5,Value="SampleBrand" },
                new Values {Id=6, AttributeId = 6,Value="4 inc" },
                new Values {Id=7, AttributeId = 7,Value="SampleOS" },
            };
        }
        private IEnumerable<AttributesValues> GetAttributesValues()
        {
            return new List<AttributesValues>
            {
                new AttributesValues {Id=1, AttributeId = 1,ValueId=1 },
                new AttributesValues {Id=2, AttributeId = 2,ValueId=2 },
                new AttributesValues {Id=3, AttributeId = 3,ValueId=3 },
                new AttributesValues {Id=4, AttributeId = 4,ValueId=4 },
                new AttributesValues {Id=5, AttributeId = 5,ValueId=5 },
                new AttributesValues {Id=6, AttributeId = 6,ValueId=6 },
                new AttributesValues {Id=7, AttributeId = 7,ValueId=7 }
            };
        }
        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category {Id=1, Name = "TShirtCategory" },
                new Category {Id=2, Name = "SmartPhoneCategory" }
            };
        }
        private IEnumerable<Product> GetProductList()
        {
            return new List<Product>
            {
                new Product {Id=1, Name = "SampleTShirtName",ProductCategoryId=1,Price=50 },
                new Product {Id=2, Name = "SamplePhone",ProductCategoryId=2,Price=4500 },
            };
        }
        private IEnumerable<Attributes> GetAttributes()
        {
            return new List<Attributes>
            {
                new Attributes {Id=1, Name = "Size",CategoryId=1 },
                new Attributes {Id=2, Name = "Color",CategoryId=1 },
                new Attributes {Id=3, Name = "Gender",CategoryId=1 },
                new Attributes {Id=4, Name = "Brand",CategoryId=1 },
                new Attributes {Id=5, Name = "Brand",CategoryId=2 },
                new Attributes {Id=6, Name = "ScreenSize",CategoryId=2 },
                new Attributes {Id=7, Name = "OS",CategoryId=2 },
            };
        }
        private IEnumerable<ProductsAttributesValues> GetProductsAttributesValues()
        {
            return new List<ProductsAttributesValues>
            {
                new ProductsAttributesValues {Id=1, ProductId = 1,AttributeId=1,ValueId=1},
                new ProductsAttributesValues {Id=2, ProductId = 1,AttributeId=2, ValueId=2 },
                new ProductsAttributesValues {Id=3, ProductId = 1,AttributeId=3,ValueId=3},
                new ProductsAttributesValues {Id=4, ProductId = 1,AttributeId=4,ValueId=4},
                new ProductsAttributesValues {Id=5, ProductId = 2,AttributeId=5,ValueId=5},
                new ProductsAttributesValues {Id=6, ProductId = 2,AttributeId=6,ValueId=6 },
                new ProductsAttributesValues {Id=7, ProductId = 2,AttributeId=7,ValueId=7}
            };
        }
        private IEnumerable<ProductsAttributes> GetProductsAttributes()
        {
            return new List<ProductsAttributes>
            {
                new ProductsAttributes {Id=1, ProductId = 1,AttributeId=1 },
                new ProductsAttributes {Id=2, ProductId = 1,AttributeId=2 },
                new ProductsAttributes {Id=3, ProductId = 1,AttributeId=3 },
                new ProductsAttributes {Id=4, ProductId = 1,AttributeId=4 },
                new ProductsAttributes {Id=5, ProductId = 2,AttributeId=5 },
                new ProductsAttributes {Id=6, ProductId = 2,AttributeId=6 },
                new ProductsAttributes {Id=7, ProductId = 2,AttributeId=7 }
            };
        }
    }
}
