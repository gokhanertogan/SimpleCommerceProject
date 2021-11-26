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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<CategoryResource>> AddAsync(CategoryResource categoryResource)
        {
            try
            {
                var category = _mapper.Map<CategoryResource, Category>(categoryResource);
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                categoryResource.Id = category.Id;
                return Response<CategoryResource>.Success(categoryResource, 200);
            }

            catch (Exception ex)
            {
                return Response<CategoryResource>.Fail($"Error an occurred while adding a category : {ex.Message}",500);
            }
        }

        public async Task<Response<NoContent>> DeleteAsync(int categoryId)
        {
            try
            {
                await _categoryRepository.DeleteAsync(categoryId);
                await _unitOfWork.CompleteAsync();

                return Response<NoContent>.Success(204);
            }

            catch (Exception ex)
            {
                return Response<NoContent>.Fail($"Error an occurred while deleting the category : {ex.Message}", 500);
            }
        }

        public async Task<Response<IEnumerable<CategoryResource>>> GetByCategoryAttributesAsync(IEnumerable<AttributeResource> attributeList)
        {
            var attributeResourceList= _mapper.Map<IEnumerable<AttributeResource>, IEnumerable<Attributes>>(attributeList);

            var categoryList = await _categoryRepository.GetByCategoryAttributesAsync(attributeResourceList);
            return Response<IEnumerable<CategoryResource>>.Success(_mapper.Map<IEnumerable<CategoryResource>>(categoryList), 200);
        }

        public async Task<Response<CategoryResource>> GetByCategoryNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetByCategoryNameAsync(categoryName);

            if (category == null)
            {
                return Response<CategoryResource>.Fail("category not found", 404);
            }

            return Response<CategoryResource>.Success(_mapper.Map<CategoryResource>(category), 200);
        }

        public Response<NoContent> Update(CategoryResource categoryResource, int categoryId)
        {
            var category = _mapper.Map<Category>(categoryResource);
            _categoryRepository.Update(category, categoryId);
            _unitOfWork.Complete();

            return Response<NoContent>.Success(204);
        }
    }
}
