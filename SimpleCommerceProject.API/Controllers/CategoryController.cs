using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCommerceProject.API.Extensions;
using SimpleCommerceProject.Service.Resources;
using SimpleCommerceProject.Service.Services.Abstract;
using SimpleCommerceProject.Shared.ControllerBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerceProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            else
            {
                var response = await _categoryService.AddAsync(categoryResource);
                return CreateActionResultInstance(response);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost("/GetByCategoryAttributes")]
        public async Task<IActionResult> GetByCategoryAttributes(IEnumerable<AttributeResource> attributeList)
        {
            var response = await _categoryService.GetByCategoryAttributesAsync(attributeList);

            return CreateActionResultInstance(response);
        }

        [HttpGet("GetByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetByCategoryName(string categoryName)
        {
            var response = await _categoryService.GetByCategoryNameAsync(categoryName);

            return CreateActionResultInstance(response);
        }


        [HttpPut("{id:int}")]
        public IActionResult Update(CategoryResource categoryResource, int id)
        {
            var response = _categoryService.Update(categoryResource, id);

            return CreateActionResultInstance(response);
        }
    }
}
