using SimpleCommerceProject.Data.Models.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Models.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetByCategoryNameAsync(string categoryName);
        Task<IEnumerable<Category>> GetByCategoryAttributesAsync(IEnumerable<Attributes> attributeList);
        Task AddAsync(Category category);
        void Update(Category category, int categoryId);
        Task DeleteAsync(int categoryId);
    }
}
