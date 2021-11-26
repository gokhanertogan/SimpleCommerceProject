using System.Threading.Tasks;

namespace SimpleCommerceProject.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Complete();
    }
}
