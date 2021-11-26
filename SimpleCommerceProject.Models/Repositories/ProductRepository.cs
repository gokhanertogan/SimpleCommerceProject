using Microsoft.EntityFrameworkCore;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Data.Models.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Models.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(CommerceContext context) : base(context)
        {

        }
        public async Task AddAsync(Product product)
        {
            product.Id = _context.Products.Max(x => x.Id) + 1; //
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> ProductByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Product>> ProductListByCategoryNameAsync(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == categoryName.ToLower());

            if (category != null)
                return await _context.Products.Where(x => x.ProductCategoryId == category.Id).ToListAsync();

            return null;
        }

        public async Task<IEnumerable<Product>> ProductListByPriceRangeAsync(int minPrice, int maxPrice)
        {
            return await _context.Products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToListAsync();
        }
        public async Task<IEnumerable<Product>> ProductListByProductAttributesAsync(IEnumerable<ProductsAttributesValues> productsAttributesValueList)
        {
            var productAttributesValuesAllData = await _context.ProductsAttributesValues.ToListAsync();

            var productIdList = (from allData in productAttributesValuesAllData
                      join attributeValue in productsAttributesValueList on new { allData.AttributeId, allData.ValueId } equals new { attributeValue.AttributeId, attributeValue.ValueId }
                      select allData.ProductId).Distinct();
             
            //foreach (ProductsAttributesValues productsAttribute in productsAttributesValueList)
            //{
            //    productAttributesValuesAllData = productAttributesValuesAllData.Where(x => x.AttributeId == productsAttribute.AttributeId && x.ValueId == productsAttribute.ValueId).ToList();
            //}

            //var productIdList = productAttributesValuesAllData.Select(x => x.ProductId).Distinct();
            return await _context.Products.Where(x => productIdList.Contains(x.Id)).ToListAsync();
        }

        public void Update(Product product, int productId)
        {
            product.Id = productId;
            _context.Products.Update(product);
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                var productAttributesValueList = await _context.ProductsAttributesValues.Where(x => x.ProductId == productId).ToListAsync();
                if (productAttributesValueList.Count > 0)
                    _context.ProductsAttributesValues.RemoveRange(productAttributesValueList);

                var productAttributeList = await _context.ProductsAttributes.Where(x => x.ProductId == productId).ToListAsync();
                if (productAttributeList.Count > 0)
                    _context.ProductsAttributes.RemoveRange(productAttributeList);

                _context.Products.Remove(product);
            }
        }
    }
}
