using SimpleCommerceProject.Data.Models.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Models.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        void Update(Product product,int productId);
        Task DeleteAsync(int productId);
        Task<Product> ProductByNameAsync(string name);
        Task<IEnumerable<Product>> ProductListByCategoryNameAsync(string categoryName);
        Task<IEnumerable<Product>> ProductListByProductAttributesAsync(IEnumerable<ProductsAttributesValues> productsAttributesValueList);
        Task<IEnumerable<Product>> ProductListByPriceRangeAsync(int minPrice, int maxPrice);
    }
}
