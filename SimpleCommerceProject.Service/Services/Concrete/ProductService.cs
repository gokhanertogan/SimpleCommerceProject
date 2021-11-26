using AutoMapper;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Models.Repositories;
using SimpleCommerceProject.Models.UnitOfWork;
using SimpleCommerceProject.Service.Resources;
using SimpleCommerceProject.Service.Services.Abstract;
using SimpleCommerceProject.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCommerceProject.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<ProductResource>> AddAsync(ProductResource productResource)
        {
            try
            {
                var product = _mapper.Map<ProductResource, Product>(productResource);
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                productResource.Id = product.Id;
                return Response<ProductResource>.Success(productResource, 200);
            }

            catch (Exception ex)
            {
                return Response<ProductResource>.Fail($"Error an occurred while adding a product : {ex.Message}", 500);
            }
        }

        public async Task<Response<NoContent>> DeleteAsync(int productId)
        {
            try
            {
                await _productRepository.DeleteAsync(productId);
                await _unitOfWork.CompleteAsync();

                return Response<NoContent>.Success(204);
            }

            catch (Exception ex)
            {
                return Response<NoContent>.Fail($"Error an occurred while deleting the product : {ex.Message}", 500);
            }
        }

        public async Task<Response<ProductResource>> GetByNameAsync(string productName)
        {
            var product = await _productRepository.ProductByNameAsync(productName);

            if (product == null)
            {
                return Response<ProductResource>.Fail("product not found", 404);
            }

            return Response<ProductResource>.Success(_mapper.Map<ProductResource>(product), 200);
        }

        public async Task<Response<IEnumerable<ProductResource>>> GetListByCategoryNameAsync(string categoryName)
        {
            var productList = await _productRepository.ProductListByCategoryNameAsync(categoryName);

            return Response<IEnumerable<ProductResource>>.Success(_mapper.Map<IEnumerable<ProductResource>>(productList), 200);
        }

        public async Task<Response<IEnumerable<ProductResource>>> GetListByPriceRangeAsync(ProductPriceRangeResource productPriceRange)
        {
            var productList = await _productRepository.ProductListByPriceRangeAsync(productPriceRange.MinPrice, productPriceRange.MaxPrice);

            return Response<IEnumerable<ProductResource>>.Success(_mapper.Map<IEnumerable<ProductResource>>(productList), 200);
        }

        public async Task<Response<IEnumerable<ProductResource>>> GetListByProductAttributesAsync(IEnumerable<ProductsAttributesValueResource> productsAttributesValueList)
        {
            var productAttributeValues = _mapper.Map<IEnumerable<ProductsAttributesValueResource>, IEnumerable<ProductsAttributesValues>>(productsAttributesValueList);

            var productList = await _productRepository.ProductListByProductAttributesAsync(productAttributeValues);
            return Response<IEnumerable<ProductResource>>.Success(_mapper.Map<IEnumerable<ProductResource>>(productList), 200);
        }

        public Response<NoContent> Update(ProductResource productResouce, int productId)
        {
            var product = _mapper.Map<Product>(productResouce);
            _productRepository.Update(product, productId);
            _unitOfWork.Complete();

            return Response<NoContent>.Success(204);
        }
    }
}
