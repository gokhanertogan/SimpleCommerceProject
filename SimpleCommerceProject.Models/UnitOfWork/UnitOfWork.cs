using SimpleCommerceProject.Data.Models.Infrastructure.Context;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CommerceContext _context;

        public UnitOfWork(CommerceContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
