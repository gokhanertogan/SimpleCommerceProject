using SimpleCommerceProject.Service.Resources;
using SimpleCommerceProject.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Service.Services.Abstract
{
    public interface IProductService
    {
        Task<Response<ProductResource>> AddAsync(ProductResource productResource);
        Response<NoContent> Update(ProductResource productResouce, int productId);
        Task<Response<NoContent>> DeleteAsync(int productId);
        Task<Response<ProductResource>> GetByNameAsync(string productName);
        Task<Response<IEnumerable<ProductResource>>> GetListByCategoryNameAsync(string categoryName);
        Task<Response<IEnumerable<ProductResource>>> GetListByProductAttributesAsync(IEnumerable<ProductsAttributesValueResource> productsAttributesValueList);
        Task<Response<IEnumerable<ProductResource>>> GetListByPriceRangeAsync(ProductPriceRangeResource productPriceRange);
    }
}
