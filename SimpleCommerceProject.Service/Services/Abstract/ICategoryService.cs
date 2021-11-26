using SimpleCommerceProject.Service.Resources;
using SimpleCommerceProject.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Service.Services.Abstract
{
    public interface ICategoryService
    {
        Task<Response<CategoryResource>> AddAsync(CategoryResource productResource);
        Task<Response<CategoryResource>> GetByCategoryNameAsync(string categoryName);
        Task<Response<IEnumerable<CategoryResource>>> GetByCategoryAttributesAsync(IEnumerable<AttributeResource> attributeList);
        Response<NoContent> Update(CategoryResource category, int categoryId);
        Task<Response<NoContent>> DeleteAsync(int categoryId);

    }
}
