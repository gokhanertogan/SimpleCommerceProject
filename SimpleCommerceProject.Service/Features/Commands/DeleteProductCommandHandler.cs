using MediatR;
using Microsoft.Extensions.Logging;
using SimpleCommerceProject.Models.Repositories;
using SimpleCommerceProject.Models.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Service.Features.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<DeleteProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteAsync(request.Id);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Product {request.Id} is successfully deleted");

            return Unit.Value;
        }
    }
}
