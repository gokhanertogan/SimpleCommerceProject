using MediatR;

namespace SimpleCommerceProject.Service.Features.Commands
{
    public class DeleteProductCommand :IRequest
    {
        public int Id { get; set; }
    }
}
