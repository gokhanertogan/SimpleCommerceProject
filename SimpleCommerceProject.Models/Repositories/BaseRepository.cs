using SimpleCommerceProject.Data.Models.Infrastructure.Context;

namespace SimpleCommerceProject.Models.Repositories
{
    public class BaseRepository
    {
        protected readonly CommerceContext _context;

        public BaseRepository(CommerceContext context)
        {
            _context = context;
        }
    }
}
