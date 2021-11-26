using Microsoft.EntityFrameworkCore;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Data.Models.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Models.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(CommerceContext context) : base(context)
        {

        }

        public async Task AddAsync(Category category)
        {
            category.Id = _context.Categories.Max(x => x.Id) + 1;
            await _context.Categories.AddAsync(category);
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category != null)
            {
                var attributeList = await _context.Attributes.Where(x => x.CategoryId == categoryId).ToListAsync();
                var attributeIdList = attributeList.Select(x => x.Id).Distinct().ToList();

                var attributeValueList = await _context.AttributesValues.Where(x => attributeIdList.Contains(x.AttributeId)).ToListAsync();
                _context.AttributesValues.RemoveRange(attributeValueList);

                var productAttributeList = await _context.ProductsAttributes.Where(x => attributeIdList.Contains(x.AttributeId)).ToListAsync();
                _context.ProductsAttributes.RemoveRange(productAttributeList);

                var productAttributeValueList = await _context.ProductsAttributesValues.Where(x => attributeIdList.Contains(x.AttributeId)).ToListAsync();
                _context.ProductsAttributesValues.RemoveRange(productAttributeValueList);

                var valueList = await _context.Values.Where(x => attributeIdList.Contains(x.AttributeId)).ToListAsync();
                _context.Values.RemoveRange(valueList);
                _context.Attributes.RemoveRange(attributeList);

                var productList = await _context.Products.Where(x => x.ProductCategoryId == categoryId).ToListAsync();
                _context.Products.RemoveRange(productList);

                _context.Categories.Remove(category);
            }
        }

        public async Task<Category> GetByCategoryNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == categoryName.ToLower());
        }

        public async Task<IEnumerable<Category>> GetByCategoryAttributesAsync(IEnumerable<Attributes> attributeList)
        {
            var attributeIdList = attributeList.Select(x => x.Id).Distinct();
            var categoryIdList = _context.Attributes.Where(x => attributeIdList.Contains(x.Id)).Select(x => x.CategoryId).Distinct();

            return await _context.Categories.Where(x => categoryIdList.Contains(x.Id)).ToListAsync();
        }

        public void Update(Category category, int categoryId)
        {
            category.Id = categoryId;
            _context.Categories.Update(category);
        }
    }
}
